
﻿using Nedeljni_III_Milos_Peric.ViewModel;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
﻿using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
