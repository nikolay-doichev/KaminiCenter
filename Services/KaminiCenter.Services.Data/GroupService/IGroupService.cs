using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KaminiCenter.Services.Data.GroupService
{
    public interface IGroupService
    {
        Task CreateAsync(string name);
    }
}
