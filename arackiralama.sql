
CREATE DATABASE KubilaySimsek
GO 
USE KubilaySimsek
GO
CREATE TABLE Uye (
ID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
Eposta NVARCHAR(30) NOT NULL,
Sifre NVARCHAR(50) NOT NULL,
Yetki INT NOT NULL
);

CREATE TABLE UyeBilgi(
TC NVARCHAR(11) PRIMARY KEY NOT NULL,
UyeID INT FOREIGN KEY REFERENCES Uye(ID) NOT NULL, --FK -> Uye
Ad NVARCHAR(20) NOT NULL,
Soyad NVARCHAR(20) NOT NULL,
DogumTarihi DATE NOT NULL,
Cinsiyet NVARCHAR(1) NOT NULL,--E(erkek),K(Kadýn)
Sehir NVARCHAR(15) NOT NULL,
Adres NVARCHAR(120) NOT NULL,
Telefon NVARCHAR(11) NOT NULL,
EhliyetSinifi NVARCHAR(3) NOT NULL,--B,B1 vs.
EhliyetYili INT NOT NULL,
KayýtTarihi DATETIME DEFAULT GETDATE()
);

CREATE TABLE SubeBilgi(
ID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
UyeID INT FOREIGN KEY REFERENCES Uye(ID) NOT NULL,--FK -> Uye
Ad NVARCHAR(20) NOT NULL,
Sehir NVARCHAR(20) NOT NULL,
Ilce NVARCHAR(20) NOT NULL,
Semt NVARCHAR(20) NOT NULL,
Telefon NVARCHAR(11) NOT NULL,
Adres NVARCHAR(120) NOT NULL,
Tarih DATETIME DEFAULT GETDATE()
);

CREATE TABLE AracKategori (
ID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
Kategori NVARCHAR(20) NOT NULL
);

CREATE TABLE AracMarka (
ID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
Marka NVARCHAR (20) NOT NULL
);

CREATE TABLE MarkaKategori (
ID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
KategoriID INT FOREIGN KEY REFERENCES AracKategori(ID) NOT NULL, --FK -> Kategori
MarkaID INT FOREIGN KEY REFERENCES AracMarka(ID) NOT NULL, --FK -> Marka
);

CREATE TABLE AracModel(
ID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
MarkaKategoriID INT FOREIGN KEY REFERENCES MarkaKategori(ID) NOT NULL,--FK -> MarkaKategori
Model NVARCHAR(20) NOT NULL
);

CREATE TABLE AracMotor(
ID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
ModelID INT FOREIGN KEY REFERENCES AracModel(ID) NOT NULL,--FK -> Model
Yakit NVARCHAR(15) NOT NULL,
Hacim NVARCHAR(10) NOT NULL,
Guc NVARCHAR(20) NOT NULL,
Sanziman NVARCHAR(10) NOT NULL
);

CREATE TABLE Arac(
Plaka NVARCHAR(10) PRIMARY KEY NOT NULL,
SubeID INT FOREIGN KEY REFERENCES SubeBilgi(ID) NOT NULL,--FK -> Sube
ModelID INT FOREIGN KEY REFERENCES AracModel(ID) NOT NULL,--FK -> Model
ModelYili INT NOT NULL,
Renk NVARCHAR(15) NOT NULL,
Kilometre DECIMAL(18,3) NOT NULL,
Donanim NVARCHAR(70),
Resim  IMAGE,
Durum NVARCHAR(20) DEFAULT 'Müsait',
Tarih DATETIME DEFAULT GETDATE()
);

CREATE TABLE Ucret (
ID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
Plaka NVARCHAR(10) FOREIGN KEY REFERENCES Arac(Plaka) NOT NULL,-- FK -> ARAC
Ucret DECIMAL(18,2) NOT NULL,
UyeUcret DECIMAL (18,2)
);

CREATE TABLE Odeme(
ID INT PRIMARY KEY IDENTITY(1,1),
OdemeTuru NVARCHAR(15) NOT NULL,
OdenenUcret DECIMAL(18,2) NOT NULL
);

CREATE TABLE Kiralama(
TakipKod NVARCHAR(10) PRIMARY KEY NOT NULL,
TC NVARCHAR(11) NOT NULL,
Plaka NVARCHAR(10) FOREIGN KEY REFERENCES Arac(Plaka) NOT NULL,
OdemeID INT FOREIGN KEY REFERENCES Odeme(ID) NOT NULL,
BaslangicTarihi DATE NOT NULL,
BitisTarihi DATE NOT NULL,
Aciklama NVARCHAR(50),
KiraDurumu NVARCHAR(20),
Uyelik NVARCHAR(6),
IslemTarih DATETIME DEFAULT GETDATE()
);

CREATE TABLE UyeOlmadanKiralama(
TC NVARCHAR(11) PRIMARY KEY NOT NULL,
Ad NVARCHAR(20) NOT NULL,
Soyad NVARCHAR(20) NOT NULL,
DogumTarihi DATE NOT NULL,
Cinsiyet NVARCHAR(1) NOT NULL,--E(erkek),K(Kadýn)
Adres NVARCHAR(120) NOT NULL,
Telefon NVARCHAR(11) NOT NULL,
EhliyetSinifi NVARCHAR(3) NOT NULL,--B,B1 vs.
EhliyetYili INT NOT NULL,
);

--ALTER TABLE Kiralama ADD CONSTRAINT fk_kira_1 FOREIGN KEY (TC) REFERENCES UyeBilgi(TC)
--ALTER TABLE Kiralama ADD CONSTRAINT fk_kira_2 FOREIGN KEY (TC) REFERENCES UyeOlmadanKiralama(TC)

CREATE TABLE IptalEdilenKiralama(
ID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
TakipKod NVARCHAR(10) FOREIGN KEY REFERENCES Kiralama(TakipKod) NOT NULL,--FK -> UyeKiralama,UyeOlmadan Kiralama
Sebep NVARCHAR(30),
Aciklama NVARCHAR(120),
Tarih DATETIME DEFAULT GETDATE()
);

CREATE TABLE BitenKiralama( --TRIGGER KULLANILACAK
ID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
TakipKod NVARCHAR(10) FOREIGN KEY REFERENCES Kiralama(TakipKod) NOT NULL,
);


CREATE TABLE TeslimAlinan(
ID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
TakipKod NVARCHAR(10) FOREIGN KEY REFERENCES Kiralama(TakipKod) NOT NULL,
HasarKontrol NVARCHAR(100),
Tarih DATETIME DEFAULT GETDATE()
);



CREATE TABLE SubeIstatistik(
SubeID INT FOREIGN KEY REFERENCES SubeBilgi(ID),--FK -> SubeBilgi
AracSayisi INT DEFAULT 0,
KiralamaSayisi INT DEFAULT 0,
KiradakiAracSayisi INT DEFAULT 0,
MusaitAracSayisi INT DEFAULT 0,
ToplamAlinanUcret DECIMAL(18,2) DEFAULT 0,
SikayetSayisi INT DEFAULT 0
);

CREATE TABLE Mesaj (
ID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
SubeID INT FOREIGN KEY REFERENCES SubeBilgi(ID) NOT NULL,--FK -> SubeBilgi
UyeTC NVARCHAR(11) FOREIGN KEY REFERENCES UyeBilgi(TC) NOT NULL,--FK -> UyeBilgi
Konu NVARCHAR(20) NOT NULL,
Mesaj NVARCHAR(120) NOT NULL,
Tarih DATETIME DEFAULT GETDATE()
);

