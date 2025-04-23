namespace AddressDirectory.API.Data.Repositories
{
    public interface IAddressRepository
    {
        Task<AddressCatalog> GetAddressById(int Id, CancellationToken cancellationToken = default);
        Task<AddressCatalog> CreateAddress(AddressCatalog address, CancellationToken cancellationToken = default);
        Task<AddressCatalog> UpdateAddress(AddressCatalog address, CancellationToken cancellationToken = default);
    }
}
