/*Create users table*/
CREATE TABLE Customer(
Customer_ID int IDENTITY(1,1) PRIMARY KEY,
First_Name VARCHAR(50) NOT NULL,
Last_Name VARCHAR(50) NOT NULL,
Email_Address VARCHAR(320) NOT NULL,
Password VARCHAR(50) NOT NULL CHECK (DATALENGTH(Password) > 5),
Age INT,
Gender bit,
Address TEXT,
Phone_Number VARCHAR(11),
Admin BIT DEFAULT 0 NOT NULL
);


/*Create sessions table*/
CREATE TABLE Sessions(
Session_ID int IDENTITY(1,1) PRIMARY KEY,
Customer_ID int NOT NULL,
Token VARCHAR(50) NOT NULL,
ExpiryTime DATETIME NOT NULL,
FOREIGN KEY (Customer_ID) REFERENCES Customer(Customer_ID)
ON DELETE CASCADE
);

CREATE TABLE Orders(
Order_ID INT IDENTITY(1,1) PRIMARY KEY,
Time_Ordered DATETIME NOT NULL,
Quantity INT NOT NULL,
Product_ID INT NOT NULL,
Customer_ID INT NOT NULL,
FOREIGN KEY (Customer_ID) REFERENCES Customer(Customer_ID)
ON DELETE CASCADE,
FOREIGN KEY (Product_ID) REFERENCES Products(Product_ID)
);


CREATE TABLE Products(
Product_ID INT IDENTITY(1,1) PRIMARY KEY,
Product_Name VARCHAR(50) NOT NULL,
Image_Url TEXT,
Stock INT NOT NULL,
Catagory VARCHAR(50) NOT NULL,
Total_Sold INT DEFAULT 0 NOT NULL,
Price MONEY NOT NULL,
Description TEXT
);

CREATE TABLE Reviews(
Product_ID INT NOT NULL,
Customer_ID INT NOT NULL,
Rating TINYINT NOT NULL CHECK(0 < Rating AND Rating <= 5),
Title VARCHAR(50),
Description TEXT,
PRIMARY KEY (Product_ID, Customer_ID),
FOREIGN KEY (Customer_ID) REFERENCES Customer(Customer_ID),
FOREIGN KEY (Product_ID) REFERENCES Products(Product_ID)
ON DELETE CASCADE
);
--Views-------------------------------------------------------------------------------------------------------

CREATE VIEW [dbo].[Top_Rated] AS 
SELECT Ratings.Rating , Products.Product_Name, Ratings.Product_ID
FROM Ratings
INNER JOIN Products ON Products.Product_ID = Ratings.Product_ID;
GO

--------------------------------------------------------------------------------------------------------------

--stored procedures


/*Validate Customer*/
CREATE PROCEDURE ValidateCustomer
@Email VARCHAR(320),
@Password VARCHAR(50),
@Token VARCHAR(25) OUTPUT
AS
BEGIN
	BEGIN TRANSACTION
		IF EXISTS (SELECT * FROM Customer WHERE Email_Address = @Email AND Password = @Password)
			BEGIN
				DECLARE @CustomerID AS INT = (SELECT Customer_ID FROM Customer WHERE Email_Address = @Email AND Password = @Password);
				
				DECLARE @TheToken AS VARCHAR(50) = CONVERT(VARCHAR(30), RIGHT(NEWID(), 25));
				
				DECLARE @ExpiryTime AS DATETIME = (SELECT DATEADD(hour, 1, CURRENT_TIMESTAMP) AS DateAdd);

				Insert INTO Sessions (Customer_ID, Token, ExpiryTime) VALUES (@CustomerID, @TheToken, @ExpiryTime);
				
				SELECT @Token = @TheToken;
			
			END
		ELSE
			BEGIN
				SELECT @Token = 208;
			END
		
		IF @@ERROR != 0
			BEGIN
				SELECT @Token = 500;
				ROLLBACK TRANSACTION
			END
		ELSE
			COMMIT TRANSACTION
END
Go

