namespace Event;
public class InventoryDeducted
{
    public Guid OrderId { get; set; }
    public bool Success { get; set; }
}