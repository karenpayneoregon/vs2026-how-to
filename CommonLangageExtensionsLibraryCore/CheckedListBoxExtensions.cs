namespace CommonLangageExtensionsLibraryCore;

public static class CheckedListBoxExtensions
{
    /// <summary>
    /// Moves the selected item in a <see cref="CheckedListBox"/> either up or down in the list.
    /// </summary>
    /// <param name="sender">
    /// The <see cref="CheckedListBox"/> instance where the item movement will occur.
    /// </param>
    /// <param name="pDirectionUp">
    /// A boolean value indicating the direction of movement. 
    /// Pass <c>true</c> to move the item up, or <c>false</c> to move it down. 
    /// The default value is <c>true</c>.
    /// </param>
    /// <remarks>
    /// If no item is selected, the method will return without making any changes.
    /// If the movement exceeds the bounds of the list, the item will be placed at the end.
    /// </remarks>
    public static void MoveItem(this CheckedListBox sender, bool pDirectionUp = true) 
    {
        if (sender.SelectedItem == null)
        {
            return;
        }

        sender.BeginUpdate();

        try
        {

            var selectedIndex = sender.SelectedIndex;
            object selectedItem = sender.SelectedItem;
            var checkedState = sender.GetItemChecked(selectedIndex);

            sender.Items.RemoveAt(selectedIndex);

            var newIndex = selectedIndex;
            if (pDirectionUp)
            {
                newIndex = newIndex - 1;
            }
            else
            {
                newIndex = newIndex + 1;
            }

            if (newIndex > sender.Items.Count - 1 | newIndex < 0)
            {
                newIndex = sender.Items.Count;
            }


            sender.Items.Insert(newIndex, selectedItem);
            sender.SetItemChecked(newIndex, checkedState);

            sender.SelectedIndex = newIndex;
        }
        finally
        {
            sender.EndUpdate();
        }
    }
}