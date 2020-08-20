<<<<<<< HEAD
﻿using Nedeljni_III_Milos_Peric.ViewModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
=======
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
>>>>>>> features/Milos

namespace Nedeljni_III_Milos_Peric.View
{
    /// <summary>
    /// Interaction logic for AddRecipeView.xaml
    /// </summary>
    public partial class AddRecipeView : Window
    {
<<<<<<< HEAD
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
=======
        public AddRecipeView()
        {
            InitializeComponent();
>>>>>>> features/Milos
        }
    }
}
