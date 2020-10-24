CREATE TABLE State 
(
    ID 			INTEGER			PRIMARY KEY AUTOINCREMENT NOT NULL,
    Name 		VARCHAR(64),
    Grade       INTEGER (1),    NOT NULL UNIQUE
	Discription TEXT,
    Timestamp 	TIMESTAMP 		DEFAULT CURRENT_TIMESTAMP NOT NULL
);

CREATE TABLE CostCenter 
(
    ID 			INTEGER			PRIMARY KEY AUTOINCREMENT NOT NULL,
    Name 		VARCHAR(64),
	Discription TEXT,
    Timestamp 	TIMESTAMP 		DEFAULT CURRENT_TIMESTAMP NOT NULL
);

CREATE TABLE GLAccount  
(
    ID 			INTEGER			PRIMARY KEY AUTOINCREMENT NOT NULL,
    Name 		VARCHAR(64),
	Discription TEXT,
    Timestamp 	TIMESTAMP 		DEFAULT CURRENT_TIMESTAMP NOT NULL
);

CREATE TABLE Manufacturer 
(
    ID          INTEGER			PRIMARY KEY AUTOINCREMENT NOT NULL,
    Name        VARCHAR (64)	UNIQUE NOT NULL,
    Address     VARCHAR (256),
    Zip         VARCHAR (10),
    Place       VARCHAR (64),
    Discription TEXT,
    Timestamp   TIMESTAMP 		DEFAULT CURRENT_TIMESTAMP NOT NULL
);

CREATE TABLE Supplier 
(
    ID          INTEGER			PRIMARY KEY AUTOINCREMENT NOT NULL,
    Name        VARCHAR (64)	UNIQUE NOT NULL,
    Address     VARCHAR (256),
    Zip         VARCHAR (10),
    Place       VARCHAR (64),
    Discription TEXT,
    Timestamp   TIMESTAMP 		DEFAULT CURRENT_TIMESTAMP NOT NULL
);

CREATE TABLE Location  
(
    ID          INTEGER			PRIMARY KEY AUTOINCREMENT NOT NULL,
    Name        VARCHAR (64)	UNIQUE NOT NULL,
    Address     VARCHAR (256),
    Zip         VARCHAR (10),
    Place       VARCHAR (64),
	Building    VARCHAR (64),
	Room        VARCHAR (64),
    Discription TEXT,
    Timestamp   TIMESTAMP 		DEFAULT CURRENT_TIMESTAMP NOT NULL
);

CREATE TABLE Attribute   
(
    ID 			INTEGER			PRIMARY KEY AUTOINCREMENT NOT NULL,
    Name 		VARCHAR(64),
	Discription TEXT,
    Timestamp 	TIMESTAMP 		DEFAULT CURRENT_TIMESTAMP NOT NULL
);

CREATE TABLE AttributeDateTimeValue    
(
    ID 			INTEGER			PRIMARY KEY NOT NULL REFERENCES Attribute (ID),
    Value 		DATETIME
);

CREATE TABLE AttributeTextValue    
(
    ID 			INTEGER			PRIMARY KEY NOT NULL REFERENCES Attribute (ID),
    Value 		TEXT
);

CREATE TABLE Template      
(
    ID 			INTEGER			PRIMARY KEY AUTOINCREMENT NOT NULL,
    Name 		VARCHAR(64),
	Discription TEXT,
    Timestamp 	TIMESTAMP 		DEFAULT CURRENT_TIMESTAMP NOT NULL
);

CREATE TABLE TemplateAttribute     
(
    Template 	INTEGER			NOT NULL REFERENCES Template (ID),
    Atrtribute 	INTEGER			NOT NULL REFERENCES Attribute (ID),
    Timestamp 	TIMESTAMP 		DEFAULT CURRENT_TIMESTAMP NOT NULL,
	PRIMARY KEY (Template, Atrtribute)
);

CREATE TABLE Inventory     
(
    ID 			INTEGER			PRIMARY KEY AUTOINCREMENT NOT NULL,
	Template	INTEGER      	REFERENCES Template (ID),
	Location	INTEGER      	REFERENCES Location (ID),
	CostCenter 	INTEGER      	REFERENCES CostCenter (ID),
	Manufacturer INTEGER      	REFERENCES Manufacturer (ID),
	State  		INTEGER      	REFERENCES State (ID),
	Supplier 	INTEGER      	REFERENCES Supplier (ID),
	GLAccount  	INTEGER      	REFERENCES GLAccount (ID),
	Name 		VARCHAR(64),
	CostValue 	DECIMAL,
	PurchaseDate DATE,
	DerecognitionDate DATE,
	Discription TEXT,
    Timestamp 	TIMESTAMP 		DEFAULT CURRENT_TIMESTAMP NOT NULL
);

CREATE TABLE InventoryAttribute     
(
    Inventory 	INTEGER			NOT NULL REFERENCES Inventory (ID),
    Atrtribute 	INTEGER			NOT NULL REFERENCES Attribute (ID),
    Timestamp 	TIMESTAMP 		DEFAULT CURRENT_TIMESTAMP NOT NULL,
	PRIMARY KEY (Inventory, Atrtribute)
);

CREATE TABLE Ascription    
(
    ID 			INTEGER			PRIMARY KEY AUTOINCREMENT NOT NULL,
    Name 		VARCHAR(64),
	Discription TEXT,
    Timestamp 	TIMESTAMP 		DEFAULT CURRENT_TIMESTAMP NOT NULL
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
