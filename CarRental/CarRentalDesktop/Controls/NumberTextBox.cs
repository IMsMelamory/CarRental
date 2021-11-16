using System.Windows.Controls;
using System.Windows.Input;

namespace CarRentalDesktop.Controls
{
    public class NumberTextBox : TextBox
    {
        protected override void OnPreviewTextInput(TextCompositionEventArgs e)
        {
            if (!int.TryParse(e.Text, out var number))
            {
                e.Handled = true;
            }

            base.OnPreviewTextInput(e);
        }
    }
}