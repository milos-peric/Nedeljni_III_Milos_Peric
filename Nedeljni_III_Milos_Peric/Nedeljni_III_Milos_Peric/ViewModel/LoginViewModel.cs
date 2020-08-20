using Nedeljni_III_Milos_Peric.Command;
using Nedeljni_III_Milos_Peric.Utility;
using Nedeljni_III_Milos_Peric.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Nedeljni_III_Milos_Peric.ViewModel
{
    class LoginViewModel : ViewModelBase
    {
        LoginView login;
        DatabaseService databaseService = new DatabaseService();
        public LoginViewModel(LoginView viewLogin)
        {
            login = viewLogin;
            user = new tblUser();
            adminUser = new tblUser();
        }

        private string userName;
        public string UserName
        {
            get
            {
                return userName;
            }
            set
            {
                userName = value;
                OnPropertyChanged("UserName");
            }
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

        private tblUser adminUser;
        public tblUser AdminUser
        {
            get
            {
                return adminUser;
            }
            set
            {
                adminUser = value;
                OnPropertyChanged("AdminUser");
            }
        }

        private ICommand submit;
        public ICommand Submit
        {
            get
            {
                if (submit == null)
                {
                    submit = new RelayCommand(SubmitCommandExecute, param => CanSubmitCommandExecute());
                }
                return submit;
            }
        }

        private void SubmitCommandExecute(object obj)
        {
            try
            {
                string password = (obj as PasswordBox).Password;
                user = databaseService.FindUserCredentials(UserName, password);
                if (UserName.Equals("Admin") && password.Equals("Admin123"))
                {
                    if (databaseService.UserExists(UserName))
                    {
                        UserView userView = new UserView(user);
                        login.Close();
                        userView.Show();
                        return;
                    }
                    else
                    {
                        adminUser.UserName = UserName;
                        string encryptPassword = EncryptionHelper.Encrypt(password);
                        adminUser.Password = encryptPassword;
                        databaseService.AddUser(adminUser);
                        UserView userView = new UserView(adminUser);
                        login.Close();
                        userView.Show();
                        return;
                    }
                }
                else if (user != null)
                {
                    UserView userView = new UserView(user);
                    login.Close();
                    userView.Show();
                    return;
                }
                else
                {
                    if (databaseService.UserExists(UserName))
                    {
                        MessageBox.Show("User with same name already exists in database, please choose another username.", "Info");
                        return;
                    }
                    else
                    {
                        user = new tblUser();
                        user.UserName = UserName;
                        string encryptPassword = EncryptionHelper.Encrypt(password);
                        user.Password = encryptPassword;
                        FirstTimeAccessView firstTimeView = new FirstTimeAccessView(user);
                        login.Close();
                        firstTimeView.Show();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanSubmitCommandExecute()
        {
            if (string.IsNullOrEmpty(UserName))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}

