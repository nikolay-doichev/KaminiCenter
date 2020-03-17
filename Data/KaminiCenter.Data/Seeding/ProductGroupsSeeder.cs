namespace KaminiCenter.Data.Seeding
{
    using KaminiCenter.Data.Models;
    using KaminiCenter.Data.Models.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ProductGroupsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Product_Groups.Any() && dbContext.Product_Groups.Count() == 4)
            {
                return;
            }

            var groups = new List<string> { "Fireplace", "Project", "Finished_Models", "Accessories" };

            foreach (var group in groups)
            {
                var groupEnum = Enum.Parse<GroupType>(group);

                if (dbContext.Product_Groups.Any(x => x.GroupName == groupEnum))
                {
                    continue;
                }
                else
                {
                    await dbContext.Product_Groups.AddAsync(new Product_Group
                    {
                        Id = Guid.NewGuid().ToString(),
                        GroupName = groupEnum,
                    });
                }
            }
        }
    }
}
