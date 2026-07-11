using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CommonLibrary;

/// <summary>
/// Provides base functionality for property setters, including change notification
/// through the <see cref="PropertyChanged"/> event.
/// </summary>
/// <remarks>
/// This abstract class is designed to simplify the implementation of property setters
/// that notify listeners when a property value changes. It includes a method to set
/// fields and raise the <see cref="PropertyChanged"/> event if the value has changed.
/// </remarks>
public abstract class PropertySetters
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
        => PropertyChanged?.Invoke(this, new(propertyName));

    /// <summary>
    /// Sets the field to the specified value and raises the <see cref="PropertyChanged"/> event if the value has changed.
    /// </summary>
    /// <typeparam name="T">The type of the field.</typeparam>
    /// <param name="field">The field to set.</param>
    /// <param name="value">The value to set the field to.</param>
    /// <param name="propertyName">The name of the property. This is optional and will be automatically provided by the compiler.</param>
    /// <returns><c>true</c> if the field was changed; otherwise, <c>false</c>.</returns>
    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}
