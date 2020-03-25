using KaminiCenter.Services.Mapping;
using KaminiCenter.Data.Models;

namespace KaminiCenter.Services.Models.Product
{
    public class ProductServiceModel : IMapFrom<KaminiCenter.Data.Models.Product>
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
