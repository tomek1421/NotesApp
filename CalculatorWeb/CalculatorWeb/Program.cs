using CalculatorWeb.Logic;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var calculator = new Calculator();

app.UseCors(corsBuilder => 
    corsBuilder.WithOrigins("*") 
        .AllowAnyMethod()
        .AllowAnyHeader());

app.MapPost("/calculate", (CalculatorRequest request) =>//this is the endpoint
    {
        /*try
        {*/
            // Use the Calculator class to evaluate the expression.
            double result = calculator.EvaluateExpression(request.Expression);
            // Check if the result is NaN or Infinity (which is also problematic in JSON).
            if (double.IsNaN(result) || double.IsInfinity(result))
            {
                return Results.BadRequest(new { Error = "Invalid mathematical operation, result is undefined." });
            }
            return Results.Ok(new { Result = result });
        /*}
        catch (Exception ex)
        {   
            return Results.BadRequest(new { Error = "Invalid expression", Details = ex.Message });
        }*/
    })
    .WithName("Calculate")
    .WithOpenApi();

// New endpoint for converting the expression to RPN
app.MapPost("/calculate-rpn", (CalculatorRequest request) =>
{
    try
    {
        string rpnResult = calculator.ConvertExpressionToRPN(request.Expression);
        return Results.Ok(new { RPN = rpnResult });
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new { Error = "Invalid expression", Details = ex.Message });
    }
})
.WithName("CalculateRPN")
.WithOpenApi();

app.Run();

public class CalculatorRequest
{
    public string Expression { get; set; }
}