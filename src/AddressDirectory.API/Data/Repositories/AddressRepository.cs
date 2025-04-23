namespace AddressDirectory.API.Data.Repositories
{
    public class AddressRepository(AddressContext dbContext, ILogger<AddressRepository> logger) : IAddressRepository
    {
        public async Task<AddressCatalog> CreateAddress(AddressCatalog address, CancellationToken cancellationToken = default)
        {
            await dbContext.Addresses.AddAsync(address, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            logger.LogInformation("Address is successfully created. Value: {Value}, Status id: {StatusId}, Created Date: {CDate}", address.Value, address.StatusId, address.CDate);

            return address;
        }

        public async Task<AddressCatalog> GetAddressById(int id, CancellationToken cancellationToken = default)
        {
            var address = await dbContext.Addresses.FirstOrDefaultAsync(a => a.Id == id , cancellationToken);
            if (address == null)
            {
                address = new AddressCatalog { Id = 0, Value = string.Empty, StatusId = 0, CDate = DateTime.Now, CUserId = 0, UDate = DateTime.Now, UUserId = 0};
            }

            logger.LogInformation("Address is retrieved with Value: {Value}, Status ID: {StatusId}, Created Date: {CDate}, Created User ID: {CUserId}, Last Updated User ID: {UUserId}, Last Updated Date: {UDate}", address.Value, address.StatusId, address.CDate, address.CUserId, address.UUserId, address.UDate);
            return address;
        }

        public async Task<AddressCatalog> UpdateAddress(AddressCatalog address, CancellationToken cancellationToken = default)
        {
            dbContext.Addresses.Update(address);
            await dbContext.SaveChangesAsync(cancellationToken);

            logger.LogInformation("Address is successfully updated. Value: {Value}, Status ID: {StatusId}, Created Date: {CDate}, Created User ID: {CUserId}, Last Updated User ID: {UUserId}, Last Updated Date: {UDate}", address.Value, address.StatusId, address.CDate, address.CUserId, address.UUserId, address.UDate);
            return address;
        }
    }
}
