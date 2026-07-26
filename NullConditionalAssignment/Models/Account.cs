namespace NullConditionalAssignment.Models;

#pragma warning disable CS8618
public sealed class Account
{
    public int Id { get; set; }
    public decimal Balance { get; set; }
    public int RewardPoints { get; set; }
    public int Flags { get; set; }
    public string? DisplayName { get; set; }

    public override string ToString() =>
        $"""
         [DeepPink1]Id:[/] {Id}, 
         [DeepPink1]Balance:[/] '{Balance:C}', 
         [DeepPink1]RewardPoints:[/] '{RewardPoints}', 
         [DeepPink1]Flags:[/] '{Flags}', 
         [DeepPink1]DisplayName:[/] '{DisplayName}'
         """;
}