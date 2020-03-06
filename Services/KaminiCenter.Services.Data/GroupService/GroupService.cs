using KaminiCenter.Data.Common.Repositories;
using KaminiCenter.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KaminiCenter.Services.Common.Exceptions;
using System.Linq;
using KaminiCenter.Services.Models.Group;
using AutoMapper;

namespace KaminiCenter.Services.Data.GroupService
{
    public class GroupService : IGroupService
    {
        private readonly IDeletableEntityRepository<Product_Group> groupRepository;
        private readonly IMapper mapper;

        public GroupService(
            IDeletableEntityRepository<Product_Group> groupRepository, IMapper mapper)
        {
            this.groupRepository = groupRepository;
            this.mapper = mapper;        }

        public async Task CreateAsync(string name)
        {
            var group = new Product_Group
            {
                Id = Guid.NewGuid().ToString(),
                GroupName = name,
            };

            await this.groupRepository.AddAsync(group);
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
