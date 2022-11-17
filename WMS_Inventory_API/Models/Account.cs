namespace WMS_Inventory_API.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public int? ZipCode { get; set; }
        public virtual List<StorageLocation>? storageLocations { get; set; }
    }
}
