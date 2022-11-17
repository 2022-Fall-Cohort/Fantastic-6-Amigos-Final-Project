namespace WMS_Inventory_API_Client.Models
{
    public class Content
    {
        public int? Id { get; set; }
        public int? Quantity { get; set; }
        public string? Description { get; set; }
        public int? ContentId { get; set; }

        public Content(int? id, int? quantity, string? description, int? contentId)
        {
            Id = id;
            Quantity = quantity;
            Description = description;
            ContentId = contentId;
        }

        public Content()
        {
            return;
        }
    }
}
