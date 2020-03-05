using KaminiCenter.Data.Common.Repositories;
using KaminiCenter.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KaminiCenter.Services.Common.Exceptions;
using System.Linq;
using KaminiCenter.Services.Models.Group;

namespace KaminiCenter.Services.Data.GroupService
{
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
            var category = new Product_Group
            {
                GroupName = name,
            };

            await this.groupRepository.AddAsync(category);
            await this.groupRepository.SaveChangesAsync();
        }

        public Product_Group FindByGroupName(string name)
        {
            var group = this.groupRepository.All().FirstOrDefault(g => g.GroupName == name);

            return group;
        }

        public Task FindById(string id)
        {
            throw new NotImplementedException();
        }
    }
}
