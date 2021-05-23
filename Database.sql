/*Create users table*/
CREATE TABLE Customer(
CustomerID int IDENTITY(1,1) PRIMARY KEY,
FirstName VARCHAR(50) NOT NULL,
LastName VARCHAR(50) NOT NULL,
EmailAddress VARCHAR(320) NOT NULL,
Password VARCHAR(50) NOT NULL CHECK (DATALENGTH(Password) > 5),
Age INT,
Gender bit,
Address TEXT,
PhoneNumber VARCHAR(11),
Admin BIT DEFAULT 0 NOT NULL
);


/*Create sessions table*/
CREATE TABLE Sessions(
Session_ID int IDENTITY(1,1) PRIMARY KEY,
CustomerID int NOT NULL,
Token VARCHAR(50) NOT NULL,
ExpiryTime DATETIME NOT NULL,
FOREIGN KEY (CustomerID) REFERENCES Customer(CustomerID)
ON DELETE CASCADE
);

CREATE TABLE Orders(
OrderID INT IDENTITY(1,1) PRIMARY KEY,
TimeOrdered DATETIME NOT NULL,
Quantity INT NOT NULL,
ProductID INT NOT NULL,
CustomerID INT NOT NULL,
FOREIGN KEY (CustomerID) REFERENCES Customer(CustomerID)
ON DELETE CASCADE,
FOREIGN KEY (ProductID) REFERENCES Products(ProductID)
);


CREATE TABLE Products(
ProductID INT IDENTITY(1,1) PRIMARY KEY,
ProductName VARCHAR(50) NOT NULL,
ImageUrl TEXT,
Stock INT NOT NULL,
Category VARCHAR(50) NOT NULL,
TotalSold INT DEFAULT 0 NOT NULL,
Price MONEY NOT NULL,
Description TEXT,
AvgRating FLOAT
);

CREATE TABLE Reviews(
ProductID INT NOT NULL,
CustomerID INT NOT NULL,
Rating TINYINT NOT NULL CHECK(0 < Rating AND Rating <= 5),
Title VARCHAR(50),
Description TEXT,
PRIMARY KEY (ProductID, CustomerID),
FOREIGN KEY (CustomerID) REFERENCES Customer(CustomerID),
FOREIGN KEY (ProductID) REFERENCES Products(ProductID)
ON DELETE CASCADE
);
--Views-------------------------------------------------------------------------------------------------------

CREATE VIEW [dbo].[TopRated] AS 
SELECT Ratings.Rating , Products.ProductName, Ratings.ProductID
FROM Ratings
INNER JOIN Products ON Products.ProductID = Ratings.ProductID;
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
		IF EXISTS (SELECT * FROM Customer WHERE EmailAddress = @Email AND Password = @Password)
			BEGIN
				DECLARE @CustomerID AS INT = (SELECT CustomerID FROM Customer WHERE EmailAddress = @Email AND Password = @Password);
				
				DECLARE @TheToken AS VARCHAR(50) = CONVERT(VARCHAR(30), RIGHT(NEWID(), 25));
				
				DECLARE @ExpiryTime AS DATETIME = (SELECT DATEADD(hour, 1, CURRENT_TIMESTAMP) AS DateAdd);

				Insert INTO Sessions (CustomerID, Token, ExpiryTime) VALUES (@CustomerID, @TheToken, @ExpiryTime);
				
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
	BEGIN TRANSACTION
		IF EXISTS (SELECT * FROM Customer WHERE EmailAddress = @Email)
			BEGIN
			--an account with this email already exists
				SELECT @ResponseMessage = 208;
			END
		ELSE
			BEGIN
				INSERT INTO Customer
				(FirstName, LastName, EmailAddress, Password, Age, Gender, Address, PhoneNumber)
				VALUES
				(@FirstName, @LastName, @Email, @Password, @Age, @Gender, @Address, @PhoneNumber);
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
DECLARE @Out as INT
exec RegisterCustomer @FirstName = 'bob', @LastName = 'bobby', @Email = 'email6', @Password = 'password', @Age = '5', @Gender = 1, @Address = 'address goes here', @PhoneNumber = '07932153300', @ResponseMessage = @Out OUTPUT;
SELECT @OUT AS 'Outputmessage';
--------



--Edit user

