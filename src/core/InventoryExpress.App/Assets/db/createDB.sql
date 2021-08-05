CREATE TABLE Condition 
(
    ID 			        INTEGER			PRIMARY KEY AUTOINCREMENT NOT NULL,
    MediaID             INTEGER         REFERENCES Media (ID) ON DELETE SET NULL,
    Name 		        VARCHAR(64)     UNIQUE NOT NULL,
    Grade               INTEGER(1)      UNIQUE NOT NULL, 
	Description         TEXT,
    Created 	        TIMESTAMP 		DEFAULT CURRENT_TIMESTAMP NOT NULL,
    Updated 	        TIMESTAMP 		DEFAULT CURRENT_TIMESTAMP NOT NULL,
    Guid                CHAR(36)        UNIQUE NOT NULL
);

CREATE TABLE CostCenter 
(
    ID 			        INTEGER			PRIMARY KEY AUTOINCREMENT NOT NULL,
    MediaID             INTEGER         REFERENCES Media (ID) ON DELETE SET NULL,
    Name 		        VARCHAR(64)     UNIQUE NOT NULL,   
	Description         TEXT,
    Tag 		        VARCHAR(256),
    Created 	        TIMESTAMP 		DEFAULT CURRENT_TIMESTAMP NOT NULL,
    Updated 	        TIMESTAMP 		DEFAULT CURRENT_TIMESTAMP NOT NULL,
    Guid                CHAR(36)        UNIQUE NOT NULL
);

CREATE TABLE LedgerAccount  
(
    ID 			        INTEGER			PRIMARY KEY AUTOINCREMENT NOT NULL,
    MediaID             INTEGER         REFERENCES Media (ID) ON DELETE SET NULL,
    Name 		        VARCHAR(64)     UNIQUE NOT NULL,
	Description         TEXT,
    Tag 		        VARCHAR(256),
    Created 	        TIMESTAMP 		DEFAULT CURRENT_TIMESTAMP NOT NULL,
    Updated 	        TIMESTAMP 		DEFAULT CURRENT_TIMESTAMP NOT NULL,
    Guid                CHAR(36)        UNIQUE NOT NULL
);

CREATE TABLE Manufacturer 
(
    ID                  INTEGER			PRIMARY KEY AUTOINCREMENT NOT NULL,
    MediaID             INTEGER         REFERENCES Media (ID) ON DELETE SET NULL,
    Name                VARCHAR(64) 	UNIQUE NOT NULL,
    Address             VARCHAR(256),
    Zip                 VARCHAR(10),
    Place               VARCHAR(64),
    Description         TEXT,
    Tag 		        VARCHAR(256),
    Created             TIMESTAMP 		DEFAULT CURRENT_TIMESTAMP NOT NULL,
    Updated 	        TIMESTAMP 		DEFAULT CURRENT_TIMESTAMP NOT NULL,
    Guid                CHAR(36)        UNIQUE NOT NULL
);

CREATE TABLE Supplier 
(
    ID                  INTEGER			PRIMARY KEY AUTOINCREMENT NOT NULL,
    MediaID             INTEGER         REFERENCES Media (ID) ON DELETE SET NULL,
    Name                VARCHAR(64) 	UNIQUE NOT NULL,
    Address             VARCHAR(256),
    Zip                 VARCHAR(10),
    Place               VARCHAR(64),
    Description         TEXT,
    Tag 		        VARCHAR(256),
    Created             TIMESTAMP 		DEFAULT CURRENT_TIMESTAMP NOT NULL,
    Updated 	        TIMESTAMP 		DEFAULT CURRENT_TIMESTAMP NOT NULL,
    Guid                CHAR(36)        UNIQUE NOT NULL
);

CREATE TABLE Location  
(
    ID                  INTEGER			PRIMARY KEY AUTOINCREMENT NOT NULL,
    MediaID             INTEGER         REFERENCES Media (ID) ON DELETE SET NULL,
    Name                VARCHAR(64)	    UNIQUE NOT NULL,
    Address             VARCHAR(256),
    Zip                 VARCHAR(10),
    Place               VARCHAR(64),
	Building            VARCHAR(64),
	Room                VARCHAR(64),
    Description         TEXT,
    Tag 		        VARCHAR(256),
    Created             TIMESTAMP 		DEFAULT CURRENT_TIMESTAMP NOT NULL,
    Updated 	        TIMESTAMP 		DEFAULT CURRENT_TIMESTAMP NOT NULL,
    Guid                CHAR(36)        UNIQUE NOT NULL
);

