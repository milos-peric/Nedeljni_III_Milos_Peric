using Nedeljni_III_Milos_Peric.ViewModel;
using System;
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

namespace Nedeljni_III_Milos_Peric.View
{
    /// <summary>
    /// Interaction logic for UserView.xaml
    /// </summary>
    public partial class UserView : Window
    {
        public UserView(tblUser user)
        {
            InitializeComponent();
            this.DataContext = new UserViewModel(this, user);
        }

        public UserView()
        {
            InitializeComponent();
            this.DataContext = new UserViewModel(this);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var view = CollectionViewSource.GetDefaultView((DataContext as UserViewModel).AllRecipes);
            view.Filter = o => (o as tblRecipe).RecipeName.Contains((sender as TextBox).Text);
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            var view = CollectionViewSource.GetDefaultView((DataContext as UserViewModel).AllRecipes);
            view.Filter = o => (o as tblRecipe).RecipeType.Contains((sender as TextBox).Text);
        }
    }
}
