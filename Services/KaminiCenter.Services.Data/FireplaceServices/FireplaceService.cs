namespace KaminiCenter.Services.Data.FireplaceServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using KaminiCenter.Data.Common.Repositories;
    using KaminiCenter.Data.Models;
    using KaminiCenter.Data.Models.Enums;
    using KaminiCenter.Services.Data.GroupService;
    using KaminiCenter.Services.Data.ProductService;
    using KaminiCenter.Services.Mapping;
    using KaminiCenter.Web.ViewModels.Fireplace;

    public class FireplaceService : IFireplaceService
    {
        private readonly IDeletableEntityRepository<Fireplace_chamber> fireplaceRepository;
        private readonly IGroupService groupService;
        private readonly IProductService productService;
        private readonly IMapper mapper;

        public FireplaceService(
            IDeletableEntityRepository<Fireplace_chamber> fireplaceRepository, IGroupService groupService, IProductService productService, IMapper mapper)
        {
            this.fireplaceRepository = fireplaceRepository;
            this.groupService = groupService;
            this.productService = productService;
            this.mapper = mapper;
        }

        public async Task AddFireplaceAsync(AddFireplaceInputModel model)
        {
            var typeOfChamber = Enum.Parse<TypeOfChamber>(model.TypeOfChamber);

            if (model.Power == null ||
                model.Size == null ||
                model.Chimney == null ||
                model.ImagePath == null)
            {
                throw new ArgumentNullException("Cannot safe null or whitespace values!");
            }

            await this.productService.AddProductAsync(model.Name, model.Group);
            var productId = this.productService.GetIdByNameAndGroup(model.Name, model.Group);
            var groupId = this.groupService.FindByGroupName(model.Group).Id;

            var fireplace = new Fireplace_chamber
            {
                Id = Guid.NewGuid().ToString(),
                Power = model.Power,
                Size = model.Size,
                Chimney = model.Chimney,
                Price = model.Price,
                Description = model.Description,
                ImagePath = model.ImagePath,
                TypeOfChamber = typeOfChamber,
                ProductId = productId,
                GroupId = groupId,
            };

            await this.fireplaceRepository.AddAsync(fireplace);
            await this.fireplaceRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllFireplaceAsync<T>(string type)
        {
            IQueryable<Fireplace_chamber> fireplaces = this.fireplaceRepository
                .All()
                .OrderBy(x => x.Product.Name);

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
