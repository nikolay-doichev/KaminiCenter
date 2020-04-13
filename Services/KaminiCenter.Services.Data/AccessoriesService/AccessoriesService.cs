namespace KaminiCenter.Services.Data.AccessoriesService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using KaminiCenter.Common;
    using KaminiCenter.Data.Common.Repositories;
    using KaminiCenter.Data.Models;
    using KaminiCenter.Services.Common.Exceptions;
    using KaminiCenter.Services.Data.GroupService;
    using KaminiCenter.Services.Data.ProductService;
    using KaminiCenter.Services.Mapping;
    using KaminiCenter.Web.ViewModels.Accessories;

    public class AccessoriesService : IAccessoriesService
    {
        private const string InvalidAccessorieNameErrorMessage = "Accessorie with Name: {0} does not exist.";

        private readonly IDeletableEntityRepository<Accessorie> accessoriesRepository;
        private readonly IProductService productService;
        private readonly IGroupService groupService;
        private readonly ICloudinaryService cloudinaryService;

        public AccessoriesService(
            IDeletableEntityRepository<Accessorie> accessoriesRepository,
            IProductService productService,
            IGroupService groupService,
            ICloudinaryService cloudinaryService)
        {
            this.accessoriesRepository = accessoriesRepository;
            this.productService = productService;
            this.groupService = groupService;
            this.cloudinaryService = cloudinaryService;
        }

        public async Task<string> AddAccessoriesAsync(AddAccessorieInputModel model, string userId)
        {
            if (model.Name == null ||
                model.ImagePath == null)
            {
                throw new ArgumentNullException(ExceptionsServices.Null_PropertiesErrorMessage);
            }

            await this.productService.AddProductAsync(model.Name, model.Group, userId);
            var productId = this.productService.GetIdByNameAndGroup(model.Name, model.Group);
            var groupId = this.groupService.FindByGroupName(model.Group).Id;

            var photoUrl = await this.cloudinaryService.UploadPhotoAsync(
               model.ImagePath,
               $"{model.Group}_{model.Name}_{model.Id}",
               GlobalConstants.CloudFolderForFireplacePhotos);

            var accessories = new Accessorie
            {
                Id = Guid.NewGuid().ToString(),
                Description = model.Description,
                ImagePath = photoUrl,
                ProductId = productId,
                GroupId = groupId,
                CreatedOn = DateTime.UtcNow,
                ModifiedOn = DateTime.UtcNow,
            };

            await this.accessoriesRepository.AddAsync(accessories);
            await this.accessoriesRepository.SaveChangesAsync();
            return accessories.Id;
        }

        public async Task DeleteAsync<T>(DeleteAccessorieViewModel deleteModel)
        {
            var accessories = this.accessoriesRepository.All().Where(f => f.Id == deleteModel.Id).FirstOrDefault();

            if (accessories == null)
            {
                throw new NullReferenceException(string.Format(ExceptionsServices.Null_Accessories_Id_ErrorMessage, deleteModel.Id));
            }

            accessories.IsDeleted = true;
            accessories.DeletedOn = DateTime.UtcNow;

            await this.productService.DeleteAsync(accessories.ProductId);
            this.accessoriesRepository.Update(accessories);

            await this.accessoriesRepository.SaveChangesAsync();
        }

        public async Task<string> EditAsync<T>(EditAccessorieViewModel editModel)
        {
            var accessories = this.accessoriesRepository.All().Where(f => f.Id == editModel.Id).FirstOrDefault();

            if (accessories == null)
            {
                throw new NullReferenceException(string.Format(ExceptionsServices.Null_Accessories_Id_ErrorMessage, editModel.Id));
            }

            var photoUrl = string.Empty;
            if (editModel.ImagePath == null)
            {
                photoUrl = accessories.ImagePath;
            }
            else
            {
                photoUrl = await this.cloudinaryService.UploadPhotoAsync(
                editModel.ImagePath,
                $"{editModel.Name}_{editModel.Id}",
                GlobalConstants.CloudFolderForFireplacePhotos);
            }

            var product = this.productService.GetById(accessories.ProductId);

            product.Name = editModel.Name;
            accessories.Description = editModel.Description;
            accessories.ImagePath = photoUrl; // To upload the new image and ad the new

            await this.accessoriesRepository.SaveChangesAsync();
            return accessories.Id;
        }

        public IEnumerable<T> GetAllAccessorieAsync<T>(int? take = null, int skip = 0)
        {
            IQueryable<Accessorie> accessories = this.accessoriesRepository
                .All()
                .OrderBy(x => x.Product.Name)
                .Skip(skip);

            if (take.HasValue)
            {
                accessories = accessories.Take(take.Value);
            }

            return accessories.To<T>().ToList();
        }

        public T GetById<T>(string id)
        {
            var accessorieId = this.accessoriesRepository
                .All()
                .Where(f => f.Id == id)
                .To<T>().FirstOrDefault();

            if (accessorieId == null)
            {
                throw new ArgumentNullException(
                    string.Format(string.Format(ExceptionsServices.Null_Accessories_Id_ErrorMessage, id)));
            }

            return accessorieId;
        }

        public T GetByName<T>(string name)
        {
            var accessorie = this.accessoriesRepository
                .All().Where(x => x.Product.Name == name)
                .To<T>().FirstOrDefault();

            if (accessorie == null)
            {
                throw new ArgumentNullException(
                    string.Format(InvalidAccessorieNameErrorMessage, name));
            }

            return accessorie;
        }

        public int GetCount()
        {
            return this.accessoriesRepository.All().Count();
        }
    }
}
