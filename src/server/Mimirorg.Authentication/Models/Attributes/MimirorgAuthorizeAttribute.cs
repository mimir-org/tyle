using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Mimirorg.Authentication.Extensions;
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
            var isAuthorized = !string.IsNullOrEmpty(context.HttpContext.User.Identity?.Name);
            var propValue = GetClaimTypeValue(context);

            var hasPermission = context.HttpContext.HasPermission(_permission, propValue);

            if (!isAuthorized)
                context.Result = new UnauthorizedResult();
            else if (!hasPermission)
                context.Result = new ForbidResult();
            else
                await next();
        }

        private string GetClaimTypeValue(ActionExecutingContext context)
        {
            if (string.IsNullOrEmpty(_property))
                return null;

            var propValue = context.ActionArguments.FirstOrDefault(x => x.Key.Equals(_property)).Value;
            if (propValue == null)
                throw new NullReferenceException($"Couldn't find a property with name {_property}");

            if (string.IsNullOrEmpty(_member))
                return propValue.ToString();

            var memberValue = propValue.GetPropValue(_member);

            if (memberValue == null)
                throw new NullReferenceException($"Couldn't find member property with name {_member}");

            return memberValue.ToString();
        }
    }
}