namespace AddressDirectory.API.Models
{
    public class AddressCatalog
    {
        public int Id { get; set; }

        public string Value { get; set; } = default!;
        public int StatusId {  get; set; }  
        public DateTime CDate {  get; set; } = DateTime.Now;
        public int CUserId { get; set; }
        public DateTime UDate { get; set; } = DateTime.Now;
        public int UUserId { get; set; }
    }
}