CREATE TABLE Attribute   
(
    ID 			        INTEGER			PRIMARY KEY AUTOINCREMENT NOT NULL,
    MediaID             INTEGER         REFERENCES Media (ID) ON DELETE SET NULL,
    Name 		        VARCHAR(64)     UNIQUE NOT NULL,
	Description         TEXT,
    Created 	        TIMESTAMP 		DEFAULT CURRENT_TIMESTAMP NOT NULL,
    Updated 	        TIMESTAMP 		DEFAULT CURRENT_TIMESTAMP NOT NULL,
    Guid                CHAR(36)        UNIQUE NOT NULL
);

CREATE TABLE Template      
(
    ID 			        INTEGER			PRIMARY KEY AUTOINCREMENT NOT NULL,
    MediaID             INTEGER         REFERENCES Media (ID) ON DELETE SET NULL,
	Name 		        VARCHAR(64)     UNIQUE NOT NULL,
    Description         TEXT,
    Tag 		        VARCHAR(256),
    Created 	        TIMESTAMP 		DEFAULT CURRENT_TIMESTAMP NOT NULL,
    Updated 	        TIMESTAMP 		DEFAULT CURRENT_TIMESTAMP NOT NULL,
    Guid                CHAR(36)        UNIQUE NOT NULL
);

CREATE TABLE Ascription    
(
    ID 			        INTEGER			PRIMARY KEY AUTOINCREMENT NOT NULL,
    MediaID             INT             REFERENCES Media (ID) ON DELETE SET NULL,
    Name 		        VARCHAR(64)     UNIQUE NOT NULL,
	Description         TEXT,
    Tag 		        VARCHAR(256),
    Created 	        TIMESTAMP 		DEFAULT CURRENT_TIMESTAMP NOT NULL,
    Updated 	        TIMESTAMP 		DEFAULT CURRENT_TIMESTAMP NOT NULL,
    Guid                CHAR(36)        UNIQUE NOT NULL
);

CREATE TABLE TemplateAttribute     
(
    TemplateID          INTEGER			NOT NULL REFERENCES Template (ID),
    AttributeID         INTEGER			NOT NULL REFERENCES Attribute (ID),
    Created 	        TIMESTAMP 		DEFAULT CURRENT_TIMESTAMP NOT NULL,
	PRIMARY KEY (TemplateID, AtrtributeID)
);

CREATE TABLE Inventory     
(
    ID                  INTEGER			PRIMARY KEY AUTOINCREMENT NOT NULL,
	TemplateID	        INTEGER      	REFERENCES Template (ID) ON DELETE NO ACTION,
	LocationID	        INTEGER      	REFERENCES Location (ID) ON DELETE NO ACTION,
	CostCenterID 	    INTEGER      	REFERENCES CostCenter (ID) ON DELETE NO ACTION,
	ManufacturerID      INTEGER      	REFERENCES Manufacturer (ID) ON DELETE NO ACTION,
	ConditionID  		INTEGER      	REFERENCES Condition (ID) ON DELETE NO ACTION,
	SupplierID 	        INTEGER      	REFERENCES Supplier (ID) ON DELETE NO ACTION,
	LedgerAccountID     INTEGER      	REFERENCES LedgerAccount (ID) ON DELETE NO ACTION,
    ParentID            INTEGER         REFERENCES Inventory (ID) ON DELETE SET NULL,
    MediaID             INTEGER         REFERENCES Media (ID) ON DELETE SET NULL,
	Name 		        VARCHAR(64)     UNIQUE NOT NULL,
    CostValue 	        DECIMAL,
	PurchaseDate        DATE,
	DerecognitionDate   DATE,
	Description         TEXT,
    Tag 		        VARCHAR(256),
    Created 	        TIMESTAMP 		DEFAULT CURRENT_TIMESTAMP NOT NULL,
    Updated 	        TIMESTAMP 		DEFAULT CURRENT_TIMESTAMP NOT NULL,
    Guid                CHAR(36)        UNIQUE NOT NULL
);

