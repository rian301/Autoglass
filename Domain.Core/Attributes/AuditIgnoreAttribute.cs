using System;

namespace Autoglass.Domain.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public sealed class AuditIgnoreAttribute : Attribute
    {

    }
}
