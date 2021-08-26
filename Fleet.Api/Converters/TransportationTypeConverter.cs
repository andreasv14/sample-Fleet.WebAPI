using Fleet.Models.Enums;
using System;

namespace Fleet.Api.Converters
{
    public static class TransportationTypeConverter
    {
        public static TransportationType ConvertFrom(string type)
        {
            if (!Enum.TryParse(type, true, out TransportationType transportationType))
            {
                throw new FormatException("Transportation type is not in the correct format");
            }

            return transportationType;
        }
    }
}
