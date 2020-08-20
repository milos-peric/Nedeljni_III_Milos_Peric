using Nedeljni_III_Milos_Peric.Command;
using Nedeljni_III_Milos_Peric.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

namespace Nedeljni_III_Milos_Peric.ViewModel
{
    class ChooseIngredientsViewModel : ViewModelBase
    {
        #region Objects

        ChooseIngredientsView ingredientsView;

        #endregion

        #region Constructors

        public ChooseIngredientsViewModel(ChooseIngredientsView ingredientsViewOpen)
        {
            ingredientsView = ingredientsViewOpen;            
        }


        #endregion

        #region Propfull

        private List<string> intgedients;

        public List<string> Intgredients
        {
            get { return intgedients; }
            set 
            {
                intgedients = value;
                OnPropertyChanged("Intgredients");
            }
        }

        private bool milk;

        public bool Milk
        {
            get { return milk; }
            set 
            {
                milk = value;
                OnPropertyChanged("Milk");
            }
        }


        private bool sugar;

        public bool Sugar
        {
            get { return sugar; }
            set
            {
                sugar = value;
                OnPropertyChanged("Sugar");
            }
        }

        private bool mayo;

        public bool Mayo
        {
            get { return mayo; }
            set
            {
                mayo = value;
                OnPropertyChanged("Mayo");
            }
        }

        private bool ketchup;

        public bool Ketchup
        {
            get { return ketchup; }
            set
            {
                ketchup = value;
                OnPropertyChanged("Ketchup");
            }
        }

        private bool egg;

        public bool Egg
        {
            get { return egg; }
            set
            {
                egg = value;
                OnPropertyChanged("Egg");
            }
        }

        private bool flour;

        public bool Flour
        {
            get { return flour; }
            set
            {
                flour = value;
                OnPropertyChanged("Flour");
            }
        }

        private bool salt;

        public bool Salt
        {
            get { return salt; }
            set
            {
                salt = value;
                OnPropertyChanged("Salt");
            }
        }

        private bool tomato;

        public bool Tomato
        {
            get { return tomato; }
            set
            {
                tomato = value;
                OnPropertyChanged("Tomato");
            }
        }


        private bool mushrooms;

        public bool Mushrooms
        {
            get { return mushrooms; }
            set
            {
                mushrooms = value;
                OnPropertyChanged("Mushrooms");
            }
        }


        private bool cheese;

        public bool Cheese
        {
            get { return cheese; }
            set
            {
                cheese = value;
                OnPropertyChanged("Cheese");
            }
        }


        private bool ham;

        public bool Ham
        {
            get { return ham; }
            set
            {
                ham = value;
                OnPropertyChanged("Ham");
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


        #endregion

        #region Functions

        private void SaveExecute()
        {
            try
            {
                List<string> allIngredients = new List<string>();
                if(Milk == true)
                {
                    allIngredients.Add("Milk");
                }
                if (Sugar == true)
                {
                    allIngredients.Add("Sugar");
                }
                if (Mayo == true)
                {
                    allIngredients.Add("Mayo");
                }
                if (Ketchup == true)
                {
                    allIngredients.Add("Ketchup");
                }
                if (Egg == true)
                {
                    allIngredients.Add("Egg");
                }
                if (Flour == true)
                {
                    allIngredients.Add("Flour");
                }
                if (Salt == true)
                {
                    allIngredients.Add("Salt");
                }
                if (Tomato == true)
                {
                    allIngredients.Add("Tomato");
                }
                if (Mushrooms == true)
                {
                    allIngredients.Add("Mushrooms");
                }
                if (Cheese == true)
                {
                    allIngredients.Add("Cheese");
                }
                if (Ham == true)
                {
                    allIngredients.Add("Ham");
                }
                Intgredients = allIngredients;
               
                MessageBox.Show("Intgredients Choosed Successfully!");

                string _location = @"~/../../../ingredients.txt";

                try
                {
                    using (StreamWriter sw = new StreamWriter(_location))
                    {
                        foreach (string ingredient in Intgredients)
                        {
                            sw.WriteLine(ingredient);
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }


                ingredientsView.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        private bool CanSaveExecute()
        {
            return true;
        }

        private void CloseExecute()
        {
            ingredientsView.Close();
        }

        private bool CanCloseExecute()
        {
            return true;
        }

        #endregion
    }
}
