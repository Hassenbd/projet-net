using System.Text.Json.Serialization;

namespace projet_net.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))] // to show the content of enum instead of the list of numbers
    public enum RpgClass
    {
        Knight=1,
        Mage=2,
        Cleric=3,
    }
}