using System;

namespace Server.Misc
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DispellableAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class DispellableFieldAttribute : Attribute
    {
    }
}
