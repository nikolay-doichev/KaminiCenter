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
        private readonly IGroupService groupService;

        public GroupService(
            IDeletableEntityRepository<Product_Group> groupRepository, IGroupService groupService)
        {
            this.groupRepository = groupRepository;
            this.groupService = groupService;
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
            var groupId = this.groupRepository.GetByIdWithDeletedAsync(id);
            var groupSubstring = this.groupService.ToString().Substring(0, this.groupService.ToString().Length - 7);

            if (groupId == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionsServices.Null_Or_NotFound_Id_ErrorMessage, groupSubstring, id));
            }

            return groupId;
        }
    }
}
