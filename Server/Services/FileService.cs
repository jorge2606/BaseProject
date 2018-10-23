using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using server.Dto;
using server.IServices;
using server.Models;
using server.ServiceResult;

namespace server.Services
{
    public class FileService : IFileService
    {

        private string applicationRoot = "D:/Dev/Devlights/BaseProject/Server/wwwroot/images/";
        private DataContext _contextFile;
        private IMapper _mapper;

        public FileService(DataContext contextFile, IMapper mapper)
        {
            _contextFile = contextFile;
            _mapper = mapper;
        }

        public async Task<ServiceResult<FileCreateDto>> Save(FileCreateDto model)
        {
            var file = model.File;

            using (var stream = file.OpenReadStream())
            {
                var newDirectory = Directory.CreateDirectory(applicationRoot + "/" + model.UserId + "/");

                var filePath = Path.Combine(applicationRoot + model.UserId + "/", file.FileName);

                model.Path = filePath;
                model.MimeType = file.ContentType;

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
            }

            _contextFile.Files.Add(_mapper.Map<models.File>(model));
            _contextFile.SaveChanges();

           return new ServiceResult<FileCreateDto>(model);
        }
    }
}
