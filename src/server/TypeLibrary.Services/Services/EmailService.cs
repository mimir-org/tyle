using Microsoft.AspNetCore.Http;
using Mimirorg.Authentication.Contracts;
using Mimirorg.Common.Enums;
using Mimirorg.Common.Exceptions;
using Mimirorg.Common.Extensions;
using Mimirorg.TypeLibrary.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Services.Services
{
    public class EmailService : IEmailService
    {
        private readonly ILogService _logService;
        private readonly IMimirorgUserService _mimirorgUserService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IMimirorgTemplateRepository _templateRepository;
        private readonly IMimirorgEmailRepository _emailRepository;

        public EmailService(ILogService logService, IMimirorgUserService mimirorgUserService, IHttpContextAccessor contextAccessor, IMimirorgTemplateRepository templateRepository, IMimirorgEmailRepository emailRepository)
        {
            _logService = logService;
            _mimirorgUserService = mimirorgUserService;
            _contextAccessor = contextAccessor;
            _templateRepository = templateRepository;
            _emailRepository = emailRepository;
        }

        /// <summary>
        /// Send email to appropiate users when state has changed
        /// </summary>
        /// <param name="objectId"></param>
        /// <param name="objectState"></param>
        /// <param name="objectName"></param>
        /// <param name="objectTypeName"></param>
        /// <returns></returns>
        public async Task SendObjectStateEmail(string objectId, State objectState, string objectName, string objectTypeName)
        {
            var sendEmailToUserIds = new List<string>();
            var users = await _mimirorgUserService.GetUsers();
            var currentUser = users.FirstOrDefault(x => x.Id == _contextAccessor.GetUserId());

            if (currentUser == null)
                throw new MimirorgNotFoundException($"Current user not found. Unable to send state '{objectState}' email for {objectTypeName} {objectName}.");

            var usersExceptCurrent = users.Where(x => x.Id != currentUser.Id).ToList();

            switch (objectState)
            {
                case State.Draft:
                    return;

                case State.Approve:
                    var canApprove = usersExceptCurrent.Where(x => x.Permissions.ContainsKey(currentUser.CompanyId) && x.Permissions[currentUser.CompanyId].HasFlag(MimirorgPermission.Approve)).DistinctBy(x => x.Id).ToList();
                    sendEmailToUserIds.AddRange(canApprove.Select(x => x.Id));
                    break;

                case State.Delete:
                    var canDelete = usersExceptCurrent.Where(x => x.Permissions.ContainsKey(currentUser.CompanyId) && x.Permissions[currentUser.CompanyId].HasFlag(MimirorgPermission.Delete)).DistinctBy(x => x.Id).ToList();
                    sendEmailToUserIds.AddRange(canDelete.Select(x => x.Id));
                    break;

                case State.Approved:
                    var sendApprovedToUsers = _logService.Get().Where(x => x.ObjectId == objectId && (x.LogTypeValue == State.Approve.ToString() || x.LogTypeValue == State.Draft.ToString()));
                    sendEmailToUserIds.AddRange(sendApprovedToUsers.Select(x => x.CreatedBy));
                    break;

                case State.Deleted:
                    var sendDeletedToUsers = _logService.Get().Where(x => x.ObjectId == objectId && (x.LogTypeValue == State.Delete.ToString() || x.LogTypeValue == State.Draft.ToString()));
                    sendEmailToUserIds.AddRange(sendDeletedToUsers.Select(x => x.CreatedBy));
                    break;

                case State.Rejected:
                    var sendRejectedToUsers = _logService.Get().Where(x => x.ObjectId == objectId && (x.LogTypeValue == State.Approve.ToString() || x.LogTypeValue == State.Delete.ToString() || x.LogTypeValue == State.Draft.ToString()));
                    sendEmailToUserIds.AddRange(sendRejectedToUsers.Select(x => x.CreatedBy));
                    break;

                default:
                    throw new ArgumentOutOfRangeException($"'SendObjectStateEmail' switch with state '{objectState}' not found");
            }

            var sendEmailToUsers = sendEmailToUserIds.Distinct().Select(id => usersExceptCurrent.FirstOrDefault(x => x.Id == id)).ToList();

            foreach (var sendToUser in sendEmailToUsers)
            {
                var email = await _templateRepository.CreateObjectStateEmail(sendToUser, currentUser, objectState, objectName, objectTypeName);
                await _emailRepository.SendEmail(email);
            }
        }
    }
}