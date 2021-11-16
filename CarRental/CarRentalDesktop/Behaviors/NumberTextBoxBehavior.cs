using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace CarRentalDesktop.Behaviors
{
    public class LengthTextBoxBehavior : Behavior<TextBox>
    {
        public int MaxLength { get; set; }
        
        protected override void OnAttached()
        {
            AssociatedObject.PreviewTextInput += OnTextChanged;
            base.OnAttached();
        }

        protected override void OnDetaching()
        {
            AssociatedObject.PreviewTextInput -= OnTextChanged;
            base.OnDetaching();
        }

        private void OnTextChanged(object sender, TextCompositionEventArgs e)
        {
            if ((e.OriginalSource as TextBox)?.Text.Length > MaxLength-1)
            {
                e.Handled = true;
            }
        }
    }

    public class NumberTextBoxBehavior : Behavior<TextBox>
    {
        protected override void OnAttached()
        {
            AssociatedObject.PreviewTextInput += OnTextChanged;
            base.OnAttached();
        }

        protected override void OnDetaching()
        {
            AssociatedObject.PreviewTextInput -= OnTextChanged;
            base.OnDetaching();
        }

        private void OnTextChanged(object sender, TextCompositionEventArgs e)
        {
            if (!int.TryParse(e.Text, out var number))
            {
                e.Handled = true;
            }
        }
    }
}