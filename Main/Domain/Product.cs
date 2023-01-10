using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSSC_lab_project.Main.Domain
{
    public record Product
    {
        public string Name { get; }
        public int Price { get; }
        public int Amount { get; }

        public Product(string Name, int Price, int Amount)
        {
            this.Name = Name;
            this.Price = Price;
            this.Amount = Amount;
        }

        public override string ToString()
        {
            return Name + " | " + Price + " | " + Amount;
        }
    }
}
