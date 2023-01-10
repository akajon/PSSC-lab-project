using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Domain
{
    public record Amount
    {
        public int Value { get; }

        public Amount(int value)
        {
            try
            {
                if (value > 0)
                {
                    Value = value;
                }
                else
                {
                    throw new InvalidAmountException($"{value:0.##} is an invalid amount.");
                }
            }
            catch
            {
                Console.WriteLine("Error at amount input!");
                Environment.Exit(1);
            }
        }

        public override string ToString()
        {
            return $"{Value:0.##}";
        }
    }
}
