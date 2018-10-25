using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public static string StaticFilesDirectory = Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles");

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

                var newDirectory =
                    Path.Combine(StaticFilesDirectory, "Profile", model.UserId.ToString());

                if (!Directory.Exists(newDirectory))
                {
                    Directory.CreateDirectory(newDirectory);
                }

                var filePath = Path.Combine(newDirectory, file.FileName);

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

        public ServiceResult<FileByIdDto> GetByIdFile(Guid userId)
        {
            var path = Path.Combine(StaticFilesDirectory,"Profile",userId.ToString());
            var files = Directory.EnumerateFiles(path, "*.*");

            FileByIdDto imagesPath = new FileByIdDto();

            if (files.Count() == 1)
            {
                string pathFile = "";

                foreach (var i in files)
                {
                    pathFile = i;
                }

                imagesPath.Paths = pathFile;
            }

            return new ServiceResult<FileByIdDto>(imagesPath);
        }
    }
}
