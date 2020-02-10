using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities.Tickets
{
    [Table("priority")]
    public class Priority
    {
        [Key]
        [Column("priID")]
        public int Id { get; set; }
        [Column("type")]
        public string Name { get; set; }
    }
}