CREATE TABLE Sikayet(
ID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
SubeID INT FOREIGN KEY REFERENCES SubeBilgi(ID) NOT NULL,--FK -> SubeBilgi
UyeTC NVARCHAR(11) FOREIGN KEY REFERENCES UyeBilgi(TC) NOT NULL,--FK -> UyeBilgi
Konu NVARCHAR(20) NOT NULL,
Sikayet NVARCHAR(120) NOT NULL,
Tarih DATETIME DEFAULT GETDATE()
);

--                                                                                VÝEW BÖLÜMÜ

GO
CREATE VIEW AracGoster
AS
SELECT S.ID,S.Ad,A.Plaka,K.Kategori,M.Marka,MDL.Model,A.ModelYili,MO.Yakit,MO.Hacim,MO.Guc,MO.Sanziman,A.Renk,A.Kilometre,A.Donanim,U.Ucret,A.Durum
	FROM AracKategori K
		INNER JOIN MarkaKategori MK ON MK.KategoriID=K.ID
			INNER JOIN AracMarka M ON M.ID=MK.MarkaID
				INNER JOIN AracModel MDL ON MDL.MarkaKategoriID=MK.ID
					INNER JOIN AracMotor MO ON MO.ModelID=MDL.ID
						INNER JOIN Arac A ON A.ModelID=MDL.ID
							INNER JOIN SubeBilgi S ON S.ID=A.SubeID
								INNER JOIN Ucret U ON U.Plaka=A.Plaka
GO
CREATE VIEW UyeKiralamaGoster
AS
SELECT K.TakipKod,U.TC,U.Ad,U.Soyad,A.Plaka,A.Marka,A.Model,K.BaslangicTarihi,K.BitisTarihi,O.OdemeTuru,O.OdenenUcret
	FROM Kiralama K,UyeBilgi U,AracGoster A,Odeme O
		WHERE U.TC=K.TC AND K.Plaka=A.Plaka AND O.ID=K.OdemeID
GO
CREATE VIEW UyeOlmayanKiralamaGoster
AS
SELECT K.TakipKod,U.TC,U.Ad,U.Soyad,A.Plaka,A.Marka,A.Model,K.BaslangicTarihi,K.BitisTarihi,O.OdemeTuru,O.OdenenUcret
	FROM Kiralama K,UyeOlmadanKiralama U,AracGoster A,Odeme O
		WHERE U.TC=K.TC AND K.Plaka=A.Plaka AND O.ID=K.OdemeID

GO

CREATE VIEW UyeGoster
AS
SELECT U.Eposta,UB.TC,UB.Ad,UB.Soyad,UB.Cinsiyet,UB.DogumTarihi,UB.Sehir,UB.Adres,UB.Telefon,UB.EhliyetSinifi,UB.EhliyetYili,UB.KayýtTarihi FROM Uye U
	INNER JOIN UyeBilgi UB ON UB.UyeID=U.ID
GO
CREATE VIEW SubeGoster
AS
SELECT U.Eposta,S.Ad,S.Sehir,S.Ilce,S.Semt,S.Adres,S.Telefon,S.Tarih FROM SubeBilgi S
	INNER JOIN Uye U ON U.ID=S.UyeID
GO

CREATE VIEW SubeSikayetler 
AS
SELECT SB.Ad,COUNT(S.Sikayet) AS 'SikayetSayisi' FROM Sikayet S
	INNER JOIN SubeBilgi SB ON SB.ID=S.SubeID
		GROUP BY(SB.Ad)
GO

--                                                                                TRIGGER BÖLÜMÜ 

GO


CREATE TRIGGER KiraDurum ON Kiralama
AFTER INSERT
AS
DECLARE @Takip NVARCHAR(10) SET @Takip=(SELECT TOP 1 TakipKod FROM Kiralama ORDER BY IslemTarih DESC)
DECLARE @TC NVARCHAR(11) SET @TC=(SELECT TOP 1 TC FROM Kiralama ORDER BY IslemTarih DESC)
IF EXISTS(SELECT TC FROM UyeBilgi WHERE TC=@TC)
BEGIN
	UPDATE Kiralama SET Uyelik='Var' WHERE TC=@TC
END
ELSE
BEGIN
	UPDATE Kiralama SET Uyelik='Yok' WHERE TC=@TC
END
IF EXISTS(SELECT K.TakipKod FROM Kiralama K WHERE K.BaslangicTarihi > CONVERT(DATE,GETDATE()) AND TakipKod=@Takip)
BEGIN
	UPDATE Kiralama SET KiraDurumu='Baþlamadý' WHERE TakipKod=@Takip
END
ELSE
BEGIN
	UPDATE Kiralama SET KiraDurumu='Kirada'	WHERE TakipKod=@Takip
END

GO
CREATE TRIGGER UcretHesapla ON Ucret  --Günlük ücreti girilen aracýn online ödeme ve üyeye özel indirim yapma
AFTER INSERT
AS 
DECLARE @ID INT SET @ID=(SELECT MAX(ID) FROM Ucret)
DECLARE @Ucret DECIMAL(18,2) SET @Ucret=(SELECT Ucret FROM Ucret WHERE ID=@ID)
DECLARe @Uye DECIMAL(18,2) SET @Uye=(SELECT @Ucret-((@Ucret*15)/100))
UPDATE Ucret SET UyeUcret=@Uye WHERE ID=@ID
GO

CREATE TRIGGER TeslimSonrasiDurum ON TeslimAlinan --Teslim sonrasý aracýn durumunu müsait yapma
AFTER INSERT
AS
DECLARE @TkpKod NVARCHAR(10) SET @TkpKod=(SELECT TOP 1 TakipKod FROM TeslimAlinan ORDER BY Tarih DESC) --SON EKLENEN KAYIT
UPDATE Arac SET Durum='Müsait' WHERE Plaka=(SELECT Plaka FROM Kiralama WHERE TakipKod=@TkpKod)
UPDATE Kiralama SET KiraDurumu='Tamamlandý' WHERE TakipKod=@TkpKod
GO

CREATE TRIGGER AracDurum ON Kiralama  --Kiralama yapýlan aracýn durumunu 'kirada' yapma
AFTER INSERT
AS
DECLARE @TkpKod NVARCHAR(10) SET @TkpKod=(SELECT TOP 1 TakipKod FROM Kiralama ORDER BY IslemTarih DESC) --SON EKLENEN KAYIT
UPDATE Arac SET Durum='Kirada' WHERE Plaka=(SELECT Plaka FROM Kiralama WHERE TakipKod=@TkpKod)
GO

CREATE TRIGGER UyeOlmayanBilgiSil ON Kiralama
AFTER UPDATE
AS
DELETE FROM UyeOlmadanKiralama WHERE TC IN (SELECT TC FROM Kiralama WHERE KiraDurumu='Teslim Alýndý')
GO

CREATE TRIGGER AracDurum2 ON Kiralama
AFTER UPDATE
AS
UPDATE Arac SET Durum='Müsait' WHERE Plaka IN (SELECT Plaka FROM Kiralama WHERE KiraDurumu='Ýptal Edildi')
GO

