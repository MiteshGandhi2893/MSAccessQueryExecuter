using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AccessQueryRunner
{
   public class Class1
    {
        static string add;
        
        public static string address
        {
            get
            {
                return add;

            }
            set
            {
                add = value;
            }
        }
    }
}