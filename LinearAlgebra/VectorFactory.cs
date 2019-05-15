using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearAlgebra
{
    public class VectorFactory
    {
        Vector CreateVector(params double[] values) 
        {
            switch (values.Length)
            {
                case 2: return new VectorR2(values);
                case 3: return new VectorR3(values);
                default: return new Vector(values);
            }
        }
    }
}