CREATE TRIGGER SubeIstatistikEkle ON SubeBilgi
AFTER INSERT
AS
IF NOT EXISTS(SELECT SubeID FROM SubeIstatistik WHERE SubeID IN(SELECT ID FROM SubeBilgi))
BEGIN
	INSERT INTO SubeIstatistik(SubeID) VALUES ((SELECT IDENT_CURRENT('SubeBilgi')))
END


--                                                                                PROCEDURE BOLUMU
GO
-- ARAÇ EKLEME
CREATE PROC SP_AracEkle
(
@Plaka NVARCHAR(10),
@SubeID INT ,
@Renk NVARCHAR(15),
@ModelYili INT,
@Kilometre DECIMAL(18,4),
@Donanim NVARCHAR(70),
@Resim  IMAGE=NULL,
@MarkaID INT,
@KategoriID INT,
@Model NVARCHAR(20),
@Yakit NVARCHAR(15),
@Hacim NVARCHAR(10),
@Guc NVARCHAR(20),
@Sanziman NVARCHAR(10),
@Ucret DECIMAL(18,2)
)
AS
IF EXISTS(SELECT * FROM Arac WHERE Plaka=@Plaka) --EÐER PLAKA VARSA
	BEGIN
		RETURN 0
	END
ELSE
	BEGIN		
		DECLARE @MKID INT SET @MKID=(SELECT MK.ID FROM MarkaKategori MK WHERE MK.KategoriID=@KategoriID AND MK.MarkaID=@MarkaID)
		INSERT INTO AracModel VALUES (@MKID,@Model)
		DECLARE @ModelID INT SET @ModelID=(SELECT MAX(ID) FROM AracModel) --SON EKLENEN KAYIT
		INSERT INTO AracMotor VALUES (@ModelID,@Yakit,@Hacim,@Guc,@Sanziman)
		INSERT INTO Arac(Plaka,SubeID,ModelID,ModelYili,Renk,Kilometre,Donanim,Resim) VALUES (@Plaka,@SubeID,@ModelID,@ModelYili,@Renk,@Kilometre,@Donanim,@Resim)
		INSERT INTO Ucret(Plaka,Ucret) VALUES (@Plaka,@Ucret)
		RETURN 1
	END
GO
-- ARAÇ ARAMA
CREATE PROC SP_AracAra
(
@AramaMetni NVARCHAR(30)
)
AS
SELECT * FROM AracGoster WHERE Kategori 
	LIKE '%'+@AramaMetni+'%'
		OR Marka LIKE '%'+@AramaMetni+'%'
			OR Model LIKE '%'+@AramaMetni+'%'
				OR Yakit LIKE '%'+@AramaMetni+'%'
					OR Hacim LIKE '%'+@AramaMetni+'%'
						OR Sanziman LIKE '%'+@AramaMetni+'%'
							OR Ad LIKE '%'+@AramaMetni+'%'
								OR Plaka LIKE '%'+@AramaMetni+'%'
									OR Durum LIKE '%'+@AramaMetni+'%'

GO
-- ÜCRET EKLEME
CREATE PROC SP_UcretEkle
(
@Ucret DECIMAL(18,2),
@Plaka NVARCHAR(10)
)
AS
INSERT INTO Ucret(Plaka,Ucret) VALUES (@Plaka,@Ucret)

GO
-- ÜYE EKLEME
CREATE PROC SP_UyeEkle
(
@Eposta NVARCHAR(30),
@Sifre NVARCHAR(50),
@TC NVARCHAR(11),
@Ad NVARCHAR(20),
@Soyad NVARCHAR(20),
@DogumTarihi DATE,
@Cinsiyet NVARCHAR(1),
@Sehir NVARCHAR(15),
@Adres NVARCHAR(120),
@Telefon NVARCHAR(11),
@EhliyetSinifi NVARCHAR(3),
@EhliyetYili INT
)
AS
IF EXISTS(SELECT * FROM Uye WHERE Eposta=@Eposta)
	BEGIN
		RETURN 0
	END
ELSE IF EXISTS(SELECT * FROM UyeBilgi WHERE TC=@TC)
	BEGIN
		RETURN 1
	END
ELSE
	BEGIN				
		INSERT INTO Uye VALUES (@Eposta,@Sifre,1) --YETKÝ ALANI -> 1=ÜYE
		DECLARE @UyeID INT SET @UyeID=(SELECT ID FROM Uye WHERE Eposta=@Eposta)
		INSERT INTO UyeBilgi(UyeID,TC,Ad,Soyad,DogumTarihi,Cinsiyet,Sehir,Adres,Telefon,EhliyetSinifi,EhliyetYili) VALUES (@UyeID,@TC,@Ad,@Soyad,@DogumTarihi,@Cinsiyet,@Sehir,@Adres,@Telefon,@EhliyetSinifi,@EhliyetYili)
		RETURN 2
	END

GO
-- Þube EKLEME
CREATE PROC SP_SubeEkle
(
@Eposta NVARCHAR(30),
@Sifre NVARCHAR(50),
@Ad NVARCHAR(20),
@Sehir NVARCHAR(20),
@Ilce NVARCHAR(20),
@Semt NVARCHAR(20),
@Telefon NVARCHAR(11),
@Adres NVARCHAR(120)
)
AS
IF EXISTS(SELECT * FROM Uye WHERE Eposta=@Eposta)
	BEGIN
		RETURN 0
	END
ELSE
	BEGIN				
		INSERT INTO Uye VALUES (@Eposta,@Sifre,2)--YETKÝ ALANI -> 2=ÞUBE
		DECLARE @UyeID INT SET @UyeID=(SELECT ID FROM Uye WHERE Eposta=@Eposta)
		INSERT INTO SubeBilgi VALUES (@UyeID,@Ad,@Sehir,@Ilce,@Semt,@Telefon,@Adres,GETDATE())
		RETURN 1
	END

GO
--Yönetici Ekleme

CREATE PROC SP_YoneticiEkle
(
@Eposta NVARCHAR(30),
@Sifre NVARCHAR(50)
)
AS
IF EXISTS(SELECT * FROM Uye WHERE Eposta=@Eposta)
	BEGIN
		RETURN 0
	END
ELSE
	BEGIN				
		INSERT INTO Uye VALUES (@Eposta,@Sifre,3)--YETKÝ ALANI -> 3=Yönetici
		RETURN 1
	END

GO
-- MESAJ EKLEME
CREATE PROC SP_MesajGonder
(
@UyeTC NVARCHAR(11),
@SubeID INT,
@Konu NVARCHAR(20),
@Mesaj NVARCHAR(120)
)
AS 
INSERT INTO Mesaj(UyeTC,SubeID,Konu,Mesaj) VALUES (@UyeTC,@SubeID,@Konu,@Mesaj)

GO
-- ÞÝKAYET EKLEME
CREATE PROC SP_SikayetGonder
(
@SubeID INT,
@UyeTC NVARCHAR(11),
@Konu NVARCHAR(30),
@Sikayet NVARCHAR(120)
)
AS 
INSERT INTO Sikayet(UyeTC,SubeID,Konu,Sikayet) VALUES (@UyeTC,@SubeID,@Konu,@Sikayet)

GO
-- ÞUBE GÖSTERME
CREATE PROC SP_SubeBilgiGoster
(
@ID INT
)
AS
SELECT U.Eposta,U.Sifre,S.Ad,S.Sehir,S.Semt,S.Ilce,S.Telefon,S.Adres FROM SubeBilgi S
	INNER JOIN Uye U ON S.UyeID=U.ID
		WHERE S.ID=@ID

