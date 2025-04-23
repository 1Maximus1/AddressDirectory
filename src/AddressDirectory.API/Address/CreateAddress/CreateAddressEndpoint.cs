namespace AddressDirectory.API.Address.StoreAdress
{
    public record CreateAddressRequest(string Value, int StatusId, int CUserId);

    public record CreateAddressResponse(int Id);

    public class CreateAddressEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/address", async (CreateAddressRequest request, ISender sender , ILogger<CreateAddressEndpoint> logger) =>
            {
                logger.LogInformation("Received create address request: {@Request}", request);
                
                var command = request.Adapt<CreateAddressCommand>();
                logger.LogInformation("Mapped to command: {@Command}", command);
                
                var result = await sender.Send(command);

                var response = result.Adapt<CreateAddressResponse>();
                logger.LogInformation("Address created with Id={Id}. Returning response: {@Response}", response.Id, response);

                return Results.Created($"/address/{response.Id}", response);
            }).WithName("CreateAddress")
              .Produces<CreateAddressEndpoint>(StatusCodes.Status201Created)
              .ProducesProblem(StatusCodes.Status400BadRequest)
              .WithSummary("Create Adress Endpoint")
              .WithDescription("Create Adress Endpoint");
        }
    }
}