CREATE PROCEDURE EditCustomer
@Token VARCHAR(25),
@FirstName VARCHAR(50),
@LastName VARCHAR(50),
@Email VARCHAR(320),
@Age INT,
@Gender BIT,
@Address TEXT,
@PhoneNumber VARCHAR(11),
@ResponseMessage INT OUTPUT
AS
BEGIN
	IF EXISTS (SELECT CustomerID FROM Sessions WHERE Token = @Token AND CURRENT_TIMESTAMP <= ExpiryTime)
		BEGIN
			Declare @CustomerID AS INT = (SELECT CustomerID FROM Sessions WHERE Token = @Token);
			IF EXISTS(SELECT * FROM Customer WHERE (CustomerID != @CustomerID AND EmailAddress = @Email))
				BEGIN
				--an account with this email already exists
					SELECT @ResponseMessage = 208;
				END
			ELSE
				BEGIN
					UPDATE Customer SET FirstName = @FirstName, LastName = @LastName, EmailAddress = @Email, Age = @Age, Gender = @Gender, Address = @Address, PhoneNumber = @PhoneNumber WHERE CustomerID = @CustomerID AND Admin = 0;
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
DECLARE @Out as INT; 
EXEC EditCustomer @Token = '8E-433D-BCB4-A596E369001C', @FirstName = 'This has', @LastName = 'been changed', @Email = 'email501', @Password = '^?H??(\u0004qQ??o??)s`=\rj???*\u0011?r\u001d\u0015B?', @Age = '34', @Gender = 1, @Address = 'address string 5', @PhoneNumber = '01454234', @ResponseMessage = @Out OUTPUT; 
SELECT @Out AS 'OutputMessage'; 
--------

--Edit admin

CREATE PROCEDURE EditAdmin
@Token VARCHAR(25),
@FirstName VARCHAR(50),
@LastName VARCHAR(50),
@Email VARCHAR(320),
@Age INT,
@Gender BIT,
@Address TEXT,
@PhoneNumber VARCHAR(11),
@ResponseMessage INT OUTPUT
AS
BEGIN
	IF EXISTS (SELECT CustomerID FROM Sessions WHERE Token = @Token AND CURRENT_TIMESTAMP <= ExpiryTime)
		BEGIN
			Declare @CustomerID AS INT = (SELECT CustomerID FROM Sessions WHERE Token = @Token);
			IF EXISTS (SELECT * FROM Customer WHERE CustomerID = @CustomerID AND Admin = 1)
				BEGIN
					IF EXISTS(SELECT * FROM Customer WHERE (CustomerID != @CustomerID AND EmailAddress = @Email))
						BEGIN
						--an account with this email already exists
							SELECT @ResponseMessage = 208;
						END
					ELSE
						BEGIN
							UPDATE Customer SET FirstName = @FirstName, LastName = @LastName, EmailAddress = @Email, Age = @Age, Gender = @Gender, Address = @Address, PhoneNumber = @PhoneNumber WHERE CustomerID = @CustomerID;
							SELECT @ResponseMessage = 200;
						END
				END		
			ELSE
				BEGIN
					-- user not admin
					SELECT @ResponseMessage = 401
				END	
		END
	ELSE
		BEGIN
		--customer not logged in
			SELECT @ResponseMessage = 400;
		END
END
GO

-- change admin password
CREATE PROCEDURE ChangeAdminPassword
@Token VARCHAR(25),
@NewPassword VARCHAR(50),
@ResponseMessage INT OUTPUT
AS
BEGIN
	BEGIN TRANSACTION
		IF EXISTS(SELECT CustomerID FROM Sessions WHERE Token = @Token AND CURRENT_TIMESTAMP <= ExpiryTime)
			BEGIN
				Declare @CustomerID AS INT = (SELECT CustomerID FROM Sessions WHERE Token = @Token);
				IF EXISTS(SELECT * FROM Customer WHERE CustomerID = @CustomerID AND Admin = 1)
					BEGIN
						UPDATE Customer SET Password = @NewPassword WHERE CustomerID = @CustomerID AND Admin = 1;

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



--Edit user as admin

CREATE PROCEDURE AdminEditCustomer
@Token VARCHAR(25),
@CustomerID INT,
@FirstName VARCHAR(50),
@LastName VARCHAR(50),
@Email VARCHAR(320),
@Age INT,
@Gender BIT,
@Address TEXT,
@PhoneNumber VARCHAR(11),
@ResponseMessage INT OUTPUT
AS
BEGIN
	IF EXISTS (SELECT CustomerID FROM Sessions WHERE Token = @Token AND CURRENT_TIMESTAMP <= ExpiryTime)
		BEGIN
			Declare @AdminID AS INT = (SELECT CustomerID FROM Sessions WHERE Token = @Token);
			
			IF EXISTS(SELECT * FROM Customer WHERE CustomerID = @AdminID AND Admin = 1)
				BEGIN
					IF EXISTS(SELECT * FROM Customer WHERE (CustomerID != @CustomerID AND EmailAddress = @Email))
						BEGIN
						--an account with this email already exists
							SELECT @ResponseMessage = 208;
						END
					ELSE
						BEGIN
							UPDATE Customer SET FirstName = @FirstName, LastName = @LastName, EmailAddress = @Email, Age = @Age, Gender = @Gender, Address = @Address, PhoneNumber = @PhoneNumber WHERE CustomerID = @CustomerID AND Admin = 0;
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