GO
-- KÝRALAMA EKLEME 1
CREATE PROC SP_UyeyeKirala
(
@TakipKod NVARCHAR(15),
@UyeTC NVARCHAR(11),
@Plaka NVARCHAR(10),
@BaslangicTarihi DATE,
@BitisTarihi DATE,
@OdemeTuru NVARCHAR(15),
@OdenenUcret DECIMAL(18,2),
@Aciklama NVARCHAR(50)
)
AS
IF EXISTS(SELECT K.TC FROM Kiralama K WHERE BitisTarihi > CONVERT(DATE,GETDATE()) AND K.TC=@UyeTC AND (K.KiraDurumu='Kirada' OR K.KiraDurumu='Baþlamadý'))
BEGIN 
	RETURN 0
END
ELSE
BEGIN
	INSERT INTO Odeme VALUES (@OdemeTuru,@OdenenUcret)
	DECLARE @OdemeID INT SET @OdemeID=(SELECT MAX(Odeme.ID) FROM Odeme)
	INSERT INTO Kiralama VALUES (@TakipKod,@UyeTC,@Plaka,@OdemeID,@BaslangicTarihi,@BitisTarihi,@Aciklama,'Kirada','Var',GETDATE())
	RETURN 1
END
GO

GO
-- KÝRALAMA EKLEME 2
CREATE PROC SP_UyeOlmayanaKirala
(
@TakipKod NVARCHAR(15),
@Plaka NVARCHAR(10),
@TC NVARCHAR(11),
@Ad NVARCHAR(20),
@Soyad NVARCHAR(20),
@DogumTarihi DATE,
@Cinsiyet NVARCHAR(1),
@Adres NVARCHAR(120),
@Telefon NVARCHAR(11),
@EhliyetSinifi NVARCHAR(3),
@EhliyetYili INT,
@BaslangicTarihi DATE,
@BitisTarihi DATE,
@OdemeTuru NVARCHAR(15),
@OdenenUcret DECIMAL(18,2),
@Aciklama NVARCHAR(50)
)
AS
IF EXISTS(SELECT * FROM UyeOlmadanKiralama U WHERE U.TC=@TC)
BEGIN
	RETURN 0
END
ELSE
BEGIN
	INSERT INTO Odeme VALUES (@OdemeTuru,@OdenenUcret)
	DECLARE @OdemeID INT SET @OdemeID=(SELECT MAX(Odeme.ID) FROM Odeme)
	INSERT INTO UyeOlmadanKiralama VALUES (@TC,@Ad,@Soyad,@DogumTarihi,@Cinsiyet,@Adres,@Telefon,@EhliyetSinifi,@EhliyetYili)
	INSERT INTO Kiralama VALUES (@TakipKod,@TC,@Plaka,@OdemeID,@BaslangicTarihi,@BitisTarihi,@Aciklama,'Kirada','Yok',GETDATE())
	RETURN 1
END

GO


GO
-- KÝRALAMA ÝPTAL
CREATE PROC SP_KiraIptal
(
@TakipKod NVARCHAR(15),
@Sebep NVARCHAR(20),
@Aciklama NVARCHAR(100)
)
AS
INSERT INTO IptalEdilenKiralama VALUES (@TakipKod,@Sebep,@Aciklama,GETDATE())
UPDATE Kiralama SET KiraDurumu='Ýptal Edildi' WHERE TakipKod=@TakipKod



GO
-- KÝRALAMA SÜRE UZATMA

CREATE PROC SP_KiraSureUzat
(
@TakipKod NVARCHAR(15),
@YeniTarih DATE,
@YeniUcret DECIMAL(18,2)
)
AS
UPDATE Kiralama SET BitisTarihi=@YeniTarih WHERE TakipKod=@TakipKod
UPDATE Odeme SET OdenenUcret=@YeniUcret WHERE ID=(SELECT OdemeID FROM Kiralama WHERE TakipKod=@TakipKod)

GO

GO				
CREATE PROC SP_UyeGiris
(
@Eposta NVARCHAR(30),
@Sifre NVARCHAR(15)
)
AS
IF EXISTS(SELECT * FROM Uye WHERE Eposta=@Eposta AND Sifre=@Sifre)
	BEGIN
		DECLARE @yetki INT SET @yetki=(SELECT Yetki FROM Uye WHERE Eposta=@Eposta AND Sifre=@Sifre)
		RETURN @yetki
	END
ELSE 
	BEGIN
		RETURN 0
	END
GO


CREATE PROC SP_MarkaListele
(
@KategoriID INT
)
AS
SELECT M.ID,M.Marka FROM AracMarka M
	INNER JOIN MarkaKategori MK ON MK.MarkaID=M.ID
		WHERE MK.KategoriID=@KategoriID

GO
CREATE PROC SP_SubeGuncelle
(
@ID INT,
@Eposta NVARCHAR(30),
@Sifre NVARCHAR(50),
@Ad NVARCHAR(20),
@Sehir NVARCHAR(20),
@Ilce NVARCHAR(20),
@Semt NVARCHAR(20),
@Telefon NVARCHAR(11),
@Adres NVARCHAR(120)
)
AS
DECLARE @UyeID INT SET @UyeID=(SELECT UyeID FROM SubeBilgi WHERE ID=@ID)
UPDATE Uye SET Eposta=@Eposta,Sifre=@Sifre WHERE ID=@UyeID
UPDATE SubeBilgi SET Ad=@Ad,Sehir=@Sehir,Ilce=@Ilce,Semt=@Semt,Telefon=@Telefon,Adres=@Adres WHERE ID=@ID
GO

CREATE PROC SP_UyeAra
(
@AramaMetni NVARCHAR(30)
)
AS
SELECT * FROM UyeGoster WHERE Eposta
	LIKE '%'+@AramaMetni+'%'
		OR Ad LIKE '%'+@AramaMetni+'%'
			OR Soyad LIKE '%'+@AramaMetni+'%'
				OR Sehir LIKE '%'+@AramaMetni+'%'

GO

CREATE PROC SP_SubeAra
(
@AramaMetni NVARCHAR(30)
)
AS
SELECT * FROM SubeGoster WHERE Eposta
	LIKE '%'+@AramaMetni+'%'
		OR Ad LIKE '%'+@AramaMetni+'%'
			OR Sehir LIKE '%'+@AramaMetni+'%'
				OR Ilce LIKE '%'+@AramaMetni+'%'
					OR Semt LIKE '%'+@AramaMetni+'%'

GO

CREATE PROC SP_UyeIstatistikleri
AS
SELECT COUNT(U.TC) AS 'UyeSayi'  FROM UyeBilgi U
SELECT COUNT(U.Cinsiyet) AS 'EUyeSayi'  FROM UyeBilgi U WHERE Cinsiyet='E'
SELECT COUNT(U.Cinsiyet) AS 'KUyeSayi'  FROM UyeBilgi U WHERE Cinsiyet='K'
SELECT COUNT(U.TC) AS 'UyeKiralama' FROM Kiralama K,UyeBilgi U WHERE K.TC=U.TC
SELECT TOP 1 Ad,Soyad FROM UyeBilgi ORDER BY KayýtTarihi DESC