-- how to run
DECLARE @Out as VARCHAR(25)
exec ValidateCustomer @Email = 'email', @Password = 'password', @Token = @Out OUTPUT;
SELECT @OUT AS 'Outmessage';
--------



CREATE PROCEDURE RegisterCustomer
@First_Name VARCHAR(50), 
@Last_Name VARCHAR(50),
@Email VARCHAR(320),
@Password VARCHAR(50),
@Age INT,
@Gender BIT,
@Address TEXT,
@Phone_Number VARCHAR(11),
@ResponseMessage INT OUTPUT
AS
BEGIN
	BEGIN TRANSACTION
		IF EXISTS (SELECT * FROM Customer WHERE Email_Address = @Email)
			BEGIN
			--an account with this email already exists
				SELECT @ResponseMessage = 208;
			END
		ELSE
			BEGIN
				INSERT INTO Customer
				(First_Name, Last_Name, Email_Address, Password, Age, Gender, Address, Phone_Number)
				VALUES
				(@First_Name, @Last_Name, @Email, @Password, @Age, @Gender, @Address, @Phone_Number);
				SELECT @ResponseMessage = 200;
			END
		IF @@ERROR != 0
			BEGIN
				SELECT @ResponseMessage = 500;
				ROLLBACK TRANSACTION
			END
		ELSE
			COMMIT TRANSACTION

END
GO

-- how to run
DECLARE @Out as BIT
exec RegisterCustomer @First_Name = 'bob', @Last_Name = 'bobby', @Email = 'email6', @Password = 'password', @Age = '5', @Gender = 1, @Address = 'address goes here', @Phone_Number = '07932153300', @ResponseMessage = @Out OUTPUT;
SELECT @OUT AS 'Outputmessage';
--------



--Edit user

CREATE PROCEDURE EditCustomer
@Token VARCHAR(25),
@FirstName VARCHAR(50),
@LastName VARCHAR(50),
@Email VARCHAR(320),
@Password VARCHAR(50),
@Age INT,
@Gender BIT,
@Address TEXT,
@PhoneNumber VARCHAR(11),
@ResponseMessage INT OUTPUT
AS
BEGIN
	IF EXISTS (SELECT Customer_ID FROM Sessions WHERE Token = @Token AND CURRENT_TIMESTAMP <= ExpiryTime)
		BEGIN
			Declare @Customer_ID AS INT = (SELECT Customer_ID FROM Sessions WHERE Token = @Token);
			IF EXISTS(SELECT * FROM Customer WHERE (Customer_ID != @Customer_ID AND Email_Address = @Email))
				BEGIN
				--an account with this email already exists
					SELECT @ResponseMessage = 208;
				END
			ELSE
				BEGIN
					UPDATE Customer SET First_Name = @FirstName, Last_Name = @LastName, Email_Address = @Email, Password = @Password, Age = @Age, Gender = @Gender, Address = @Address, Phone_Number = @PhoneNumber WHERE Customer_ID = @Customer_ID AND Admin = 0;
					SELECT @ResponseMessage = 200;
				END
		END
	ELSE
		BEGIN
		--customer not logged in
			SELECT @ResponseMessage = 400;
		END
END
GO

-- how to run
DECLARE @Out as BIT; 
EXEC EditCustomer @Token = '8E-433D-BCB4-A596E369001C', @FirstName = 'This has', @LastName = 'been changed', @Email = 'email501', @Password = '^?H??(\u0004qQ??o??)s`=\rj???*\u0011?r\u001d\u0015B?', @Age = '34', @Gender = 1, @Address = 'address string 5', @PhoneNumber = '01454234', @ResponseMessage = @Out OUTPUT; 
SELECT @Out AS 'OutputMessage'; 
--------

--Edit user as admin

