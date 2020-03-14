namespace KaminiCenter.Services.Data.FireplaceServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using KaminiCenter.Common;
    using KaminiCenter.Data.Common.Repositories;
    using KaminiCenter.Data.Models;
    using KaminiCenter.Data.Models.Enums;
    using KaminiCenter.Services;
    using KaminiCenter.Services.Data.GroupService;
    using KaminiCenter.Services.Data.ProductService;
    using KaminiCenter.Services.Mapping;
    using KaminiCenter.Web.ViewModels.Fireplace;

    public class FireplaceService : IFireplaceService
    {
        private readonly IDeletableEntityRepository<Fireplace_chamber> fireplaceRepository;
        private readonly IGroupService groupService;
        private readonly IProductService productService;
        private readonly ICloudinaryService cloudinaryService;
        private readonly IEnumParseService enumParse;

        public FireplaceService(
            IDeletableEntityRepository<Fireplace_chamber> fireplaceRepository, IGroupService groupService, IProductService productService, ICloudinaryService cloudinaryService, IEnumParseService enumParse)
        {
            this.fireplaceRepository = fireplaceRepository;
            this.groupService = groupService;
            this.productService = productService;
            this.cloudinaryService = cloudinaryService;
            this.enumParse = enumParse;
        }

        public async Task AddFireplaceAsync(FireplaceInputModel model)
        {
            var typeOfChamber = Enum.Parse<TypeOfChamber>(model.TypeOfChamber);

            if (model.Power == null ||
                model.Size == null ||
                model.Chimney == null ||
                model.ImagePath == null)
            {
                throw new ArgumentNullException("Cannot safe null or whitespace values!");
            }

            await this.productService.AddProductAsync(model.Name);
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
        }

        public IEnumerable<T> GetAllFireplaceAsync<T>(string type)
        {
            IQueryable<Fireplace_chamber> fireplaces = this.fireplaceRepository
                .AllAsNoTracking();

            return fireplaces.To<T>().ToList();
        }

        public T GetByName<T>(string name)
        {
            var fireplace = this.fireplaceRepository
                .All().Where(x => x.Product.Name == name)
                .To<T>().FirstOrDefault();

            return fireplace;
        }
    }
}