GO
CREATE PROC SP_SubeIstatistikleri
(
@SubeID INT
)
AS
IF EXISTS(SELECT * FROM UyeBilgi)
BEGIN
	SELECT COUNT(Plaka) AS 'AracSayi'  FROM Arac WHERE SubeID=@SubeID
	SELECT COUNT(TakipKod) AS 'ToplamKiraSayi'  FROM Kiralama K 
		INNER JOIN Arac A ON A.Plaka=K.Plaka 
			INNER JOIN SubeBilgi S ON S.ID=A.SubeID
				WHERE S.ID=@SubeID
	SELECT COUNT(Plaka) AS 'KiradakiAracSayi'  FROM Arac A WHERE A.Durum='Kirada' AND SubeID=@SubeID
	SELECT COUNT(Plaka) AS 'MusaitAracSayi'  FROM Arac A WHERE A.Durum='Müsait' AND SubeID=@SubeID
	SELECT COUNT(ID) AS 'SikayetSayi' FROM Sikayet WHERE SubeID=@SubeID
END
ELSE
BEGIN
	RETURN 0
END

GO
CREATE PROC SP_BitenKiralamaGoster
AS
SELECT BK.TakipKod,K.TC,K.Uyelik,K.Plaka,K.BaslangicTarihi,k.BitisTarihi,O.OdenenUcret,K.KiraDurumu FROM BitenKiralama BK
	INNER JOIN Kiralama K ON BK.TakipKod=K.TakipKod
		INNER JOIN Odeme O ON O.ID=K.OdemeID
GO
CREATE PROC SP_IptalEdilenKiralamalar
AS
SELECT K.TakipKod,K.Plaka,K.TC,K.Uyelik,K.IslemTarih,IK.Aciklama,IK.Sebep,IK.Tarih FROM IptalEdilenKiralama IK
	INNER JOIN Kiralama K ON K.TakipKod=IK.TakipKod
GO
CREATE PROC SP_SubeIDGetir
(
@Eposta NVARCHAR(30),
@Sifre NVARCHAR(15)
)
AS 
SELECT S.ID,S.Ad FROM SubeBilgi S
	INNER JOIN Uye U ON U.ID=S.UyeID
		WHERE U.Eposta=@Eposta AND U.Sifre=@Sifre
GO
CREATE PROC SP_TekSubeninAraclari
(
@ID INT
)
AS
SELECT A.Plaka,K.Kategori,M.Marka,MDL.Model,A.ModelYili,MO.Yakit,MO.Hacim,MO.Guc,MO.Sanziman,A.Renk,A.Kilometre,A.Donanim,U.Ucret,A.Durum
	FROM AracKategori K 
		INNER JOIN MarkaKategori MK ON MK.KategoriID=K.ID
			INNER JOIN AracMarka M ON M.ID=MK.MarkaID
				INNER JOIN AracModel MDL ON MDL.MarkaKategoriID=MK.ID
					INNER JOIN AracMotor MO ON MO.ModelID=MDL.ID
						INNER JOIN Arac A ON A.ModelID=MDL.ID
							INNER JOIN SubeBilgi S ON S.ID=A.SubeID
								INNER JOIN Ucret U ON U.Plaka=A.Plaka
									WHERE S.ID=@ID

GO

GO
CREATE PROC SP_SubedenAracSil
(
@Plaka NVARCHAR(10)
)
AS
IF EXISTS(SELECT * FROM Kiralama WHERE Plaka=@Plaka)
BEGIN
	DELETE FROM TeslimAlinan WHERE TakipKod IN (SELECT TakipKod FROM Kiralama WHERE Plaka=@Plaka)
	DELETE FROM BitenKiralama WHERE TakipKod IN (SELECT TakipKod FROM Kiralama WHERE Plaka=@Plaka)
	DELETE FROM IptalEdilenKiralama WHERE TakipKod IN (SELECT TakipKod FROM Kiralama WHERE Plaka=@Plaka)
	DELETE FROM Kiralama WHERE Plaka=@Plaka
END
DELETE FROM Ucret WHERE Plaka=@Plaka
DECLARE @ModelID INT SET @ModelID =(SELECT ModelID FROM Arac WHERE Plaka=@Plaka)
DELETE FROM AracMotor WHERE ModelID=@ModelID
DELETE FROM Arac WHERE Plaka=@Plaka
DELETE FROM AracModel WHERE ID=@ModelID
GO


CREATE PROC SP_UyeUcretGetir
(
@Plaka NVARCHAR(10)
)
AS
SELECT U.UyeUcret FROM Ucret U WHERE U.Plaka=@Plaka


GO
CREATE PROC SP_TakipKodKontrol
(
@TakipKod NVARCHAR(10)
)
AS
IF EXISTS(SELECT K.TakipKod FROM Kiralama K WHERE K.TakipKod=@TakipKod)
	BEGIN
		RETURN 0
	END
ELSE
	BEGIN 
		RETURN 1
	END
GO

CREATE PROC SP_UyeBilgiGetir
(
@Eposta NVARCHAR(30),
@Sifre NVARCHAR(15)
)
AS
SELECT UB.Ad,UB.Soyad,UB.TC,U.Yetki FROM UyeBilgi UB
	INNER JOIN Uye U ON U.ID=UB.UyeID
		WHERE U.Eposta=@Eposta AND U.Sifre =@Sifre



GO
CREATE PROC SP_BitenleriGuncelle
AS
IF EXISTS(SELECT * FROM Kiralama WHERE BitisTarihi=CONVERT(DATE,GETDATE()))
BEGIN
	IF EXISTS(SELECT * FROM BitenKiralama WHERE TakipKod IN (SELECT TakipKod FROM Kiralama))
	BEGIN
		RETURN 0
	END
	ELSE 
	BEGIN
		UPDATE Kiralama SET KiraDurumu='Bitti' WHERE TakipKod IN (SELECT TakipKod FROM Kiralama K WHERE K.BitisTarihi=CONVERT(DATE,GETDATE()) AND K.KiraDurumu = 'Kirada')
		INSERT INTO BitenKiralama(TakipKod) ((SELECT TakipKod FROM Kiralama WHERE KiraDurumu='Bitti'))
		UPDATE Arac SET Durum='Teslim Bekliyor' WHERE Plaka IN (SELECT Plaka FROM Kiralama K WHERE K.BitisTarihi=CONVERT(DATE,GETDATE()) AND K.KiraDurumu = 'Bitti')
	END
END



GO
CREATE PROC SP_TakipSorgula
(
@TakipKod NVARCHAR(15)
)
AS
SELECT K.TakipKod,K.TC,K.Plaka,MA.Marka,MO.Model,MOT.Hacim,MOT.Guc,MOT.Yakit,MOT.Sanziman,A.Kilometre,A.Renk,A.Donanim,K.BaslangicTarihi,K.BitisTarihi,O.OdemeTuru,O.OdenenUcret,K.KiraDurumu,K.Uyelik
 FROM Kiralama K
	INNER JOIN Arac A ON A.Plaka=K.Plaka
		INNER JOIN AracModel MO ON MO.ID=A.ModelID
			INNER JOIN AracMotor MOT ON MOT.ModelID=MO.ID
				INNER JOIN MarkaKategori MK ON MK.ID=MO.MarkaKategoriID
					INNER JOIN AracMarka MA ON MA.ID=MK.MarkaID
						INNER JOIN Odeme O ON O.ID=K.OdemeID
							WHERE K.TakipKod=@TakipKod