CREATE PROCEDURE AdminEditCustomer
@Token VARCHAR(25),
@Customer_ID INT,
@FirstName VARCHAR(50),
@LastName VARCHAR(50),
@Email VARCHAR(320),
@Password VARCHAR(50),
@Age INT,
@Gender BIT,
@Address TEXT,
@PhoneNumber VARCHAR(11),
@ResponseMessage INT OUTPUT
AS
BEGIN
	IF EXISTS (SELECT Customer_ID FROM Sessions WHERE Token = @Token AND CURRENT_TIMESTAMP <= ExpiryTime)
		BEGIN
			Declare @Admin_ID AS INT = (SELECT Customer_ID FROM Sessions WHERE Token = @Token);
			
			IF EXISTS(SELECT * FROM Customer WHERE Customer_ID = @Admin_ID AND Admin = 1)
				BEGIN
					IF EXISTS(SELECT * FROM Customer WHERE (Customer_ID != @Customer_ID AND Email_Address = @Email))
						BEGIN
						--an account with this email already exists
							SELECT @ResponseMessage = 208;
						END
					ELSE
						BEGIN
							UPDATE Customer SET First_Name = @FirstName, Last_Name = @LastName, Email_Address = @Email, Password = @Password, Age = @Age, Gender = @Gender, Address = @Address, Phone_Number = @PhoneNumber WHERE Customer_ID = @Customer_ID AND Admin = 0;
							SELECT @ResponseMessage = 200;
						END
				END
			ELSE
				BEGIN
				
					SELECT @ResponseMessage = 401;
				
				END
		END
	ELSE
		BEGIN
		--customer not logged in
			SELECT @ResponseMessage = 400;
		END
END
GO

--Change password

CREATE PROCEDURE ChangePassword
@Token VARCHAR(25),
@NewPassword VARCHAR(50),
@ResponseMessage INT OUTPUT
AS
BEGIN
	BEGIN TRANSACTION
		IF EXISTS(SELECT Customer_ID FROM Sessions WHERE Token = @Token AND CURRENT_TIMESTAMP <= ExpiryTime)
			BEGIN
				Declare @Customer_ID AS INT = (SELECT Customer_ID FROM Sessions WHERE Token = @Token);
				IF EXISTS(SELECT * FROM Customer WHERE Customer_ID = @Customer_ID AND Admin = 0)
					BEGIN
						UPDATE Customer SET Password = @NewPassword WHERE Customer_ID = @Customer_ID AND Admin = 0;

						SELECT @ResponseMessage = 200;
					END
				ELSE
					BEGIN
						--customer does not exist
						SELECT @ResponseMessage = 401;
					END
			END
		ELSE
			BEGIN
			--user not logged in
				SELECT @ResponseMessage = 400;
			END	
	IF @@ERROR != 0
	BEGIN
		SELECT @ResponseMessage = 500;
		ROLLBACK TRANSACTION
	END
	ELSE
		COMMIT TRANSACTION
END
GO

-- how to run
DECLARE @Out as BIT; 
EXEC ChangePassword @Token = 'FD-48BA-8080-EE76E5F9FAEC', @NewPassword = 'password', @ResponseMessage = @Out OUTPUT; 
SELECT @Out AS 'OutputMessage';
--------


--Delete User
CREATE PROCEDURE DeleteCustomer
@Token VARCHAR(25),
@ResponseMessage INT OUTPUT
AS
BEGIN
	IF EXISTS (SELECT Customer_ID FROM Sessions WHERE Token = @Token AND CURRENT_TIMESTAMP <= ExpiryTime)
		BEGIN
			Declare @Customer_ID AS INT = (SELECT Customer_ID FROM Sessions WHERE Token = @Token);
			IF EXISTS(SELECT * FROM Customer WHERE Customer_ID = @Customer_ID AND Admin = 0)
				BEGIN
					Delete customer WHERE Customer_ID = @Customer_ID AND Admin = 0;
					SELECT @ResponseMessage = 200;
				END
			ELSE
				BEGIN
					--customer does not exist or is an admin
					SELECT @ResponseMessage = 401;
				END
		END
	ELSE
		BEGIN
			--not logged in
			SELECT @ResponseMessage = 400 ;
		END
END
GO

-- how to run
DECLARE @Out as BIT; 
EXEC DeleteCustomer @Token = 'FD-48BA-8080-EE76E5F9FAEC', @Success = @Out OUTPUT; 
SELECT @Out AS 'OutputMessage'; 
--------

