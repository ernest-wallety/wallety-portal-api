namespace Wallety.Portal.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Enum | AttributeTargets.Class | AttributeTargets.Interface)]
    public class ExportToTypescriptAttribute : Attribute
    {
        // Doesn't need anything in it; just its presence is enough.
    }
}