GO
CREATE PROC SP_TumKiralamaGoster
AS
SELECT SB.ID,SB.Ad AS 'Þube Adý',K.TakipKod,K.TC 'Kiralayan TC',AK.Kategori,MA.Marka,MO.Model,K.BaslangicTarihi,K.BitisTarihi,O.OdemeTuru,O.OdenenUcret,K.Uyelik,K.KiraDurumu FROM Kiralama K
	INNER JOIN Arac A ON A.Plaka=K.Plaka
		INNER JOIN SubeBilgi SB ON SB.ID=A.SubeID
			INNER JOIN AracModel MO ON MO.ID=A.ModelID
				INNER JOIN MarkaKategori MK ON MK.ID=MO.MarkaKategoriID
					INNER JOIN AracMarka MA ON MA.ID=MK.MarkaID
						INNER JOIN AracKategori AK ON AK.ID=MK.KategoriID
							INNER JOIN Odeme O ON O.ID=K.OdemeID
								WHERE K.KiraDurumu='Kirada' OR K.KiraDurumu='Baþlamadý'
GO


CREATE PROC SP_BaslayanlariGuncelle
AS
IF EXISTS(SELECT * FROM Kiralama WHERE BaslangicTarihi=CONVERT(DATE,GETDATE()))
BEGIN
	UPDATE Kiralama SET KiraDurumu='Kirada' WHERE TakipKod IN (SELECT TakipKod FROM Kiralama K WHERE K.BaslangicTarihi=CONVERT(DATE,GETDATE()) AND K.KiraDurumu = 'Baþlamadý')
END
GO

CREATE PROC SP_UyeKiralamaGoster
(
@TC NVARCHAR(11)
)
AS
SELECT K.TakipKod,K.TC,K.Plaka,MA.Marka,MO.Model,MOT.Hacim,MOT.Guc,MOT.Yakit,MOT.Sanziman,A.Kilometre,A.Renk,A.Donanim,K.BaslangicTarihi,K.BitisTarihi,O.OdemeTuru,O.OdenenUcret,K.KiraDurumu,K.Uyelik
 FROM Kiralama K
	INNER JOIN Arac A ON A.Plaka=K.Plaka
		INNER JOIN AracModel MO ON MO.ID=A.ModelID
			INNER JOIN AracMotor MOT ON MOT.ModelID=MO.ID
				INNER JOIN MarkaKategori MK ON MK.ID=MO.MarkaKategoriID
					INNER JOIN AracMarka MA ON MA.ID=MK.MarkaID
						INNER JOIN Odeme O ON O.ID=K.OdemeID
							WHERE K.TC=@TC AND (K.KiraDurumu='Kirada' OR K.KiraDurumu='Baþlamadý')
GO

CREATE PROC SP_AracTeslimAl
(
@TakipKod NVARCHAR(10),
@HasarKontrol NVARCHAR(50),
@KM DECIMAL(18,3)
)
AS
INSERT INTO TeslimAlinan VALUES(@TakipKod,@HasarKontrol,GETDATE())
UPDATE Kiralama SET KiraDurumu='Tamamlandý' WHERE TakipKod=@TakipKod
UPDATE Arac SET Durum='Müsait',Kilometre=@KM WHERE Plaka=(SELECT Plaka FROM Kiralama WHERE TakipKod=@TakipKod)
DELETE FROM BitenKiralama WHERE TakipKod=@TakipKod

GO
CREATE PROC SP_UyeGecmisKiralama
(
@TC NVARCHAR(11)
)
AS
SELECT K.TakipKod,K.TC,K.Plaka,MA.Marka,MO.Model,MOT.Hacim,MOT.Guc,MOT.Yakit,MOT.Sanziman,A.Kilometre,A.Renk,A.Donanim,K.BaslangicTarihi,K.BitisTarihi,O.OdemeTuru,O.OdenenUcret,K.KiraDurumu
 FROM Kiralama K
	INNER JOIN Arac A ON A.Plaka=K.Plaka
		INNER JOIN AracModel MO ON MO.ID=A.ModelID
			INNER JOIN AracMotor MOT ON MOT.ModelID=MO.ID
				INNER JOIN MarkaKategori MK ON MK.ID=MO.MarkaKategoriID
					INNER JOIN AracMarka MA ON MA.ID=MK.MarkaID
						INNER JOIN Odeme O ON O.ID=K.OdemeID
							WHERE K.TC=@TC AND (K.KiraDurumu='Tamamlandý' OR K.KiraDurumu='Ýptal Edildi')


GO
CREATE PROC SP_UyeGecmisSikayet
(
@TC NVARCHAR(11)
)
AS
SELECT SB.Ad AS 'Þikayet Edilen Þube',S.Konu,S.Sikayet,S.Tarih FROM Sikayet S
	INNER JOIN SubeBilgi SB ON SB.ID=S.SubeID
		WHERE S.UyeTC=@TC
GO
CREATE PROC SP_UyeGecmisMesaj
(
@TC NVARCHAR(11)
)
AS
SELECT SB.Ad AS 'Gönderilen Þube Adý',M.Konu,M.Mesaj,M.Tarih FROM Mesaj M
	INNER JOIN SubeBilgi SB ON SB.ID=M.SubeID
		WHERE M.UyeTC=@TC

GO
CREATE PROC SP_UyeninProfilVerileri
(
@TC NVARCHAR(11)
)
AS
SELECT U.Eposta,UB.Ad,UB.Soyad,UB.DogumTarihi,UB.Cinsiyet,UB.Sehir,UB.Adres,UB.Telefon,UB.EhliyetSinifi,UB.EhliyetYili,UB.KayýtTarihi FROM UyeBilgi UB
	INNER JOIN Uye U ON U.ID=UB.UyeID
		WHERE UB.TC=@TC


GO
CREATE PROC SP_TumSikayetler
AS
SELECT U.Eposta AS 'Üye E-Postasý', UB.Ad AS 'Üye Adý', UB.Soyad AS 'Üye Soyadý',SB.Ad AS 'Þube Adý',S.Konu, S.Sikayet, S.Tarih FROM Sikayet S 
		INNER JOIN UyeBilgi UB ON S.UyeTC = UB.TC 
			INNER JOIN SubeBilgi SB ON SB.ID = S.SubeID 
				INNER JOIN Uye U ON U.ID = UB.UyeID
GO


CREATE PROC SP_SubeBitenKiralamaGoster
(
@SubeID INT
)
AS
SELECT BK.TakipKod,K.TC,K.Uyelik,K.Plaka,K.BaslangicTarihi,k.BitisTarihi,O.OdenenUcret,K.KiraDurumu FROM BitenKiralama BK
	INNER JOIN Kiralama K ON BK.TakipKod=K.TakipKod
		INNER JOIN Odeme O ON O.ID=K.OdemeID
			INNER JOIN Arac A ON A.Plaka=K.Plaka
				WHERE A.SubeID=@SubeID


GO

CREATE PROC SP_AracKMGetir
(
@Plaka NVARCHAR(10)
)
AS
SELECT Kilometre FROM Arac WHERE Plaka=@Plaka

GO

