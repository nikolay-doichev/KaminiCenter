namespace KaminiCenter.Services.Data.SuggestProduct
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface ISuggestProduct
    {
        Task<string> AddSuggestProductAsync(string fireplaceId, string productId);

        IEnumerable<T> GetAllSuggestion<T>(string fireplaceId);
    }
}
