using System;
using System.Collections.Generic;
using System.Text;

namespace CrocusoftProje.Identity.ViewModel
{
    public class PermissionParameterDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string DefaultValue { get; set; }

        public List<string> Values { get; set; }
    }
}
