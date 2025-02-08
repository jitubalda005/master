using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Master
{
    public class UserManagment
    {
        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Username
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// DeviceType use mobile and web login same time
        /// </summary>
        [DefaultValue("Web")]
        public string DeviceType { get; set; } = "Web";

        /// <summary>
        /// Type
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// Credential
        /// </summary>
        public string Credential { get; set; }

    }

    public class LoginRequestBM
    {
        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Username
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// DeviceType use mobile and web login same time
        /// </summary>
        [DefaultValue("Web")]
        public string DeviceType { get; set; } = "Web";
    }
  
    /// <summary>
    /// LoginResponseBM
    /// </summary>
    public class LoginResponseBM
    {
        /// <summary>
        /// UserId
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// RoleId
        /// </summary>
        public int RoleId { get; set; }
        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Username
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// RoleName
        /// </summary>
        public string RoleName { get; set; }
        /// <summary>
        /// RoleDescription
        /// </summary>
        [RegularExpression(@"^[a-zA-Z][a-zA-Z0-9 ]+$", ErrorMessage = "Special characters not allowed")]
        public string RoleDescription { get; set; }
        /// <summary>
        /// RoleUserMapId
        /// </summary>
        public int RoleUserMapId { get; set; }
        /// <summary>
        /// IsFirstLogin
        /// </summary>
        public bool IsFirstLogin { get; set; }
        /// <summary>
        /// Mobile
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Phone
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// DesignationName
        /// </summary>
        public string DesignationName { get; set; }
    }


}
