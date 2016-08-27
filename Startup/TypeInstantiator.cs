using System;
using System.Collections.Generic;
using System.Linq;

namespace TheSeeker.Startup
{
    public static class TypeInstantiator
    {
        /// <summary>
        /// Gets the specified type and checks if it implements the desired interface throwing an exception if it doesn't
        /// </summary>
        /// <param name="typeName">A qualified name of the type to get</param>
        /// <param name="requiredInterfaceType">An interface that the desired type must implement</param>
        /// <param name="genericTypeArguments">An optional array of types to be for creation of a generic type</param>
        /// <returns></returns>
        /// <exception cref="TypeDoesNotImplementException">When the target type does not implement</exception>
        public static Type GetType(string typeName, Type requiredInterfaceType, Type[] genericTypeArguments = null)
        {
            Type type = Type.GetType(typeName, true);

            if (type.IsGenericType && genericTypeArguments != null)
                type = type.MakeGenericType(genericTypeArguments);

            if (!type.GetInterfaces().Any(t => t == requiredInterfaceType))
                throw new TypeDoesNotImplementException(type, requiredInterfaceType);

            return type;
        }

        /// <summary>
        /// Creates an instance of the specified type checking if the type implements the desired interface beforehand
        /// </summary>
        /// <param name="typeName">A qualified name of the desired type</param>
        /// <param name="requiredInterfaceType">An interface that the desired type must implement</param>
        /// <param name="genericTypeArguments">An array of types to be for creation of a generic type</param>
        /// <param name="arguments">An array of objects to be passed to the constructor when instantiating the type</param>
        /// <returns></returns>
        /// <exception cref="TypeDoesNotImplementException">When the target type does not implement</exception>
        public static TypeInstance CreateInstance(string typeName, Type requiredInterfaceType, Type[] genericTypeArguments, params object[] arguments)
        {
            Type type = GetType(typeName, requiredInterfaceType, genericTypeArguments);

            object instance = Activator.CreateInstance(type, arguments);

            return new TypeInstance
            {
                Type = type,
                Instance = instance
            };
        }
    }

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
