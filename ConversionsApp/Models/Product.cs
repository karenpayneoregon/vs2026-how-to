namespace ConversionsApp.Models;

public record Product(int? Id, string Name, int? UnitsInStock, decimal? UnitPrice, int? CategoryId)
{
    public override string ToString() => $"{{ Id = {Id}, Name = {Name}, CategoryId = {CategoryId} }}";
}