CREATE TABLE InventoryAttribute     
(
    InventoryID 	    INTEGER			NOT NULL REFERENCES Inventory (ID) ON DELETE CASCADE,
    AttributeID 	    INTEGER			NOT NULL REFERENCES Attribute (ID) ON DELETE CASCADE,
    Value 		        TEXT,
    Created 	        TIMESTAMP 		DEFAULT CURRENT_TIMESTAMP NOT NULL,
	PRIMARY KEY (InventoryID, AtrtributeID)
);

CREATE TABLE InventoryAttachment     
(
    InventoryID 	    INTEGER			NOT NULL REFERENCES Inventory (ID) ON DELETE CASCADE,
    MediaID 	        INTEGER			NOT NULL REFERENCES Media (ID) ON DELETE CASCADE,
    Created 	        TIMESTAMP 		DEFAULT CURRENT_TIMESTAMP NOT NULL,
	PRIMARY KEY (InventoryID, MediaID)
);

CREATE TABLE InventoryComment    
(
    ID                  INTEGER			PRIMARY KEY AUTOINCREMENT NOT NULL,
    InventoryID 	    INTEGER			NOT NULL REFERENCES Inventory (ID) ON DELETE CASCADE,
    Comment 		    TEXT,
    Created 	        TIMESTAMP 		DEFAULT CURRENT_TIMESTAMP NOT NULL,
    Updated 	        TIMESTAMP 		DEFAULT CURRENT_TIMESTAMP NOT NULL,
    Guid                CHAR (36)       UNIQUE  NOT NULL
);

CREATE TABLE InventoryJournal    
(
    ID                  INTEGER			PRIMARY KEY AUTOINCREMENT NOT NULL,
    InventoryID 	    INTEGER			NOT NULL REFERENCES Inventory (ID) ON DELETE CASCADE,
    Action 		        VARCHAR(256),
    ActionParam 	    VARCHAR(256),
    Created 	        TIMESTAMP 		DEFAULT CURRENT_TIMESTAMP NOT NULL,
    Guid                CHAR (36)       UNIQUE  NOT NULL
);

CREATE TABLE Media 
(
    ID                  INTEGER			PRIMARY KEY AUTOINCREMENT NOT NULL,
    Name 		        VARCHAR(64)     NOT NULL,
    Data                BLOB,
    Tag 		        VARCHAR(256),
    Created 	        TIMESTAMP 		DEFAULT CURRENT_TIMESTAMP NOT NULL,
    Updated 	        TIMESTAMP 		DEFAULT CURRENT_TIMESTAMP NOT NULL,
    Guid                CHAR(36)        UNIQUE NOT NULL
);

CREATE TABLE Setting
(
    ID                 INTEGER			PRIMARY KEY AUTOINCREMENT NOT NULL,
    Currency           VARCHAR(10)      
);

-- Example
INSERT INTO Setting (Currency) VALUES ('€');

INSERT INTO Condition (ID, Name, Grade, Description, Created, Guid) VALUES (1, 'Ungenügend', 6, NULL, '2020-10-11 14:24:32', 'e9746bf2-9592-44e0-9013-9ba0c0008071');
INSERT INTO Condition (ID, Name, Grade, Description, Created, Guid) VALUES (2, 'Mangelhaft', 5, NULL, '2020-10-11 14:24:32', 'c315b705-d73c-4e42-bdcb-39f4a9d0b4de');
INSERT INTO Condition (ID, Name, Grade, Description, Created, Guid) VALUES (3, 'Ausreichend', 4, NULL, '2020-10-11 14:24:32', '6505c67f-45e4-49f0-8e1b-40488e16deec');
INSERT INTO Condition (ID, Name, Grade, Description, Created, Guid) VALUES (4, 'Befriedigend', 3, NULL, '2020-10-11 14:24:32', '2c3526b4-5855-4f6d-b22a-c1ad3a90fa79');
INSERT INTO Condition (ID, Name, Grade, Description, Created, Guid) VALUES (5, 'Gut', 2, NULL, '2020-10-11 14:24:32', '24985685-3fa2-4ece-bee5-13b76def881c');
INSERT INTO Condition (ID, Name, Grade, Description, Created, Guid) VALUES (6, 'Sehr gut', 1, NULL, '2020-10-11 14:24:32', '78f2acbc-2a21-4cd2-ba59-73ac67fbd60d');

