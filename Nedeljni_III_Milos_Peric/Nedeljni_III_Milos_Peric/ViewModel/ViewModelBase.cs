using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

<<<<<<< HEAD
namespace Dan_LIII_Milos_Peric.ViewModel
=======
namespace Nedeljni_III_Milos_Peric.ViewModel
>>>>>>> features/Milos
{
	public abstract class ViewModelBase : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
<<<<<<< HEAD
}
=======
}
>>>>>>> features/Milos
