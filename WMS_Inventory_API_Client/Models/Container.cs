namespace WMS_Inventory_API_Client.Models
{
    public class Container
    {
        public int? Id { get; set; }
        public string? Type { get; set; }
        public string? Description { get; set; }
        public int? StorageLocationId { get; set; }
        public virtual StorageLocation? StorageLocation { get; set; }
        public virtual List<Content>? contents { get; set; }

        
    public Container(int? id, string? type, string? description, int? storageLocationId)
    {
        Id = id;
        Type = type;
        Description = description;
        StorageLocationId = storageLocationId;
    }

    public Container()
    {
        return;
    }

    }

}