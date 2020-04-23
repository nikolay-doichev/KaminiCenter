namespace KaminiCenter.Services.Data.FireplaceServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using KaminiCenter.Common;
    using KaminiCenter.Data.Common.Repositories;
    using KaminiCenter.Data.Models;
    using KaminiCenter.Data.Models.Enums;
    using KaminiCenter.Services;
    using KaminiCenter.Services.Common.Exceptions;
    using KaminiCenter.Services.Data.CommentServices;
    using KaminiCenter.Services.Data.GroupService;
    using KaminiCenter.Services.Data.ProductService;
    using KaminiCenter.Services.Data.SuggestProduct;
    using KaminiCenter.Services.Mapping;
    using KaminiCenter.Web.ViewModels.Fireplace;

    public class FireplaceService : IFireplaceService
    {
        private const string InvalidFireplaceNameErrorMessage = "Fireplace with Name: {0} does not exist.";

        private readonly IDeletableEntityRepository<Fireplace_chamber> fireplaceRepository;
        private readonly IGroupService groupService;
        private readonly IProductService productService;
        private readonly ICloudinaryService cloudinaryService;
        private readonly ISuggestProduct suggestProduct;

        public FireplaceService(
            IDeletableEntityRepository<Fireplace_chamber> fireplaceRepository,
            IGroupService groupService,
            IProductService productService,
            ICloudinaryService cloudinaryService,
            ISuggestProduct suggestProduct)
        {
            this.fireplaceRepository = fireplaceRepository;
            this.groupService = groupService;
            this.productService = productService;
            this.cloudinaryService = cloudinaryService;
            this.suggestProduct = suggestProduct;
        }

        public async Task<string> AddFireplaceAsync<T>(FireplaceInputModel model, string userId)
        {
            var typeOfChamber = Enum.Parse<TypeOfChamber>(model.TypeOfChamber);

            if (model.Name == null ||
                model.Power == null ||
                model.Size == null ||
                model.Chimney == null ||
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

            var fireplace = new Fireplace_chamber
            {
                Id = Guid.NewGuid().ToString(),
                Power = model.Power,
                Size = model.Size,
                Chimney = model.Chimney,
                Price = model.Price,
                Description = model.Description,
                ImagePath = photoUrl,
                TypeOfChamber = typeOfChamber,
                ProductId = productId,
                GroupId = groupId,
                CreatedOn = DateTime.UtcNow,
                ModifiedOn = DateTime.UtcNow,
            };

            await this.fireplaceRepository.AddAsync(fireplace);
            await this.fireplaceRepository.SaveChangesAsync();
            return fireplace.Id;
        }

        public async Task AddSuggestionToFireplaceAsync(
            string productName,
            string fireplaceId,
            string[] selectedFireplaces,
            string[] selectedFinishedModels,
            string[] selectedProjects,
            string[] selectedAccessories)
        {
            var fireplace = this.fireplaceRepository.All().Where(f => f.Id == fireplaceId).FirstOrDefault();

            if (fireplace == null)
            {
                throw new NullReferenceException(string.Format(ExceptionsServices.Null_Fireplace_Id_ErrorMessage, fireplaceId));
            }

            // Adding Fireplace
            if (selectedFireplaces != null)
            {
                foreach (var fireplaceName in selectedFireplaces)
                {
                    await this.AddProductToFireplace(fireplaceId, fireplaceName, GroupType.Fireplace.ToString());
                }
            }

            if (selectedFinishedModels != null)
            {
                // Adding FinishedModels
                foreach (var finishedModel in selectedFinishedModels)
                {
                    await this.AddProductToFireplace(fireplaceId, finishedModel, GroupType.Finished_Models.ToString());
                }
            }

            if (selectedProjects != null)
            {
                // Adding Project
                foreach (var project in selectedProjects)
                {
                    await this.AddProductToFireplace(fireplaceId, project, GroupType.Project.ToString());
                }
            }

            if (selectedAccessories != null)
            {
                // Adding Accessories
                foreach (var accessorie in selectedAccessories)
                {
                    await this.AddProductToFireplace(fireplaceId, accessorie, GroupType.Accessories.ToString());
                }
            }

            await this.fireplaceRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync<T>(DeleteFireplaceViewModel deleteModel)
        {
            var fireplace = this.fireplaceRepository.All().Where(f => f.Id == deleteModel.Id).FirstOrDefault();

            if (fireplace == null)
            {
                throw new NullReferenceException(string.Format(ExceptionsServices.Null_Fireplace_Id_ErrorMessage, deleteModel.Id));
            }

            fireplace.IsDeleted = true;
            fireplace.DeletedOn = DateTime.UtcNow;

            await this.productService.DeleteAsync(fireplace.ProductId);
            this.fireplaceRepository.Update(fireplace);

            await this.fireplaceRepository.SaveChangesAsync();
        }

        public async Task<string> EditAsync<T>(EditFireplaceViewModel editModel)
        {
            var fireplace = this.fireplaceRepository.All().Where(f => f.Id == editModel.Id).FirstOrDefault();

            if (fireplace == null)
            {
                throw new NullReferenceException(string.Format(ExceptionsServices.Null_Fireplace_Id_ErrorMessage, editModel.Id));
            }

            var photoUrl = string.Empty;

            if (editModel.ImagePath == null)
            {
                photoUrl = fireplace.ImagePath;
            }
            else
            {
                photoUrl = await this.cloudinaryService.UploadPhotoAsync(
                editModel.ImagePath,
                $"{editModel.Name}_{editModel.Id}",
                GlobalConstants.CloudFolderForFireplacePhotos);
            }

            var product = this.productService.GetById(fireplace.ProductId);
            var typeOfChamber = Enum.Parse<TypeOfChamber>(editModel.TypeOfChamber);

            product.Name = editModel.Name;
            fireplace.Power = editModel.Power;
            fireplace.Chimney = editModel.Chimney;
            fireplace.Price = editModel.Price;
            fireplace.Size = editModel.Size;
            fireplace.TypeOfChamber = typeOfChamber;
            fireplace.Description = editModel.Description;
            fireplace.ImagePath = photoUrl; // To upload the new image and ad the new

            await this.fireplaceRepository.SaveChangesAsync();
            return fireplace.Id;
        }

        public IEnumerable<T> GetAllFireplace<T>(int? take = null, int skip = 0)
        {
            IQueryable<Fireplace_chamber> fireplaces = this.fireplaceRepository
                .AllAsNoTracking()
                .OrderBy(x => x.Product.Name)
                .Skip(skip);

            if (take.HasValue)
            {
                fireplaces = fireplaces.Take(take.Value);
            }

            return fireplaces.To<T>().ToList();
        }

        public IEnumerable<T> GetAllFireplaceAsync<T>(string type, int? take = null, int skip = 0)
        {
            var typeOfChamber = Enum.Parse<TypeOfChamber>(type);

            IQueryable<Fireplace_chamber> fireplaces = this.fireplaceRepository
                .All()
                .OrderBy(x => x.Product.Name)
                .Where(x => x.TypeOfChamber == typeOfChamber)
                .Skip(skip);

            if (take.HasValue)
            {
                fireplaces = fireplaces.Take(take.Value);
            }

            return fireplaces.To<T>().ToList();
        }

        public T GetById<T>(string id)
        {
            var fireplaceId = this.fireplaceRepository
                .All()
                .Where(f => f.Id == id)
                .To<T>().FirstOrDefault();

            if (fireplaceId == null)
            {
                throw new ArgumentNullException(
                    string.Format(string.Format(ExceptionsServices.Null_Fireplace_Id_ErrorMessage, id)));
            }

            return fireplaceId;
        }

        public T GetByName<T>(string name)
        {
            var fireplace = this.fireplaceRepository
                .All().Where(x => x.Product.Name == name)
                .To<T>().FirstOrDefault();

            if (fireplace == null)
            {
                throw new ArgumentNullException(
                    string.Format(InvalidFireplaceNameErrorMessage, name));
            }

            return fireplace;
        }

        public int GetCountByTypeOfChamber(string type)
        {
            var typeOfChamber = Enum.Parse<TypeOfChamber>(type);

            return this.fireplaceRepository.All().Count(x => x.TypeOfChamber == typeOfChamber);
        }

        private async Task AddProductToFireplace(string fireplaceId, string productName, string groupName)
        {
            var productId = this.productService.GetIdByNameAndGroup(productName, groupName);
            if (productId == null)
            {
                throw new NullReferenceException();
            }

            await this.suggestProduct.AddSuggestProductAsync(fireplaceId, productId);
        }
    }
}
