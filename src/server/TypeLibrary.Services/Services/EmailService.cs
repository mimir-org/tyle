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

        public EmailService(ILogService logService, IMimirorgUserService mimirorgUserService, IHttpContextAccessor contextAccessor)
        {
            _logService = logService;
            _mimirorgUserService = mimirorgUserService;
            _contextAccessor = contextAccessor;
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
            var allUsers = await _mimirorgUserService.GetUsers();
            var currentUser = allUsers.FirstOrDefault(x => x.Id == _contextAccessor.GetUserId());

            if (currentUser == null)
                throw new MimirorgNotFoundException($"Current user not found. Unable to sendt state '{objectState}' email for object '{objectTypeName} {objectName}.");

            var companyApproveAndManagers = allUsers.Where(x => x.CompanyId == currentUser.CompanyId && x.Permissions.ContainsValue(MimirorgPermission.Approve) || x.Permissions.ContainsValue(MimirorgPermission.Manage)).ToList();
            var companyDeleteAndManagers = allUsers.Where(x => x.CompanyId == currentUser.CompanyId && x.Permissions.ContainsValue(MimirorgPermission.Delete) || x.Permissions.ContainsValue(MimirorgPermission.Manage)).ToList();

            switch (objectState)
            {
                case State.Draft:
                    return;

                case State.Approve:
                    sendEmailToUserIds.AddRange(companyApproveAndManagers.Select(x => x.Id));
                    break;

                case State.Approved:
                    var sendApprovedToUsers = _logService.Get().Where(x => x.ObjectId == objectId && x.LogTypeValue == State.Approve.ToString() || x.LogTypeValue == State.Draft.ToString()).ToList();
                    sendEmailToUserIds.AddRange(sendApprovedToUsers.Select(x => x.CreatedBy));
                    break;

                case State.Delete:
                    sendEmailToUserIds.AddRange(companyDeleteAndManagers.Select(x => x.Id));
                    break;

                case State.Deleted:
                    var sendDeletedToUsers = _logService.Get().Where(x => x.ObjectId == objectId && x.LogTypeValue == State.Delete.ToString() || x.LogTypeValue == State.Draft.ToString()).ToList();
                    sendEmailToUserIds.AddRange(sendDeletedToUsers.Select(x => x.CreatedBy));
                    break;

                default:
                    throw new ArgumentOutOfRangeException($"Switch with state '{objectState}' not found");
            }

            var sendEmailToUsers = sendEmailToUserIds.Distinct().Select(id => allUsers.FirstOrDefault(x => x.Id == id)).ToList();

            await _mimirorgUserService.CreateAndSendObjectStateEmail(sendEmailToUsers, currentUser, objectState, objectName, objectTypeName);
        }
    }
}