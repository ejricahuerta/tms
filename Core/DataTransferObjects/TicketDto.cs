namespace Core.DataTransferObjects
{
    public class TicketDto : TicketListItemDto
    {
        public string Resolution { get; set; }

        public string BelongsToUsername { get; set; }
        public int? BelongsToId { get; set; }

        public string AssignedToUsername { get; set; }
        public int? AssignedToId { get; set; }

        public int? SiteId { get; set; }
        public string SiteName { get; set; }
    }
}
