using PSSC_lab_project.Main.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Domain
{
    public record ValidatedOrder(Account account, Product product, Amount amount);
}