CREATE PROC SP_SubeTeslimAlinanAraclar
(
@SubeID INT
)
AS
SELECT TA.TakipKod,K.TC AS 'Kiralayan TC',K.Plaka,K.BaslangicTarihi,K.BitisTarihi,TA.HasarKontrol,TA.Tarih AS 'Teslim Tarihi' FROM TeslimAlinan TA
	INNER JOIN Kiralama K ON K.TakipKod=TA.TakipKod
		INNER JOIN Arac A ON A.Plaka=K.Plaka
			INNER JOIN Odeme O ON O.ID=K.OdemeID
				WHERE A.SubeID=@SubeID

GO
CREATE PROC SP_SubeKiralamaGoster
(
@SubeID INT
)
AS
SELECT SB.ID,K.TakipKod,K.TC 'Kiralayan TC',A.Plaka,MA.Marka,MO.Model,K.BaslangicTarihi,K.BitisTarihi,O.OdemeTuru,O.OdenenUcret,U.Ucret AS 'GunlukUcret',U.UyeUcret AS 'UyeGunlukUcret',K.Uyelik,K.KiraDurumu FROM Kiralama K
	INNER JOIN Arac A ON A.Plaka=K.Plaka
		INNER JOIN SubeBilgi SB ON SB.ID=A.SubeID
			INNER JOIN AracModel MO ON MO.ID=A.ModelID
				INNER JOIN MarkaKategori MK ON MK.ID=MO.MarkaKategoriID
					INNER JOIN AracMarka MA ON MA.ID=MK.MarkaID
						INNER JOIN AracKategori AK ON AK.ID=MK.KategoriID
							INNER JOIN Odeme O ON O.ID=K.OdemeID
								INNER JOIN Ucret U ON U.Plaka=A.Plaka
								WHERE SB.ID=@SubeID AND (K.KiraDurumu='Kirada'  OR K.KiraDurumu='Baþlamadý')

GO

CREATE PROC SP_SubeMesajGoster
(
@SubeID INT
)
AS
SELECT U.Eposta,UB.TC, UB.Ad,UB.Soyad,M.Konu,M.Mesaj,M.Tarih FROM Mesaj M
	INNER JOIN UyeBilgi UB ON UB.TC=M.UyeTC
		INNER JOIN Uye U ON U.ID=UB.UyeID
			WHERE M.SubeID=@SubeID
GO

CREATE PROC SP_SubeTumKiralamalar
(
@SubeID INT
)
AS
SELECT K.TakipKod,K.TC,K.Plaka,MA.Marka,MO.Model,A.Kilometre,K.BaslangicTarihi,K.BitisTarihi,O.OdemeTuru,O.OdenenUcret,K.Uyelik,K.KiraDurumu FROM Kiralama K
	INNER JOIN Arac A ON A.Plaka=K.Plaka
		INNER JOIN AracModel MO ON MO.ID=A.ModelID
			INNER JOIN MarkaKategori MK ON MK.ID=MO.MarkaKategoriID
				INNER JOIN AracMarka MA ON MA.ID=MK.MarkaID
					INNER JOIN Odeme O ON O.ID=K.OdemeID
					WHERE A.SubeID=@SubeID

GO
CREATE PROC SP_AracResimGoster
(
@Plaka NVARCHAR(10)
)
AS
SELECT Resim FROM Arac WHERE Plaka=@Plaka

GO

CREATE PROC SP_AracGuncelle
(
@Plaka NVARCHAR(10),
@Ucret DECIMAL(18,2),
@KM DECIMAL(18,3)
)
AS
DECLARe @Uye DECIMAL(18,2) SET @Uye=(SELECT @Ucret-((@Ucret*15)/100))
UPDATE Ucret SET Ucret=@Ucret,UyeUcret=@Uye WHERE Plaka=@Plaka
UPDATE Arac SET Kilometre=@KM WHERE Plaka=@Plaka
GO

CREATE PROC SP_UyeProfilGuncelle
(
@TC NVARCHAR(11),
@Sehir NVARCHAR(20),
@Adres NVARCHAR(120),
@Tel NVARCHAR(11),
@EhSinif NVARCHAR(3),
@EhYil INT
)
AS
UPDATE UyeBilgi SET Sehir=@Sehir,Adres=@Adres,Telefon=@Tel,EhliyetSinifi=@EhSinif,EhliyetYili=@EhYil WHERE TC=@TC
GO

CREATE PROC SP_IstatistikGuncelle
(
@SubeID INT
)
AS
DECLARE @AracSayi INT SET @AracSayi=(SELECT COUNT(Plaka) AS 'AracSayi'  FROM Arac WHERE SubeID=@SubeID)
DECLARE @KiraSayi INT SET @KiraSayi=(	SELECT COUNT(TakipKod) AS 'ToplamKiraSayi'  FROM Kiralama K 
		INNER JOIN Arac A ON A.Plaka=K.Plaka 
			INNER JOIN SubeBilgi S ON S.ID=A.SubeID
				WHERE S.ID=@SubeID)
DECLARE @KiradakiAracSayi INT SET @KiradakiAracSayi=(SELECT COUNT(Plaka) AS 'KiradakiAracSayi'  FROM Arac A WHERE A.Durum='Kirada' AND SubeID=@SubeID)
DECLARE @MusaitAracSayi INT SET @MusaitAracSayi=(SELECT COUNT(Plaka) AS 'MusaitAracSayi'  FROM Arac A WHERE A.Durum='Müsait' AND SubeID=@SubeID)
DECLARE @SikayetSayi INT SET @SikayetSayi=(SELECT COUNT(ID) AS 'SikayetSayi' FROM Sikayet WHERE SubeID=@SubeID)
DECLARE @ToplamUcret DECIMAL(18,2) SET @ToplamUcret=(SELECT SUM(O.OdenenUcret) FROM Odeme O
	INNER JOIN Kiralama K ON K.OdemeID=O.ID
		INNER JOIN Arac A ON A.Plaka=K.Plaka
			WHERE A.SubeID=@SubeID)
UPDATE SubeIstatistik SET AracSayisi=@AracSayi,KiralamaSayisi=@KiraSayi,KiradakiAracSayisi=@KiradakiAracSayi,MusaitAracSayisi=@MusaitAracSayi,
	ToplamAlinanUcret=@ToplamUcret,SikayetSayisi=@SikayetSayi WHERE SubeID=@SubeID

GO

CREATE PROC SP_TekSubeIstatistik
(
@SubeID INT
)
AS
SELECT * FROM SubeIstatistik WHERE SubeID=@SubeID
GO

CREATE PROC SP_GrafikKiralamaVerileri
AS
SELECT COUNT(Takipkod)AS 'Sayi',MONTH(IslemTarih) AS 'Ay' FROM Kiralama
	GROUP BY(MONTH(IslemTarih))
GO
CREATE PROC SP_GrafikKategoriVerileri
AS
SELECT AK.Kategori,COUNT(K.Plaka) AS 'Sayi' FROM AracModel MO
	INNER JOIN MarkaKategori MK ON MK.ID=MO.MarkaKategoriID
		INNER JOIN AracKategori AK ON AK.ID=MK.KategoriID
			INNER JOIN Arac A ON A.ModelID=MO.ID
				INNER JOIN Kiralama K ON K.Plaka=A.Plaka
					GROUP BY(AK.Kategori)
