namespace CommonLangageExtensionsLibraryCore;

public enum MoveDirection { Up = -1, Down = 1 };
public static class ListViewExtensions
{
    /// <summary>
    /// Moves the selected items in a <see cref="ListView"/> either up or down based on the specified direction.
    /// </summary>
    /// <param name="sender">The <see cref="ListView"/> control containing the items to be moved.</param>
    /// <param name="direction">The direction in which to move the items. Use <see cref="MoveDirection.Up"/> to move up and <see cref="MoveDirection.Down"/> to move down.</param>
    /// <remarks>
    /// This method ensures that the selected items are moved within the valid bounds of the <see cref="ListView"/>.
    /// If the operation is not valid (e.g., attempting to move the first item up or the last item down), no action is performed.
    /// </remarks>
    public static void MoveListViewItems(this ListView sender, MoveDirection direction)
    {
        int dir = (int)direction;

        bool valid = sender.SelectedItems.Count > 0 &&
                     ((direction == MoveDirection.Down &&
                       (sender.SelectedItems[^1]
                            .Index <
                        sender.Items.Count - 1)) ||
                      (direction == MoveDirection.Up &&
                       (sender.SelectedItems[0]
                            .Index >
                        0)));

        if (valid)
        {
            sender.SuspendLayout();

            try
            {
                foreach (ListViewItem item in sender.SelectedItems)
                {
                    var index = item.Index + dir;
                    sender.Items.RemoveAt(item.Index);
                    sender.Items.Insert(index, item);
                    sender.Items[index].Selected = true;
                    sender.Focus();
                }
            }
            finally
            {
                sender.ResumeLayout();
            }
        }
    }
}