using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrocusoftProje.Identity.ViewModel
{
    public class PermissionDTO
    {
        public string Name { get; set; }

        public List<PermissionParameterDTO> Parameters { get; set; }

        public T GetParameterValue<T>(string permissionParameterName)
        {
            var scopeParameter = Parameters?.Where(p => p.Name.ToLower() == permissionParameterName.ToLower()).SingleOrDefault();

            string paramValue = scopeParameter.Values.FirstOrDefault();
            paramValue = paramValue ?? scopeParameter.DefaultValue;
            var type = typeof(T);
            if (type.IsEnum)
            {
                return (T)Enum.Parse(type, paramValue);
            }
            else
            {
                return (T)Convert.ChangeType(paramValue, typeof(T));
            }
        }
    }
}
