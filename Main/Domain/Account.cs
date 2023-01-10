using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSSC_lab_project.Main.Domain
{
    public record Account
    {
        public enum RoleType{
            User,
            Admin
        }

        public RoleType Role { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string ShippingAddress { get; }

        public Account(string FirstName, string LastName, string Role, string ShippingAddress)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.ShippingAddress = ShippingAddress;
            switch(Role)
            {
                case "USER":
                    this.Role = RoleType.User;
                    break;
                case "ADMIN":
                    this.Role = RoleType.Admin;
                    break;
            }
        }

        public override string ToString()
        {
            return FirstName + " | " + LastName + " | " + Role + " | " + ShippingAddress;
        }

    }
}
