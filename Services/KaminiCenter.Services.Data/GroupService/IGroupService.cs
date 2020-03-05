using KaminiCenter.Data.Models;
using KaminiCenter.Services.Models.Group;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KaminiCenter.Services.Data.GroupService
{
    public interface IGroupService
    {
        Task CreateAsync(string name);

        Task FindById(string id);

        Product_Group FindByGroupName(string name);
    }
}
