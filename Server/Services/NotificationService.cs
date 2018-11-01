using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using server.Dto;
using server.IServices;
using server.Models;
using server.ServiceResult;

namespace server.Services
{
    public class NotificationService : INotificationService
    {
        private DataContext _contextNotification;
        private IMapper _mapper;

        public NotificationService(DataContext context, IMapper mapper)
        {
            _contextNotification = context;
            _mapper = mapper;
        }


        public ActionResult<List<NotificationDto>> GetAllNotifications()
        {
            return _contextNotification.Notifications.Select(_mapper.Map<NotificationDto>).ToList();
        }


        public ActionResult<List<NotificationDto>> GetNotificationsById(Guid id)
        {
            return _contextNotification.Notifications.Select(_mapper.Map<NotificationDto>).
                    Where(x => x.Id == id).
                    Take(2).ToList();
            
        }

        public ServiceResult<NotificationDto> NotificationRidden(Guid id)
        {
            var notif = _contextNotification.Notifications.FirstOrDefault(x => x.Id == id);
            
            if (notif == null)
            {
                return new ServiceResult<NotificationDto>(null);
            }
            notif.Read = true;
            _contextNotification.Notifications.Update(notif);
            _contextNotification.SaveChanges();

            return new ServiceResult <NotificationDto>(_mapper.Map<NotificationDto>(notif));

        }


    }
}
