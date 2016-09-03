using System;
using System.Collections.Generic;
using System.Linq;

namespace TheSeeker.Initialization
{
    /// <summary>
    /// An exception that indicates that the type does not implement the target type
    /// </summary>
    public class TypeDoesNotImplementException : Exception
    {
        public Type Type { get; }
        public Type TargetType { get; }

        public TypeDoesNotImplementException(Type type, Type targetType) :
            base($"Type \"{type.AssemblyQualifiedName}\" does not implement \"{targetType.AssemblyQualifiedName}\"")
        {
            Type = type;
            TargetType = targetType;
        }
    }
}
