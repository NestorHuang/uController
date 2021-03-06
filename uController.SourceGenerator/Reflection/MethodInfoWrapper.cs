﻿using System.Collections.Generic;
using System.Data;
using System.Globalization;
using Microsoft.CodeAnalysis;

namespace System.Reflection
{
    internal partial class MethodInfoWrapper : MethodInfo
    {
        private IMethodSymbol _method;

        public MethodInfoWrapper(IMethodSymbol method)
        {
            _method = method;
        }

        public override ICustomAttributeProvider ReturnTypeCustomAttributes => throw new NotImplementedException();

        public override MethodAttributes Attributes => throw new NotImplementedException();

        public override RuntimeMethodHandle MethodHandle => throw new NotImplementedException();

        public override Type DeclaringType => new TypeWrapper(_method.ContainingType);

        public override Type ReturnType => new TypeWrapper((INamedTypeSymbol)_method.ReturnType);

        public override string Name => _method.Name;

        public override Type ReflectedType => throw new NotImplementedException();
        public override IList<CustomAttributeData> GetCustomAttributesData()
        {
            var attributes = new List<CustomAttributeData>();
            foreach (var a in _method.GetAttributes())
            {
                attributes.Add(new CustomAttributeDataWrapper(a));
            }
            return attributes;
        }

        public override MethodInfo GetBaseDefinition()
        {
            throw new NotImplementedException();
        }

        public override object[] GetCustomAttributes(bool inherit)
        {
            throw new NotImplementedException();
        }

        public override object[] GetCustomAttributes(Type attributeType, bool inherit)
        {
            throw new NotImplementedException();
        }

        public override MethodImplAttributes GetMethodImplementationFlags()
        {
            throw new NotImplementedException();
        }

        public override ParameterInfo[] GetParameters()
        {
            var parameters = new List<ParameterInfo>();
            foreach (var p in _method.Parameters)
            {
                parameters.Add(new ParameterWrapper(p));
            }
            return parameters.ToArray();
        }

        public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override bool IsDefined(Type attributeType, bool inherit)
        {
            throw new NotImplementedException();
        }
    }
}