using Blacksmith.TypeExtensions;
using Blacksmith.TypeExtensions.Exceptions;
using Blacksmith.Validations;
using System;
using System.Linq;

namespace Blacksmith.Extensions.Types
{
    public static class TypeExtensions
    {
        private static readonly Validator<TypeException> assert;

        static TypeExtensions()
        {
            assert = new Validator<TypeException>(new EnValidatorStrings(), prv_buildException);
        }

        public static bool implementsInterface(this Type type, Type interfaceType)
        {
            return prv_implementsInterface(type, interfaceType);
        }

        public static bool implementsInterface<T>(this Type type) where T : class
        {
            return prv_implementsInterface(type, typeof(T));
        }

        public static bool extends(this Type type, Type predecesorType)
        {
            return prv_extends(type, predecesorType);
        }

        public static bool extends<T>(this Type type) where T : class
        {
            return prv_extends(type, typeof(T));
        }

        private static bool prv_implementsInterface(Type type, Type interfaceType)
        {
            assert.isNotNull(type);
            assert.isNotNull(interfaceType);
            assert.isFalse(type.IsInterface, $"Parameter {nameof(type)} cannot be interface.");
            assert.isTrue(interfaceType.IsInterface, $"Parameter {nameof(interfaceType)} must be interface.");

            return type
                .GetInterfaces()
                .Any(t => t.Equals(interfaceType))
                ;
        }

        private static bool prv_extends(Type type, Type predecesorType)
        {
            bool result, bothAreInterfaces, bothAreClasses;

            assert.isNotNull(type);
            assert.isNotNull(predecesorType);
            assert.isFalse(type.Equals(predecesorType), $"Types cannot be the same type.");

            bothAreInterfaces = type.IsInterface && predecesorType.IsInterface;
            bothAreClasses = type.IsClass && predecesorType.IsClass;

            assert.isTrue(bothAreInterfaces || bothAreClasses, $"Both types must be interfaces or classes.");

            if (bothAreInterfaces)
                result = type
                    .GetInterfaces()
                    .Any(t => t.Equals(predecesorType));
            else
                result = prv_extendsRecursive(type, predecesorType, 1);

            return result;
        }

        private static bool prv_extendsRecursive(Type type, Type predecesorType, int recursion)
        {
            bool result;
            Type baseType;

            assert.isTrue(recursion <= 1000, $"Max recursion level reached.");

            baseType = type.BaseType;

            if (baseType == null)
                result = false;
            else if (baseType.Equals(predecesorType))
                result = true;
            else
                result = prv_extendsRecursive(baseType, predecesorType, recursion + 1);

            return result;
        }

        private static TypeException prv_buildException(string message)
        {
            return new TypeException(message);
        }

    }
}