-- how to run
DECLARE @Out as INT; 
EXEC AdminEditCustomer @Token = '8E-433D-BCB4-A596E369001C', @CustomerID = 1, @FirstName = 'This has', @LastName = 'been changed', @Email = 'email501', @Password = '^?H??(\u0004qQ??o??)s`=\rj???*\u0011?r\u001d\u0015B?', @Age = '34', @Gender = 1, @Address = 'address string 5', @PhoneNumber = '01454234', @ResponseMessage = @Out OUTPUT; 
SELECT @Out AS 'OutputMessage'; 
--------

--Change password

CREATE PROCEDURE ChangePassword
@Token VARCHAR(25),
@NewPassword VARCHAR(50),
@ResponseMessage INT OUTPUT
AS
BEGIN
	BEGIN TRANSACTION
		IF EXISTS(SELECT CustomerID FROM Sessions WHERE Token = @Token AND CURRENT_TIMESTAMP <= ExpiryTime)
			BEGIN
				Declare @CustomerID AS INT = (SELECT CustomerID FROM Sessions WHERE Token = @Token);
				IF EXISTS(SELECT * FROM Customer WHERE CustomerID = @CustomerID AND Admin = 0)
					BEGIN
						UPDATE Customer SET Password = @NewPassword WHERE CustomerID = @CustomerID AND Admin = 0;

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
DECLARE @Out as INT; 
EXEC ChangePassword @Token = 'FD-48BA-8080-EE76E5F9FAEC', @NewPassword = 'password', @ResponseMessage = @Out OUTPUT; 
SELECT @Out AS 'OutputMessage';
--------


--Delete User
CREATE PROCEDURE DeleteCustomer
@Token VARCHAR(25),
@ResponseMessage INT OUTPUT
AS
BEGIN
	IF EXISTS (SELECT CustomerID FROM Sessions WHERE Token = @Token AND CURRENT_TIMESTAMP <= ExpiryTime)
		BEGIN
			Declare @CustomerID AS INT = (SELECT CustomerID FROM Sessions WHERE Token = @Token);
			IF EXISTS(SELECT * FROM Customer WHERE CustomerID = @CustomerID AND Admin = 0)
				BEGIN
					Delete customer WHERE CustomerID = @CustomerID AND Admin = 0;
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
			SELECT @ResponseMessage = 402 ;
		END
END
GO

-- how to run
DECLARE @Out as INT; 
EXEC DeleteCustomer @Token = 'FD-48BA-8080-EE76E5F9FAEC', @ResponseMessage = @Out OUTPUT; 
SELECT @Out AS 'OutputMessage'; 
--------



------Admin ---------

--Register Admin
CREATE PROCEDURE RegisterAdmin
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
	BEGIN TRANSACTION
		IF EXISTS (SELECT CustomerID FROM Sessions WHERE Token = @Token AND CURRENT_TIMESTAMP <= ExpiryTime)
			BEGIN
				DECLARE @CustomerID AS INT = (SELECT CustomerID FROM Sessions WHERE Token = @Token AND CURRENT_TIMESTAMP <= ExpiryTime);
				IF EXISTS(SELECT * FROM Customer WHERE CustomerID = @CustomerID AND Admin = 1)
					BEGIN
						IF EXISTS (SELECT * FROM Customer WHERE EmailAddress = @Email)
							BEGIN
							--an account with this email already exists
								SELECT @ResponseMessage = 208;
							END
						ELSE
							BEGIN
								INSERT INTO Customer
								(FirstName, LastName, EmailAddress, Password, Age, Gender, Address, PhoneNumber, Admin)
								VALUES
								(@FirstName, @LastName, @Email, @Password, @Age, @Gender, @Address, @PhoneNumber, 1);
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
DECLARE @Out as INT
exec RegisterAdmin @Token = 'FD-48BA-8080-EE76E5F9FAEC', @FirstName = 'bob', @LastName = 'bobby', @Email = 'email3@admin.com', @Password = 'password', @Age = '5', @Gender = 1, @Address = 'address goes here', @PhoneNumber = '07932153300', @success = @Out OUTPUT;
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
		IF EXISTS (SELECT * FROM Customer WHERE EmailAddress = @Email AND Password = @Password AND Admin = 1)
			BEGIN
				DECLARE @CustomerID AS INT = (SELECT CustomerID FROM Customer WHERE EmailAddress = @Email AND Password = @Password AND Admin = 1);
				
				DECLARE @TheToken AS VARCHAR(50) = CONVERT(VARCHAR(30), RIGHT(NEWID(), 25));
				
				DECLARE @ExpiryTime AS DATETIME = (SELECT DATEADD(hour, 1, CURRENT_TIMESTAMP) AS DateAdd);

				Insert INTO Sessions (CustomerID, Token, ExpiryTime) VALUES (@CustomerID, @TheToken, @ExpiryTime);
				
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
EXEC ValidateAdmin @Email = 'Email@Admin.com', @Password = 'password', @Token = @Out OUTPUT; 
SELECT @Out AS 'OutputMessage'; 
--------


