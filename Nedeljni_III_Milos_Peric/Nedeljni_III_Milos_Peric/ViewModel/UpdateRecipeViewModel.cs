using Nedeljni_III_Milos_Peric.Command;
using Nedeljni_III_Milos_Peric.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Nedeljni_III_Milos_Peric.ViewModel
{
    class UpdateRecipeViewModel : ViewModelBase
    {
        #region Objects

        UpdateRecipeView updateView;

        #endregion

        #region Constructors

        public UpdateRecipeViewModel(UpdateRecipeView updateViewOpen, tblRecipe recipe, tblUser user)
        {
            updateView = updateViewOpen;
            Recipe = recipe;
            RecipeTypes = GetRecipeTypes();
            User = user;
            EmptyTxtFile();
        }

        #endregion

        #region Properties

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

        private List<string> GetRecipeTypes()
        {
            List<string> types = new List<string>();
            types.Add("Appetizer");
            types.Add("Main Course");
            types.Add("Desert");

            return types;
        }

        private void SaveExecute()
        {
            try
            {
                Ingredients = GetAllIngredients();
                using (RecipeDatabaseEntities db = new RecipeDatabaseEntities())
                {
                    tblRecipe oldRecipe = new tblRecipe();
                    oldRecipe = db.tblRecipes.Where(r => r.RecipeID == Recipe.RecipeID).FirstOrDefault();

                    oldRecipe.RecipeName = Recipe.RecipeName;
                    oldRecipe.Portions = Recipe.Portions;
                    oldRecipe.RecipeType = Type;
                    oldRecipe.RecipeDateOfCreation = DateTime.Now;
                    oldRecipe.RecipeText = AllIngredientsToString(Ingredients);

                    if (oldRecipe.RecipeText == "")
                    {
                        MessageBox.Show("Please choose ingredients.");
                        return;
                    }
                    else
                    {
                        if(User.UserName == "Admin")
                        {
                            oldRecipe.UserID = User.UserID;
                        }
                        if(Ingredients != null)
                        {
                            foreach (tblIngredient ingredient in Ingredients)
                            {
                                tblRecipeIngredient recipeIngredient = new tblRecipeIngredient();
                                recipeIngredient.RecipeID = Recipe.RecipeID;
                                recipeIngredient.IngredientID = ingredient.IngredientID;
                                db.tblRecipeIngredients.Add(recipeIngredient);
                            }
                        }
                   
                        db.SaveChanges();
                    }

                }
                MessageBox.Show("Recipe Updated Successfully!");
                updateView.Close();
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
            updateView.Close();
        }

        private bool CanCloseExecute()
        {
            return true;
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
                        if (line == "")
                        {
                            continue;
                        }
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

        #endregion
    }
}
