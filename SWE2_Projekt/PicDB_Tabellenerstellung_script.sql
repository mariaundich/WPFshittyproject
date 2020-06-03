CREATE TABLE dbo.FotografInnen  
(  
    ID_FotografIn int IDENTITY (1, 1) NOT NULL,  
    Vorname varchar(100) NOT NULL,  
    Nachname varchar(50) NOT NULL,
    Geburtsdatum datetime NOT NULL,
    Notiz text,
    PRIMARY KEY CLUSTERED (ID_FotografIn ASC)
); 

ALTER TABLE FotografInnen ADD CONSTRAINT Geburtsdatum CHECK (Geburtsdatum BETWEEN '1930-01-01' AND '2020-05-23');

CREATE TABLE dbo.ITPC
(
    ID_ITPC int IDENTITY (1, 1) NOT NULL,
    Titel varchar(100) NOT NULL,
    Urheber varchar(150)Kom NULL,
    Beschreibung text,
    PRIMARY KEY CLUSTERED (ID_ITPC ASC)
);

CREATE TABLE dbo.EXIF
(
    ID_EXIF int IDENTITY (1, 1) NOT NULL,
    Kameramodell varchar(100),
    Auflösung varchar(50),
    Datum datetime,
    Ort varchar(100),
    Land varchar(100),
    PRIMARY KEY CLUSTERED (ID_EXIF ASC)
);

CREATE TABLE dbo.Tags
(
    ID_Tag int IDENTITY (1, 1) NOT NULL,
    Bezeichnung varchar(100) NOT NULL,
    PRIMARY KEY CLUSTERED (ID_Tag ASC)
);

CREATE TABLE dbo.Bilder
(
    ID_Bild int IDENTITY (1, 1) NOT NULL,
    Titel varchar(100) NOT NULL,
    fk_FotografIn_ID int,
    fk_EXIF_ID int,
    fk_ITPC_ID int,
    foreign key (fk_FotografIn_ID) references FotografInnen (ID_FotografIn) on delete set null,
    foreign key (fk_EXIF_ID) references EXIF (ID_EXIF) on delete set null,
    foreign key (fk_ITPC_ID) references ITPC (ID_ITPC) on delete set null,
    PRIMARY KEY CLUSTERED (ID_Bild ASC)
);

CREATE TABLE Bild_Tag
(
    fk_Bild_ID int NOT NULL,
    fk_Tag_ID int NOT NULL,
    PRIMARY KEY CLUSTERED (fk_Bild_ID, fk_Tag_ID  ASC),
    foreign key (fk_Bild_ID) references Bilder (ID_Bild),
    foreign key (fk_Tag_ID) references Tags (ID_Tag),
);