INSERT INTO Manufacturer (ID, Name, Place, Zip, Address, Description, Tag, Created, Guid) VALUES (1, 'RFT', NULL, NULL, 'Rundfunk- und FernmeldeTechnik', NULL, 'ddr', '2020-10-11 12:57:38', '3e4c09ef-aa85-425f-ac24-4733463f6046');
INSERT INTO Manufacturer (ID, Name, Place, Zip, Address, Description, Tag, Created, Guid) VALUES (2, 'ROBOTRON', NULL, NULL, NULL, 'VEB Kombinat Robotron', 'ddr;veb', '2020-10-11 12:57:38', '67d35a0f-7e94-4bfd-a309-36e9162a67ff');                            
INSERT INTO Manufacturer (ID, Name, Place, Zip, Address, Description, Created, Guid) VALUES (3, 'Acer', NULL, NULL, NULL, 'VAcer Group', '2020-10-11 12:57:38', 'e06bbef2-4c24-4512-a4d3-e4363aa88687');                            
INSERT INTO Manufacturer (ID, Name, Place, Zip, Address, Description, Created, Guid) VALUES (4, 'Atari', NULL, NULL, NULL, 'Atari Inc.', '2020-10-11 12:57:38', 'f9b28160-42d6-4b76-8c61-1f591780576a');                            
INSERT INTO Manufacturer (ID, Name, Place, Zip, Address, Description, Created, Guid) VALUES (5, 'Commodore', NULL, NULL, NULL, 'Commodore International', '2020-10-11 12:57:38', '4cfa73c2-0e76-4fc1-b74a-9a345ade2bc4');                            
INSERT INTO Manufacturer (ID, Name, Place, Zip, Address, Description, Created, Guid) VALUES (6, 'Datel', NULL, NULL, NULL, 'Datel Electronics Limited', '2020-10-11 12:57:38', 'eb68c4bf-d1ba-40a9-ac47-97a2d4d80f16');                            
INSERT INTO Manufacturer (ID, Name, Place, Zip, Address, Description, Created, Guid) VALUES (7, 'Dell', NULL, NULL, NULL, 'Dell Technologies Inc. (vormals Dell Inc. bzw. Dell Computer Corporation', '2020-10-11 12:57:38', '9c7c1341-b8c3-4abf-bb87-d715c6d165bd');                            
INSERT INTO Manufacturer (ID, Name, Place, Zip, Address, Description, Created, Guid) VALUES (8, 'VEB Microelektronik Wilhelm Pieck Mühlhausen', NULL, NULL, NULL, 'Kombinat Mikroelektronik Erfurt', '2020-10-11 12:57:38', '18b0c9f7-df52-4550-83d2-0809ca8e15d9');                            

INSERT INTO Location (ID, Name, Place, Zip, Address, Building, Room, Description, Created, Guid) VALUES (1, 'Kiste 1',  NULL, NULL, NULL, NULL, NULL, NULL, '2020-10-11 14:53:19', '53c21bbc-4d40-4e0a-be59-513a12d07b08');
INSERT INTO Location (ID, Name, Place, Zip, Address, Building, Room, Description, Created, Guid) VALUES (2, 'Kiste 2',  NULL, NULL, NULL, NULL, NULL, NULL, '2020-10-11 14:53:19', '54318497-65e6-45f2-9d0a-39d4fbf794e7');
INSERT INTO Location (ID, Name, Place, Zip, Address, Building, Room, Description, Created, Guid) VALUES (3, 'Kiste 3',  NULL, NULL, NULL, NULL, NULL, NULL, '2020-10-11 14:53:19', '805c19cc-dbe1-4d23-8fe0-b9a3a4e6290c');
INSERT INTO Location (ID, Name, Place, Zip, Address, Building, Room, Description, Created, Guid) VALUES (4, 'Regal',  NULL, NULL, NULL, NULL, NULL, NULL, '2020-10-11 14:53:19', '495578f0-a16f-44d7-b3ed-4def8522b41e');

