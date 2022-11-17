namespace WMS_Inventory_API_Client.Models
{
    public class Account
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public int? ZipCode { get; set; }
        public virtual List<StorageLocation>? storageLocations { get; set; }

        public Account(int? id, string? name, string? address1, string? address2, string? city, string? state, int? zipCode)
        {
            Id = id;
            Name = name;
            Address1 = address1;
            Address2 = address2;
            City = city;
            State = state;
            ZipCode = zipCode;
        }

        public Account()
        {
            return;
        }
    }
}