GO
CREATE PROC SP_GrafikAracVerileri
AS
SELECT TOP 5 MA.Marka,COUNT(Plaka) AS 'Sayi' FROM AracMarka MA
	INNER JOIN MarkaKategori MK ON MK.MarkaID=MA.ID
		INNER JOIN AracModel MO ON MO.MarkaKategoriID=MK.ID
			INNER JOIN Arac A ON A.ModelID=MO.ID
				GROUP BY(MA.Marka)
GO
CREATE PROC SP_GrafikUyeVerileri
AS
SELECT Cinsiyet,COUNT(Cinsiyet) AS 'Sayi' FROM UyeBilgi
	GROUP BY(Cinsiyet)
GO


-- GEREKLÝ VERÝ GÝRÝÞLERÝ

INSERT INTO Uye VALUES ('yonetici','12345',3)

INSERT INTO AracKategori VALUES ('Sedan')
INSERT INTO AracKategori VALUES ('Hatchback')
INSERT INTO AracKategori VALUES ('SUV')
INSERT INTO AracKategori VALUES ('Arazi')

INSERT INTO AracMarka VALUES ('Alfa Romeo')
INSERT INTO AracMarka VALUES ('Audi')
INSERT INTO AracMarka VALUES ('BMW')
INSERT INTO AracMarka VALUES ('Chevrolet')
INSERT INTO AracMarka VALUES ('Citroen')
INSERT INTO AracMarka VALUES ('Dacia')
INSERT INTO AracMarka VALUES ('Fiat')
INSERT INTO AracMarka VALUES ('Ford')
INSERT INTO AracMarka VALUES ('Honda')
INSERT INTO AracMarka VALUES ('Hyundai')
INSERT INTO AracMarka VALUES ('Kia')
INSERT INTO AracMarka VALUES ('Mazda')
INSERT INTO AracMarka VALUES ('Mercedes-Benz')
INSERT INTO AracMarka VALUES ('Nissan')
INSERT INTO AracMarka VALUES ('Mitsubishi')
INSERT INTO AracMarka VALUES ('Mini')
INSERT INTO AracMarka VALUES ('Opel')
INSERT INTO AracMarka VALUES ('Peugeot')
INSERT INTO AracMarka VALUES ('Renault')
INSERT INTO AracMarka VALUES ('Seat')
INSERT INTO AracMarka VALUES ('Skoda')
INSERT INTO AracMarka VALUES ('Toyota')
INSERT INTO AracMarka VALUES ('Volkswagen')
INSERT INTO AracMarka VALUES ('Volvo')

--ALFA ROMEO 1
INSERT INTO MarkaKategori VALUES(1,1)
INSERT INTO MarkaKategori VALUES(2,1)
--AUDÝ 2
INSERT INTO MarkaKategori VALUES (1,2)
INSERT INTO MarkaKategori VALUES (2,2)
INSERT INTO MarkaKategori VALUES (3,2)
--BMW 3
INSERT INTO MarkaKategori VALUES (1,3)
INSERT INTO MarkaKategori VALUES (2,3)
INSERT INTO MarkaKategori VALUES (3,3)
--Chevrolet 4
INSERT INTO MarkaKategori VALUES (1,4)
INSERT INTO MarkaKategori VALUES (2,4)
INSERT INTO MarkaKategori VALUES (3,4)
--Citroen 5
INSERT INTO MarkaKategori VALUES (1,5)
INSERT INTO MarkaKategori VALUES (2,5)
INSERT INTO MarkaKategori VALUES (3,5)
--Dacia 6
INSERT INTO MarkaKategori VALUES (1,6)
INSERT INTO MarkaKategori VALUES (2,6)
INSERT INTO MarkaKategori VALUES (3,6)
--Fiat 7
INSERT INTO MarkaKategori VALUES (1,7)
INSERT INTO MarkaKategori VALUES (2,7)
INSERT INTO MarkaKategori VALUES (3,7)
--Ford 8
INSERT INTO MarkaKategori VALUES (1,8)
INSERT INTO MarkaKategori VALUES (2,8)
INSERT INTO MarkaKategori VALUES (3,8)
INSERT INTO MarkaKategori VALUES (4,8)
--Honda 9
INSERT INTO MarkaKategori VALUES (1,9)
INSERT INTO MarkaKategori VALUES (2,9)
INSERT INTO MarkaKategori VALUES (3,9)
--Hyundai 10
INSERT INTO MarkaKategori VALUES (1,10)
INSERT INTO MarkaKategori VALUES (2,10)
INSERT INTO MarkaKategori VALUES (3,10)
--Kia 11
INSERT INTO MarkaKategori VALUES (1,11)
INSERT INTO MarkaKategori VALUES (2,11)
INSERT INTO MarkaKategori VALUES (3,11)
--Mazda 12
INSERT INTO MarkaKategori VALUES (1,12)
INSERT INTO MarkaKategori VALUES (3,12)
--Mercedes 13
INSERT INTO MarkaKategori VALUES (1,13)
INSERT INTO MarkaKategori VALUES (2,13)
INSERT INTO MarkaKategori VALUES (3,13)
INSERT INTO MarkaKategori VALUES (4,13)
--Nissan 14
INSERT INTO MarkaKategori VALUES (1,14)
INSERT INTO MarkaKategori VALUES (2,14)
INSERT INTO MarkaKategori VALUES (3,14)
INSERT INTO MarkaKategori VALUES (4,14)
--Mitsubishi 15
INSERT INTO MarkaKategori VALUES (1,15)
INSERT INTO MarkaKategori VALUES (3,15)
INSERT INTO MarkaKategori VALUES (4,15)
--Mini 16
INSERT INTO MarkaKategori VALUES (2,16)
INSERT INTO MarkaKategori VALUES (3,16)
--Opel 17
INSERT INTO MarkaKategori VALUES (1,17)
INSERT INTO MarkaKategori VALUES (2,17)
INSERT INTO MarkaKategori VALUES (3,17)
--Peugeot 18
INSERT INTO MarkaKategori VALUES (1,18)
INSERT INTO MarkaKategori VALUES (2,18)
INSERT INTO MarkaKategori VALUES (3,18)
--Renault 19 
INSERT INTO MarkaKategori VALUES (1,19)
INSERT INTO MarkaKategori VALUES (2,19)
INSERT INTO MarkaKategori VALUES (3,19)
--Seat 20
INSERT INTO MarkaKategori VALUES (1,20)
INSERT INTO MarkaKategori VALUES (2,20)
INSERT INTO MarkaKategori VALUES (3,20)
--Skoda 21
INSERT INTO MarkaKategori VALUES (1,21)
INSERT INTO MarkaKategori VALUES (2,21)
INSERT INTO MarkaKategori VALUES (3,21)
--Toyota 22
INSERT INTO MarkaKategori VALUES (1,22)
INSERT INTO MarkaKategori VALUES (2,22)
INSERT INTO MarkaKategori VALUES (3,22)
INSERT INTO MarkaKategori VALUES (4,22)
--Volkswagen 23
INSERT INTO MarkaKategori VALUES (1,23)
INSERT INTO MarkaKategori VALUES (2,23)
INSERT INTO MarkaKategori VALUES (3,23)
INSERT INTO MarkaKategori VALUES (4,23)
--Volvo 24
INSERT INTO MarkaKategori VALUES (1,24)
INSERT INTO MarkaKategori VALUES (2,24)
INSERT INTO MarkaKategori VALUES (3,24)
