CREATE TABLE State 
(
    ID 			        INTEGER			PRIMARY KEY AUTOINCREMENT NOT NULL,
    Name 		        VARCHAR(64),
    Grade               INTEGER (1),    NOT NULL UNIQUE
	Discription         TEXT,
    Timestamp 	        TIMESTAMP 		DEFAULT CURRENT_TIMESTAMP NOT NULL
);

CREATE TABLE CostCenter 
(
    ID 			        INTEGER			PRIMARY KEY AUTOINCREMENT NOT NULL,
    Name 		        VARCHAR(64),
	Discription         TEXT,
    Timestamp 	        TIMESTAMP 		DEFAULT CURRENT_TIMESTAMP NOT NULL
);

CREATE TABLE GLAccount  
(
    ID 			        INTEGER			PRIMARY KEY AUTOINCREMENT NOT NULL,
    Name 		        VARCHAR(64),
	Discription         TEXT,
    Timestamp 	        TIMESTAMP 		DEFAULT CURRENT_TIMESTAMP NOT NULL
);

CREATE TABLE Manufacturer 
(
    ID                  INTEGER			PRIMARY KEY AUTOINCREMENT NOT NULL,
    Name                VARCHAR (64)	UNIQUE NOT NULL,
    Address             VARCHAR (256),
    Zip                 VARCHAR (10),
    Place               VARCHAR (64),
    Discription         TEXT,
    Timestamp           TIMESTAMP 		DEFAULT CURRENT_TIMESTAMP NOT NULL
);

CREATE TABLE Supplier 
(
    ID                  INTEGER			PRIMARY KEY AUTOINCREMENT NOT NULL,
    Name                VARCHAR (64)	UNIQUE NOT NULL,
    Address             VARCHAR (256),
    Zip                 VARCHAR (10),
    Place               VARCHAR (64),
    Discription         TEXT,
    Timestamp           TIMESTAMP 		DEFAULT CURRENT_TIMESTAMP NOT NULL
);

CREATE TABLE Location  
(
    ID                  INTEGER			PRIMARY KEY AUTOINCREMENT NOT NULL,
    Name                VARCHAR (64)	UNIQUE NOT NULL,
    Address             VARCHAR (256),
    Zip                 VARCHAR (10),
    Place               VARCHAR (64),
	Building            VARCHAR (64),
	Room                VARCHAR (64),
    Discription         TEXT,
    Timestamp           TIMESTAMP 		DEFAULT CURRENT_TIMESTAMP NOT NULL
);

CREATE TABLE Attribute   
(
    ID 			        INTEGER			PRIMARY KEY AUTOINCREMENT NOT NULL,
    Name 		        VARCHAR(64),
	Discription         TEXT,
    Timestamp 	        TIMESTAMP 		DEFAULT CURRENT_TIMESTAMP NOT NULL
);

CREATE TABLE AttributeDateTimeValue    
(
    ID 			        INTEGER			PRIMARY KEY NOT NULL REFERENCES Attribute (ID),
    Value 		        DATETIME
);

CREATE TABLE AttributeTextValue    
(
    ID 			        INTEGER			PRIMARY KEY NOT NULL REFERENCES Attribute (ID),
    Value 		        TEXT
);

CREATE TABLE Template      
(
    ID 			        INTEGER			PRIMARY KEY AUTOINCREMENT NOT NULL,
    Name 		        VARCHAR(64),
	Discription         TEXT,
    Timestamp 	        TIMESTAMP 		DEFAULT CURRENT_TIMESTAMP NOT NULL
);

CREATE TABLE Ascription    
(
    ID 			        INTEGER			PRIMARY KEY AUTOINCREMENT NOT NULL,
    Name 		        VARCHAR(64),
	Discription         TEXT,
    Timestamp 	        TIMESTAMP 		DEFAULT CURRENT_TIMESTAMP NOT NULL
);

CREATE TABLE TemplateAttribute     
(
    TemplateID          INTEGER			NOT NULL REFERENCES Template (ID),
    AtrtributeID        INTEGER			NOT NULL REFERENCES Attribute (ID),
    Timestamp 	        TIMESTAMP 		DEFAULT CURRENT_TIMESTAMP NOT NULL,
	PRIMARY KEY (TemplateID, AtrtributeID)
);