INSERT INTO Supplier (ID, Name, Place, Zip, Address, Description, Created, Guid) VALUES (1, 'Amazon',  NULL, NULL, NULL, 'Online-Marktplatz', '2020-10-11 14:53:19', '80dc29ee-2bb3-4ba3-b787-9a3e6fd7e2d1');
INSERT INTO Supplier (ID, Name, Place, Zip, Address, Description, Created, Guid) VALUES (2, 'eBay Inc.',  NULL, NULL, NULL, 'Online-Marktplatz', '2020-10-11 14:53:19', '7bc5e2ac-2de1-4512-bb6a-06928335bc97');
INSERT INTO Supplier (ID, Name, Place, Zip, Address, Description, Created, Guid) VALUES (3, 'eBay Kleinanzeigen',  NULL, NULL, NULL, 'Online-Marktplatz', '2020-10-11 14:53:19', '00884111-d29f-431b-a729-77ae1eee45aa');
INSERT INTO Supplier (ID, Name, Place, Zip, Address, Description, Created, Guid) VALUES (4, 'Flohmarkt',  NULL, NULL, NULL, 'Offline-Marktplatz', '2020-10-11 14:53:19', 'e06cc6fd-bcbb-46c9-b520-8fef185c8644');
INSERT INTO Supplier (ID, Name, Place, Zip, Address, Description, Created, Guid) VALUES (5, 'Conrad',  NULL, NULL, NULL, 'Conrad Electronic SE', '2020-10-11 14:53:19', 'caa05e58-39ec-4a06-be5d-cd88a0caadfc');
INSERT INTO Supplier (ID, Name, Place, Zip, Address, Description, Created, Guid) VALUES (6, 'Escom',  NULL, NULL, NULL, 'ESCOM Computer GmbH', '2020-10-11 14:53:19', 'e5e3b144-f47a-482d-a15b-8db1b204fa15');
INSERT INTO Supplier (ID, Name, Place, Zip, Address, Description, Created, Guid) VALUES (7, 'Media Markt',  NULL, NULL, NULL, 'MediaMartSaturn Retail Group', '2020-10-11 14:53:19', '1cb9ff91-9998-44fe-b128-7243b061bc93');
INSERT INTO Supplier (ID, Name, Place, Zip, Address, Description, Created, Guid) VALUES (8, 'O2',  NULL, NULL, NULL, '', '2020-10-11 14:53:19', '1d79f441-8730-48dc-98c7-9882d2de82db');
INSERT INTO Supplier (ID, Name, Place, Zip, Address, Description, Created, Guid) VALUES (9, 'Photo Porst',  NULL, NULL, NULL, '', '2020-10-11 14:53:19', 'a5a74266-b4bb-4cab-96b9-8a28ab2e1cb0');
INSERT INTO Supplier (ID, Name, Place, Zip, Address, Description, Created, Guid) VALUES (10, 'Quelle',  NULL, NULL, NULL, 'Quelle GmbH', '2020-10-11 14:53:19', '49f1c415-7ed0-4fa7-9d19-9b196f449e0a');
INSERT INTO Supplier (ID, Name, Place, Zip, Address, Description, Created, Guid) VALUES (11, 'Saturn',  NULL, NULL, NULL, 'MediaMartSaturn Retail Group', '2020-10-11 14:53:19', '151aa4f2-ef6e-4837-919f-0cbaf7e30c25');
INSERT INTO Supplier (ID, Name, Place, Zip, Address, Description, Created, Guid) VALUES (12, 'Vobis',  NULL, NULL, NULL, 'Vobis AG', '2020-10-11 14:53:19', '0de3af7d-faeb-48a3-9218-2f54ce66d4af');

INSERT INTO LedgerAccount (ID, Name, Description, Created, Guid) VALUES (1, 'Haushalt', '', '2020-10-11 14:24:32', '2ca06694-84e0-410d-997a-f71c5c95336b');

