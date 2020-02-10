using Data.Entities.Users;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities.Tickets
{
    [Table("tickets")]
    public class Ticket : IEntity
    {
        [Column("tid")]
        [Key]
        public int Id { get; set; }

        [NotMapped]

        public string Title { get; set; }

        [Column("description")]

        public string Description { get; set; }
        [Column("resolution")]
        public string Resolution { get; set; }


        [Column("reportedBy")]
        public string ReportedBy { get; set; }

        [Column("createDate")]
        [Display(Name = "Date Added")]

        public DateTime DateAdded { get; set; } = DateTime.Now;

        [Column("lastActive")]
        [Display(Name = "Date Modified")]
        public DateTime? DateModified { get; set; }

        [Column("resolveDate")]
        [Display(Name = "Date Modified")]
        public DateTime? ResolveDate { get; set; }

        //Nav Properties
        [Column("status")]
        public int? StatusId { get; set; }

        [ForeignKey("StatusId")]
        public Status Status { get; set; }

        [Column("sid")]
        public int? SiteId { get; set; }
        [ForeignKey("SiteId")]
        public Site Site { get; set; }


        [Column("priority")]
        public int? PriorityId { get; set; }
        [ForeignKey("PriorityId")]
        public Priority Priority { get; set; }

        [Column("belongsTo")]
        public int? BelongsToId { get; set; }
        [Column("belongsTo")]
        public int? AssignedToId { get; set; }
        [ForeignKey("BelongsToId")]
        public User BelongsTo { get; set; }
        [ForeignKey("AssignedToId")]
        public User AssignedTo { get; set; }
    }
}