CREATE TABLE Inventory     
(
    ID                  INTEGER			PRIMARY KEY AUTOINCREMENT NOT NULL,
	TemplateID	        INTEGER      	REFERENCES Template (ID),
	LocationID	        INTEGER      	REFERENCES Location (ID),
	CostCenterID 	    INTEGER      	REFERENCES CostCenter (ID),
	ManufacturerID      INTEGER      	REFERENCES Manufacturer (ID),
	StateID  		    INTEGER      	REFERENCES State (ID),
	SupplierID 	        INTEGER      	REFERENCES Supplier (ID),
	GLAccountID  	    INTEGER      	REFERENCES GLAccount (ID),
	Name 		        VARCHAR(64),
	CostValue 	        DECIMAL,
	PurchaseDate        DATE,
	DerecognitionDate   DATE,
	Discription         TEXT,
    Timestamp 	        TIMESTAMP 		DEFAULT CURRENT_TIMESTAMP NOT NULL
);

CREATE TABLE InventoryAttribute     
(
    InventoryID 	    INTEGER			NOT NULL REFERENCES Inventory (ID),
    AtrtributeID 	    INTEGER			NOT NULL REFERENCES Attribute (ID),
    Timestamp 	        TIMESTAMP 		DEFAULT CURRENT_TIMESTAMP NOT NULL,
	PRIMARY KEY (InventoryID, AtrtributeID)
);

-- Example
INSERT INTO State (ID, Name, Grade, Discription, Timestamp) VALUES (1, 'Ungenügend', 6, NULL, '2020-10-11 14:24:32');
INSERT INTO State (ID, Name, Grade, Discription, Timestamp) VALUES (2, 'Mangelhaft', 5, NULL, '2020-10-11 14:24:32');
INSERT INTO State (ID, Name, Grade, Discription, Timestamp) VALUES (3, 'Ausreichend', 4, NULL, '2020-10-11 14:24:32');
INSERT INTO State (ID, Name, Grade, Discription, Timestamp) VALUES (4, 'Befriedigend', 3, NULL, '2020-10-11 14:24:32');
INSERT INTO State (ID, Name, Grade, Discription, Timestamp) VALUES (5, 'Gut', 2, NULL, '2020-10-11 14:24:32');
INSERT INTO State (ID, Name, Grade, Discription, Timestamp) VALUES (6, 'Sehr gut', 1, NULL, '2020-10-11 14:24:32');

INSERT INTO Manufacturer (ID, Name, Place, Zip, Address, Discription, Timestamp) VALUES (1, 'RFT', NULL, NULL, 'Rundfunk- und FernmeldeTechnik', NULL, '2020-10-11 12:57:38');
INSERT INTO Manufacturer (ID, Name, Place, Zip, Address, Discription, Timestamp) VALUES (2, 'ROBOTRON', NULL, NULL, NULL, 'VEB Kombinat Robotron', '2020-10-11 12:57:38');                            
INSERT INTO Manufacturer (ID, Name, Place, Zip, Address, Discription, Timestamp) VALUES (3, 'Acer', NULL, NULL, NULL, 'VAcer Group', '2020-10-11 12:57:38');                            
INSERT INTO Manufacturer (ID, Name, Place, Zip, Address, Discription, Timestamp) VALUES (4, 'Atari', NULL, NULL, NULL, 'Atari Inc.', '2020-10-11 12:57:38');                            
INSERT INTO Manufacturer (ID, Name, Place, Zip, Address, Discription, Timestamp) VALUES (5, 'Commodore', NULL, NULL, NULL, 'Commodore International', '2020-10-11 12:57:38');                            
INSERT INTO Manufacturer (ID, Name, Place, Zip, Address, Discription, Timestamp) VALUES (6, 'Datel', NULL, NULL, NULL, 'Datel Electronics Limited', '2020-10-11 12:57:38');                            
INSERT INTO Manufacturer (ID, Name, Place, Zip, Address, Discription, Timestamp) VALUES (7, 'Dell', NULL, NULL, NULL, 'Dell Technologies Inc. (vormals Dell Inc. bzw. Dell Computer Corporation', '2020-10-11 12:57:38');                            

