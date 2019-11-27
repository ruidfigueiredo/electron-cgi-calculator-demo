using System;
using ElectronCgi.DotNet;
using Microsoft.Extensions.Logging;

namespace DotNetCalculator
{
    class CalculatorRequest
    {
        public double Num1 { get; set; }
        public double Num2 { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var connection = new ConnectionBuilder().WithLogging(minimumLogLevel: LogLevel.Trace).Build();

            connection.On<dynamic, double>("sum", numbers =>
            {
                return numbers.num1 + numbers.num2;
            });
            connection.On<CalculatorRequest, double>("subtraction", numbers =>
            {
                return numbers.Num1 - numbers.Num2;
            });
            connection.On<dynamic, double>("multiplication", numbers =>
            {
                return numbers.num1 * numbers.num2;
            });
            connection.On<dynamic, double>("division", numbers => {
                if (numbers.num2 == 0){
                    Console.Error.WriteLine("Error: Division by 0");
                    throw new InvalidOperationException("Division by 0");
                }
                //when using dynamic the real type of num1 and num2 will be JValue because ElectronCGI intrenally uses Newtonsoft.Json for serialisation: https://www.newtonsoft.com/json/help/html/T_Newtonsoft_Json_Linq_JValue.htm
                return numbers.num1.ToObject<double>() / numbers.num2.ToObject<double>();
            });

            connection.Listen();
        }
    }
}
