/* Jan-Justin van Tonder */

DROP DATABASE IF EXISTS GameRentals;
CREATE DATABASE GameRentals;
USE GameRentals;

--
-- Table structure for table `Game`
--

CREATE TABLE Game (
  GameID INT NOT NULL IDENTITY,
  Title VARCHAR(100) NOT NULL,
  ReleaseYear NUMERIC(4) DEFAULT NULL,
  RentalRrate DECIMAL(4,2) NOT NULL DEFAULT 24.99,
  PRIMARY KEY  (GameID),
);

--
-- Table structure for table `Developer`
--

CREATE TABLE Developer (
  DeveloperID INT NOT NULL IDENTITY,
  Name VARCHAR(50) NOT NULL,
  PRIMARY KEY  (DeveloperID)
);

--
-- Table structure for table `Genre`
--

CREATE TABLE Genre (
  GenreID INT NOT NULL IDENTITY,
  Name VARCHAR(50) NOT NULL,
  PRIMARY KEY  (GenreID)
);

--
-- Table structure for table `Publisher`
--

CREATE TABLE Publisher (
  PublisherID INT NOT NULL IDENTITY,
  Name VARCHAR(50) NOT NULL,
  PRIMARY KEY  (PublisherID)
);

--
-- Table structure for table `GameDeveloper`
--

CREATE TABLE GameDeveloper (
  GameID INT NOT NULL,
  DeveloperID INT NOT NULL,
  PRIMARY KEY  (GameID,DeveloperID),
  CONSTRAINT [fk_game_developer_game] FOREIGN KEY (GameID) REFERENCES Game (GameID),
  CONSTRAINT [fk_game_developer_developer] FOREIGN KEY (DeveloperID) REFERENCES Developer (DeveloperID)
);

--
-- Table structure for table `GameGenre`
--

CREATE TABLE GameGenre (
  GameID INT NOT NULL,
  GenreID INT NOT NULL,
  PRIMARY KEY  (GameID,GenreID),
  CONSTRAINT [fk_game_genre_game] FOREIGN KEY (GameID) REFERENCES Game (GameID),
  CONSTRAINT [fk_game_genre_genre] FOREIGN KEY (GenreID) REFERENCES Genre (GenreID)
);

--
-- Table structure for table `GamePublisher`
--

CREATE TABLE GamePublisher (
  GameID INT NOT NULL,
  PublisherID INT NOT NULL,
  PRIMARY KEY  (GameID,PublisherID),
  CONSTRAINT [fk_game_publisher_game] FOREIGN KEY (GameID) REFERENCES Game (GameID),
  CONSTRAINT [fk_game_publisher_publisher] FOREIGN KEY (PublisherID) REFERENCES Publisher (PublisherID)
);

--
-- Table structure for table `Country`
--

CREATE TABLE Country (
  CountryID INT NOT NULL IDENTITY,
  Country VARCHAR(50) NOT NULL,
  PRIMARY KEY  (CountryID)
);

--
-- Table structure for table `City`
--

CREATE TABLE City (
  CityID INT NOT NULL IDENTITY,
  City VARCHAR(50) NOT NULL,
  CountryID INT NOT NULL,
  PRIMARY KEY  (CityID)
 ,
  CONSTRAINT [fk_city_country] FOREIGN KEY (CountryID) REFERENCES Country (CountryID)
);

--
-- Table structure for table `Address`
--

CREATE TABLE Address (
  AddressID INT NOT NULL IDENTITY,
  Address VARCHAR(50) NOT NULL,
  Address2 VARCHAR(50) DEFAULT NULL,
  District VARCHAR(20) NOT NULL,
  CityID INT NOT NULL,
  PostalCode VARCHAR(10) DEFAULT NULL,
  Phone VARCHAR(20) NOT NULL,
  PRIMARY KEY  (AddressID),
  CONSTRAINT [fk_address_city] FOREIGN KEY (CityID) REFERENCES City (CityID)
);


--
-- Table structure for table `Staff`
--

