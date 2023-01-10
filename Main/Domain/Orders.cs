using CSharp.Choices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Domain
{
    [AsChoice]
    public static partial class Orders
    {
        public interface IOrders { }

        public record UnvalidatedOrders(IReadOnlyCollection<UnvalidatedOrder> OrdersList) : IOrders;

        public record InvalidatedOrders(IReadOnlyCollection<UnvalidatedOrder> OrdersList, string reason) : IOrders;

        public record ValidatedOrders(IReadOnlyCollection<ValidatedOrder> OrdersList) : IOrders;

        public record PublishedOrders(IReadOnlyCollection<ValidatedOrder> OrdersList, DateTime PublishedDate) : IOrders;
    }
}
