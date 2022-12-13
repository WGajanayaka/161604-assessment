namespace Mbrrace.Models
{
    public class AuditedEntity
    {
        public bool IsDeleted { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public DateTime? ModifiedDateTime { get; set; }

        public DateTime? DeletedDateTime { get; set; }
    }
}
