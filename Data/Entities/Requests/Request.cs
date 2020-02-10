using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities.Requests
{

    /// <summary>
    /// Base for all request
    /// </summary>
    public class Request
    {
        [Key]
        public int Id { get; set; }

        public DateTime DateAdded { get; set; } = DateTime.Now;

        [Required]
        public string RequestedBy { get; set; } // Current Logged In User 

        public string Department { get; set; }

        [Required]
        public string DepartmentManager { get; set; }

        public string Director { get; set; }

        [Required]
        public string Title { get; set; }

        public bool? DirectorSigned { get; set; }
        public DateTime? DirectorSignedDate { get; set; }

        [Required]
        public bool ManagerSigned { get; set; } = false;

        public DateTime? ManagerSignedDate { get; set; }

        [Required]
        public bool Signed { get; set; } = false;

        [Required]
        public string Description { get; set; }

    }

    public class AXRequest : Request
    {
        public bool NeedsInstall { get; set; } = false;

        public ICollection<AXRequestAccess> AxRequestAccesses { get; set; } = new List<AXRequestAccess>();
    }

    public class AXRequestAccess
    {
        public int Id { get; set; }

        [Column("AXRequest")]
        public int AxRequestId { get; set; }

        [ForeignKey("AxRequestId")]
        public AXRequest AxRequest { get; set; }

        [Required]
        public string MenuItem { get; set; }

        [Required]
        public string Company { get; set; }

        public bool FullControl { get; set; } = false;
        public bool NoAccess { get; set; } = false;
        public bool View { get; set; } = false;
        public bool Write { get; set; } = false;
        public bool Create { get; set; } = false;

    }

    public class ITRequest : Request
    {

        public ICollection<RequestItem> RequestItems { get; set; } = new List<RequestItem>();
    }


    public class EmployeeRequest : Request
    {
        public EmployeeRequestType EmployeeRequestType { get; set; } = EmployeeRequestType.New;
        public ICollection<RequestItem> RequestItems { get; set; } = new List<RequestItem>();

        public string Office { get; set; }

        public DateTime? DateRequestCompletion { get; set; }

    }

    /// <summary>
    /// This class represent all items to be managed by admin
    /// </summary>
    public class RequestItem
    {
        public int Id { get; set; }
        public RequestItemType RequestItemType { get; set; }
    }


    public class OfficeAsset
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Identifier { get; set; } // Like serial number
    }

    public enum EmployeeRequestType
    {
        Current = 1, New = 2, Returning = 3, Departing = 4
    }

    public enum RequestItemType
    {
        Request = 1, Return = 2
    }

}
