using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Mimirorg.Authentication.Models.Constants;
using Mimirorg.Common.Extensions;
using Mimirorg.TypeLibrary.Enums;

namespace Mimirorg.Authentication.Models.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class MimirorgAuthorizeAttribute : TypeFilterAttribute
    {
        public MimirorgAuthorizeAttribute(MimirorgPermission permission) : base(typeof(MimirorgAuthorizeActionFilter))
        {
            Arguments = new object[] { permission, string.Empty, string.Empty };
        }

        public MimirorgAuthorizeAttribute(MimirorgPermission permission, string property) : base(typeof(MimirorgAuthorizeActionFilter))
        {
            Arguments = new object[] { permission, property, string.Empty };
        }

        public MimirorgAuthorizeAttribute(MimirorgPermission permission, string property, string member) : base(typeof(MimirorgAuthorizeActionFilter))
        {
            Arguments = new object[] { permission, property, member };
        }
    }

    public class MimirorgAuthorizeActionFilter : IAsyncActionFilter
    {
        private readonly MimirorgPermission _permission;
        private readonly string _property;
        private readonly string _member;

        public MimirorgAuthorizeActionFilter(MimirorgPermission permission, string property, string member)
        {
            _permission = permission;
            _property = property;
            _member = member;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var isAuthorized = Authorize(context, _permission);

            if (!isAuthorized)
                context.Result = new UnauthorizedResult();
            else
                await next();
        }

        public bool Authorize(ActionExecutingContext context, MimirorgPermission permission)
        {
            if (string.IsNullOrEmpty(context?.HttpContext.User.Identity?.Name))
                return false;

            // If the user is in administrator role, always return true
            if (context.HttpContext.User.IsInRole(MimirorgDefaultRoles.Administrator))
                return true;

            // If manage flag and is in account manager role, always return true 
            if (MimirorgPermission.Manage.HasFlag(permission) && context.HttpContext.User.IsInRole(MimirorgDefaultRoles.AccountManager))
                return true;

            // If delete flag and is in moderator role, always return true 
            if (MimirorgPermission.Delete.HasFlag(permission) && context.HttpContext.User.IsInRole(MimirorgDefaultRoles.Moderator))
                return true;

            var propValue = GetClaimTypeValue(context);
            if (propValue == null)
                return false;

            var userPermission = GetUserPermission(context, propValue);
            return userPermission.HasFlag(permission);
        }

        private static MimirorgPermission GetUserPermission(ActionContext context, string propValue)
        {
            var allClaimsForUser = context.HttpContext.User.Claims.ToList();
            var propertyValues = allClaimsForUser.Where(x => x.Type.Equals(propValue)).Select(x => x.Value).ToList();
            var claimsPropertyPermissions = new List<MimirorgPermission>();
            foreach (var propertyValue in propertyValues)
            {
                if (Enum.TryParse(propertyValue, out MimirorgPermission p))
                    claimsPropertyPermissions.Add(p);
            }
            return claimsPropertyPermissions.ConvertToFlag();
        }

        private string GetClaimTypeValue(ActionExecutingContext context)
        {
            if (string.IsNullOrEmpty(_property))
                return null;

            var propValue = context.ActionArguments.FirstOrDefault(x => x.Key.Equals(_property)).Value;

            return string.IsNullOrEmpty(_member) ?
                propValue?.ToString() :
                propValue.GetPropValue<string>(_member);
        }
    }
}
