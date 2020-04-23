namespace KaminiCenter.Services.Data.SuggestProduct
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using KaminiCenter.Data.Common.Repositories;
    using KaminiCenter.Data.Models;
    using KaminiCenter.Services.Mapping;

    public class SuggestProdcut : ISuggestProduct
    {
        private readonly IDeletableEntityRepository<SuggestProduct> suggestProductRepository;

        public SuggestProdcut(IDeletableEntityRepository<SuggestProduct> suggestProductRepository)
        {
            this.suggestProductRepository = suggestProductRepository;
        }

        public async Task AddSuggestProductAsync(string fireplaceId, string productId)
        {
            var suggestProduct = new SuggestProduct
            {
                Id = Guid.NewGuid().ToString(),
                FireplaceId = fireplaceId,
                ProductId = productId,
            };

            await this.suggestProductRepository.AddAsync(suggestProduct);
            await this.suggestProductRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllSuggestion<T>(string fireplaceId)
        {
            IQueryable<SuggestProduct> suggestions = this.suggestProductRepository
                .All()
                .Where(x => x.FireplaceId == fireplaceId);

            return suggestions.To<T>().ToList();
        }
    }
}