CREATE TABLE Staff (
  StaffID INT NOT NULL IDENTITY,
  FirstName VARCHAR(50) NOT NULL,
  LastName VARCHAR(50) NOT NULL,
  AddressID INT NOT NULL,
  Email VARCHAR(50) DEFAULT NULL,
  StoreID INT NOT NULL,
  Active BIT NOT NULL DEFAULT 1,
  PRIMARY KEY  (StaffID),
  CONSTRAINT [fk_staff_address] FOREIGN KEY (AddressID) REFERENCES Address (AddressID)
);

--
-- Table structure for table `Store`
--

CREATE TABLE Store (
  StoreID INT NOT NULL IDENTITY,
  ManagerStaffID INT NOT NULL,
  AddressID INT NOT NULL,
  PRIMARY KEY  (StoreID),
  CONSTRAINT [fk_store_staff] FOREIGN KEY (ManagerStaffID) REFERENCES Staff (StaffID),
  CONSTRAINT [fk_store_address] FOREIGN KEY (AddressID) REFERENCES Address (AddressID)
);

ALTER Table Staff ADD CONSTRAINT [fk_staff_store] FOREIGN KEY (StoreID) REFERENCES Store (StoreID)

--
-- Table structure for table `Customer`
--

CREATE TABLE Customer (
  CustomerID INT NOT NULL IDENTITY,
  StoreID INT NOT NULL,
  FirstName VARCHAR(50) NOT NULL,
  LastName VARCHAR(50) NOT NULL,
  Email VARCHAR(50) DEFAULT NULL,
  AddressID INT NOT NULL,
  Active BIT NOT NULL DEFAULT 1,
  CreateDate DATETIME2(0) NOT NULL,
  PRIMARY KEY  (CustomerID),
  CONSTRAINT [fk_customer_address] FOREIGN KEY (AddressID) REFERENCES Address (AddressID),
  CONSTRAINT [fk_customer_store] FOREIGN KEY (StoreID) REFERENCES Store (StoreID)
);

--
-- Table structure for table `Inventory`
--

CREATE TABLE Inventory (
  InventoryID INT NOT NULL IDENTITY,
  GameID INT NOT NULL,
  StoreID INT NOT NULL,
  PRIMARY KEY  (InventoryID),
  CONSTRAINT [fk_inventory_store] FOREIGN KEY (StoreID) REFERENCES Store (StoreID),
  CONSTRAINT [fk_inventory_game] FOREIGN KEY (GameID) REFERENCES Game (GameID)
);

--
-- Table structure for table `Rental`
--

CREATE TABLE Rental (
  RentalID INT NOT NULL IDENTITY,
  RentalDate DATETIME2(0) NOT NULL,
  InventoryID INT NOT NULL,
  CustomerID INT NOT NULL,
  ReturnDate DATETIME2(0) DEFAULT NULL,
  StaffID INT NOT NULL,
  PRIMARY KEY (RentalID),
  CONSTRAINT [fk_rental_staff] FOREIGN KEY (StaffID) REFERENCES Staff (StaffID),
  CONSTRAINT [fk_rental_inventory] FOREIGN KEY (InventoryID) REFERENCES Inventory (InventoryID),
  CONSTRAINT [fk_rental_customer] FOREIGN KEY (CustomerID) REFERENCES Customer (CustomerID)
);

--
-- Table structure for table `Payment`
--

CREATE TABLE Payment (
  PaymentID INT NOT NULL IDENTITY,
  CustomerID INT NOT NULL,
  StaffID INT NOT NULL,
  RentalID INT DEFAULT NULL,
  Amount DECIMAL(5,2) NOT NULL,
  PaymentDate DATETIME2(0) NOT NULL,
  PRIMARY KEY  (PaymentID),
  CONSTRAINT [fk_payment_rental] FOREIGN KEY (RentalID) REFERENCES Rental (RentalID),
  CONSTRAINT [fk_payment_customer] FOREIGN KEY (CustomerID) REFERENCES Customer (CustomerID),
  CONSTRAINT [fk_payment_staff] FOREIGN KEY (StaffID) REFERENCES Staff (StaffID)
);