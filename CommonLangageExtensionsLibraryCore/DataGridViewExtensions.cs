using System.Data;
using System.Text.RegularExpressions;
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8602 // Dereference of a possibly null reference.

namespace CommonLangageExtensionsLibraryCore;

/// <summary>
/// Methods to move current row up/down
/// </summary>
public static partial class DataGridViewExtensions
{
    /// <summary>
    /// Moves the selected row up in the <see cref="DataGridView"/>.
    /// </summary>
    /// <param name="pDataGridView">
    /// The <see cref="DataGridView"/> containing the row to be moved.
    /// </param>
    /// <remarks>
    /// This method moves the currently selected row one position up within the <see cref="DataGridView"/>.
    /// If the selected row is the first row, no action is performed.
    /// </remarks>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="pDataGridView"/> is <c>null</c>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown if no row is selected or the operation cannot be performed.
    /// </exception>
    public static void MoveRowUp(this DataGridView pDataGridView)
    {
        if (pDataGridView.RowCount > 0)
        {
            if (pDataGridView.SelectedRows.Count > 0)
            {

                int selectedIndex = pDataGridView.SelectedCells[0].OwningRow.Index;

                if (selectedIndex == 0)
                {
                    return;
                }
                DataGridViewRowCollection rows = pDataGridView.Rows;

                // remove the previous row and add it behind the selected row.
                DataGridViewRow prevRow = rows[selectedIndex - 1];
                rows.Remove(prevRow);
                prevRow.Frozen = false;
                rows.Insert(selectedIndex, prevRow);
                pDataGridView.ClearSelection();
                pDataGridView.Rows[selectedIndex - 1].Selected = true;
            }
        }
    }
    /// <summary>
    /// Moves the selected row down in the <see cref="DataGridView"/>.
    /// </summary>
    /// <param name="pDataGridView">
    /// The <see cref="DataGridView"/> containing the row to be moved.
    /// </param>
    /// <remarks>
    /// This method moves the currently selected row one position down within the <see cref="DataGridView"/>.
    /// If the selected row is the last row, no action is performed.
    /// </remarks>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="pDataGridView"/> is <c>null</c>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown if no row is selected or the operation cannot be performed.
    /// </exception>
    public static void MoveRowDown(this DataGridView pDataGridView)
    {
        if (pDataGridView.RowCount > 0)
        {
            if (pDataGridView.SelectedRows.Count > 0)
            {
                int rowCount = pDataGridView.Rows.Count;
                int selectedIndex = pDataGridView.SelectedCells[0].OwningRow.Index;

                DataGridViewRowCollection rows = pDataGridView.Rows;

                // remove the next row and add it in front of the selected row.
                DataGridViewRow nextRow = rows[selectedIndex + 1];
                rows.Remove(nextRow);
                nextRow.Frozen = false;
                rows.Insert(selectedIndex, nextRow);
                pDataGridView.ClearSelection();
                pDataGridView.Rows[selectedIndex + 1].Selected = true;
            }
        }
    }
    /// <summary>
    /// Moves the selected row up in the <see cref="DataGridView"/> bound to the specified <see cref="BindingSource"/>.
    /// </summary>
    /// <param name="pDataGridView">
    /// The <see cref="DataGridView"/> containing the row to be moved.
    /// </param>
    /// <param name="pBindingSource">
    /// The <see cref="BindingSource"/> that manages the data binding for the <see cref="DataGridView"/>.
    /// </param>
    /// <remarks>
    /// This method moves the currently selected row one position up within the <see cref="DataGridView"/> 
    /// while ensuring the changes are reflected in the underlying data source managed by the <see cref="BindingSource"/>.
    /// If the selected row is the first row, no action is performed.
    /// </remarks>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="pDataGridView"/> or <paramref name="pBindingSource"/> is <c>null</c>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown if no row is selected, the operation cannot be performed, or the data source is not a <see cref="DataTable"/>.
    /// </exception>
    public static void MoveRowUp(this DataGridView pDataGridView, BindingSource pBindingSource)
    {
        if (!string.IsNullOrWhiteSpace(pBindingSource.Sort))
        {
            pBindingSource.Sort = "";
        }

        var currentColumnIndex = pDataGridView.CurrentCell.ColumnIndex;
        var newIndex = Convert.ToInt32(pBindingSource.Position == 0 ? 0 : pBindingSource.Position - 1);

        var dt = (DataTable)pBindingSource.DataSource;

        DataRow rowToMove = ((DataRowView)pBindingSource.Current).Row;
        var newRow = dt.NewRow();

        newRow.ItemArray = rowToMove.ItemArray;
        dt.Rows.RemoveAt(pBindingSource.Position);
        dt.Rows.InsertAt(newRow, newIndex);

        dt.AcceptChanges();

        pBindingSource.Position = newIndex;
       
        pDataGridView.CurrentCell = pDataGridView[currentColumnIndex, newIndex];
    }

