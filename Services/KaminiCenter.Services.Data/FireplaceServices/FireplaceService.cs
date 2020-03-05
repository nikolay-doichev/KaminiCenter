namespace KaminiCenter.Services.Data.FireplaceServices
{
    using System;
    using System.Threading.Tasks;
    using KaminiCenter.Data.Common.Repositories;
    using KaminiCenter.Data.Models;
    using KaminiCenter.Data.Models.Enums;
    using KaminiCenter.Services.Data.GroupService;
    using KaminiCenter.Services.Data.ProductService;
    using KaminiCenter.Web.ViewModels.Fireplace;

    public class FireplaceService : IFireplaceService
    {
        private readonly IDeletableEntityRepository<Fireplace_chamber> fireplaceRepository;
        private readonly IGroupService groupService;
        private readonly IProductService productService;

        public FireplaceService(
            IDeletableEntityRepository<Fireplace_chamber> fireplaceRepository, IGroupService groupService, IProductService productService)
        {
            this.fireplaceRepository = fireplaceRepository;
            this.groupService = groupService;
            this.productService = productService;
        }

        public async Task AddFireplaceAsync(AddFireplaceInputModel model)
        {
            var typeOfChamber = Enum.Parse<TypeOfChamber>(model.TypeOfChamber);

            if (model.Power == null ||
                model.Size == null ||
                model.Chimney == null ||
                model.Price == null ||
                model.ImagePath == null ||
                typeOfChamber == null)
            {
                throw new ArgumentNullException("Cannot safe null or whitespace values!");
            }

            var product = this.productService.AddProductAsync(model.Name, model.Group);
            

            var fireplace = new Fireplace_chamber
            {
                Power = model.Power,
                Size = model.Size,
                Chimney = model.Chimney,
                Price = model.Price,
                Description = model.Description,
                ImagePath = model.ImagePath,
                TypeOfChamber = typeOfChamber,
                //ProductId = model.PorductId,
                //GroupId = model.GroupId,
            };

            await this.fireplaceRepository.AddAsync(fireplace);
            await this.fireplaceRepository.SaveChangesAsync();
        }
    }
}
