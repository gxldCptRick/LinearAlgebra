using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearAlgebra
{
    /// <summary>
    /// The different States that a system of equations could possibly be in.
    /// </summary>
    public enum SystemState
    {
        Consistent = 1,
        Inconsistent =  1 << 1,
        Dependent = 1 << 2
    }
}
