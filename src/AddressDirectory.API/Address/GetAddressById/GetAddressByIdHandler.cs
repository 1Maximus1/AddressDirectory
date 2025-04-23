namespace AddressDirectory.API.Address.GetAddress
{
    public record GetAddressByIdQuery(int Id) : IQuery<GetAddressByIdResult>;
    public record GetAddressByIdResult(AddressCatalog AddressCatalog);

    internal class GetAddressByIdQueryHandler(IAddressRepository repository, ILogger<GetAddressByIdQueryHandler> logger) : IQueryHandler<GetAddressByIdQuery, GetAddressByIdResult>
    {
        public async Task<GetAddressByIdResult> Handle(GetAddressByIdQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("Starting address creation. Command: {@Query}", query);
            logger.LogInformation("Starting to process GetAddressByIdQuery for ID: {AddressId}", query.Id);

            var address = await repository.GetAddressById(query.Id, cancellationToken);

            if (address == null)
            {
                logger.LogWarning("Address with ID {AddressId} not found", query.Id);
                throw new AddressNotFoundException(query.Id);
            }

            logger.LogInformation("Successfully retrieved address with ID: {AddressId}", query.Id);
            return new GetAddressByIdResult(address);
        }
    }
}
