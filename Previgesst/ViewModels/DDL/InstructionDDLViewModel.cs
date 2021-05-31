using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Previgesst.ViewModels
{
    public class InstructionDDLViewModel
    {
        public int InstructionId { get; set; }
        public string Identificateur { get; set; }

        public string Dispositif { get; set; }

        public string Accessoire { get; set; }

        public string Description { get; set; }
    }
}