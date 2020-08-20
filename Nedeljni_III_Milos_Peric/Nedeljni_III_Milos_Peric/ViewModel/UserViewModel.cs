using Nedeljni_III_Milos_Peric.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nedeljni_III_Milos_Peric.ViewModel
{
    class UserViewModel : ViewModelBase
    {
        private UserView userView;

        public UserViewModel(UserView userView, tblUser user)
        {
            this.userView = userView;
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
    }
}