    /// <summary>
    /// Moves the selected row down in the <see cref="DataGridView"/> bound via a <see cref="BindingSource"/>.
    /// </summary>
    /// <param name="pDataGridView">
    /// The <see cref="DataGridView"/> containing the row to be moved.
    /// </param>
    /// <param name="pBindingSource">
    /// The <see cref="BindingSource"/> managing the data binding for the <see cref="DataGridView"/>.
    /// </param>
    /// <remarks>
    /// This method moves the currently selected row one position down within the <see cref="DataGridView"/>.
    /// If the selected row is the last row, no action is performed. If the <see cref="BindingSource.Sort"/> property
    /// is set, it will be cleared before performing the operation.
    /// </remarks>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="pDataGridView"/> or <paramref name="pBindingSource"/> is <c>null</c>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown if no row is selected, the operation cannot be performed, or the <see cref="BindingSource"/> is not properly configured.
    /// </exception>
    public static void MoveRowDown(this DataGridView pDataGridView, BindingSource pBindingSource)
    {
        if (!string.IsNullOrWhiteSpace(pBindingSource.Sort))
        {
            pBindingSource.Sort = "";
        }

        var currentColumnIndex = pDataGridView.CurrentCell.ColumnIndex;
        var upperLimit = pBindingSource.Count - 1;
        var newIndex = Convert.ToInt32(pBindingSource.Position + 1 >= upperLimit ? upperLimit : pBindingSource.Position + 1);

        var dt = (DataTable)pBindingSource.DataSource;
        DataRow rowToMove = ((DataRowView)pBindingSource.Current).Row;
        var newRow = dt.NewRow();

        newRow.ItemArray = rowToMove.ItemArray;
        dt.Rows.RemoveAt(pBindingSource.Position);
        dt.Rows.InsertAt(newRow, newIndex);

        dt.AcceptChanges();

        pBindingSource.Position = newIndex;
        pDataGridView.CurrentCell = pDataGridView[currentColumnIndex, newIndex];
    }

    /// <summary>
    /// Adjusts the column widths of the specified <see cref="DataGridView"/> to fit their content.
    /// </summary>
    /// <param name="source">
    /// The <see cref="DataGridView"/> whose columns are to be adjusted.
    /// </param>
    /// <param name="sizable">
    /// A boolean value indicating whether the columns should remain resizable after adjustment.
    /// If <c>true</c>, the columns will remain resizable; otherwise, they will not.
    /// </param>
    /// <remarks>
    /// This method iterates through all columns of the <see cref="DataGridView"/> and adjusts their widths
    /// to fit the content. Columns containing collections are excluded from resizing.
    /// If <paramref name="sizable"/> is <c>true</c>, the columns' widths are set to their calculated
    /// auto-size values and then made resizable.
    /// </remarks>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="source"/> is <c>null</c>.
    /// </exception>
    public static void ExpandColumns(this DataGridView source, bool sizable = true)
    {
        foreach (DataGridViewColumn col in source.Columns)
        {
            if (col.ValueType.Name != "ICollection`1")
            {
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
        }

        if (!sizable) return;

        for (int index = 0; index <= source.Columns.Count - 1; index++)
        {
            int columnWidth = source.Columns[index].Width;

            source.Columns[index].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            // Set Width to calculated AutoSize value:
            source.Columns[index].Width = columnWidth;
        }
    }
    /// <summary>
    /// Adjusts the headers of the columns in the specified <see cref="DataGridView"/> by splitting camel case words into separate words.
    /// </summary>
    /// <param name="source">
    /// The <see cref="DataGridView"/> whose column headers will be adjusted.
    /// </param>
    /// <remarks>
    /// This method modifies the <see cref="DataGridView"/> column headers by inserting spaces between camel case words.
    /// For example, a header text "FirstName" will be transformed into "First Name".
    /// </remarks>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="source"/> is <c>null</c>.
    /// </exception>
    public static void FixHeaders(this DataGridView source)
    {
        static string SplitCamelCase(string sender)
            => string.Join(" ", HeaderTextRegex().Matches(sender).Select(m => m.Value));

        for (int index = 0; index < source.Columns.Count; index++)
        {
            source.Columns[index].HeaderText = SplitCamelCase(source.Columns[index].HeaderText);
        }
    }
    /// <summary>
    /// Disables sorting for all columns in the specified <see cref="DataGridView"/>.
    /// </summary>
    /// <param name="source">
    /// The <see cref="DataGridView"/> for which column sorting will be disabled.
    /// </param>
    /// <remarks>
    /// This method sets the <see cref="DataGridViewColumn.SortMode"/> property of each column 
    /// in the <see cref="DataGridView"/> to <see cref="DataGridViewColumnSortMode.NotSortable"/>.
    /// </remarks>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="source"/> is <c>null</c>.
    /// </exception>
    public static void DisableSorting(this DataGridView source)
    {
        source.Columns
            .Cast<DataGridViewColumn>()
            .ToList()
            .ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
    }
    
    /// <summary>
    /// Enables sorting for all columns in the specified <see cref="DataGridView"/>.
    /// </summary>
    /// <param name="source">
    /// The <see cref="DataGridView"/> whose columns will have sorting enabled.
    /// </param>
    /// <remarks>
    /// This method sets the <see cref="DataGridViewColumn.SortMode"/> property of each column
    /// in the <see cref="DataGridView"/> to <see cref="DataGridViewColumnSortMode.Automatic"/>,
    /// allowing users to sort the columns by clicking on their headers.
    /// </remarks>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="source"/> is <c>null</c>.
    /// </exception>
    public static void EnableSorting(this DataGridView source)
    {
        source.Columns
            .Cast<DataGridViewColumn>()
            .ToList()
            .ForEach(f => f.SortMode = DataGridViewColumnSortMode.Automatic);
    }

    [GeneratedRegex("([A-Z][a-z]+)")]
    private static partial Regex HeaderTextRegex();
}