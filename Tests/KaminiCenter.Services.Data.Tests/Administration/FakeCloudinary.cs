namespace KaminiCenter.Services.Data.Tests.Administration
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public class FakeCloudinary : ICloudinaryService
    {
        public bool ReturnNullOnCreate { get; set; } = false;

        public async Task<string> UploadPhotoAsync(IFormFile image, string fileName, string folder)
        {
            if (this.ReturnNullOnCreate)
            {
                return null;
            }

            return image.Name;
        }

        public void DeleteCloudinary(string id)
        {
        }
    }
}