--Delete Customer Admin
CREATE PROCEDURE AdminDeleteCustomer
@Token VARCHAR(25),
@Customer_ID_Delete INT,
@Success BIT OUTPUT
AS
BEGIN
	IF EXISTS (SELECT Customer_ID FROM Sessions WHERE Token = @Token AND CURRENT_TIMESTAMP <= ExpiryTime)
		BEGIN
			Declare @Admin_ID AS INT = (SELECT Customer_ID FROM Sessions WHERE Token = @Token);
			IF EXISTS(SELECT * FROM Customer WHERE Customer_ID = @Customer_ID AND Admin = 1)
				BEGIN
					Delete customer WHERE Customer_ID = @Customer_ID_Delete AND Admin = 0;
					SELECT @Success = 200;
				END
			ELSE
				BEGIN
				--user customer does not exist or is an admin
					SELECT @Success = 401;
				END
		END
	ELSE
		BEGIN
		--user not logged in
			SELECT @Success = 400;
		END
END
GO

------Admin ---------

--Register Admin
CREATE PROCEDURE RegisterAdmin
@Token VARCHAR(25),
@First_Name VARCHAR(50), 
@Last_Name VARCHAR(50),
@Email VARCHAR(320),
@Password VARCHAR(50),
@Age INT,
@Gender BIT,
@Address TEXT,
@Phone_Number VARCHAR(11),
@ResponseMessage BIT OUTPUT
AS
BEGIN
	BEGIN TRANSACTION
		IF EXISTS (SELECT Customer_ID FROM Sessions WHERE Token = @Token AND CURRENT_TIMESTAMP <= ExpiryTime)
			BEGIN
				DECLARE @Customer_ID AS INT = (SELECT Customer_ID FROM Sessions WHERE Token = @Token AND CURRENT_TIMESTAMP <= ExpiryTime);
				IF EXISTS(SELECT * FROM Customer WHERE Customer_ID = @Customer_ID AND Admin = 1)
					BEGIN
						IF EXISTS (SELECT * FROM Customer WHERE Email_Address = @Email)
							BEGIN
							--an account with this email already exists
								SELECT @ResponseMessage = 208;
							END
						ELSE
							BEGIN
								INSERT INTO Customer
								(First_Name, Last_Name, Email_Address, Password, Age, Gender, Address, Phone_Number, Admin)
								VALUES
								(@First_Name, @Last_Name, @Email, @Password, @Age, @Gender, @Address, @Phone_Number, 1);
								SELECT @ResponseMessage = 200;
							END
					END
				ELSE
					BEGIN
						--not admin
						SELECT ResponseMessage = 401;
					END
			END
		ELSE
			BEGIN
				--not logged in
				SELECT @ResponseMessage = 400;
			END
		IF @@ERROR != 0
			BEGIN
				SELECT @ResponseMessage = 500;
				ROLLBACK TRANSACTION
			END
		ELSE
			COMMIT TRANSACTION

END
GO

-- how to run
DECLARE @Out as BIT
exec RegisterAdmin @Token = 'FD-48BA-8080-EE76E5F9FAEC', @First_Name = 'bob', @Last_Name = 'bobby', @Email = 'email3@admin.com', @Password = 'password', @Age = '5', @Gender = 1, @Address = 'address goes here', @Phone_Number = '07932153300', @success = @Out OUTPUT;
SELECT @OUT AS 'Outputmessage';
--------


--Login Admin
CREATE PROCEDURE ValidateAdmin
@Email VARCHAR(320),
@Password VARCHAR(50),
@Token VARCHAR(25) OUTPUT
AS
BEGIN
	BEGIN TRANSACTION
		IF EXISTS (SELECT * FROM Customer WHERE Email_Address = @Email AND Password = @Password AND Admin = 1)
			BEGIN
				DECLARE @CustomerID AS INT = (SELECT Customer_ID FROM Customer WHERE Email_Address = @Email AND Password = @Password AND Admin = 1);
				
				DECLARE @TheToken AS VARCHAR(50) = CONVERT(VARCHAR(30), RIGHT(NEWID(), 25));
				
				DECLARE @ExpiryTime AS DATETIME = (SELECT DATEADD(hour, 1, CURRENT_TIMESTAMP) AS DateAdd);

				Insert INTO Sessions (Customer_ID, Token, ExpiryTime) VALUES (@CustomerID, @TheToken, @ExpiryTime);
				
				SELECT @Token = @TheToken;
			
			END
		ELSE
			BEGIN
				SELECT @Token = 208;
			END
		
		IF @@ERROR != 0
			BEGIN
				SELECT @Token = 500;
				ROLLBACK TRANSACTION
			END
		ELSE
			COMMIT TRANSACTION
