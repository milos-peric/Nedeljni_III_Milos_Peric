//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Nedeljni_III_Milos_Peric
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblRecipe
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblRecipe()
        {
            this.tblRecipeIngredients = new HashSet<tblRecipeIngredient>();
        }
    
        public int RecipeID { get; set; }
        public string RecipeName { get; set; }
        public string RecipeType { get; set; }
        public Nullable<int> Portions { get; set; }
        public string RecipeText { get; set; }
        public Nullable<System.DateTime> RecipeDateOfCreation { get; set; }
        public Nullable<int> UserID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblRecipeIngredient> tblRecipeIngredients { get; set; }
        public virtual tblUser tblUser { get; set; }
    }
}
