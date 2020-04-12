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
    using KaminiCenter.Web.ViewModels.Accessories;

    public class AccessoriesService : IAccessoriesService
    {
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

        public Task<string> EditAsync<T>(EditAccessorieViewModel editModel)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAllAccessorieAsync<T>(int? take = null, int skip = 0)
        {
            throw new NotImplementedException();
        }

        public T GetById<T>(string id)
        {
            throw new NotImplementedException();
        }

        public T GetByName<T>(string name)
        {
            throw new NotImplementedException();
        }

        public int GetCount()
        {
            throw new NotImplementedException();
        }
    }
}
