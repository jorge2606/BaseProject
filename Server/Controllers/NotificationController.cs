using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using server.Dto;
using server.IServices;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class NotificationController : ControllerBase
    {
        private INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

   
        [HttpGet("GetAllNotifications")]
        [AllowAnonymous]
        public ActionResult<List<NotificationDto>> GetAllNotifications()
        {
            return _notificationService.GetAllNotifications();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public ActionResult<List<NotificationDto>> GetNotificationsById(Guid id)
        {
            return _notificationService.GetNotificationsById(id);
        }


    }

}