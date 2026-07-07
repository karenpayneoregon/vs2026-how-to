using System.Data;
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.

namespace CommonLangageExtensionsLibraryCore;

public static class BindingSourceExtensions
{
    /// <summary>
    /// Moves the current row in the <see cref="BindingSource"/> up by one position.
    /// </summary>
    /// <param name="sender">
    /// The <see cref="BindingSource"/> instance containing the data and the current row to move.
    /// </param>
    /// <remarks>
    /// If the current row is already at the first position, it will remain in place.
    /// Any existing sort applied to the <see cref="BindingSource"/> will be cleared before moving the row.
    /// </remarks>
    /// <exception cref="InvalidCastException">
    /// Thrown if the <see cref="BindingSource.DataSource"/> is not a <see cref="DataTable"/> 
    /// or if the current item is not a <see cref="DataRowView"/>.
    /// </exception>
    public static void MoveRowUp(this BindingSource sender)
    {
        if (!string.IsNullOrWhiteSpace(sender.Sort))
        {
            sender.Sort = "";
        }

        var newIndex = Convert.ToInt32((sender.Position == 0) ? 
            0 : 
            sender.Position - 1);

        var dt = (DataTable)sender.DataSource;
        DataRow rowToMove = (((DataRowView)sender.Current)!).Row;
        var newRow = dt.NewRow();

        newRow.ItemArray = rowToMove.ItemArray;
        dt.Rows.RemoveAt(sender.Position);
        dt.Rows.InsertAt(newRow, newIndex);

        dt.AcceptChanges();

        sender.Position = newIndex;

    }

    /// <summary>
    /// Moves the current row in the <see cref="BindingSource"/> down by one position.
    /// </summary>
    /// <param name="sender">
    /// The <see cref="BindingSource"/> instance containing the data and the current row to move.
    /// </param>
    /// <remarks>
    /// If the current row is already at the last position, it will remain in place.
    /// Any existing sort applied to the <see cref="BindingSource"/> will be cleared before moving the row.
    /// </remarks>
    /// <exception cref="InvalidCastException">
    /// Thrown if the <see cref="BindingSource.DataSource"/> is not a <see cref="DataTable"/> 
    /// or if the current item is not a <see cref="DataRowView"/>.
    /// </exception>
    public static void MoveRowDown(this BindingSource sender)
    {
        if (!string.IsNullOrWhiteSpace(sender.Sort))
        {
            sender.Sort = "";
        }

        var upperLimit = sender.Count - 1;
        var newIndex = Convert.ToInt32((sender.Position + 1 >= upperLimit) ? 
            upperLimit : 
            sender.Position + 1);

        var dt = (DataTable)sender.DataSource;
        DataRow rowToMove = ((DataRowView)sender.Current).Row;
        var newRow = dt.NewRow();

        newRow.ItemArray = rowToMove.ItemArray;
        dt.Rows.RemoveAt(sender.Position);
        dt.Rows.InsertAt(newRow, newIndex);

        dt.AcceptChanges();

        sender.Position = newIndex;

    }

}