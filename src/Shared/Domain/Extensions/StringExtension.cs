using System.Globalization;
using Mcm.Shared.Domain.Enums;
using Mcm.Shared.Domain.ValueObjects;

namespace Mcm.Shared.Domain.Extensions
{
    public static class StringExtension
    {
        extension(string text)
        {
            public string ToTitleCase(){
                return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text);
            }

            public bool IsValid()
            {
                return (!string.IsNullOrEmpty(text) && !string.IsNullOrWhiteSpace(text));
            }
            public string ToCapitalize()
            {
                if (!text.IsValid()) return text;
                return char.ToUpper(text[0]) + text[1..];
            }

            public string ToCloseString()
            {
                if (!text.IsValid()) return text;
                var t = text.ToLower()
                            .Split(" ")
                            .Select(x => char.ToUpper(x[0]) + x[1..]);

                return t.Aggregate((a, b) => a + "" + b);
            }

            public object ConvertTo(PropertyType type)
            {
                return type switch
                {
                    PropertyType.Text
                    => text,
                    PropertyType.Number
                    => double.TryParse(text, out var number)
                        ? number
                        : throw new FormatException($"Value '{text}' is not a valid number."),
                    PropertyType.Bool
                    => bool.TryParse(text, out var boolean) 
                        ? boolean
                        : throw new FormatException($"Value '{text}' is not a valid boolean."),
                    PropertyType.Decimal 
                    => decimal.TryParse(text, out var decimalValue) 
                        ? decimalValue
                        : throw new FormatException($"Value '{text}' is not a valid decimal."),
                    PropertyType.Email 
                    => Email.TryParse(text, out var emailValue) 
                        ? emailValue
                        : throw new FormatException($"Value '{text}' is not a valid Email."),
                    _ => throw new NotSupportedException($"Property type '{type}' is not supported.")
                };
            }
        }
    } 
}