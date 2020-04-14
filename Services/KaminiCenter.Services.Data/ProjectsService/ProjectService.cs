namespace KaminiCenter.Services.Data.ProjectsService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using KaminiCenter.Common;
    using KaminiCenter.Data.Common.Repositories;
    using KaminiCenter.Data.Models;
    using KaminiCenter.Data.Models.Enums;
    using KaminiCenter.Services.Common.Exceptions;
    using KaminiCenter.Services.Data.GroupService;
    using KaminiCenter.Services.Data.ProductService;
    using KaminiCenter.Services.Mapping;
    using KaminiCenter.Web.ViewModels.Projects;

    public class ProjectService : IProjectService
    {
        private const string NullProjectPropertiesErrorMessage = "There is a property with null or whitespec value";
        private const string InvalidProjectNameErrorMessage = "Project with Name: {0} does not exist.";

        private readonly IDeletableEntityRepository<Project> projectRepository;
        private readonly ICloudinaryService cloudinaryService;
        private readonly IProductService productService;
        private readonly IGroupService groupService;

        public ProjectService(
            IDeletableEntityRepository<Project> projectRepository,
            ICloudinaryService cloudinaryService,
            IProductService productService,
            IGroupService groupService)
        {
            this.projectRepository = projectRepository;
            this.cloudinaryService = cloudinaryService;
            this.productService = productService;
            this.groupService = groupService;
        }

        public async Task<string> AddProjectAsync(AddProjectInputModel model, string userId)
        {
            var typeOfProject = Enum.Parse<TypeProject>(model.TypeProject);
            var typeOfLocation = Enum.Parse<TypeLocation>(model.TypeLocation);

            if (model.Name == null ||
                model.ImagePath == null)
            {
                throw new ArgumentNullException(NullProjectPropertiesErrorMessage);
            }

            await this.productService.AddProductAsync(model.Name, model.Group, userId);
            var productId = this.productService.GetIdByNameAndGroup(model.Name, model.Group);
            var groupId = this.groupService.FindByGroupName(model.Group).Id;

            var photoUrl = await this.cloudinaryService.UploadPhotoAsync(
               model.ImagePath,
               $"{model.Group}_{model.Name}_{model.Id}",
               GlobalConstants.CloudFolderForProjectPhotos);

            var projectModel = new Project
            {
                Id = Guid.NewGuid().ToString(),
                CreatedOn = DateTime.UtcNow,
                Description = model.Description,
                IsDeleted = false,
                ImagePath = photoUrl,
                GroupId = groupId,
                ProductId = productId,
                TypeProject = typeOfProject,
                TypeLocation = typeOfLocation,
            };

            await this.projectRepository.AddAsync(projectModel);
            await this.projectRepository.SaveChangesAsync();

            return projectModel.Id;
        }

        public async Task DeleteAsync<T>(DeleteProjectViewModel deleteModel)
        {
            var projectModel = this.projectRepository.All().Where(f => f.Id == deleteModel.Id).FirstOrDefault();

            if (projectModel == null)
            {
                throw new NullReferenceException(string.Format(ExceptionsServices.Null_Project_Id_ErrorMessage, deleteModel.Id));
            }

            projectModel.IsDeleted = true;
            projectModel.DeletedOn = DateTime.UtcNow;

            await this.productService.DeleteAsync(projectModel.ProductId);
            this.projectRepository.Update(projectModel);

            await this.projectRepository.SaveChangesAsync();
        }

        public async Task<string> EditAsync<T>(EditProjectViewModel editModel)
        {
            var projectModel = this.projectRepository.All().Where(f => f.Id == editModel.Id).FirstOrDefault();

            if (projectModel == null)
            {
                throw new NullReferenceException(string.Format(ExceptionsServices.Null_Project_Id_ErrorMessage, editModel.Id));
            }

            var photoUrl = string.Empty;
            if (editModel.ImagePath == null)
            {
                photoUrl = projectModel.ImagePath;
            }
            else
            {
                photoUrl = await this.cloudinaryService.UploadPhotoAsync(
                editModel.ImagePath,
                $"{editModel.Name}_{editModel.Id}",
                GlobalConstants.CloudFolderForProjectPhotos);
            }

            var product = this.productService.GetById(projectModel.ProductId);
            var typeOfProject = Enum.Parse<TypeProject>(editModel.TypeProject);
            var typeOfLocation = Enum.Parse<TypeLocation>(editModel.TypeLocation);

            product.Name = editModel.Name;
            projectModel.TypeProject = typeOfProject;
            projectModel.TypeLocation = typeOfLocation;
            projectModel.Description = editModel.Description;
            projectModel.ImagePath = photoUrl; // To upload the new image and ad the new

            await this.projectRepository.SaveChangesAsync();
            return projectModel.Id;
        }

        public IEnumerable<T> GetAllProjectAsync<T>(string typeLocation, string type, int? take = null, int skip = 0)
        {
            var typeOfProject = Enum.Parse<TypeProject>(type);
            var typeOfLocation = Enum.Parse<TypeLocation>(typeLocation);

            IQueryable<Project> finishedModels = this.projectRepository
                .All()
                .OrderBy(x => x.Product.Name)
                .Where(x => x.TypeProject == typeOfProject)
                .Where(x => x.TypeLocation == typeOfLocation)
                .Skip(skip);

            if (take.HasValue)
            {
                finishedModels = finishedModels.Take(take.Value);
            }

            return finishedModels.To<T>().ToList();
        }

        public T GetById<T>(string id)
        {
            var projectId = this.projectRepository
                .All()
                .Where(f => f.Id == id)
                .To<T>().FirstOrDefault();

            if (projectId == null)
            {
                throw new ArgumentNullException(
                    string.Format(string.Format(ExceptionsServices.Null_Project_Id_ErrorMessage, id)));
            }

            return projectId;
        }

        public T GetByName<T>(string name)
        {
            var project = this.projectRepository
                .All().Where(x => x.Product.Name == name)
                .To<T>().FirstOrDefault();

            if (project == null)
            {
                throw new ArgumentNullException(
                    string.Format(InvalidProjectNameErrorMessage, name));
            }

            return project;
        }

        public int GetCountByTypeOfChamber(string typeLocation, string type)
        {
            var typeOfProject = Enum.Parse<TypeProject>(type);
            var typeOfLocation = Enum.Parse<TypeLocation>(typeLocation);

            return this.projectRepository
                .All()
                .Count(x => x.TypeProject == typeOfProject && x.TypeLocation == typeOfLocation);
        }
    }
}