INSERT INTO Location (ID, Name, Place, Zip, Address, Building, Room, Discription, Timestamp) VALUES (1, 'Kiste 1',  NULL, NULL, NULL, NULL, NULL, NULL, '2020-10-11 14:53:19');
INSERT INTO Location (ID, Name, Place, Zip, Address, Building, Room, Discription, Timestamp) VALUES (2, 'Kiste 2',  NULL, NULL, NULL, NULL, NULL, NULL, '2020-10-11 14:53:19');
INSERT INTO Location (ID, Name, Place, Zip, Address, Building, Room, Discription, Timestamp) VALUES (3, 'Kiste 3',  NULL, NULL, NULL, NULL, NULL, NULL, '2020-10-11 14:53:19');
INSERT INTO Location (ID, Name, Place, Zip, Address, Building, Room, Discription, Timestamp) VALUES (4, 'Regal',  NULL, NULL, NULL, NULL, NULL, NULL, '2020-10-11 14:53:19');

INSERT INTO Supplier (ID, Name, Place, Zip, Address, Discription, Timestamp) VALUES (1, 'Amazon',  NULL, NULL, NULL, 'Online-Marktplatz', '2020-10-11 14:53:19');
INSERT INTO Supplier (ID, Name, Place, Zip, Address, Discription, Timestamp) VALUES (2, 'eBay Inc.',  NULL, NULL, NULL, 'Online-Marktplatz', '2020-10-11 14:53:19');
INSERT INTO Supplier (ID, Name, Place, Zip, Address, Discription, Timestamp) VALUES (3, 'eBay Kleinanzeigen',  NULL, NULL, NULL, 'Online-Marktplatz', '2020-10-11 14:53:19');
INSERT INTO Supplier (ID, Name, Place, Zip, Address, Discription, Timestamp) VALUES (4, 'Flohmarkt',  NULL, NULL, NULL, 'Offline-Marktplatz', '2020-10-11 14:53:19');
INSERT INTO Supplier (ID, Name, Place, Zip, Address, Discription, Timestamp) VALUES (5, 'Conrad',  NULL, NULL, NULL, 'OConrad Electronic SE', '2020-10-11 14:53:19');
INSERT INTO Supplier (ID, Name, Place, Zip, Address, Discription, Timestamp) VALUES (6, 'Escom',  NULL, NULL, NULL, 'ESCOM Computer GmbH', '2020-10-11 14:53:19');
INSERT INTO Supplier (ID, Name, Place, Zip, Address, Discription, Timestamp) VALUES (7, 'Media Markt',  NULL, NULL, NULL, 'MediaMartSaturn Retail Group', '2020-10-11 14:53:19');
INSERT INTO Supplier (ID, Name, Place, Zip, Address, Discription, Timestamp) VALUES (8, 'O2',  NULL, NULL, NULL, '', '2020-10-11 14:53:19');
INSERT INTO Supplier (ID, Name, Place, Zip, Address, Discription, Timestamp) VALUES (9, 'Photo Porst',  NULL, NULL, NULL, '', '2020-10-11 14:53:19');
INSERT INTO Supplier (ID, Name, Place, Zip, Address, Discription, Timestamp) VALUES (10, 'Quelle',  NULL, NULL, NULL, 'Quelle GmbH', '2020-10-11 14:53:19');
INSERT INTO Supplier (ID, Name, Place, Zip, Address, Discription, Timestamp) VALUES (11, 'Saturn',  NULL, NULL, NULL, 'MediaMartSaturn Retail Group', '2020-10-11 14:53:19');
INSERT INTO Supplier (ID, Name, Place, Zip, Address, Discription, Timestamp) VALUES (12, 'Vobis',  NULL, NULL, NULL, 'Vobis AG', '2020-10-11 14:53:19');

INSERT INTO GLAccount (ID, Name, Discription, Timestamp) VALUES (1, 'Haushalt', '', '2020-10-11 14:24:32');

INSERT INTO CostCenter (ID, Name, Discription, Timestamp) VALUES (1, 'Fuhrpark', '', '2020-10-11 14:24:32');
INSERT INTO CostCenter (ID, Name, Discription, Timestamp) VALUES (2, 'Hobby', '', '2020-10-11 14:24:32');

