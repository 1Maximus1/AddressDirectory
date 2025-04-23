namespace AddressDirectory.API.Address.GetAddress
{
    //public record GetAddressByIdRequest();
    public record GetAddressByIdResponse(AddressCatalog AddressCatalog);

    public class GetAddressByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/address/{id}", async (int id, ISender sender, ILogger<GetAddressByIdEndpoint> logger) =>
            {
                logger.LogInformation("Received request to get address with ID: {Id}", id);

                var result = await sender.Send(new GetAddressByIdQuery(id));

                var response = result.Adapt<GetAddressByIdResponse>();
                
                logger.LogInformation("Successfully retrieved address with ID: {Id}", id);
                return Results.Ok(response);

            }).WithName("GetAddressById")
              .Produces<GetAddressByIdResponse>(StatusCodes.Status200OK)
              .ProducesProblem(StatusCodes.Status400BadRequest)
              .WithSummary("Get Address By Id")
              .WithDescription("Get Address By Id");
        }
    }
}
