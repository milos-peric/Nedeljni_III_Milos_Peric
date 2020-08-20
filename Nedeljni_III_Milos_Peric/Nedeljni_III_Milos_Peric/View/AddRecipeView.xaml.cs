using Nedeljni_III_Milos_Peric.ViewModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace Nedeljni_III_Milos_Peric.View
{
    /// <summary>
    /// Interaction logic for AddRecipeView.xaml
    /// </summary>
    public partial class AddRecipeView : Window
    {
        public AddRecipeView(tblUser user)
        {
            InitializeComponent();
            this.DataContext = new AddRecipeViewModel(this, user);
        }

        /// <summary>
        /// Validate that User input are just letters
        /// </summary>
        private void LetterValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^a-zA-Z ]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        /// <summary>
        /// Validate that User input is just letters and digits
        /// </summary>
        private void LetterAndNumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^a-zA-Z0-9 ]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        /// <summary>
        /// Validate that User input is just numbers
        /// </summary>
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
