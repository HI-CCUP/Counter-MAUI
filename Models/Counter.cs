using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestMauiApp.Models
{
    public class Counter
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Name {get; set;} = string.Empty;
        public int Count {get;set;}
        public int Red {get;set;} = 255;
        public int Green {get;set;} = 255;
        public int Blue {get;set;} = 255;

        public int DefaultValue {get; set;}
        public string IncrementButton => "+";

        public string DecrementButton => "-";

        public string DeleteButtonText => "X";

    }
}