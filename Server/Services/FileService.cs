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

            var path = Path.Combine(StaticFilesDirectory, "Profile", model.UserId.ToString());

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var newDirectory = Path.Combine(StaticFilesDirectory, "Profile", model.UserId.ToString());
            var files = Directory.EnumerateFiles(path, "*.*" );

            
            if (files.Count() == 1)
            {
               Directory.Delete(path,true);

                Directory.CreateDirectory(newDirectory);
            }

            var file = model.File;

            using (var stream = file.OpenReadStream())
            {
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

            FileInfo file1;

            FileByIdDto imagesPath = new FileByIdDto();

            if (files.Count() == 1)
            {
                string pathFile = "";

                foreach (var i in files)
                {
                    file1 = new FileInfo(i.ToString());
                    pathFile = file1.Name;
                }

                imagesPath.Paths = pathFile;
            }

            return new ServiceResult<FileByIdDto>(imagesPath);
        }

        public ServiceResult<FileByIdDto> GetCompletePath(Guid userId)
        {
            var path = Path.Combine(StaticFilesDirectory, "Profile", userId.ToString());

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                
                File.Copy(Path.Combine(StaticFilesDirectory, "user.png"), Path.Combine(path, "user.png"));
            }

            FileByIdDto imagesPath = new FileByIdDto();

            var files = Directory.EnumerateFiles(path, "*.*");
     
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
