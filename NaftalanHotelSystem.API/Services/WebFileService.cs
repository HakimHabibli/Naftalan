using NaftalanHotelSystem.Application.Abstractions.Services;

namespace NaftalanHotelSystem.API.Services
{
   
        public class WebFileService : IFileService
        {
            private readonly IWebHostEnvironment _env;
            private readonly string _rootPath;

            public WebFileService(IWebHostEnvironment env)
            {
                _env = env;
                _rootPath = _env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            }

            public string GetRootPath()
            {
                return _rootPath;
            }

            public string GetImagesPath()
            {
                // uploads/images alt folder yaradır
                var path = Path.Combine(_rootPath, "uploads", "images");
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                return path;
            }

            public string GenerateUniqueFileName(string originalFileName)
            {
                var extension = Path.GetExtension(originalFileName);
                return $"{Guid.NewGuid():N}{extension}";
            }
        }
    

}
