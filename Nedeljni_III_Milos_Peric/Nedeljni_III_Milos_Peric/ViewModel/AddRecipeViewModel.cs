<<<<<<< HEAD
﻿using Dan_LIII_Milos_Peric.Command;
using Dan_LIII_Milos_Peric.ViewModel;
using Nedeljni_III_Milos_Peric.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Nedeljni_III_Milos_Peric.ViewModel
{
    class AddRecipeViewModel : ViewModelBase
    {
        #region Objects

        AddRecipeView addRecipe;

        #endregion

        #region Constructors

        public AddRecipeViewModel(AddRecipeView addRecipeOpen, tblUser user)
        {
            addRecipe = addRecipeOpen;
            User = user;
            Recipe = new tblRecipe();
            RecipeTypes = GetRecipeTypes();
        }

        public AddRecipeViewModel(AddRecipeView addRecipeOpen)
        {
            addRecipe = addRecipeOpen;
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

        private string type;

        public string Type
        {
            get { return type; }
            set 
            {
                type = value;
                OnPropertyChanged("Type");
            }
        }

        private List<string> recipeTypes;

        public List<string> RecipeTypes
        {
            get { return recipeTypes; }
            set 
            {
                recipeTypes = value;
                OnPropertyChanged("RecipeTypes");

            }
        }

        private List<tblIngredient> ingredients;

        public List<tblIngredient> Ingredients
        {
            get { return ingredients; }
            set 
            {
                ingredients = value;
                OnPropertyChanged("Ingredients");
            }
        }


        #endregion

        #region Commands

        private ICommand save;
        public ICommand Save
        {
            get
            {
                if (save == null)
                {
                    save = new RelayCommand(param => SaveExecute(), param => CanSaveExecute());
                }
                return save;
            }
        }

        private ICommand close;
        public ICommand Close
        {
            get
            {
                if (close == null)
                {
                    close = new RelayCommand(param => CloseExecute(), param => CanCloseExecute());
                }
                return close;
            }
        }

        private ICommand chooseIngredients;
        public ICommand ChooseIngredients
        {
            get
            {
                if (chooseIngredients == null)
                {
                    chooseIngredients = new RelayCommand(param => ChooseIngredientsExecute(), param => CanChooseIngredientsExecute());
                }
                return chooseIngredients;
            }
        }
        

        #endregion

        #region Functions

        private void SaveExecute()
        {
            try
            {
                Ingredients = GetAllIngredients();
                Recipe.RecipeType = Type;
                Recipe.RecipeDateOfCreation = DateTime.Now;
                Recipe.RecipeText = AllIngredientsToString(Ingredients);
                using (RecipeDatabaseEntities db = new RecipeDatabaseEntities())
                {
                    //TODO ovde treba dodati Id od korisnika za Recept
                    db.tblRecipes.Add(Recipe);
                    db.SaveChanges();

                    foreach (tblIngredient ingredient in Ingredients)
                    {
                        tblRecipeIngredient recipeIngredient = new tblRecipeIngredient();
                        recipeIngredient.RecipeID = Recipe.RecipeID;
                        recipeIngredient.IngredientID = ingredient.IngredientID;
                        db.tblRecipeIngredients.Add(recipeIngredient);
                    }

                    db.SaveChanges();
                    
                }
                MessageBox.Show("Recipe Created Successfully!");
                addRecipe.Close();
                //TODO trebace refresovati sve recepte
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        private bool CanSaveExecute()
        {
            if (string.IsNullOrEmpty(Recipe.RecipeName) || string.IsNullOrEmpty(Type) 
                || string.IsNullOrEmpty(Recipe.Portions.ToString()))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void CloseExecute()
        {
            addRecipe.Close();
        }

        private bool CanCloseExecute()
        {
            return true;
        }


        private List<string> GetRecipeTypes()
        {
            List<string> types = new List<string>();
            types.Add("Appetizer");
            types.Add("Main Course");
            types.Add("Desert");

            return types;
        }

        private void ChooseIngredientsExecute()
        {
            ChooseIngredientsView view = new ChooseIngredientsView();
            view.ShowDialog();

        }

        private bool CanChooseIngredientsExecute()
        {
            return true;
        }

        private List<tblIngredient> GetAllIngredients()
        {
            List<string> allIngredients = new List<string>();
            string _location = @"~/../../../ingredients.txt";

            try
            {
                if (File.Exists(_location))
                {
                    string[] allLines = File.ReadAllLines(_location);
                    foreach (string line in allLines)
                    {
                        allIngredients.Add(line);
                    }
                }

                List<tblIngredient> realIngredients = new List<tblIngredient>();
                using (RecipeDatabaseEntities db = new RecipeDatabaseEntities())
                {
                    foreach (string ing in allIngredients)
                    {
                        tblIngredient i = new tblIngredient();
                        i = db.tblIngredients.Where(ingr => ingr.IngredientName == ing).FirstOrDefault();
                        realIngredients.Add(i);
                    }
                }

                return realIngredients;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return null;
            }
        }


        private string AllIngredientsToString(List<tblIngredient> ingredients)
        {
            string allIngredients = "";
            foreach (tblIngredient ingredient in ingredients)
            {
                allIngredients += $"\n {ingredient.IngredientName} " + " " + $"{ingredient.IngredientAmount}";
            }
            return allIngredients;
        }


        #endregion
=======
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nedeljni_III_Milos_Peric.ViewModel
{
    class AddRecipeViewModel
    {
>>>>>>> features/Milos
    }
}