--Delete admin
CREATE PROCEDURE DeleteAdmin
@Token VARCHAR(25),
@ResponseMessage INT OUTPUT
AS
BEGIN
	IF EXISTS (SELECT CustomerID FROM Sessions WHERE Token = @Token AND CURRENT_TIMESTAMP <= ExpiryTime)
		BEGIN
			Declare @CustomerID AS INT = (SELECT CustomerID FROM Sessions WHERE Token = @Token);
			IF EXISTS(SELECT * FROM Customer WHERE CustomerID = @CustomerID AND Admin = 1)
				BEGIN
					Delete customer WHERE CustomerID = @CustomerID AND Admin = 1;
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
			SELECT @ResponseMessage = 402 ;
		END
END
GO






--Admin Delete Customer
CREATE PROCEDURE AdminDeleteCustomer
@Token VARCHAR(25),
@CustomerDeletingID INT,
@ResponseMessage INT OUTPUT
AS
BEGIN
	BEGIN TRANSACTION
		IF EXISTS (SELECT CustomerID FROM Sessions WHERE Token = @Token AND CURRENT_TIMESTAMP <= ExpiryTime)
			BEGIN
				Declare @CustomerID AS INT = (SELECT CustomerID FROM Sessions WHERE Token = @Token);
				IF EXISTS(SELECT * FROM Customer WHERE CustomerID = @CustomerID AND Admin = 1)
					BEGIN
						Delete Customer WHERE CustomerID = @CustomerDeletingID AND Admin = 0;
						SELECT @ResponseMessage = 200;
					END
				ELSE
					BEGIN
						SELECT @ResponseMessage = 401;
					END
			END
		ELSE
			BEGIN
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
DECLARE @Out as INT; 
EXEC AdminDeleteCustomer @Token = 'FD-48BA-8080-EE76E5F9FAEC', @CustomerDeletingID = 2, @ResponseMessage = @Out OUTPUT; 
SELECT @Out AS 'OutputMessage'; 
--------


------Product and order ---------


CREATE PROCEDURE GetOrders
@Token VARCHAR(25),
@ResponseMessage INT OUTPUT
AS
BEGIN
	BEGIN TRANSACTION
		IF EXISTS (SELECT * FROM Sessions WHERE Token = @Token AND CURRENT_TIMESTAMP <= ExpiryTime)
			BEGIN
				DECLARE @CustomerID AS INT = (SELECT CustomerID FROM Sessions WHERE Token = @Token);
				
				SELECT * FROM Orders WHERE CustomerID = @CustomerID
				
				SELECT @ResponseMessage = 200;
			END
		ELSE
			BEGIN
				SELECT @ResponseMessage = 401;
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




--add order

CREATE PROCEDURE AddOrder
@Token VARCHAR(25),
@Quantity INT,
@ProductID INT,
@ResponseMessage INT OUTPUT
AS
BEGIN
	BEGIN TRANSACTION
		IF EXISTS (SELECT * FROM Sessions WHERE Token = @Token AND CURRENT_TIMESTAMP <= ExpiryTime)
			BEGIN
				DECLARE @CustomerID AS INT = (SELECT CustomerID FROM Sessions WHERE Token = @Token);
				
				INSERT INTO Orders
				(TimeOrdered, Quantity, ProductID, CustomerID)
				VALUES
				(CURRENT_TIMESTAMP, @Quantity, @ProductID, @CustomerID);
				
				UPDATE Products
				SET 
				Stock = (Stock - @Quantity), TotalSold = (TotalSold + @Quantity)
				WHERE
				ProductID = @ProductID;
				
				SELECT @ResponseMessage = 200;
			END
		ELSE
			BEGIN
				SELECT @ResponseMessage = 401;
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
DECLARE @Out as INT; 
EXEC AddOrder @Token = 'FD-48BA-8080-EE76E5F9FAEC', @Quantity = 3, @ProductID = 1, @ResponseMessage = @Out OUTPUT; 
SELECT @Out AS 'OutputMessage'; 
--------

--cancel order --------

