namespace UserShiftsApiService.UserContext;

public class UserContextProvider : IUserContextProvider
{
    private UserContext _userContext;
    
    public UserContext GetUserContext()
    {
        return _userContext;
    }

    public void SetUserContext(UserContext userContext)
    {
        _userContext = userContext;
    }
}