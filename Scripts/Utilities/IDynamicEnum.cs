using System;

namespace Server
{
    public interface IDynamicEnum
    {
        string Value { get; set; }
        string[] Values { get; }
        Boolean IsValid { get; }
    }
}
