-- Create a database
CREATE DATABASE ProductService;

-- Change database to use the above database that was created
USE ProductService;

-- Create product table
 CREATE TABLE Products (
    ProductID INT IDENTITY(1,1) PRIMARY KEY,
    Name VARCHAR(32) NOT NULL,
    Description TEXT,
    Specification TEXT,
    Delivery TEXT,
    ModelNumber VARCHAR(16) NOT NULL,    
    CreateDate datetime2,    
    ModifiedDate datetime2,
    Timestamp ROWVERSION,
    Status TINYINT NOT NULL
);