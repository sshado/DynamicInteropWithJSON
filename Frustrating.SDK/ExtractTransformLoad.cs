using System;
using System.Security.Cryptography;
using Newtonsoft.Json;

namespace Frustrating.SDK
{
    public class ExtractTransformLoad
    {
        public event EventHandler<string> DataPipe;

        /// <summary>
        ///     This function can be called whenever you want this library to send a string of JSON through the exposed event handler <see cref="DataPipe"/>
        /// </summary>
        public void SendTestData()
        {
            var generator = new Random();
            var randomFormat = (FormatType) generator.Next(1, 4);
            string test = FormatDataForJson(randomFormat);
            this.DataPipe?.Invoke(null, FormatDataForJson(randomFormat));
        }

        internal string FormatDataForJson(FormatType type)
        {
            switch (type)
            {
                case FormatType.Display:
                    var displayFormat = new FormatForDisplay()
                    {
                        FirstName = "John",
                        LastName = "Smith",
                        UserIdentifier = "3"
                    };
                    return JsonConvert.SerializeObject(displayFormat);

                case FormatType.Postal:
                    var postalFormat = new FormatForPostalSearch()
                    {
                        Address = "1234 Foo Bar Cir",
                        City = "Atlanta",
                        PostalCode = "12345",
                        UserIdentifier = "3"
                    };
                    return JsonConvert.SerializeObject(postalFormat);

                case FormatType.All:
                    var allFields = new FormatAllFields()
                    {
                        FirstName = "John",
                        LastName = "Smith",
                        Address = "1234 Foo Bar Cir",
                        City = "Atlanta",
                        PostalCode = "12345",
                        UserIdentifier = "3"
                    };
                    return JsonConvert.SerializeObject(allFields);
            }
            return "Wow, something broke in the class library.";
        }

        internal enum FormatType
        {
            Display = 1,
            Postal = 2,
            All = 3
        }
    }
}
