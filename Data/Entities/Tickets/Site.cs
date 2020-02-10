using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities.Tickets
{
    [Table("sites")]
    public class Site
    {
        [Key]
        [Column("sid")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }
        [Column("address1")]
        public string Address1 { get; set; }
        [Column("address2")]
        public string Address2 { get; set; }
        [Column("province")]
        [StringLength(20)]
        public string Province { get; set; }
        [Column("city")]
        [StringLength(50)]
        public string City { get; set; }
        [StringLength(7)]
        [Column("postalcode")]
        public string PostalCode { get; set; }
        [StringLength(14)]
        [Column("centralNumber")]
        public string PhoneNumber { get; set; }
        [StringLength(14)]
        [Column("centralFax")]
        public string FaxNumber { get; set; }

    }
}
