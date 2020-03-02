using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KaminiCenter.Services
{
    public interface ICloudinaryService
    {
        Task<string> UploadPhotoAsync(IFormFile file, string fileName, string folder);
    }
}
