﻿using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using Microsoft.CodeAnalysis;

namespace System.Reflection
{
    public class MetadataLoadContext
    {
        private readonly Compilation _compilation;
        private readonly Dictionary<string, IAssemblySymbol> _assemblies = new Dictionary<string, IAssemblySymbol>(StringComparer.OrdinalIgnoreCase);

        public MetadataLoadContext(Compilation compilation)
        {
            _compilation = compilation;
            var assemblies = _compilation.References
                                         .OfType<PortableExecutableReference>()
                                         .ToDictionary(r => AssemblyName.GetAssemblyName(r.FilePath),
                                                       r => (IAssemblySymbol)_compilation.GetAssemblyOrModuleSymbol(r));

            foreach (var item in assemblies)
            {
                // REVIEW: Is the full name important
                // _assemblies[item.Key.FullName] = item.Value;
                _assemblies[item.Key.Name] = item.Value;
            }

            CoreAssembly = new AssemblyWrapper(compilation.GetTypeByMetadataName("System.Object").ContainingAssembly);
            MainAssembly = new AssemblyWrapper(compilation.Assembly);
        }

        public Assembly CoreAssembly { get; internal set; }
        public Assembly MainAssembly { get; internal set; }

        internal Assembly LoadFromAssemblyName(string fullName)
        {
            if (_assemblies.TryGetValue(new AssemblyName(fullName).Name, out var assembly))
            {
                return new AssemblyWrapper(assembly);
            }
            return null;
        }
    }
}