INSERT INTO CostCenter (ID, Name, Description, Created, Guid) VALUES (1, 'Fuhrpark', '', '2020-10-11 14:24:32', '11775459-6b52-4e1d-9455-7f2459b37fca');
INSERT INTO CostCenter (ID, Name, Description, Created, Guid) VALUES (2, 'Hobby', '', '2020-10-11 14:24:32', 'ad8ba6b1-46f5-4240-825b-a960bfd772b5');

INSERT INTO Template (ID, Name, Description, Created, Guid) VALUES (1, 'Druckmedien', 'Gedruckte Informationsquellen wie Zeitschriften, Zeitungen, Bücher oder Kataloge.', '2020-10-11 14:24:32', '7e64df5b-0947-4b57-9bf3-ada7a863c060');
INSERT INTO Template (ID, Name, Description, Created, Guid) VALUES (2, 'Heimcomputer', 'Mikrocomputer der 80-er Jahre, welche überwiegend im privaten Haushalten genutzt wurden.', '2020-10-11 14:24:32', 'dd459727-c0e3-485a-8fc0-ec64f2056467');
INSERT INTO Template (ID, Name, Description, Created, Guid) VALUES (3, 'Personal Computer', 'Mikrocomputer, welche überwiegend im geschäftlichem Umfeld genutzt wurden.', '2020-10-11 14:24:32', 'a55e8d78-7324-4ef1-9d9e-d4bc15e0a11c');
INSERT INTO Template (ID, Name, Description, Created, Guid) VALUES (4, 'Pocket PC', 'Handheld und PocketPSs', '2020-10-11 14:24:32', '2dfb9581-8480-4b4d-9e8e-a95f7ab00ff1');
INSERT INTO Template (ID, Name, Description, Created, Guid) VALUES (5, 'Radio-, Kasetten- und Tonbandgeräte', 'Audiogeräte', '2020-10-11 14:24:32', 'e4dbcedf-994c-4ac7-8dd5-5eb2c257c0ac');
INSERT INTO Template (ID, Name, Description, Created, Guid) VALUES (6, 'Smartphone', 'Handy', '2020-10-11 14:24:32', '7d8d8b16-67d7-4e2c-946d-c4a5254710b9');
INSERT INTO Template (ID, Name, Description, Created, Guid) VALUES (7, 'Software', 'Computerprogramme', '2020-10-11 14:24:32', 'fb939b76-bdb9-4766-9cad-9813c13f2236');
INSERT INTO Template (ID, Name, Description, Created, Guid) VALUES (8, 'Spiel', 'Computerspiele', '2020-10-11 14:24:32', '6dbbc3f8-9f97-44ed-9bb3-0bc3c3b01d5b');
INSERT INTO Template (ID, Name, Description, Created, Guid) VALUES (9, 'Spielekonsole', '', '2020-10-11 14:24:32', '17540bfc-e734-4834-8122-c914e2cf5b17');
INSERT INTO Template (ID, Name, Description, Created, Guid) VALUES (10, 'Telespiel', 'Handheld Spielekonsole', '2020-10-11 14:24:32', 'c69e6da8-286f-4556-b49d-60e849dfbd35');
INSERT INTO Template (ID, Name, Description, Created, Guid) VALUES (11, 'Tischrechner', 'Rechenmaschienen und Taschenrechner', '2020-10-11 14:24:32', 'c827d356-7ea4-4bbd-86f2-127fa786a279');
INSERT INTO Template (ID, Name, Description, Created, Guid) VALUES (12, 'Zubehör', 'Computerzubehör', '2020-10-11 14:24:32', 'd47a028e-0635-458d-b16e-e407d4d5c597');

