using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace FieldKeywordSample.Models;

/// <summary>
/// Represents a view model that implements the <see cref="INotifyPropertyChanged"/> interface.
/// </summary>
/// <remarks>
/// This class provides properties with change notification support, enabling data binding scenarios.
/// </remarks>
public sealed class SomeViewModel : INotifyPropertyChanged
{
    public string Name
    {
        get;
        set => SetValue(ref field, value);
    }

    public bool IsActive
    {
        get;
        set => SetValue(ref field, value);
    } = true;

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    /// <summary>
    /// Sets the specified field to the provided value and raises the <see cref="PropertyChanged"/> event if the value changes.
    /// </summary>
    /// <typeparam name="T">The type of the field and value.</typeparam>
    /// <param name="field">A reference to the field to be updated.</param>
    /// <param name="value">The new value to assign to the field.</param>
    /// <param name="propertyName">
    /// The name of the property that changed. This parameter is optional and is automatically provided by the compiler.
    /// </param>
    /// <remarks>
    /// This method ensures that the field is only updated if the new value is different from the current value.
    /// It also triggers the <see cref="PropertyChanged"/> event to notify listeners of the change.
    /// </remarks>
    private void SetValue<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return;
        field = value;
        OnPropertyChanged(propertyName);
    }
}