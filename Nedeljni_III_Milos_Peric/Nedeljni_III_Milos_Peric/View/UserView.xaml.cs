using Nedeljni_III_Milos_Peric.ViewModel;
using System.Windows;

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
    }
}
