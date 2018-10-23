using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using server.Dto;
using server.IServices;

namespace server.Controllers
{
    [Route("api/[controller]")]
    public class FileController : ControllerBase
    {
        private IFileService _fileService;

        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> saveFile(FileCreateDto fileCreateDto)
        {
           var result = await _fileService.Save(fileCreateDto);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result.Response);
        }


    }
}