INSERT INTO Attribute (ID, Name, Description, Created, Guid) VALUES (1, 'ISBN', '', '2020-10-11 14:24:32', '79ad94bb-2e16-4ddc-8018-e67b6401adaa');
INSERT INTO Attribute (ID, Name, Description, Created, Guid) VALUES (2, 'Autor', '', '2020-10-11 14:24:32', 'bdbff0b7-4b44-4c04-9c97-009f3dc58f14');
INSERT INTO Attribute (ID, Name, Description, Created, Guid) VALUES (3, 'Baujahr', '', '2020-10-11 14:24:32', 'dbdab181-bd64-4328-98e6-7ff2f47b3842');
INSERT INTO Attribute (ID, Name, Description, Created, Guid) VALUES (4, 'Betriebssystem', '', '2020-10-11 14:24:32', '85bdcacf-4cc4-4b16-ba5f-7d5f7cecac17');
INSERT INTO Attribute (ID, Name, Description, Created, Guid) VALUES (5, 'Gewicht', '', '2020-10-11 14:24:32', '6d140a1f-da15-4885-b601-78dae67f68f5');
INSERT INTO Attribute (ID, Name, Description, Created, Guid) VALUES (6, 'Hauptspeicher', '', '2020-10-11 14:24:32', 'edfaefe9-5c23-4649-b09a-8a3ab7cbdea7');
INSERT INTO Attribute (ID, Name, Description, Created, Guid) VALUES (7, 'Kaufdatum', '', '2020-10-11 14:24:32', 'f1edf0ea-c625-44ca-82bc-7ad6ee2785aa');
INSERT INTO Attribute (ID, Name, Description, Created, Guid) VALUES (8, 'Lizenzschlüssel', '', '2020-10-11 14:24:32', '8f68be66-2883-43e6-a4bf-ab3f5ad9e955');
INSERT INTO Attribute (ID, Name, Description, Created, Guid) VALUES (9, 'Medium', '', '2020-10-11 14:24:32', '619fa976-de3e-4477-b39a-ad7a50502acc');
INSERT INTO Attribute (ID, Name, Description, Created, Guid) VALUES (10, 'Modell', '', '2020-10-11 14:24:32', '66a24c4d-b579-4867-82d6-6a4cdad3efae');
INSERT INTO Attribute (ID, Name, Description, Created, Guid) VALUES (11, 'Preis', '', '2020-10-11 14:24:32', '6cd4cbd0-f73f-4a38-a0b1-d5cc74c4d17a');
INSERT INTO Attribute (ID, Name, Description, Created, Guid) VALUES (12, 'Prozessor', '', '2020-10-11 14:24:32', 'ea9aea66-39a3-4303-9ec7-32e0e5077872');
INSERT INTO Attribute (ID, Name, Description, Created, Guid) VALUES (13, 'Prüfdatum', '', '2020-10-11 14:24:32', '6cd231a5-2fcb-4ec8-9038-4692d87d42b5');
INSERT INTO Attribute (ID, Name, Description, Created, Guid) VALUES (14, 'Seriennummer', '', '2020-10-11 14:24:32', '184cc9de-91ed-4bba-b10f-07225c33fcfd');
INSERT INTO Attribute (ID, Name, Description, Created, Guid) VALUES (15, 'Taktrate', '', '2020-10-11 14:24:32', '2acdd6ba-f8c5-490d-87b4-e16f2b1a41e3');
INSERT INTO Attribute (ID, Name, Description, Created, Guid) VALUES (16, 'URL', '', '2020-10-11 14:24:32', 'a8393edc-0881-47fa-a11b-1efe465f3beb');
INSERT INTO Attribute (ID, Name, Description, Created, Guid) VALUES (17, 'Verpackung', '', '2020-10-11 14:24:32', 'fbabf826-e05f-46b7-972b-d0532714fd28');

INSERT INTO TemplateAttribute (TemplateID, AttributeID) VALUES (2, 3);
INSERT INTO TemplateAttribute (TemplateID, AttributeID) VALUES (2, 12);
INSERT INTO TemplateAttribute (TemplateID, AttributeID) VALUES (2, 14);
INSERT INTO TemplateAttribute (TemplateID, AttributeID) VALUES (2, 15);
INSERT INTO TemplateAttribute (TemplateID, AttributeID) VALUES (2, 3);

INSERT INTO Inventory (ID, Name, ManufacturerID, SupplierID, LedgerAccountID, CostCenterID, ConditionID, CostValue, Description, Created, Guid) VALUES (1, 'KC compact', 8, 2, 1, 2, 5, 600, 'letzter DDR-Heimcomputer', '2020-10-11 14:24:32', 'd24e656a-186a-4936-bb81-940e08d75ab1');