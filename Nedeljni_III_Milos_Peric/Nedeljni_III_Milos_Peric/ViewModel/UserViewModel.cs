using Dan_LIII_Milos_Peric.Command;
using Dan_LIII_Milos_Peric.ViewModel;
using Nedeljni_III_Milos_Peric.View;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace Nedeljni_III_Milos_Peric.ViewModel
{
    class UserViewModel : ViewModelBase
    {
        #region Objects

        UserView userView;

        #endregion

        #region Constructors

        public UserViewModel(UserView userViewOpen, tblUser user)
        {
            userView = userViewOpen;
            User = user;
            AllRecipes = GetAllRecipes();
        }

        public UserViewModel(UserView userViewOpen)
        {
            userView = userViewOpen;
            AllRecipes = GetAllRecipes();

        }

        #endregion

        #region Properties

        private tblUser user;

        public tblUser User
        {
            get { return user; }
            set 
            {
                user = value;
                OnPropertyChanged("User");
            }
        }

        private List<tblRecipe> allRecipes;

        public List<tblRecipe> AllRecipes
        {
            get { return allRecipes; }
            set 
            { 
                allRecipes = value;
                OnPropertyChanged("AllRecipes");
            }
        }

        private tblRecipe recipe;

        public tblRecipe Recipe
        {
            get { return recipe; }
            set 
            {
                recipe = value;
                OnPropertyChanged("Recipe");
            }
        }



        #endregion

        #region Commands

        private ICommand addRecipe;
        public ICommand AddRecipe
        {
            get
            {
                if (addRecipe == null)
                {
                    addRecipe = new RelayCommand(param => AddRecipeExecute(), param => CanAddRecipeExecute());
                }
                return addRecipe;
            }
        }


        private ICommand logout;
        public ICommand Logout
        {
            get
            {
                if (logout == null)
                {
                    logout = new RelayCommand(param => LogoutExecute(), param => CanLogoutExecute());
                }
                return logout;
            }
        }



        #endregion

        #region Functions

        private void AddRecipeExecute()
        {
            AddRecipeView view = new AddRecipeView(User);
            view.ShowDialog();
            AllRecipes = GetAllRecipes();
        }

        private bool CanAddRecipeExecute()
        {
            return true;
        }

        private void LogoutExecute()
        {
            userView.Close();
        }

        private bool CanLogoutExecute()
        {
            return true;
        }

        private List<tblRecipe> GetAllRecipes()
        {
            List<tblRecipe> recipes = new List<tblRecipe>();
            try
            {
                using (RecipeDatabaseEntities db = new RecipeDatabaseEntities())
                {
                    foreach (tblRecipe recipe in db.tblRecipes)
                    {
                        recipes.Add(recipe);
                    }
                }
                return recipes;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return null;
            }
        }

        #endregion
    }
}
