﻿namespace KaminiCenter.Services.Data.FinishedModelService
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
    using KaminiCenter.Web.ViewModels.FinishedModels;

    public class FinishedModelService : IFinishedModelService
    {
        private const string NullFinishedModelPropertiesErrorMessage = "There is a property with null or whitespec value";
        private const string InvalidFireplaceNameErrorMessage = "Fireplace with Name: {0} does not exist.";

        private readonly IDeletableEntityRepository<Finished_Model> finishedModelRepository;
        private readonly IProductService productService;
        private readonly IGroupService groupService;
        private readonly ICloudinaryService cloudinaryService;

        public FinishedModelService(
            IDeletableEntityRepository<Finished_Model> finishedModelRepository,
            IProductService productService,
            IGroupService groupService,
            ICloudinaryService cloudinaryService)
        {
            this.finishedModelRepository = finishedModelRepository;
            this.productService = productService;
            this.groupService = groupService;
            this.cloudinaryService = cloudinaryService;
        }

        public async Task<string> AddFinishedModelAsync(AddFinishedModelInputModel model, string userId)
        {
            var typeOfProject = Enum.Parse<TypeProject>(model.TypeProject);

            if (model.Name == null ||
                model.ImagePath == null)
            {
                throw new ArgumentNullException(NullFinishedModelPropertiesErrorMessage);
            }

            await this.productService.AddProductAsync(model.Name, userId);
            var productId = this.productService.GetIdByNameAndGroup(model.Name, model.Group);
            var groupId = this.groupService.FindByGroupName(model.Group).Id;

            var photoUrl = await this.cloudinaryService.UploadPhotoAsync(
               model.ImagePath,
               $"{model.Group}_{model.Name}_{model.Id}",
               GlobalConstants.CloudFolderForFireplacePhotos);

            var finishedModel = new Finished_Model
            {
                Id = Guid.NewGuid().ToString(),
                CreatedOn = DateTime.UtcNow,
                Description = model.Description,
                IsDeleted = false,
                ImagePath = photoUrl,
                GroupId = groupId,
                ProductId = productId,
                TypeProject = typeOfProject,
            };

            await this.finishedModelRepository.AddAsync(finishedModel);
            await this.finishedModelRepository.SaveChangesAsync();

            return finishedModel.Id;
        }

        public Task DeleteAsync<T>(DeleteFinishedViewModel deleteModel)
        {
            throw new NotImplementedException();
        }

        public Task<string> EditAsync<T>(EditFinishedModelViewModel editModel)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAllFinishedModelsAsync<T>(string type, int? take = null, int skip = 0)
        {
            var typeOfProject = Enum.Parse<TypeProject>(type);

            IQueryable<Finished_Model> finishedModels = this.finishedModelRepository
                .All()
                .OrderBy(x => x.Product.Name)
                .Where(x => x.TypeProject == typeOfProject)
                .Skip(skip);

            if (take.HasValue)
            {
                finishedModels = finishedModels.Take(take.Value);
            }

            return finishedModels.To<T>().ToList();
        }

        public T GetById<T>(string id)
        {
            var finishedModelId = this.finishedModelRepository
                .All()
                .Where(f => f.Id == id)
                .To<T>().FirstOrDefault();

            if (finishedModelId == null)
            {
                throw new ArgumentNullException(
                    string.Format(string.Format(ExceptionsServices.Null_Fireplace_Id_ErrorMessage, id)));
            }

            return finishedModelId;
        }

        public T GetByName<T>(string name)
        {
            var finishedModel = this.finishedModelRepository
                .All().Where(x => x.Product.Name == name)
                .To<T>().FirstOrDefault();

            if (finishedModel == null)
            {
                throw new ArgumentNullException(
                    string.Format(InvalidFireplaceNameErrorMessage, name));
            }

            return finishedModel;
        }

        public int GetCountByTypeOfProject(string type)
        {
            throw new NotImplementedException();
        }
    }
}
