namespace KaminiCenter.Services.Data.FireplaceServices
{
    using System;
    using System.Threading.Tasks;
    using KaminiCenter.Data.Common.Repositories;
    using KaminiCenter.Data.Models;
    using KaminiCenter.Data.Models.Enums;
    using KaminiCenter.Web.ViewModels.Fireplace;

    public class FireplaceService : IFireplaceService
    {
        private readonly IDeletableEntityRepository<Fireplace_chamber> fireplaceRepository;

        public FireplaceService(
            IDeletableEntityRepository<Fireplace_chamber> fireplaceRepository)
        {
            this.fireplaceRepository = fireplaceRepository;
        }

        public async Task AddFireplaceAsync(AddFireplaceInputModel model)
        {
            var typeOfChamber = Enum.Parse<TypeOfChamber>(model.TypeOfChamber);

            var fireplace = new Fireplace_chamber
            {
                Power = model.Power,
                Size = model.Size,
                Chimney = model.Chimney,
                Price = model.Price,
                Description = model.Description,
                ImagePath = model.ImagePath,
                TypeOfChamber = typeOfChamber,
                PorductId = model.PorductId,
                GroupId = model.GroupId,
            };

            await this.fireplaceRepository.AddAsync(fireplace);
            await this.fireplaceRepository.SaveChangesAsync();
        }
    }
}
