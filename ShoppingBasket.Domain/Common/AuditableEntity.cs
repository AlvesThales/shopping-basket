namespace ShoppingBasket.Domain.Common;

public class AuditableEntity : BaseEntity
{
   public string CreatedBy { get; set; } = string.Empty;
   public string ModifiedBy { get; set; } = string.Empty;
   public DateTime CreatedAt { get; set; }
   public DateTime ModifiedAt { get; set; }
}