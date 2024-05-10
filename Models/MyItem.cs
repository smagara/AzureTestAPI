
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace azure_test_api.Models
{
    [Table("TestData")]
    public record MyItem
    {
        [Key]
        public int ItemNo {get; set;}
        public required string ItemName {get; set;}
        
    }
}