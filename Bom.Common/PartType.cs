﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bom.Common
{
    public enum PartType : int
    {
        [Description("")]
        Undefined = 0,
        [Description("RHS")]
        RHS,
        [Description("Pipe")]
        Pipe,
        [Description("Flat")]
        Flat,
        [Description("Assembly")]
        Assembly
    }
}
