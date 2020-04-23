namespace KaminiCenter.Services.Data.GroupService
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using KaminiCenter.Data.Common.Repositories;
    using KaminiCenter.Data.Models;
    using KaminiCenter.Data.Models.Enums;

    public class GroupService : IGroupService
    {
        private readonly IDeletableEntityRepository<Product_Group> groupRepository;

        public GroupService(
            IDeletableEntityRepository<Product_Group> groupRepository)
        {
            this.groupRepository = groupRepository;
        }

        public async Task CreateAsync(string name)
        {
            var group = new Product_Group
            {
                Id = Guid.NewGuid().ToString(),
                GroupName = Enum.Parse<GroupType>(name),
                CreatedOn = DateTime.UtcNow,
                ModifiedOn = DateTime.UtcNow,
            };

            await this.groupRepository.AddAsync(group);
            await this.groupRepository.SaveChangesAsync();
        }

        public Product_Group FindByGroupName(string name)
        {
            var inputGroupName = Enum.Parse<GroupType>(name);
            var groupName = this.groupRepository.All().First(x => x.GroupName == inputGroupName);

            return groupName;
        }
    }
}
