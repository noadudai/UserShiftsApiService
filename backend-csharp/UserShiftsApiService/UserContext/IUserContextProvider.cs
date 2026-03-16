namespace UserShiftsApiService.UserContext;

public interface IUserContextProvider
{
    public UserContext GetUserContext();
    
    public void SetUserContext(UserContext userContext);
}