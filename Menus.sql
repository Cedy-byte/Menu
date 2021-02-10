CREATE DATABASE Menus
/*
CREATE TABLE Users (
Username VARCHAR (50)NOT NULL PRIMARY KEY,
Password VARCHAR (50)NOT NULL,
);
*/

CREATE TABLE Drink_Item(
ID INT NOT NULL IDENTITY(100,1) PRIMARY KEY,
ItemName VARCHAR(50) NOT NULL,
Description VARCHAR(50) NOT NULL,
Price DOUBLE PRECISION NOT NULL,
CostPrice DOUBLE PRECISION NOT NULL,
Container VARCHAR(50) NOT NULL,
DrinkType VARCHAR(50) NOT NULL,
);

CREATE TABLE Food_Item(
ID INT NOT NULL IDENTITY(200,1) PRIMARY KEY,
ItemName VARCHAR(50) NOT NULL,
Description VARCHAR(50) NOT NULL,
Price DOUBLE PRECISION NOT NULL,
CostPrice DOUBLE PRECISION NOT NULL,
FoodType VARCHAR(50) NOT NULL,
Cuisine VARCHAR(50) NOT NULL
);


INSERT INTO Drink_Item VALUES
('Robertson','White Wine',30,45,'Glass','Alcoholic'),
('Fanta Grape','Large 2L BTL',12,18,'Plastic','Soft Drink'),
('Water','Standard 1L',10,12,'Plastic','Soft Drink'),
('Heineken','Beer 750 ml',20,27,'Glass','Alcoholic'),
('Cappucino','Coffee 160 ml',25,35,'Polisterine','Cafe');


INSERT INTO Food_Item VALUES
('Tacos','Shrimp Tacos',69,50,'Regular','Mexican'),
('Pizza','Chreamy Chicken',60,39,'Medium','Italian'),
('Sushi','Salmon Tuna Prawn',42,35,'Sea Food','Japanese'),
('Buffalo Wings','With Dip Sauce',140,90,'Steak','American'),
('Beef Burger','Mustard Lettuce',67,48,'Large','American');

drop database Menus

/*--Includes the user login
INSERT INTO Users VALUES
('cedy','123'),
('Wishiya','123');

INSERT INTO Drink_Item VALUES
('Robertson','White Wine',30,45,'Glass','Alcoholic','cedy'),
('Fanta Grape','Large 2L BTL',12,18,'Plastic','Soft Drink','cedy'),
('Water','Standard 1L',10,12,'Plastic','Soft Drink','Wishiya'),
('Heineken','Beer 750 ml',20,27,'Glass','Alcoholic','Wishiya'),
('Cappucino','Coffee 160 ml',25,35,'Polisterine','Cafe','Wishiya');


INSERT INTO Food_Item VALUES
('Tacos','Shrimp Tacos',69,50,'Regular','Mexican','Wishiya'),
('Pizza','Chreamy Chicken',60,39,'Medium','Italian','Wishiya'),
('Sushi','Salmon Tuna Prawn',42,35,'Sea Food','Japanese','Wishiya'),
('Buffalo Wings','With Dip Sauce',140,90,'Steak','American','cedy'),
('Beef Burger','Mustard Lettuce',67,48,'Large','American','cedy');

Username VARCHAR (50)  NOT NULL,
FOREIGN KEY (Username) REFERENCES Users (Username)
*/

--SELECT*FROM Menu
SELECT*FROM Drink_Item
SELECT*FROM Food_Item