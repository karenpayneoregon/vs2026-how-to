namespace CommonLibrary.Models;

public record MonthItem(int Index, string Name)
{
    public override string ToString() => Name;
}