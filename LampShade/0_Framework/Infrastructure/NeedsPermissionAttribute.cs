namespace _0_Framework.Infrastructure
{
    public class NeedsPermissionAttribute : Attribute
    {
        public int Permission { get; }

        public NeedsPermissionAttribute(int permission)
        {
            Permission = permission;
        }
    }
}
