using System.Data;
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8602

namespace CommonLangageExtensionsLibraryCore;

public static class ListBoxExtensions
{
    /// <summary>
    /// Moves the currently selected item in the <see cref="ListBox"/> up by one position.
    /// </summary>
    /// <param name="sender">The <see cref="ListBox"/> instance whose selected item is to be moved.</param>
    public static void MoveRowUp(this ListBox sender) 
    {
        var selectedIndex = sender.SelectedIndex;

        if (selectedIndex <= 0) return;
        sender.Items.Insert(selectedIndex - 1, sender.Items[selectedIndex]);
        sender.Items.RemoveAt(selectedIndex + 1);
        sender.SelectedIndex = selectedIndex - 1;

    }
    
    /// <summary>
    /// Moves the currently selected item in the <see cref="ListBox"/> down by one position.
    /// </summary>
    /// <param name="sender">The <see cref="ListBox"/> instance whose selected item is to be moved.</param>
    public static void MoveRowDown(this ListBox sender)
    {
        var selectedIndex = sender.SelectedIndex;

        if (!(selectedIndex < sender.Items.Count - 1 & selectedIndex != -1)) return;
        sender.Items.Insert(selectedIndex + 2, sender.Items[selectedIndex]);
        sender.Items.RemoveAt(selectedIndex);
        sender.SelectedIndex = selectedIndex + 1;

    }
    
    /// <summary>
    /// Moves the currently selected item in the <see cref="ListBox"/> up by one position 
    /// while updating the associated <see cref="BindingSource"/> and maintaining the 
    /// specified row position field in the underlying <see cref="DataTable"/>.
    /// </summary>
    /// <param name="sender">The <see cref="ListBox"/> instance whose selected item is to be moved.</param>
    /// <param name="pBindingSource">The <see cref="BindingSource"/> associated with the <see cref="ListBox"/>.</param>
    /// <param name="pRowPositionFieldName">
    /// The name of the field in the <see cref="DataTable"/> that stores the row position.
    /// This field is updated to reflect the new order.
    /// </param>
    /// <remarks>
    /// This method ensures that the selected item's position is updated both in the UI and in the 
    /// underlying data source. The <paramref name="pRowPositionFieldName"/> is used to maintain 
    /// the correct order in the database table.
    /// </remarks>
    public static void MoveRowUp(this ListBox sender, BindingSource pBindingSource, string pRowPositionFieldName)
    {
        if (!string.IsNullOrWhiteSpace(pBindingSource.Sort))
        {
            pBindingSource.Sort = "";
        }

        string displayText = sender.Text;
        var selectedIndex = pBindingSource.Position;
        var newIndex = Convert.ToInt32((pBindingSource.Position == 0) ? 0 : pBindingSource.Position - 1);

        var dt = (DataTable)pBindingSource.DataSource;
        var moveColIndex = dt.Columns[pRowPositionFieldName].Ordinal;

        DataRow rowToMove = ((DataRowView)pBindingSource.Current).Row;
        DataRow newRow = dt.NewRow();
        newRow.ItemArray = rowToMove.ItemArray;
        dt.Rows.RemoveAt(selectedIndex);
        dt.Rows.InsertAt(newRow, newIndex);

        dt.AcceptChanges();

        pBindingSource.Position = pBindingSource.Find(sender.DisplayMember, displayText);

        for (var index = 0; index < dt.Rows.Count; index++)
        {
            dt.Rows[index][moveColIndex] = index;
        }

    }
    
    /// <summary>
    /// Moves the currently selected item in the <see cref="ListBox"/> down by one position
    /// while updating the specified field in the associated data source to reflect the new position.
    /// </summary>
    /// <param name="sender">The <see cref="ListBox"/> instance whose selected item is to be moved.</param>
    /// <param name="pBindingSource">The <see cref="BindingSource"/> representing the data source of the <see cref="ListBox"/>.</param>
    /// <param name="pRowPositionFieldName">
    /// The name of the field in the data source used to track the position of the row.
    /// </param>
    /// <remarks>
    /// This method ensures that the data source remains consistent with the visual representation
    /// of the <see cref="ListBox"/> after the item is moved.
    /// </remarks>
    public static void MoveRowDown(this ListBox sender, BindingSource pBindingSource, string pRowPositionFieldName)
    {
        if (!string.IsNullOrWhiteSpace(pBindingSource.Sort))
        {
            pBindingSource.Sort = "";
        }

        string displayText = sender.Text;
        var selectIndex = pBindingSource.Position;

        var upperLimit = pBindingSource.Count - 1;
        var newIndex = Convert.ToInt32((pBindingSource.Position + 1 >= upperLimit) ? upperLimit : pBindingSource.Position + 1);

        var dt = (DataTable)pBindingSource.DataSource;
        var moveColIndex = dt.Columns[pRowPositionFieldName].Ordinal;

        DataRow rowToMove = ((DataRowView)pBindingSource.Current).Row;
        DataRow newRow = dt.NewRow();
        newRow.ItemArray = rowToMove.ItemArray;
        dt.Rows.RemoveAt(selectIndex);
        dt.Rows.InsertAt(newRow, newIndex);

        dt.AcceptChanges();

        pBindingSource.Position = pBindingSource.Find(sender.DisplayMember, displayText);

        for (var index = 0; index < dt.Rows.Count; index++)
        {
            dt.Rows[index][moveColIndex] = index;

        }

    }

}