INSERT INTO Template (ID, Name, Discription, Timestamp) VALUES (1, 'Druckmedien', 'Gedruckte Informationsquellen wie Zeitschriften, Zeitungen, Bücher oder Kataloge.', '2020-10-11 14:24:32');
INSERT INTO Template (ID, Name, Discription, Timestamp) VALUES (2, 'Heimcomputer', 'Mikrocomputer der 80-er Jahre, welche überwiegend im privaten Haushalten genutzt wurden.', '2020-10-11 14:24:32');
INSERT INTO Template (ID, Name, Discription, Timestamp) VALUES (3, 'Personal Computer', 'Mikrocomputer, welche überwiegend im geschäftlichem Umfeld genutzt wurden.', '2020-10-11 14:24:32');
INSERT INTO Template (ID, Name, Discription, Timestamp) VALUES (4, 'Pocket PC', 'Handheld und PocketPSs', '2020-10-11 14:24:32');
INSERT INTO Template (ID, Name, Discription, Timestamp) VALUES (5, 'Radio-, Kasetten- und Tonbandgeräte', 'Audiogeräte', '2020-10-11 14:24:32');
INSERT INTO Template (ID, Name, Discription, Timestamp) VALUES (6, 'Smartphone', 'Handy', '2020-10-11 14:24:32');
INSERT INTO Template (ID, Name, Discription, Timestamp) VALUES (7, 'Software', 'Computerprogramme', '2020-10-11 14:24:32');
INSERT INTO Template (ID, Name, Discription, Timestamp) VALUES (8, 'Spiel', 'Computerspiele', '2020-10-11 14:24:32');
INSERT INTO Template (ID, Name, Discription, Timestamp) VALUES (9, 'Spielekonsole', '', '2020-10-11 14:24:32');
INSERT INTO Template (ID, Name, Discription, Timestamp) VALUES (10, 'Telespiel', 'Handheld Spielekonsole', '2020-10-11 14:24:32');
INSERT INTO Template (ID, Name, Discription, Timestamp) VALUES (11, 'Tischrechner', 'Rechenmaschienen und Taschenrechner', '2020-10-11 14:24:32');
INSERT INTO Template (ID, Name, Discription, Timestamp) VALUES (12, 'Zubehör', 'Computerzubehör', '2020-10-11 14:24:32');

INSERT INTO Attribute (ID, Name, Discription, Timestamp) VALUES (1, 'ISBN', '', '2020-10-11 14:24:32');
INSERT INTO Attribute (ID, Name, Discription, Timestamp) VALUES (2, 'Autor', '', '2020-10-11 14:24:32');
INSERT INTO Attribute (ID, Name, Discription, Timestamp) VALUES (3, 'Baujahr', '', '2020-10-11 14:24:32');
INSERT INTO Attribute (ID, Name, Discription, Timestamp) VALUES (4, 'Betriebssystem', '', '2020-10-11 14:24:32');
INSERT INTO Attribute (ID, Name, Discription, Timestamp) VALUES (5, 'Gewicht', '', '2020-10-11 14:24:32');
INSERT INTO Attribute (ID, Name, Discription, Timestamp) VALUES (6, 'Hauptspeicher', '', '2020-10-11 14:24:32');
INSERT INTO Attribute (ID, Name, Discription, Timestamp) VALUES (7, 'Kaufdatum', '', '2020-10-11 14:24:32');
INSERT INTO Attribute (ID, Name, Discription, Timestamp) VALUES (8, 'Lizenzschlüssel', '', '2020-10-11 14:24:32');
INSERT INTO Attribute (ID, Name, Discription, Timestamp) VALUES (9, 'Medium', '', '2020-10-11 14:24:32');
INSERT INTO Attribute (ID, Name, Discription, Timestamp) VALUES (10, 'Modell', '', '2020-10-11 14:24:32');
INSERT INTO Attribute (ID, Name, Discription, Timestamp) VALUES (11, 'Preis', '', '2020-10-11 14:24:32');
INSERT INTO Attribute (ID, Name, Discription, Timestamp) VALUES (12, 'Prozessor', '', '2020-10-11 14:24:32');
INSERT INTO Attribute (ID, Name, Discription, Timestamp) VALUES (13, 'Prüfdatum', '', '2020-10-11 14:24:32');
INSERT INTO Attribute (ID, Name, Discription, Timestamp) VALUES (14, 'Sereinnummer', '', '2020-10-11 14:24:32');
INSERT INTO Attribute (ID, Name, Discription, Timestamp) VALUES (15, 'Taktrate', '', '2020-10-11 14:24:32');
INSERT INTO Attribute (ID, Name, Discription, Timestamp) VALUES (16, 'URL', '', '2020-10-11 14:24:32');
INSERT INTO Attribute (ID, Name, Discription, Timestamp) VALUES (17, 'Verpackung', '', '2020-10-11 14:24:32');
