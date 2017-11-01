using System;
using System.Reflection;
using Newtonsoft.Json;
using Frustrating.SDK;
using Newtonsoft.Json.Linq;

namespace Testing.Library
{   
    public class Tester
    {
        public static ExtractTransformLoad thirdPartyModule;

        public static void ReceiveData(object sender, string eventArgs)
        {
            // Safely unpack using the Elvis operator.
            dynamic data = JObject.Parse(eventArgs);
            var formattedData = new FormatAllFields()
            {
                FirstName = data?.FirstName,
                LastName = data?.LastName,
                Address = data?.Address,
                City = data?.City,
                PostalCode = data?.PostalCode,
                UserIdentifier = data?.UserIdentifier
            };

            // Find out which fields were populated through reflection.
            foreach (PropertyInfo pi in formattedData.GetType().GetProperties())
            {
                if (pi.PropertyType != typeof(string)) continue;
                var value = (string)pi.GetValue(formattedData);
                if (!string.IsNullOrEmpty(value))
                {
                    // If not null then report data received.
                    Console.WriteLine(String.Format("{0} received from data pipe.", pi.Name));
                }
            }
        }

        public static void Initialize()
        {
            thirdPartyModule = new ExtractTransformLoad();
            thirdPartyModule.DataPipe += ReceiveData;
            thirdPartyModule.SendTestData();
            Looper();
        }

        public static void Looper()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to try again.");
            Console.ReadLine();
            Initialize();
        }

        public static void Main(string[] args)
        {
            Initialize();
        }
    }
}