END
Go

-- how to run
DECLARE @Out as VARCHAR(25); 
EXEC ValidateAdmin @Email = 'Email@Admin.com', @Password = 'password', @ResponseMessage = @Out OUTPUT; 
SELECT @Out AS 'OutputMessage'; 
--------


--Admin Delete Customer
CREATE PROCEDURE AdminDeleteCustomer
@Token VARCHAR(25),
@Customer_Deleting_ID INT,
@Success BIT OUTPUT
AS
BEGIN
	BEGIN TRANSACTION
		IF EXISTS (SELECT Customer_ID FROM Sessions WHERE Token = @Token AND CURRENT_TIMESTAMP <= ExpiryTime)
			BEGIN
				Declare @Customer_ID AS INT = (SELECT Customer_ID FROM Sessions WHERE Token = @Token);
				IF EXISTS(SELECT * FROM Customer WHERE Customer_ID = @Customer_ID AND Admin = 1)
					BEGIN
						Delete Customer WHERE Customer_ID = @Customer_Deleting_ID AND Admin = 0;
						SELECT @Success = 1;
					END
				ELSE
					BEGIN
						SELECT @Success = 0;
					END
			END
		ELSE
			BEGIN
				SELECT @Success = 0;
			END
	IF @@ERROR != 0
		BEGIN
			SELECT @Success = 0;
			ROLLBACK TRANSACTION
		END
	ELSE
		COMMIT TRANSACTION
END
GO

-- how to run
DECLARE @Out as BIT; 
EXEC AdminDeleteCustomer @Token = 'FD-48BA-8080-EE76E5F9FAEC', @Customer_Deleting_ID = 2, @Success = @Out OUTPUT; 
SELECT @Out AS 'OutputMessage'; 
--------


------Product and order ---------
--add order start here

CREATE PROCEDURE AddOrder
@Token VARCHAR(25),
@Quantity INT,
@Product_ID INT,
@Success BIT OUTPUT
AS
BEGIN
	BEGIN TRANSACTION
		IF EXISTS (SELECT * FROM Sessions WHERE Token = @Token AND CURRENT_TIMESTAMP <= ExpiryTime)
			BEGIN
				DECLARE @Customer_ID AS INT = (SELECT Customer_ID FROM Sessions WHERE Token = @Token);
				
				INSERT INTO Orders
				(Time_Ordered, Quantity, Product_ID, Customer_ID)
				VALUES
				(CURRENT_TIMESTAMP, @Quantity, @Product_ID, @Customer_ID);
				
				UPDATE Products
				SET 
				Stock = (Stock - @Quantity), Total_Sold = (Total_Sold + @Quantity)
				WHERE
				Product_ID = @Product_ID;
				
				SELECT @Success = 1;
			END
		ELSE
			BEGIN
				SELECT @Success = 0;
			END
			
	IF @@ERROR != 0
		BEGIN
			SELECT @Success = 0;
			ROLLBACK TRANSACTION
		END
	ELSE
		COMMIT TRANSACTION
END
GO

-- how to run
DECLARE @Out as BIT; 
EXEC AddOrder @Token = 'FD-48BA-8080-EE76E5F9FAEC', @Quantity = 3, @Product_ID = 1, @Success = @Out OUTPUT; 
SELECT @Out AS 'OutputMessage'; 
--------

--cancel order --------

