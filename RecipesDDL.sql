IF DB_ID('RecipeDatabase') IS NULL
CREATE DATABASE RecipeDatabase
GO
use RecipeDatabase;
IF EXISTS (SELECT NAME FROM sys.sysobjects WHERE NAME = 'tblRecipeIngredient')
DROP TABLE tblRecipeIngredient
IF EXISTS (SELECT NAME FROM sys.sysobjects WHERE NAME = 'tblRecipe')
DROP TABLE tblRecipe
IF EXISTS (SELECT NAME FROM sys.sysobjects WHERE NAME = 'tblIngredient')
DROP TABLE tblIngredient
IF EXISTS (SELECT NAME FROM sys.sysobjects WHERE NAME = 'tblUser')
DROP TABLE tblUser

 CREATE TABLE tblUser (
    UserID int NOT NULL IDENTITY(1,1) PRIMARY KEY,
    FirstName varchar(50),
	LastName varchar(50),
	UserName varchar(50),
	Password varchar(70)	
);

 CREATE TABLE tblRecipe (
    RecipeID int NOT NULL IDENTITY(1,1) PRIMARY KEY,
    RecipeName varchar(50),
	RecipeType varchar(50),
	Portions int,	
	RecipeText varchar(255),
	RecipeDateOfCreation datetime,
	UserID int FOREIGN KEY REFERENCES tblUser(UserID)
);

 CREATE TABLE tblIngredient (
    IngredientID int NOT NULL IDENTITY(1,1) PRIMARY KEY,
	IngredientName varchar(50),
	IngredientAmount int,	
);

 CREATE TABLE tblRecipeIngredient (
    RecipeIngredientID int NOT NULL IDENTITY(1,1) PRIMARY KEY,
    RecipeID int FOREIGN KEY REFERENCES tblRecipe(RecipeID) ON DELETE CASCADE,
	IngredientID int FOREIGN KEY REFERENCES tblIngredient(IngredientID) ON DELETE CASCADE
);

GO

INSERT INTO tblIngredient values('Milk',1);
INSERT INTO tblIngredient values('Sugar',1);
INSERT INTO tblIngredient values('Mayo',1);
INSERT INTO tblIngredient values('Ketchup',1);
INSERT INTO tblIngredient values('Egg',1);
INSERT INTO tblIngredient values('Flour',1);
INSERT INTO tblIngredient values('Salt',1);
INSERT INTO tblIngredient values('Tomato',1);
INSERT INTO tblIngredient values('Mushrooms',1);
INSERT INTO tblIngredient values('Cheese',1);
INSERT INTO tblIngredient values('Ham',1);