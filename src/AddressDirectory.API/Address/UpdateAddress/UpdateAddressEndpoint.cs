namespace AddressDirectory.API.Address.UpdateAddress
{
    public record UpdateAddressRequest(int Id, string Value, int StatusId, int UUserId);
    public record UpdateProductResponse(bool IsSuccess);

    public class UpdateAddressEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/address", async (UpdateAddressRequest request, ISender sender, ILogger<UpdateAddressEndpoint> logger) =>
            {
                logger.LogInformation("Received update address request: {@Request}", request);

                var command = request.Adapt<UpdateAddressCommand>();
                logger.LogInformation("Received update address request: {@Request}", request);

                var result = await sender.Send(command);

                var response = result.Adapt<UpdateProductResponse>();
                logger.LogInformation("Address update result: {@Response}", response);

                return Results.Ok(response);
            }).WithName("UpdateAddressEndpoint")
              .Produces<UpdateProductResponse>(StatusCodes.Status200OK)
              .ProducesProblem(StatusCodes.Status400BadRequest)
              .WithSummary("Update Address Endpoint")
              .WithDescription("Update Address Endpoint");
        }
    }
}
