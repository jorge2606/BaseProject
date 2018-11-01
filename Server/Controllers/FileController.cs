using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.FileProviders;
using server.Dto;
using server.IServices;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace server.Controllers
{
    [Route("api/[controller]")]
    public class FileController : ControllerBase
    {
        private IFileService _fileService;
        private IFileProvider _fileProvider;

        public FileController(IFileService fileService, IHostingEnvironment env)
        {
            _fileService = fileService;
            _fileProvider = env.ContentRootFileProvider;
        }

        [HttpPost("{userId}")]
        [AllowAnonymous]
        public async Task<IActionResult> saveFile(FileCreateDto fileCreateDto,Guid userId)
        {
           fileCreateDto.UserId = userId;

           var result = await _fileService.Save(fileCreateDto);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result.Response.File);
        }

        [HttpGet("{userId}")]
        [AllowAnonymous]
        public IActionResult FileById(Guid userId)
        {
           var result = _fileService.GetByIdFile(userId);
            if (!result.IsSuccess)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpDelete("removePhoto/{userId}")]
        [AllowAnonymous]
        public IActionResult RemoveFile(Guid userId)
        {
            var result = _fileService.RemoveProfilePhoto(userId);
            if (!result.IsSuccess)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpGet("{userId}/{width}/{height}")]
        [AllowAnonymous]
        public async Task<IActionResult> ResizeImage(Guid userId, int width, int height)
        {
            if (width < 0 || height < 0) { return BadRequest(); }

            var result = _fileService.GetCompletePath(userId);

            FileInfo fileInfo = new FileInfo(result.Response.Paths);

            var outputStream = new MemoryStream();

            if (!fileInfo.Exists) { return NotFound(); }

            using (var image = Image.Load(fileInfo.FullName))
            {
                image.Mutate(x => x
                    .Resize(width, height));
                
                image.SaveAsJpeg(outputStream);

                outputStream.Seek(0, SeekOrigin.Begin);

                return File(outputStream, "image/jpg");
            }
            
        }
    }

}