CREATE PROCEDURE CancelOrder
@Token VARCHAR(25),
@Order_ID INT,
@Success BIT OUTPUT
AS
BEGIN
	BEGIN TRANSACTION
		IF EXISTS(SELECT * FROM Sessions WHERE Token = @Token AND CURRENT_TIMESTAMP <= ExpiryTime)
			BEGIN
				DECLARE @Customer_ID AS INT = (SELECT Customer_ID FROM Sessions WHERE Token = @Token)
				
				IF EXISTS(SELECT * FROM Orders WHERE Order_ID = @Order_ID AND Customer_ID = @Customer_ID)
					BEGIN
						DECLARE @Quantity AS INT = (SELECT Quantity FROM Orders WHERE Order_ID = @Order_ID)
						
						DECLARE @Product_ID AS INT = (SELECT Product_ID FROM Orders WHERE Order_ID = @Order_ID)
						
						DELETE FROM Orders WHERE Order_ID = @Order_ID AND Customer_ID = @Customer_ID
						
						UPDATE Products
						SET 
						Stock = (Stock + @Quantity), Total_Sold = (Total_Sold - @Quantity)
						WHERE
						Product_ID = @Product_ID;
						
						SELECT @Success = 1;
					END
				ELSE
					BEGIN
					--order does not exist
						SELECT @Success = 0;
					END
			END
		ELSE
			BEGIN
			--user not logged in
				SELECT @Success = 0;
			END

	IF @@ERROR != 0
		BEGIN
			SELECT @Success = 0;
			ROLLBACK TRANSACTION
		END
	ELSE
		COMMIT TRANSACTION
END
GO

-- how to run
DECLARE @Out as BIT; 
EXEC CancelOrder @Token = 'D1-46D3-953F-D28AD246A235', @Order_ID = 1, @Success = @Out OUTPUT; 
SELECT @Out AS 'OutputMessage'; 
--------


--cancel order as admin --------

CREATE PROCEDURE CancelOrderAdmin
@Token VARCHAR(25),
@Order_ID INT,
@Customer_ID
@Success BIT OUTPUT
AS
BEGIN
	BEGIN TRANSACTION
		IF EXISTS(SELECT * FROM Sessions WHERE Token = @Token AND CURRENT_TIMESTAMP <= ExpiryTime)
			BEGIN
				DECLARE @Admin_ID AS INT = (SELECT Customer_ID FROM Sessions WHERE Token = @Token)
				
				IF EXISTS(SELECT * FROM Customer WHERE @Admin_ID = Customer_ID AND Admin=1)
				
				BEGIN
				
				IF EXISTS(SELECT * FROM Orders WHERE Order_ID = @Order_ID AND Customer_ID = @Customer_ID)
					BEGIN
						DECLARE @Quantity AS INT = (SELECT Quantity FROM Orders WHERE Order_ID = @Order_ID)
						
						DECLARE @Product_ID AS INT = (SELECT Product_ID FROM Orders WHERE Order_ID = @Order_ID)
						
						DELETE FROM Orders WHERE Order_ID = @Order_ID AND Customer_ID = @Customer_ID
						
						UPDATE Products
						SET 
						Stock = (Stock + @Quantity), Total_Sold = (Total_Sold - @Quantity)
						WHERE
						Product_ID = @Product_ID;
						
						SELECT @Success = 1;
					END
				ELSE
					BEGIN
					--order does not exist
						SELECT @Success = 0;
					END
				END
				
				ELSE 
					BEGIN
					
					SELECT @Success = 0;
					
					END
			END
		ELSE
			BEGIN
			--user not logged in
				SELECT @Success = 0;
			END

	IF @@ERROR != 0
		BEGIN
			SELECT @Success = 0;
			ROLLBACK TRANSACTION
		END
	ELSE
		COMMIT TRANSACTION
END
GO


--add product

