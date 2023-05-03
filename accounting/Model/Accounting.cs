namespace accounting.Model;

public class Accounting
{
    public Accounting(int id, User user, Device device, ConnectionPermission connectionPermission,
        ConnectionPermission disconnectionPermission)
    {
        Id = id;
        User = user;
        Device = device;
        ConnectionPermission = connectionPermission;
        DisconnectionPermission = disconnectionPermission;
    }

    public int Id { get; set; }

    public User User { get; }

    public Device Device { get; }

    public ConnectionPermission ConnectionPermission { get; }

    public ConnectionPermission DisconnectionPermission { get; }

    public override string ToString()
    {
        return
            $"{Id}, {ConnectionPermission.PermissionDate}, {Device.Model}, {User.DisplayName}, {User.Department}, {ConnectionPermission.PermissionNumber}, {DisconnectionPermission.PermissionNumber}";
    }
}