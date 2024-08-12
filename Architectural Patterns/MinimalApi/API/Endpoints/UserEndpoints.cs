
using Application.Services.Interfaces;
using Domain.DTO;

namespace API.Endpoints
{
    public static class UserEndpoints
    {
        public static void MapUserEndpoints(this IEndpointRouteBuilder app)
        {
            var users = app.MapGroup("/api/users");

            users.MapGet("/list", async (IUserService userService) =>
            {
                try
                {
                    var users = await userService.GetAllAsync();
                    return Results.Ok(users);
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            });

            users.MapGet("/{id:guid}", async (Guid id, IUserService userService) =>
            {
                try
                {
                    var user = await userService.GetByIdAsync(id);
                    return user != null ? Results.Ok(user) : Results.NotFound();
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            });

            users.MapPost("/add", async (UserAddDTO userDto, IUserService userService) =>
            {
                try
                {
                    await userService.AddAsync(userDto);
                    return Results.Created();
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            });

            users.MapPut("/update", async (UserDTO userDto, IUserService userService) =>
            {
                try
                {
                    await userService.UpdateAsync(userDto);
                    return Results.Ok();
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            });

            users.MapDelete("/{id:guid}", async (Guid id, IUserService userService) =>
            {
                try
                {
                    await userService.DeleteAsync(id);
                    return Results.NoContent();
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            });
        }
    }
}