CREATE PROCEDURE CancelOrder
@Token VARCHAR(25),
@OrderID INT,
@ResponseMessage INT OUTPUT
AS
BEGIN
	BEGIN TRANSACTION
		IF EXISTS(SELECT * FROM Sessions WHERE Token = @Token AND CURRENT_TIMESTAMP <= ExpiryTime)
			BEGIN
				DECLARE @CustomerID AS INT = (SELECT CustomerID FROM Sessions WHERE Token = @Token)
				
				IF EXISTS(SELECT * FROM Orders WHERE OrderID = @OrderID AND CustomerID = @CustomerID)
					BEGIN
						DECLARE @Quantity AS INT = (SELECT Quantity FROM Orders WHERE OrderID = @OrderID)
						
						DECLARE @ProductID AS INT = (SELECT ProductID FROM Orders WHERE OrderID = @OrderID)

                        IF @Quantity = 1
                            BEGIN
                                DELETE FROM Orders WHERE OrderID = @OrderID AND CustomerID = @CustomerID

                                UPDATE Products
                                SET 
                                Stock = (Stock + 1), TotalSold = (TotalSold - 1)
                                WHERE
                                ProductID = @ProductID;
                            END
                        ELSE
                            BEGIN
                                UPDATE Orders
                                SET
                                Quantity = @Quantity - 1
                                WHERE
                                OrderID = @OrderID

                                UPDATE Products
                                SET 
                                Stock = (Stock + 1), TotalSold = (TotalSold - 1)
                                WHERE
                                ProductID = @ProductID;
                            END
						
						SELECT @ResponseMessage = 200;
					END
				ELSE
					BEGIN
					--order does not exist
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
DECLARE @Out as INT; 
EXEC CancelOrder @Token = 'D1-46D3-953F-D28AD246A235', @OrderID = 1, @ResponseMessage = @Out OUTPUT; 
SELECT @Out AS 'OutputMessage'; 
--------


--cancel order as admin --------

CREATE PROCEDURE CancelOrderAdmin
@Token VARCHAR(25),
@OrderID INT,
@CustomerID INT,
@ResponseMessage INT OUTPUT
AS
BEGIN
	BEGIN TRANSACTION
		IF EXISTS(SELECT * FROM Sessions WHERE Token = @Token AND CURRENT_TIMESTAMP <= ExpiryTime)
			BEGIN
				DECLARE @AdminID AS INT = (SELECT CustomerID FROM Sessions WHERE Token = @Token)
				
				IF EXISTS(SELECT * FROM Customer WHERE @AdminID = CustomerID AND Admin=1)
				
				BEGIN
				
				IF EXISTS(SELECT * FROM Orders WHERE OrderID = @OrderID AND CustomerID = @CustomerID)
					BEGIN
						DECLARE @Quantity AS INT = (SELECT Quantity FROM Orders WHERE OrderID = @OrderID)
						
						DECLARE @ProductID AS INT = (SELECT ProductID FROM Orders WHERE OrderID = @OrderID)
						
						DELETE FROM Orders WHERE OrderID = @OrderID AND CustomerID = @CustomerID
						
						UPDATE Products
						SET 
						Stock = (Stock + @Quantity), TotalSold = (TotalSold - @Quantity)
						WHERE
						ProductID = @ProductID;
						
						SELECT @ResponseMessage = 200;
					END
				ELSE
					BEGIN
					--order does not exist
						SELECT @ResponseMessage = 401;
					END
				END
				
				ELSE 
					BEGIN
					
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
DECLARE @Out as INT; 
EXEC CancelOrderAdmin @Token = 'D1-46D3-953F-D28AD246A235', @OrderID = 1, @CustomerID = 1, @ResponseMessage = @Out OUTPUT; 
SELECT @Out AS 'OutputMessage'; 
--------

--add product

CREATE PROCEDURE AddProduct
@Token VARCHAR(25),
@ProductName VARCHAR(50),
@ImageUrl TEXT,
@Stock INT,
@Category VARCHAR(50),
@Price MONEY,
@Description TEXT,
@ResponseMessage INT OUTPUT
AS
BEGIN
	BEGIN TRANSACTION
		IF EXISTS(SELECT * FROM Sessions WHERE Token = @Token AND CURRENT_TIMESTAMP <= ExpiryTime)
			BEGIN
				DECLARE @CustomerID AS INT = (SELECT CustomerID FROM Sessions WHERE Token = @Token);
				
				IF EXISTS(SELECT * FROM Customer WHERE CustomerID = @CustomerID AND Admin = 1)
					BEGIN
						IF EXISTS(SELECT * FROM Products WHERE ProductName = @ProductName)
							BEGIN
								--product name already exists
								SELECT @ResponseMessage = 409;
							END
						ELSE
							BEGIN
								INSERT INTO Products
								(ProductName, ImageUrl, Stock, Category, Price, Description)
								VALUES
								(@ProductName, @ImageUrl, @Stock, @Category, @Price, @Description)
								
								SELECT @ResponseMessage = 200;
							END
					END
				ELSE
					BEGIN
						--USER NOT ADMIN
						SELECT @ResponseMessage = 401;
					END
				
			END
		ELSE
			BEGIN
				--Not Logged in
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
DECLARE @Out as INT; 
EXEC AddProduct @Token = 'FD-48BA-8080-EE76E5F9FAEC', @ProductName = 'P Name', @ImageUrl = 'url goes here', @Stock = 10, @Category='TV', @Price='10.11', @Description = "description", @ResponseMessage = @Out OUTPUT; 
SELECT @Out AS 'OutputMessage'; 
--------

