﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class SampleModule : Nancy.NancyModule
    {
        public SampleModule()
        {
            Get["/"] = _ => "Hello World!";
        }
    }
}
