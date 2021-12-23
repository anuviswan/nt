namespace UserService.Service.Command;
public class ChangePasswordCommand:IRequest<bool>
{
    public User UserToUpdate { get; set; }
}