--remove product

CREATE PROCEDURE DeleteProduct
@Token VARCHAR(25),
@ProductID INT,
@ResponseMessage INT OUTPUT
AS
BEGIN
	BEGIN TRANSACTION
		IF EXISTS (SELECT * FROM Sessions WHERE Token = @Token AND CURRENT_TIMESTAMP <= ExpiryTime)
			BEGIN
				DECLARE @ID AS INT = (SELECT CustomerID FROM Sessions WHERE Token = @Token);
				
				IF EXISTS (SELECT * FROM Customer WHERE CustomerID = @ID AND Admin = 1)
					BEGIN
						IF EXISTS(SELECT * FROM Products WHERE ProductID = @ProductID)
							BEGIN					
								DELETE FROM Products WHERE ProductID = @ProductID
								
								SELECT @ResponseMessage = 200;
							END
						ELSE
							BEGIN
							--product does not exist
								SELECT @ResponseMessage = 401;
							END
					END
				ELSE
					BEGIN
					--user not admin
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
DECLARE @Out as INT; 
EXEC DeleteProduct @Token = 'FD-48BA-8080-EE76E5F9FAEC', @ProductID = 1, @ResponseMessage = @Out OUTPUT; 
SELECT @Out AS 'OutputMessage'; 
--------


--edit product

CREATE PROCEDURE EditProduct
@Token VARCHAR(25),
@ProductID INT,
@ProductName VARCHAR(50),
@ImageUrl TEXT,
@Stock INT,
@Category VARCHAR(50),
@TotalSold INT,
@Price MONEY,
@Description TEXT,
@ResponseMessage INT OUTPUT
AS
BEGIN
	BEGIN TRANSACTION
		IF EXISTS (SELECT * FROM Sessions WHERE Token = @Token AND CURRENT_TIMESTAMP <= ExpiryTime)
			BEGIN
				DECLARE @ID AS INT = (SELECT CustomerID FROM Sessions WHERE Token = @Token);
				
				IF EXISTS (SELECT * FROM Customer WHERE CustomerID = @ID AND Admin = 1)
					BEGIN
						IF EXISTS(SELECT * FROM Products WHERE ProductID = @ProductID)
							BEGIN													
								UPDATE Products
								SET ProductName = @ProductName, ImageUrl = @ImageUrl, Stock = @Stock, Category = @Category, TotalSold = @TotalSold, Price = @Price, Description = @Description
								WHERE ProductID = @ProductID;
								
								SELECT @ResponseMessage = 200;
							END
						ELSE
							BEGIN
							--product does not exist
								SELECT @ResponseMessage = 401;
							END
					END
				ELSE
					BEGIN
					--user not admin
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
DECLARE @Out as INT; 
EXEC EditProduct @Token = 'FD-48BA-8080-EE76E5F9FAEC', @ProductID = 1, @ProductName = 'Changed the name', @ImageUrl = 'The url has changed', @Stock=100, @Category='Phone', @TotalSold=11, @Price= 12.00, @Description = "description", @ResponseMessage = @Out OUTPUT; 
SELECT @Out AS 'OutputMessage';
--------

Create View [ProductsForCustomer]
AS
SELECT ProductID, ProductName, ImageUrl, Price, Description, Category
FROM Products

Create View [ReviewsForProduct]
AS
SELECT Rating, Title, Description
FROM Reviews

Create View [CustomerOrders]
AS
SELECT OrderId, TimeOrdered, Quantity, ProductID
FROM Orders

CREATE View [TrendingProduct]
AS
SELECT TOP 1 ProductID, ProductName, ImageUrl, Price, Description, Category 
FROM Products 
ORDER BY TotalSold DESC;
 

CREATE View [FeaturedProduct]
AS
SELECT TOP 1 ProductID, ProductName, ImageUrl, Price, Description, Category 
FROM Products 
ORDER BY AvgRating DESC;

---Reviews

