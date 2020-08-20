using Nedeljni_III_Milos_Peric.ViewModel;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;

namespace Nedeljni_III_Milos_Peric.View
{
    /// <summary>
    /// Interaction logic for ChooseIngredientsView.xaml
    /// </summary>
    public partial class ChooseIngredientsView : Window
    {
        public ChooseIngredientsView()
        {
            InitializeComponent();
            this.DataContext = new ChooseIngredientsViewModel(this);
        }
    }
}
