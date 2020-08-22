using Nedeljni_III_Milos_Peric.Command;
﻿using Nedeljni_III_Milos_Peric.ViewModel;
using Nedeljni_III_Milos_Peric.View;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Windows;
using System.Runtime.InteropServices.WindowsRuntime;
using System.IO;
using System.Linq;
using System.ComponentModel;
using Nedeljni_III_Milos_Peric.Utility;
using System.Diagnostics;
using System.Threading;

namespace Nedeljni_III_Milos_Peric.ViewModel
{
    class UserViewModel : ViewModelBase
    {
        #region Objects

        UserView userView;
        ActionEvent actionEventObject;
        BackgroundWorker backgroundWorker1;
        #endregion

        #region Constructors

        public UserViewModel(UserView userViewOpen, tblUser user)
        {
            userView = userViewOpen;
            User = user;
            AllRecipes = GetAllRecipes();
            EmptyTxtFile();
            actionEventObject = new ActionEvent();
            backgroundWorker1 = new BackgroundWorker()
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true,
            };
            backgroundWorker1.DoWork += DoWork;
            backgroundWorker1.RunWorkerAsync();
            actionEventObject.ActionPerformed += ActionPerformed;
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

        private ICommand updateRecipe;
        public ICommand UpdateRecipe
        {
            get
            {
                if (updateRecipe == null)
                {
                    updateRecipe = new RelayCommand(param => UpdateRecipeExecute(), param => CanUpdateRecipeExecute());
                }
                return updateRecipe;
            }
        }

        private ICommand deleteRecipe;
        public ICommand DeleteRecipe
        {
            get
            {
                if (deleteRecipe == null)
                {
                    deleteRecipe = new RelayCommand(param => DeleteRecipeExecute(), param => CanDeleteRecipeExecute());
                }
                return deleteRecipe;
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

        private void UpdateRecipeExecute()
        {
            if(User.UserID == Recipe.UserID || User.UserName == "Admin")
            {
                UpdateRecipeView view = new UpdateRecipeView(Recipe, User);
                view.ShowDialog();
                AllRecipes = GetAllRecipes();
            }
            else
            {
                MessageBox.Show("You can not Update Recipe that you did not create");
            }
        }

        private bool CanUpdateRecipeExecute()
        {
            return true;
        }

        private void DeleteRecipeExecute()
        {
            try
            {               
                MessageBoxResult result = MessageBox.Show("Are you sure?", "Confirm Deleting", MessageBoxButton.YesNo);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        using (RecipeDatabaseEntities db = new RecipeDatabaseEntities())
                        {
                            tblRecipe deleteRecipe = new tblRecipe();
                            deleteRecipe = db.tblRecipes.Where(r => r.RecipeID == Recipe.RecipeID).FirstOrDefault();

                            db.tblRecipes.Remove(deleteRecipe);
                            db.SaveChanges();
                        }
                        MessageBox.Show("Recipe Deleted Successfully!");
                        AllRecipes = GetAllRecipes();
                        break;
                    case MessageBoxResult.No:                        
                        break;
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        private bool CanDeleteRecipeExecute()
        {
            if (User.UserName == "Admin")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void EmptyTxtFile()
        {
            string _location = @"~/../../../ingredients.txt";

            try
            {
                using (StreamWriter sw = new StreamWriter(_location))
                {
                    sw.WriteLine("");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        private void DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                if (!RecipeLogger.logMessage.Equals(""))
                {
                    RecipeLogger.LogToFile();
                    Debug.WriteLine("Action was logged to file.");
                }
                Thread.Sleep(500);
                if (backgroundWorker1.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
            }
        }

        void ActionPerformed(object source, ActionEventArgs args)
        {
            RecipeLogger.logMessage = args.LogMessage;
            RecipeLogger.logUserName = args.UserName;
        }

        //string logMessage = string.Format("Ingredient {0} was bought. Amount: {1}. Ingredient.IngredientName, Ingredient.IngredientAmount);
        //actionEventObject.OnActionPerformed(logMessage, User.UserName);

        #endregion
    }
}