--Write review

CREATE PROCEDURE WriteReview
@Token VARCHAR(25),
@Rating INT,
@ProductID INT,
@Title VARCHAR(50),
@Description TEXT,
@ResponseMessage Int OUTPUT
AS
BEGIN
	BEGIN TRANSACTION
		IF EXISTS (SELECT * FROM Sessions WHERE Token = @Token AND CURRENT_TIMESTAMP <= ExpiryTime)
			BEGIN
				IF Exists(SELECT * FROM Products WHERE ProductID = @ProductID)
					BEGIN
						DECLARE @CustomerID AS INT = (SELECT CustomerID FROM Sessions WHERE Token = @Token);

                        IF EXISTS (SELECT * FROM Orders WHERE CustomerID = @CustomerID AND ProductID = @ProductID) 
                            BEGIN
                                IF EXISTS (SELECT * FROM Reviews WHERE CustomerID = @CustomerID AND ProductID = @ProductID)
                                    BEGIN
                                        UPDATE Reviews SET Title = @Title, Description = @Description, Rating = @Rating WHERE CustomerID = @CustomerID AND ProductID = @ProductID;
                                    END
                                ELSE
                                    BEGIN
                                        INSERT INTO Reviews
                                        (ProductID, CustomerID, Rating, Title, Description)
                                        VALUES
                                        (@ProductID, @CustomerID, @Rating, @Title, @Description);                                
                                    END
                                EXEC CalculateInsertAverage @ProdID = @ProductID;
                                SELECT @ResponseMessage = 200;
                            END
                        ELSE
                            BEGIN
                                SELECT @ResponseMessage = 208;
                            END
					END
				ELSE
					BEGIN
					--Product does not exist
						SELECT @ResponseMessage = 401;
					END
			END
		ELSE
			BEGIN
			--user not logged in
				SELECT @ResponseMessage = 402;
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


CREATE PROCEDURE GetCustomerDetails
@Token VARCHAR(25)
AS
BEGIN
	IF EXISTS(SELECT * FROM Sessions WHERE Token = @Token AND CURRENT_TIMESTAMP <= ExpiryTime)
		BEGIN
			DECLARE @CustomerID AS INT = (SELECT CustomerID FROM Sessions WHERE Token = @Token);
			SELECT * FROM Customer WHERE CustomerID = @CustomerID;
		END
END
GO

-- how to run
DECLARE @Out as INT; 
EXEC WriteReview @Token = '00-4B68-A7CA-EA85320CB2ED', @Title = 'title', @Rating = 3, @ProductID = 2, @Description = 'this is the description', @ResponseMessage = @Out OUTPUT; 
SELECT @Out AS 'OutputMessage';
--------


CREATE PROCEDURE Recommending
@Token VARCHAR(25),
@ProductId INT
AS
BEGIN
	IF EXISTS(SELECT * FROM Sessions WHERE Token = @Token AND CURRENT_TIMESTAMP <= ExpiryTime)
		BEGIN
			DECLARE @CustomerID AS INT = (SELECT CustomerID FROM Sessions WHERE Token = @Token);
			
			Declare @Rating AS FLOAT = (SELECT AvgRating from Products WHERE ProductId = @ProductId);

			Declare @Category AS VARCHAR(50) = (SELECT Category from Products WHERE ProductId = @ProductId)

			-- select top 5 

			SELECT TOP 5* from Products WHERE AvgRating = @Rating AND Category = @Category;
			ORDER BY NEWID();

		END
END
GO


EXEC Recommending @Token = '00-4B68-A7CA-EA85320CB2ED', @ProductId = 4; 


-- how to run
DECLARE @Out as INT; 
EXEC Recommending @Token = '00-4B68-A7CA-EA85320CB2ED', @ProductId = 4, @ResponseMessage = @Out OUTPUT; 
SELECT @Out AS 'OutputMessage';
--------


-- View reviews for a product

CREATE VIEW ProductReviews AS 
SELECT ProductId, CustomerID, Rating, Title, Description
FROM Reviews
Order By Rating;

-- how to run
SELECT * FROM [ProductReviews] --WHERE ProductIDGoesHere


CREATE VIEW NumReviews AS 
SELECT COUNT(ProductId)
FROM Reviews
--WHERE ProductID;

-- how to run
SELECT * FROM [NumReviews]



--- Updating Average Rating every time a new review is made.
-- CREATE TRIGGER UpdateProductRating
-- ON Reviews
-- AFTER INSERT, DELETE
-- AS
-- BEGIN
-- 	DECLARE @ProdID INT;
-- 	SET @ProdID = ( SELECT ProductID from inserted)
-- 	EXEC dbo.CalculateInsertAverage @ProductID = @ProdID
-- END;

