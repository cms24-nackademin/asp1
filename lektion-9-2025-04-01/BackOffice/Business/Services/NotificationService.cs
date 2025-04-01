using Business.Models;
using Data.Entitites;
using Data.Repositories;
using Domain.Extensions;
using Domain.Models;
using Domain.Responses;
using Hubs;
using Microsoft.AspNetCore.SignalR;


namespace Business.Services;

public interface INotificationService
{
    Task<NotificationResult> AddNotificationAsync(NotificationFormData formData);
    Task DismissNotificationAsync(string notificationId, string userId);
    Task<NotificationResult<IEnumerable<Notification>>> GetNotificationsAsync(string userId, string? roleName = null, int take = 10);
}

public class NotificationService(INotificationRepository notificationRepository, INotificationTypeRepository notificationTypeRepository, INotificationTargetRepository notificationTargetRepository, IUserDismissedNotificationRepository userDismissedNotificationRepository, IHubContext<NotificationHub> notificationHub) : INotificationService
{
    private readonly INotificationRepository _notificationRepository = notificationRepository;
    private readonly INotificationTypeRepository _notificationTypeRepository = notificationTypeRepository;
    private readonly INotificationTargetRepository _notificationTargetRepository = notificationTargetRepository;
    private readonly IUserDismissedNotificationRepository _userDismissedNotificationRepository = userDismissedNotificationRepository;
    private readonly IHubContext<NotificationHub> _notificationHub =  notificationHub;


    public async Task<NotificationResult> AddNotificationAsync(NotificationFormData formData)
    {
        if (formData == null)
            return new NotificationResult { Succeeded = false, StatusCode = 400 };

        if (string.IsNullOrEmpty(formData.Image))
        {
            switch (formData.NotificationTypeId)
            {
                case 1:
                    formData.Image = "/images/profiles/user-template.svg";
                    break;

                case 2:
                    formData.Image = "/images/projects/project-template.svg";
                    break;
            }
        }

        var notificationEntity = formData.MapTo<NotificationEntity>();
        var result = await _notificationRepository.AddAsync(notificationEntity);

        if (result.Succeeded)
        {
            var notificationResult = await _notificationRepository.GetLatestNotification();
            await _notificationHub.Clients.All.SendAsync("ReceiveNotification", notificationResult.Result);
        }


        return result.Succeeded
            ? new NotificationResult { Succeeded = true, StatusCode = 200 }
            : new NotificationResult { Succeeded = false, StatusCode = result.StatusCode, Error = result.Error };
    }


    public async Task<NotificationResult<IEnumerable<Notification>>> GetNotificationsAsync(string userId, string? roleName = null, int take = 10)
    {
        var adminTargetName = "Admin";
        var dismissedNotificationResult = await _userDismissedNotificationRepository.GetNotificationsIdsAsync(userId);
        var dismissedNotificationIds = dismissedNotificationResult.Result;

        var notificationResult = (!string.IsNullOrEmpty(roleName) && roleName == adminTargetName)
            ? await _notificationRepository.GetAllAsync(orderByDescending: true, sortByColumn: x => x.CreateDate,
                filterBy: x => !dismissedNotificationIds!.Contains(x.Id), take: take)

            : await _notificationRepository.GetAllAsync(orderByDescending: true, sortByColumn: x => x.CreateDate,
                filterBy: x => !dismissedNotificationIds!.Contains(x.Id) && x.NotificationTarget.TargetName != adminTargetName, take: take,
                includes: x => x.NotificationTarget);

        if (!notificationResult.Succeeded)
            return new NotificationResult<IEnumerable<Notification>> { Succeeded = false, StatusCode = 404 };

        var notifications = notificationResult.Result!.Select(entity => entity.MapTo<Notification>());
        return new NotificationResult<IEnumerable<Notification>> { Succeeded = true, StatusCode = 200, Result = notifications };
    }


    public async Task DismissNotificationAsync(string notificationId, string userId)
    {
        var exists = await _userDismissedNotificationRepository.ExistsAsync(x => x.NotificationId == notificationId && x.UserId == userId);
        if (!exists.Succeeded)
        {
            var entity = new UserDismissedNotificationEntity
            {
                NotificationId = notificationId,
                UserId = userId
            };

            await _userDismissedNotificationRepository.AddAsync(entity);
            await _notificationHub.Clients.User(userId).SendAsync("NotificationDismissed", notificationId);
        }
    }
}