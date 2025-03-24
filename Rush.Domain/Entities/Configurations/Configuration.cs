using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using Rush.Domain.Common.ViewModels.JSONModels;

namespace Rush.Domain.Entities.Configurations
{
    [Table("Tbl_Configurations")]
    public class Configuration : BaseEntity
    {

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public string ConfigurationJson { get; set; }

        public T GetValue<T>(string propertyName)
        {
            var config = JsonSerializer.Deserialize<ConfigurationFileVM>(ConfigurationJson);
            return (T)typeof(ConfigurationFileVM).GetProperty(propertyName).GetValue(config);
        }

        public void SetValue<T>(string propertyName, T value)
        {
            var config = JsonSerializer.Deserialize<ConfigurationFileVM>(ConfigurationJson);
            typeof(ConfigurationFileVM).GetProperty(propertyName).SetValue(config, value);
            ConfigurationJson = JsonSerializer.Serialize(config);
        }
    }
}