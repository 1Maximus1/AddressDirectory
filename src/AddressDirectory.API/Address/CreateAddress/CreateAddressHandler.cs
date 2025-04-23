namespace AddressDirectory.API.Address.StoreAdress
{
    public record CreateAddressCommand(string Value, int StatusId, int CUserId) : ICommand<CreateAddressResult>;
    public record CreateAddressResult(int Id);

    public class CreateAddressCommandValidator : AbstractValidator<CreateAddressCommand>
    {
        public CreateAddressCommandValidator()
        {
            RuleFor(x => x.Value).NotEmpty().WithMessage("Value is required").Length(2, 250).WithMessage("Value must be between 2 and 250 characters");
            RuleFor(x => x.StatusId).InclusiveBetween(0, 1).WithMessage("StatusId must be 0 or 1");
            RuleFor(x => x.CUserId).NotEmpty().WithMessage("Who created is required");
        }
    }

    internal class CreateAddressCommandHandler(IAddressRepository repository, ILogger<CreateAddressCommandHandler> logger) : ICommandHandler<CreateAddressCommand, CreateAddressResult>
    {
        public async Task<CreateAddressResult> Handle(CreateAddressCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("Starting address creation. Command: {@Command}", command);

            var address = command.Adapt<AddressCatalog>();

            address.CDate = DateTime.UtcNow;
            address.UDate = DateTime.UtcNow;
            address.UUserId = address.CUserId; 

            await repository.CreateAddress(address);

            logger.LogInformation("Address created: Id={Id}, Value='{Value}', StatusId={StatusId}, CDate={CDate}, CUserId={CUserId}, UDate={UDate}, UUserId={UUserId}",
                address.Id,
                address.Value,
                address.StatusId,
                address.CDate,
                address.CUserId,
                address.UDate,
                address.UUserId
            );

            return new CreateAddressResult(address.Id);
        }
    }
}
