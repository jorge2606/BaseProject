using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using server.Dto;
using System;
using server.ServiceResult;

namespace server.IServices
{
    public interface INotificationService
    {
        ActionResult<List<NotificationDto>> GetAllNotifications();
        ActionResult<List<NotificationDto>> GetNotificationsById(Guid id);
        ServiceResult<NotificationDto> NotificationRidden(Guid id);
    }
}