CREATE PROCEDURE AddProduct
@Token VARCHAR(25),
@Product_Name VARCHAR(50),
@Image_Url TEXT,
@Stock INT,
@Catagory VARCHAR(50),
@Price MONEY,
@Description TEXT,
@Success BIT OUTPUT
AS
BEGIN
	BEGIN TRANSACTION
		IF EXISTS(SELECT * FROM Sessions WHERE Token = @Token AND CURRENT_TIMESTAMP <= ExpiryTime)
			BEGIN
				DECLARE @Customer_ID AS INT = (SELECT Customer_ID FROM Sessions WHERE Token = @Token);
				
				IF EXISTS(SELECT * FROM Customer WHERE Customer_ID = @Customer_ID AND Admin = 1)
					BEGIN
						IF EXISTS(SELECT * FROM Products WHERE Product_Name = @Product_Name)
							BEGIN
								--product name already exists
								SELECT Success = 0;
							END
						ELSE
							BEGIN
								INSERT INTO Products
								(Product_Name, Image_Url, Stock, Catagory, Price, Description)
								VALUES
								(@Product_Name, @Image_Url, @Stock, @Catagory, @Price, @Description)
								
								SELECT @Success = 1;
							END
					END
				ELSE
					BEGIN
						--USER NOT ADMIN
						SELECT @Success = 0;
					END
				
			END
		ELSE
			BEGIN
				--Not Logged in
				SELECT @Success = 0;
			END
	IF @@ERROR != 0
		BEGIN
			SELECT @Success = 0;
			ROLLBACK TRANSACTION
		END
	ELSE
		COMMIT TRANSACTION
END
GO

-- how to run
DECLARE @Out as BIT; 
EXEC AddProduct @Token = 'FD-48BA-8080-EE76E5F9FAEC', @Product_Name = 'P Name', @Image_Url = 'url goes here', @Stock = 10, @Catagory='TV', @Price='10.11', @Description = "description", @Success = @Out OUTPUT; 
SELECT @Out AS 'OutputMessage'; 
--------

--remove product

CREATE PROCEDURE DeleteProduct
@Token VARCHAR(25),
@Product_ID INT,
@Success BIT OUTPUT
AS
BEGIN
	BEGIN TRANSACTION
		IF EXISTS (SELECT * FROM Sessions WHERE Token = @Token AND CURRENT_TIMESTAMP <= ExpiryTime)
			BEGIN
				DECLARE @ID AS INT = (SELECT Customer_ID FROM Sessions WHERE Token = @Token);
				
				IF EXISTS (SELECT * FROM Customer WHERE Customer_ID = @ID AND Admin = 1)
					BEGIN
						IF EXISTS(SELECT * FROM Products WHERE Product_ID = @Product_ID)
							BEGIN					
								DELETE FROM Products WHERE Product_ID = @Product_ID
								
								SELECT @Success = 1;
							END
						ELSE
							BEGIN
							--product does not exist
								SELECT @Success = 0;
							END
					END
				ELSE
					BEGIN
					--user not admin
						SELECT @Success = 0;
					END
			END
		ELSE
			BEGIN
			--user not logged in
				SELECT @Success = 0;
			END

	IF @@ERROR != 0
		BEGIN
			SELECT @Success = 0;
			ROLLBACK TRANSACTION
		END
	ELSE
		COMMIT TRANSACTION
END
GO

-- how to run
DECLARE @Out as BIT; 
EXEC DeleteProduct @Token = 'FD-48BA-8080-EE76E5F9FAEC', @Product_ID = 1, @Success = @Out OUTPUT; 
SELECT @Out AS 'OutputMessage'; 
--------


--edit product

CREATE PROCEDURE EditProduct
@Token VARCHAR(25),
@Product_ID INT,
@Product_Name VARCHAR(50),
@Image_Url TEXT,
@Stock INT,
@Catagory VARCHAR(50),
@Total_Sold INT,
@Price MONEY,
@Description TEXT,
@Success BIT OUTPUT
AS
BEGIN
	BEGIN TRANSACTION
		IF EXISTS (SELECT * FROM Sessions WHERE Token = @Token AND CURRENT_TIMESTAMP <= ExpiryTime)
			BEGIN
				DECLARE @ID AS INT = (SELECT Customer_ID FROM Sessions WHERE Token = @Token);
				
				IF EXISTS (SELECT * FROM Customer WHERE Customer_ID = @ID AND Admin = 1)
					BEGIN
						IF EXISTS(SELECT * FROM Products WHERE Product_ID = @Product_ID)
							BEGIN													
								UPDATE Products
								SET Product_Name = @Product_Name, Image_Url = Image_Url, Stock = @Stock, Catagory = @Catagory, Total_Sold = @Total_Sold, Price = @Price, Description = @Description
								WHERE Product_ID = @Product_ID;
								
								SELECT @Success = 1;
							END
						ELSE
							BEGIN
							--product does not exist
								SELECT @Success = 0;
							END
					END
				ELSE
					BEGIN
					--user not admin
						SELECT @Success = 0;
					END
			END
		ELSE
			BEGIN
			--user not logged in
				SELECT @Success = 0;
			END

	IF @@ERROR != 0
		BEGIN
			SELECT @Success = 0;
			ROLLBACK TRANSACTION
		END
	ELSE
		COMMIT TRANSACTION
