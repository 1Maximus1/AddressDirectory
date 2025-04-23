namespace AddressDirectory.API.Exceptions
{
    public class AddressNotFoundException : NotFoundException
    {
        public AddressNotFoundException(int id) : base("Address", id) { }
    }
}
