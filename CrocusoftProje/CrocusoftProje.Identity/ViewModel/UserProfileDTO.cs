using System;
using System.Collections.Generic;
using System.Text;

namespace CrocusoftProje.Identity.ViewModel
{
    public class UserProfileDTO
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public bool ForcePasswordChange { get; set; }

        public DateTime? LastPasswordChangeDateTime { get; set; }

        public IDictionary<string, PermissionDTO> Permissions { get; set; }
    }
}
