using Nedeljni_III_Milos_Peric.Command;
using Nedeljni_III_Milos_Peric.Utility;
using Nedeljni_III_Milos_Peric.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Nedeljni_III_Milos_Peric.ViewModel
{
    class FirstTimeAccessViewModel : ViewModelBase
    {
        private FirstTimeAccessView firstTimeAccessView;
        private DatabaseService databaseService = new DatabaseService();

        public FirstTimeAccessViewModel(FirstTimeAccessView firstTimeAccessView, tblUser user)
        {
            this.firstTimeAccessView = firstTimeAccessView;
            this.user = user;
        }

        private tblUser user;
        public tblUser User
        {
            get
            {
                return user;
            }
            set
            {
                user = value;
                OnPropertyChanged("User");
            }
        }

        private ICommand confirmCommand;
        public ICommand ConfirmCommand
        {
            get
            {
                if (confirmCommand == null)
                {
                    confirmCommand = new RelayCommand(param => ConfirmCommandExecute(), param => CanConfirmCommandExecute());
                }
                return confirmCommand;
            }
        }

        private void ConfirmCommandExecute()
        {
            try
            {
                if (EntryValidation.IsOnlyLetters(user.FirstName) == false)
                {
                    MessageBox.Show("First name can only contain letters. Please try again", "Invalid input");
                    return;
                }
                if (EntryValidation.IsOnlyLetters(user.LastName) == false)
                {
                    MessageBox.Show("Last name can only contain letters. Please try again", "Invalid input");
                    return;
                }
                databaseService.AddUser(user);               
                UserView userView = new UserView(user);
                MessageBox.Show("User registered successfully.", "Info");
                firstTimeAccessView.Close();
                userView.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanConfirmCommandExecute()
        {
            if (string.IsNullOrEmpty(user.FirstName) || string.IsNullOrEmpty(user.LastName))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private ICommand cancelCommand;
        public ICommand CancelCommand
        {
            get
            {
                if (cancelCommand == null)
                {
                    cancelCommand = new RelayCommand(param => CancelCommandExecute());
                }
                return cancelCommand;
            }
        }

        private void CancelCommandExecute()
        {
            try
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to close window?", "Close Window", MessageBoxButton.YesNo);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        firstTimeAccessView.Close();
                        break;
                    case MessageBoxResult.No:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


    }
}
