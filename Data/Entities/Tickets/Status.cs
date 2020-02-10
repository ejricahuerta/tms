using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities.Tickets
{
    [Table("status")]
    public class Status : IEntity
    {
        [Key]
        [Column("statId")]
        public int Id { get; set; }
        [Column("type")]
        public string Name { get; set; }
        [NotMapped]
        public DateTime? DateAdded { get; set; } = DateTime.Now;
    }
}