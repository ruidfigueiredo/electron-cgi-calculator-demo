using System;
using ElectronCgi.DotNet;

namespace DotNetCalculator
{
    class CalculatorRequest
    {
        public int Num1 { get; set; }
        public int Num2 { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var connection = new ConnectionBuilder().WithLogging().Build();

            connection.On<dynamic, int>("sum", numbers =>
            {
                return numbers.num1 + numbers.num2;
            });
            connection.On<CalculatorRequest, int>("subtraction", numbers =>
            {
                return numbers.Num1 - numbers.Num2;
            });
            connection.On<dynamic, int>("multiplication", numbers =>
            {
                return numbers.num1 * numbers.num2;
            });
            connection.On<dynamic, int>("division", numbers => {
                return numbers.num1 / numbers.num2;
            });

            connection.Listen();
        }
    }
}