CREATE PROCEDURE CalculateInsertAverage @ProdID int
AS
BEGIN
	Declare @CurrentRating FLOAT;
	Set @CurrentRating = (SELECT AVG(Rating) 
	FROM dbo.Reviews
	Where ProductID = @ProdID)

	UPDATE dbo.Products
	SET AVGRating = @CurrentRating
	WHERE ProductID = @ProdID
END
Go

---------

CREATE PROCEDURE [dbo].[AddStock]
@Token VARCHAR(25),
@ProductId INT,
@Stock INT,
@ResponseMessage INT OUTPUT
AS
BEGIN
	BEGIN TRANSACTION
		IF EXISTS(SELECT * FROM Sessions WHERE Token = @Token AND CURRENT_TIMESTAMP <= ExpiryTime)
			BEGIN
				DECLARE @CustomerID AS INT = (SELECT CustomerID FROM Sessions WHERE Token = @Token);
				
				IF EXISTS(SELECT * FROM Customer WHERE CustomerID = @CustomerID AND Admin = 1)
					BEGIN
						IF EXISTS(SELECT * FROM Products WHERE ProductId = @ProductId)
							BEGIN

                                DECLARE @CurrentStock AS INT = (SELECT Stock FROM Products WHERE ProductId = @ProductId);


								UPDATE dbo.Products SET Products.Stock = (@CurrentStock + @Stock)
								WHERE Products.ProductID = @ProductId
								
								SELECT @ResponseMessage = 200;
								
							END
						ELSE
							BEGIN

                                --product id already exists
								SELECT @ResponseMessage = 409;
                                
							END
					END
				ELSE
					BEGIN
						--USER NOT ADMIN
						SELECT @ResponseMessage = 401;
					END
				
			END
		ELSE
			BEGIN
				--Not Logged in
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


-----------------------------------------------

CREATE PROCEDURE dbo.EditReview
@Token VARCHAR(25),
@Rating INT,
@ProductID INT,
@Title VARCHAR(50),
@Description TEXT,
@ResponseMessage Int OUTPUT
AS
BEGIN
	BEGIN TRANSACTION
		IF EXISTS (SELECT * FROM Sessions WHERE Token = @Token AND CURRENT_TIMESTAMP <= ExpiryTime)
			BEGIN
				IF Exists(SELECT * FROM Products WHERE ProductID = @ProductID)
					BEGIN
						DECLARE @CustomerID AS INT = (SELECT CustomerID FROM Sessions WHERE Token = @Token);
						
                        UPDATE Reviews SET Rating = @Rating, Title = @Title, Description = @Description
                        WHERE ProductID = @ProductID;
						
						SELECT @ResponseMessage = 200;
					END
				ELSE
					BEGIN
					--Product does not exist
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


CREATE PROCEDURE DeleteReview
@Token VARCHAR(25),
@ProductID INT,
@ResponseMessage INT OUTPUT
AS
BEGIN
	BEGIN TRANSACTION
		IF EXISTS(SELECT * FROM Sessions WHERE Token = @Token AND CURRENT_TIMESTAMP <= ExpiryTime)
			BEGIN
				DECLARE @CustomerID AS INT = (SELECT CustomerID FROM Sessions WHERE Token = @Token)
				
				IF EXISTS(SELECT * FROM Reviews WHERE ProductID = @ProductID AND CustomerID = @CustomerID)
					BEGIN												
						DELETE FROM Reviews WHERE ProductID = @ProductID AND CustomerID = @CustomerID
						
						SELECT @ResponseMessage = 200;
					END
				ELSE
					BEGIN
					--order does not exist
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

----------------

CREATE PROCEDURE [dbo].[RecommendMostCategory]
@Token VARCHAR(25)
AS
BEGIN
	IF EXISTS(SELECT * FROM Sessions WHERE Token = @Token AND CURRENT_TIMESTAMP <= ExpiryTime)
		BEGIN
			DECLARE @CustomerID AS INT = (SELECT CustomerID FROM Sessions WHERE Token = @Token);
			
			-- select the most common value for category 

			DECLARE @TopCat AS VARCHAR(50) = (SELECT TOP 1 Category AS MOST_FREQUENT
            FROM Products
            GROUP BY Category
            ORDER BY COUNT(Category) DESC)

            DECLARE @TopCat2 AS VARCHAR(50) = (SELECT Category AS MOST_FREQUENT
            FROM Products
            GROUP BY Category
            ORDER BY COUNT(Category) DESC
            OFFSET 1 ROW 
            FETCH NEXT 1 ROW ONLY);

            SELECT TOP 5* FROM Products WHERE Category = @TopCat OR Category = @TopCat2
            ORDER BY NEWID();

		END
END
GO


















