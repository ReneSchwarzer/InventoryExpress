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

INSERT INTO State (ID, Name, Grade, Discription, Timestamp) VALUES (1, 'Ungenügend', 6, NULL, '2020-10-11 14:24:32');
INSERT INTO State (ID, Name, Grade, Discription, Timestamp) VALUES (2, 'Mangelhaft', 5, NULL, '2020-10-11 14:24:32');
INSERT INTO State (ID, Name, Grade, Discription, Timestamp) VALUES (3, 'Ausreichend', 4, NULL, '2020-10-11 14:24:32');
INSERT INTO State (ID, Name, Grade, Discription, Timestamp) VALUES (4, 'Befriedigend', 3, NULL, '2020-10-11 14:24:32');
INSERT INTO State (ID, Name, Grade, Discription, Timestamp) VALUES (5, 'Gut', 2, NULL, '2020-10-11 14:24:32');
INSERT INTO State (ID, Name, Grade, Discription, Timestamp) VALUES (6, 'Sehr gut', 1, NULL, '2020-10-11 14:24:32');
