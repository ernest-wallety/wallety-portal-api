namespace Wallety.Portal.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class IgnoreMappingAttribute : Attribute
    {
        // Doesn't need anything in it; just its presence is enough. Used to ignore the mapping on criteria.
    }
}
