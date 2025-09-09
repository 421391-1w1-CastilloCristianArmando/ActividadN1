using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActividadN1.Data.Utils
{
    public class Parameters
    {
        public string Name { get; set; }
        public object Value { get; set; }

        public Parameters(string name, object id)
        {
            Name = name;
            Value = id;
        }
    }
}
