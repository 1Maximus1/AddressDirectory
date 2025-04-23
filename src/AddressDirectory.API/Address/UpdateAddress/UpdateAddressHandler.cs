namespace AddressDirectory.API.Address.UpdateAddress
{
    public record UpdateAddressCommand(int Id, string Value, int StatusId, int UUserId)
        : ICommand<UpdateAddressResult>;

    public record UpdateAddressResult(bool IsSuccess);

    public class UpdateAddressCommandValidator : AbstractValidator<UpdateAddressCommand>
    {
        public UpdateAddressCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty().WithMessage("Address ID is required");

            RuleFor(command => command.Value).NotEmpty().WithMessage("Value is required").Length(2, 300).WithMessage("Value must be between 2 and 250 characters");

            RuleFor(x => x.StatusId).InclusiveBetween(0, 1).WithMessage("StatusId must be 0 or 1");

            RuleFor(x => x.UUserId).NotEmpty().WithMessage("Who updated is required");
        }
    }


    internal class UpdateAddressCommandHandler(IAddressRepository repository, ILogger<UpdateAddressCommandHandler> logger) : ICommandHandler<UpdateAddressCommand, UpdateAddressResult>
    {
        public async Task<UpdateAddressResult> Handle(UpdateAddressCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("Received update request for Address Id={Id} with data: {@Command}", command.Id, command);
            var address = await repository.GetAddressById(command.Id, cancellationToken);

            if (address == null)
            {
                logger.LogWarning("Address with Id={Id} not found. Throwing AddressNotFoundException.", command.Id);
                throw new AddressNotFoundException(command.Id);
            }

            address.Value = command.Value;
            address.StatusId = command.StatusId;
            address.UUserId = command.UUserId;
            address.UDate = DateTime.UtcNow;

            logger.LogInformation("Address updated: Id={Id}, Value='{Value}', StatusId={StatusId}, UDate={UDate}, UUserId={UUserId}",
            address.Id, address.Value, address.StatusId, address.UDate, address.UUserId);

            await repository.UpdateAddress(address);

            return new UpdateAddressResult(true);
        }
    }
}