END
GO

-- how to run
DECLARE @Out as BIT; 
EXEC EditProduct @Token = 'FD-48BA-8080-EE76E5F9FAEC', @Product_ID = 1, @Product_Name = 'Changed the name', @Image_Url = 'The url has changed', @Stock=100, @Catagory='Phone', @Total_Sold=11, @Price= 12.00, @Description = "description", @Success = @Out OUTPUT; 
SELECT @Out AS 'OutputMessage';
--------

---Reviews

--Write review

CREATE PROCEDURE WriteReview
@Token VARCHAR(25),
@Rating INT,
@Product_ID INT,
@Title VARCHAR(50),
@Description TEXT,
@Success BIT OUTPUT
AS
BEGIN
	BEGIN TRANSACTION
		IF EXISTS (SELECT * FROM Sessions WHERE Token = @Token AND CURRENT_TIMESTAMP <= ExpiryTime)
			BEGIN
				IF Exists(SELECT * FROM Products WHERE Product_ID = @Product_ID)
					BEGIN
						DECLARE @Customer_ID AS INT = (SELECT Customer_ID FROM Sessions WHERE Token = @Token);
						
						INSERT INTO Reviews
						(Product_ID, Customer_ID, Rating, Title, Description)
						VALUES
						(@Product_ID, @Customer_ID, @Rating, @Title, @Description);
						
						SELECT @Success = 1;
					END
				ELSE
					BEGIN
					--Product does not exist
						SELECT @Success = 0;
					END
			END
		ELSE
			BEGIN
			--user not logged in
				SELECT @Success = 0;
			END
			
	IF @@ERROR != 0
		BEGIN
			SELECT @Success = 0;
			ROLLBACK TRANSACTION
		END
	ELSE
		COMMIT TRANSACTION
END
GO


CREATE PROCEDURE GetCustomerDetails
@Token VARCHAR(25)
AS
BEGIN
	IF EXISTS(SELECT * FROM Sessions WHERE Token = @Token AND CURRENT_TIMESTAMP <= ExpiryTime)
		BEGIN
			DECLARE @Customer_ID AS INT = (SELECT Customer_ID FROM Sessions WHERE Token = @Token);
			SELECT * FROM Customer WHERE Customer_ID = @Customer_ID;
		END
END
GO

-- how to run
DECLARE @Out as BIT; 
EXEC WriteReview @Token = '00-4B68-A7CA-EA85320CB2ED', @Rating = 3, @Product_ID = 2, @Description = 'this is the description', @Success = @Out OUTPUT; 
SELECT @Out AS 'OutputMessage';
--------


--- Updating Average Rating every time a new review is made.
CREATE TRIGGER dbo.Update_Product_Rating
ON dbo.Reviews
AFTER INSERT, DELETE
AS
BEGIN
	DECLARE @Product_ID INT;
	SET @Product_ID = ( SELECT Product_ID from inserted)
	EXEC dbo.Calculate_Insert_Average
END;

CREATE PROCEDURE Calculate_Insert_Average (@Product_ID int, @Current_Rating int)
AS
BEGIN
	Set @Current_Rating = (SELECT AVG(Rating) 
	FROM dbo.Reviews
	Where Product_ID = @Product_ID)
	UPDATE dbo.Products
	SET AVG_Rating = @Current_Rating
	WHERE Product_ID = @Product_ID
END
Go

---------























