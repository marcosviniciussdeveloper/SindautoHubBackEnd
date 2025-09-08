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
        ["Agent"] = new[] {
            Permission.CreateTicket,
            Permission.ViewQueue,
            Permission.AttendAssignedTicket,
            Permission.CloseTicket
        },
        ["Supervisor"] = new[] {
            Permission.ViewQueue,
            Permission.ManageUsers,
            Permission.ViewStats,
            Permission.CloseTicket
        },
        ["Admin"] = Enum.GetValues<Permission>()
    };

    public static bool HasPermission(string role, Permission permission)
    {
        return _permissions.TryGetValue(role, out var list) && list.Contains(permission);
    }

    public static IEnumerable<Permission> GetPermissions(string role) 
     {
         return _permissions.TryGetValue(role , out var list) ? list : Enumerable.Empty<Permission>();

}   }
