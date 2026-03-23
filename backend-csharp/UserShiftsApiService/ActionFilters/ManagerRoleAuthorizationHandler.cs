using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using UserShiftsApiService.Services;

namespace UserShiftsApiService.ActionFilters;

public class ManagerRoleAuthorizationHandler : AuthorizationHandler<ManagerRoleRequirement>
{
    private readonly IAuth0UserRoleService _auth0UserRoleService;

    public ManagerRoleAuthorizationHandler(IAuth0UserRoleService auth0UserRoleService)
    {
        _auth0UserRoleService = auth0UserRoleService;
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ManagerRoleRequirement requirement)
    {
        if (_auth0UserRoleService.IsManager(context.User))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
