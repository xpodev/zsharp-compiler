using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSharp.Core;

namespace ZSharp.Engine
{
    public class TypeReference : INamedObject
    {
        public string Name { get; set; }

        public CG.TypeReference CG { get; }

        public SR.TypeReference SR { get; }

        public TypeReference(CG.TypeReference cg, SR.TypeReference sr)
        {
            CG = cg;
            SR = sr;

            if (cg is not null && sr is not null && cg.Name != sr.Name) throw new Exception("TypeReference name mismatch");

            Name = ((INamedItem)cg ?? sr).Name;
        }
    }
}
