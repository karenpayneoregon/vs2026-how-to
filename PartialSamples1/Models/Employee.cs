using System.ComponentModel;

namespace PartialSamples1.Models;

public partial class Employee : INotifyPropertyChanged
{
    public partial int Id { get; set; }
    public partial string? FirstName { get; set; }
    public partial string? LastName { get; set; }
    public partial string? BadgeId { get; set; }
}
