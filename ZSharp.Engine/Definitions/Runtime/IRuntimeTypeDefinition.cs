using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZSharp.Engine.Definitions
{
    /// <summary>
    /// Defines a runtime type definition
    /// </summary>
    public interface IRuntimeTypeDefinition
        : INamedItem
    {
        /// <summary>
        /// The runtime definition of the type
        /// </summary>
        TypeDefinition Definition { get; }
    }
}
