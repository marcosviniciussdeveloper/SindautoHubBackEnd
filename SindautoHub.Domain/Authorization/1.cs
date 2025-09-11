namespace SindautoHub.Domain.Entities;

public enum Permission
{
    CreateTicket,
    ViewQueue,
    AttendAssignedTicket,
    CloseTicket,
    ManageUsers,
    ViewStats
}


public static class RolePermissions
{
    private static readonly Dictionary<string, Permission[]> _permissions = new()
    {
        ["Agent"] = new[] { Permission.CreateTicket, Permission.ViewQueue },
        ["Admin"] = new[] { Permission.ViewQueue, Permission.ManageUsers, Permission.ViewStats },
        ["Master"] = Enum.GetValues<Permission>().ToArray()
    };

    public static IEnumerable<Permission> GetPermissions(string role)
    {
        if (string.IsNullOrWhiteSpace(role)) return Enumerable.Empty<Permission>();
        return _permissions.TryGetValue(role, out var list) ? list : Enumerable.Empty<Permission>();
    }



    public static bool HasPermission(string role, Permission permission)
    {
        return _permissions.TryGetValue(role, out var list) && list.Contains(permission);
    }

}
