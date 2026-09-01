/*

DEV

CREATE USER [developer@yourcompany.com] FROM EXTERNAL PROVIDER;
ALTER ROLE db_owner ADD MEMBER [developer@yourcompany.com];

ConnectionString = "Server=tcp:lci-dt-weu.database.windows.net,1433;Database=textbase2;Authentication=Active Directory Default;Encrypt=True;TrustServerCertificate=False;"

TEST

App Service → Settings → Identity → System assigned
	Status = On

CREATE USER [YOUR-APP-SERVICE] FROM EXTERNAL PROVIDER;
ALTER ROLE db_datareader ADD MEMBER [YOUR-APP-SERVICE];
ALTER ROLE db_datawriter ADD MEMBER [YOUR-APP-SERVICE];
GRANT EXECUTE TO [YOUR-APP-SERVICE];

App Service → Networking → Virtual network integration
	Virtual network: lci-dt-vnet
	Subnet: appservice

ConnectionString = "Server=tcp:lci-dt-weu.database.windows.net,1433;Database=textbase2;Authentication=Active Directory Default;Encrypt=True;TrustServerCertificate=False;"

*/

----------------------------------------------------------------------------------------------------------------------------------------------------------------
-- SCHEMAS
----------------------------------------------------------------------------------------------------------------------------------------------------------------

IF NOT EXISTS (SELECT 1 FROM sys.schemas AS S WHERE S.[name] = N'flat')
	EXECUTE(N'CREATE SCHEMA [flat]')
GO

IF NOT EXISTS (SELECT 1 FROM sys.schemas AS S WHERE S.[name] = N'log')
	EXECUTE(N'CREATE SCHEMA [log]')
GO

----------------------------------------------------------------------------------------------------------------------------------------------------------------
-- TABLES
----------------------------------------------------------------------------------------------------------------------------------------------------------------

IF OBJECT_ID(N'dbo.ClientApplication', N'U') IS NULL
BEGIN
	CREATE TABLE dbo.ClientApplication
	(
		ClientApplicationGuid uniqueidentifier NOT NULL,
		[Name] nvarchar(128) NOT NULL,
		[Description] nvarchar(1024) NULL,
		DefaultLanguageTag varchar(8) NULL,
		DefaultFormat json NULL,
		DefaultFileName varchar(128) NULL,
		IsActive bit NOT NULL,

		CONSTRAINT PK_ClientApplication PRIMARY KEY (ClientApplicationGuid),
		CONSTRAINT UQ_ClientApplication_Name UNIQUE ([Name])
	)
END

IF OBJECT_ID(N'dbo.ClientApplicationLocale', N'U') IS NULL
BEGIN
	CREATE TABLE dbo.ClientApplicationLocale
	(
		ClientApplicationGuid uniqueidentifier NOT NULL,
		LocaleKey varchar(85) NOT NULL,
		IsDefault bit NOT NULL,

		CONSTRAINT PK_ClientApplicationLocale PRIMARY KEY (ClientApplicationGuid, LocaleKey)
	)

	CREATE INDEX IX_ClientApplicationLocale_ClientApplicationGuid ON dbo.ClientApplicationLocale (ClientApplicationGuid)
	CREATE INDEX IX_ClientApplicationLocale_Locale ON dbo.ClientApplicationLocale (LocaleKey)
	CREATE UNIQUE INDEX UX_ClientApplicationLocale_Default ON dbo.ClientApplicationLocale (ClientApplicationGuid) WHERE IsDefault = 1
END

IF OBJECT_ID(N'dbo.ClientApplicationTextResource', N'U') IS NULL
BEGIN
	CREATE TABLE dbo.ClientApplicationTextResource
	(
		ClientApplicationGuid uniqueidentifier NOT NULL,
		TextKey varchar(128) NOT NULL,
		ReferenceId varchar(256) NULL,

		CONSTRAINT PK_ApplicationTextResource PRIMARY KEY (ClientApplicationGuid, TextKey)
	)

	CREATE INDEX IX_ClientApplicationTextResource_ClientApplicationGuid ON dbo.ClientApplicationTextResource (ClientApplicationGuid)
	CREATE INDEX IX_ClientApplicationTextResource_TextKey ON dbo.ClientApplicationTextResource (TextKey)
END

IF OBJECT_ID(N'dbo.Locale', N'U') IS NULL
BEGIN
	CREATE TABLE dbo.Locale
	(
		LocaleKey varchar(85) NOT NULL,
		ParentLocaleKey varchar(85) NULL,
		LanguageIso2 char(2) NULL,
		LanguageIso3 char(3) NULL,
		LanguageIsoN int NULL,
		LanguageLCID int NULL,
		LanguageWinApi char(3) NULL,
		CountryIso2 char(2) NULL,
		CountryIso3 char(3) NULL,
		NativeName nvarchar(128) NOT NULL,
		EnglishName nvarchar(128) NOT NULL,

		CONSTRAINT PK_Locale PRIMARY KEY (LocaleKey),
		CONSTRAINT CK_Locale_ParentLocaleKey CHECK (ParentLocaleKey IS NULL OR ParentLocaleKey <> LocaleKey)
	)
END

IF OBJECT_ID(N'dbo.TextResource', N'U') IS NULL
BEGIN
	CREATE TABLE dbo.TextResource
	(
		TextKey varchar(128) NOT NULL,
		[Description] nvarchar(1024) NULL,

		CONSTRAINT PK_TextResource PRIMARY KEY (TextKey)
	)
END

IF OBJECT_ID(N'dbo.Formality', N'U') IS NULL
BEGIN
	CREATE TABLE dbo.Formality
	(
		FormalityKey varchar(16) NOT NULL,
		[Description] nvarchar(1024) NULL,

		CONSTRAINT PK_Formality PRIMARY KEY (FormalityKey)
	)
END

IF OBJECT_ID(N'dbo.Presentation', N'U') IS NULL
BEGIN
	CREATE TABLE dbo.Presentation
	(
		PresentationKey varchar(16) NOT NULL,
		[Description] nvarchar(1024) NULL,

		CONSTRAINT PK_Presentation PRIMARY KEY (PresentationKey)
	)
END

IF OBJECT_ID(N'dbo.Translation', N'U') IS NULL
BEGIN
	CREATE TABLE dbo.Translation
	(
		LocaleKey varchar(85) NOT NULL,
		TextKey varchar(128) NOT NULL,
		FormalityKey varchar(16) NOT NULL CONSTRAINT DF_Translation_FormalityKey DEFAULT ('Default'),
		PresentationKey varchar(16) NOT NULL CONSTRAINT DF_Translation_PresentationKey DEFAULT ('Default'),
		[Value] nvarchar(max) NOT NULL,

		CONSTRAINT PK_Translation PRIMARY KEY (LocaleKey, TextKey, FormalityKey, PresentationKey)
	)

	CREATE INDEX IX_Translation_Locale ON dbo.Translation (LocaleKey)
	CREATE INDEX IX_Translation_TextKey ON dbo.Translation (TextKey)
END

IF OBJECT_ID(N'flat.Translation', N'U') IS NULL
BEGIN
	CREATE TABLE flat.Translation
	(
		LocaleKey varchar(85) NOT NULL,
		SourceLocaleKey varchar(85) NOT NULL,
		TextKey varchar(128) NOT NULL,
		FormalityKey varchar(16) NOT NULL,
		PresentationKey varchar(16) NOT NULL,
		[Value] nvarchar(max) NOT NULL,

		CONSTRAINT PK_flat_Translation PRIMARY KEY (LocaleKey, TextKey, FormalityKey, PresentationKey)
	)
END

----------------------------------------------------------------------------------------------------------------------------------------------------------------
-- FOREIGN KEYS
----------------------------------------------------------------------------------------------------------------------------------------------------------------

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys AS FK WHERE FK.[name] = 'FK_ClientApplicationLocale_Application')
	ALTER TABLE dbo.ClientApplicationLocale ADD CONSTRAINT FK_ClientApplicationLocale_Application FOREIGN KEY (ClientApplicationGuid)
		REFERENCES dbo.ClientApplication (ClientApplicationGuid)
IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys AS FK WHERE FK.[name] = 'FK_ClientApplicationLocale_Locale')
	ALTER TABLE dbo.ClientApplicationLocale ADD CONSTRAINT FK_ClientApplicationLocale_Locale FOREIGN KEY (LocaleKey) REFERENCES dbo.Locale (LocaleKey)

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys AS FK WHERE FK.[name] = 'FK_ClientApplicationTextResource_ClientApplication')
	ALTER TABLE dbo.ClientApplicationTextResource ADD CONSTRAINT FK_ClientApplicationTextResource_ClientApplication FOREIGN KEY (ClientApplicationGuid)
		REFERENCES dbo.ClientApplication (ClientApplicationGuid)
IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys AS FK WHERE FK.[name] = 'FK_ClientApplicationTextResource_TextResource')
	ALTER TABLE dbo.ClientApplicationTextResource ADD CONSTRAINT FK_ClientApplicationTextResource_TextResource FOREIGN KEY (TextKey)
		REFERENCES dbo.TextResource (TextKey)

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys AS FK WHERE FK.[name] = 'FK_Locale_ParentLocaleKey')
	ALTER TABLE dbo.Locale ADD CONSTRAINT FK_Locale_ParentLocaleKey FOREIGN KEY (ParentLocaleKey) REFERENCES dbo.Locale (LocaleKey)
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys AS FK WHERE FK.[name] = 'FK_Translation_Locale')
	ALTER TABLE dbo.Translation ADD CONSTRAINT FK_Translation_Locale FOREIGN KEY (LocaleKey) REFERENCES dbo.Locale (LocaleKey)
GO
IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys AS FK WHERE FK.[name] = 'FK_Translation_TextResource')
	ALTER TABLE dbo.Translation ADD CONSTRAINT FK_Translation_TextResource FOREIGN KEY (TextKey) REFERENCES dbo.TextResource (TextKey)
GO
IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys AS FK WHERE FK.[name] = 'FK_Translation_Formality')
	ALTER TABLE dbo.Translation ADD CONSTRAINT FK_Translation_Formality FOREIGN KEY (FormalityKey) REFERENCES dbo.Formality (FormalityKey)
GO
IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys AS FK WHERE FK.[name] = 'FK_Translation_Presentation')
	ALTER TABLE dbo.Translation ADD CONSTRAINT FK_Translation_Presentation FOREIGN KEY (PresentationKey) REFERENCES dbo.Presentation (PresentationKey)
GO

----------------------------------------------------------------------------------------------------------------------------------------------------------------
-- PROCEDURES
----------------------------------------------------------------------------------------------------------------------------------------------------------------

CREATE OR ALTER PROCEDURE flat.RebuildTranslation
	@NeutralLocaleKey varchar(85) = 'und'
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	CREATE TABLE #Translation
	(
		LocaleKey varchar(85) NOT NULL,
		SourceLocaleKey varchar(85) NOT NULL,
		TextKey varchar(128) NOT NULL,
		FormalityKey varchar(16) NOT NULL,
		PresentationKey varchar(16) NOT NULL,
		[Value] nvarchar(max) NOT NULL,

		CONSTRAINT PK_TempTranslation PRIMARY KEY (LocaleKey, TextKey, FormalityKey, PresentationKey)
	);

	;WITH RequiredText AS
	(
		SELECT DISTINCT
			CAL.LocaleKey,
			CATR.TextKey
		FROM dbo.ClientApplication AS CA
		INNER JOIN dbo.ClientApplicationLocale AS CAL ON CAL.ClientApplicationGuid = CA.ClientApplicationGuid
		INNER JOIN dbo.ClientApplicationTextResource AS CATR ON CATR.ClientApplicationGuid = CA.ClientApplicationGuid
		WHERE CA.IsActive = 1
	),
	RequiredTranslation AS
	(
		SELECT
			RT.LocaleKey,
			RT.TextKey,
			F.FormalityKey,
			P.PresentationKey
		FROM RequiredText AS RT
		CROSS JOIN dbo.Formality AS F
		CROSS JOIN dbo.Presentation AS P
	),
	RequiredLocale AS
	(
		SELECT DISTINCT RT.LocaleKey
		FROM RequiredText AS RT
	),
	LocaleFallback AS
	(
		SELECT
			RL.LocaleKey,
			RL.LocaleKey AS SourceLocaleKey,
			0 AS [Priority],
			CAST('|' + RL.LocaleKey + '|' AS varchar(max)) AS [Path]
		FROM RequiredLocale AS RL

		UNION ALL

		SELECT
			LF.LocaleKey,
			L.ParentLocaleKey,
			LF.[Priority] + 1,
			CAST(LF.[Path] + L.ParentLocaleKey + '|' AS varchar(max))
		FROM LocaleFallback AS LF
		INNER JOIN dbo.Locale AS L ON L.LocaleKey = LF.SourceLocaleKey
		WHERE L.ParentLocaleKey IS NOT NULL
			AND CHARINDEX('|' + L.ParentLocaleKey + '|', LF.[Path]) = 0
	),
	LocaleCandidate AS
	(
		SELECT
			RT.LocaleKey,
			LF.SourceLocaleKey,
			RT.TextKey,
			RT.FormalityKey,
			RT.PresentationKey,
			LF.[Priority]
		FROM RequiredTranslation AS RT
		INNER JOIN LocaleFallback AS LF ON LF.LocaleKey = RT.LocaleKey

		UNION ALL

		SELECT
			RT.LocaleKey,
			@NeutralLocaleKey,
			RT.TextKey,
			RT.FormalityKey,
			RT.PresentationKey,
			1000000
		FROM RequiredTranslation AS RT
		WHERE NOT EXISTS
		(
			SELECT 1
			FROM LocaleFallback AS LF
			WHERE LF.LocaleKey = RT.LocaleKey
				AND LF.SourceLocaleKey = @NeutralLocaleKey
		)
	),
	Candidate AS
	(
		SELECT
			LC.LocaleKey,
			LC.SourceLocaleKey,
			LC.TextKey,
			LC.FormalityKey,
			LC.PresentationKey,
			T.[Value],
			LC.[Priority] AS LocalePriority,
			CASE
				WHEN T.FormalityKey = LC.FormalityKey AND T.PresentationKey = LC.PresentationKey THEN 0
				WHEN T.FormalityKey = LC.FormalityKey AND T.PresentationKey = 'Default' THEN 1
				WHEN T.FormalityKey = 'Default' AND T.PresentationKey = LC.PresentationKey THEN 2
				WHEN T.FormalityKey = 'Default' AND T.PresentationKey = 'Default' THEN 3
			END AS VariantPriority
		FROM LocaleCandidate AS LC
		INNER JOIN dbo.Translation AS T ON T.LocaleKey = LC.SourceLocaleKey
			AND T.TextKey = LC.TextKey
			AND T.FormalityKey IN (LC.FormalityKey, 'Default')
			AND T.PresentationKey IN (LC.PresentationKey, 'Default')
	),
	Resolved AS
	(
		SELECT
			C.LocaleKey,
			C.SourceLocaleKey,
			C.TextKey,
			C.FormalityKey,
			C.PresentationKey,
			C.[Value],
			ROW_NUMBER() OVER
			(
				PARTITION BY C.LocaleKey, C.TextKey, C.FormalityKey, C.PresentationKey
				ORDER BY C.LocalePriority, C.VariantPriority
			) AS RowNumber
		FROM Candidate AS C
	)
	INSERT INTO #Translation
	(
		LocaleKey,
		SourceLocaleKey,
		TextKey,
		FormalityKey,
		PresentationKey,
		[Value]
	)
	SELECT
		R.LocaleKey,
		R.SourceLocaleKey,
		R.TextKey,
		R.FormalityKey,
		R.PresentationKey,
		R.[Value]
	FROM Resolved AS R
	WHERE R.RowNumber = 1
	OPTION (MAXRECURSION 100);

	BEGIN TRY
		BEGIN TRANSACTION;

		TRUNCATE TABLE flat.Translation;

		INSERT INTO flat.Translation
		(
			LocaleKey,
			SourceLocaleKey,
			TextKey,
			FormalityKey,
			PresentationKey,
			[Value]
		)
		SELECT
			T.LocaleKey,
			T.SourceLocaleKey,
			T.TextKey,
			T.FormalityKey,
			T.PresentationKey,
			T.[Value]
		FROM #Translation AS T;

		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION;

		THROW;
	END CATCH;
END
GO

----------------------------------------------------------------------------------------------------------------------------------------------------------------
-- TRIGGERS
----------------------------------------------------------------------------------------------------------------------------------------------------------------

IF OBJECT_ID(N'dbo.TR_Translation_ValidateRebuildFlatTranslation', N'TR') IS NULL
BEGIN
	EXEC
	(
		N'
		CREATE TRIGGER dbo.TR_Translation_ValidateRebuildFlatTranslation
		ON dbo.Translation
		AFTER INSERT, UPDATE, DELETE
		AS
		BEGIN
			SET NOCOUNT ON;

			IF EXISTS
			(
				SELECT 1
				FROM
				(
					SELECT LocaleKey, TextKey FROM inserted
					UNION
					SELECT LocaleKey, TextKey FROM deleted
				) AS A
				WHERE EXISTS
				(
					SELECT 1
					FROM dbo.Translation AS T
					WHERE T.LocaleKey = A.LocaleKey
						AND T.TextKey = A.TextKey
				)
				AND NOT EXISTS
				(
					SELECT 1
					FROM dbo.Translation AS T
					WHERE T.LocaleKey = A.LocaleKey
						AND T.TextKey = A.TextKey
						AND T.FormalityKey = ''Default''
						AND T.PresentationKey = ''Default''
				)
			)
			BEGIN
				THROW 50001, ''Every Locale/TextKey translation group must contain a Default/Default translation.'', 1;
			END

			EXEC flat.RebuildTranslation;
		END
		'
	)
END
GO

IF OBJECT_ID(N'dbo.TR_Locale_RebuildFlatTranslation', N'TR') IS NULL
BEGIN
	EXEC
	(
		N'
		CREATE TRIGGER dbo.TR_Locale_RebuildFlatTranslation
		ON dbo.Locale
		AFTER INSERT, UPDATE, DELETE
		AS
		BEGIN
			SET NOCOUNT ON;

			EXEC flat.RebuildTranslation;
		END
		'
	)
END
GO

IF OBJECT_ID(N'dbo.TR_ClientApplication_RebuildFlatTranslation', N'TR') IS NULL
BEGIN
	EXEC
	(
		N'
		CREATE TRIGGER dbo.TR_ClientApplication_RebuildFlatTranslation
		ON dbo.ClientApplication
		AFTER INSERT, UPDATE, DELETE
		AS
		BEGIN
			SET NOCOUNT ON;

			EXEC flat.RebuildTranslation;
		END
		'
	)
END
GO

IF OBJECT_ID(N'dbo.TR_ClientApplicationLocale_RebuildFlatTranslation', N'TR') IS NULL
BEGIN
	EXEC
	(
		N'
		CREATE TRIGGER dbo.TR_ClientApplicationLocale_RebuildFlatTranslation
		ON dbo.ClientApplicationLocale
		AFTER INSERT, UPDATE, DELETE
		AS
		BEGIN
			SET NOCOUNT ON;

			EXEC flat.RebuildTranslation;
		END
		'
	)
END
GO

IF OBJECT_ID(N'dbo.TR_ClientApplicationTextResource_RebuildFlatTranslation', N'TR') IS NULL
BEGIN
	EXEC
	(
		N'
		CREATE TRIGGER dbo.TR_ClientApplicationTextResource_RebuildFlatTranslation
		ON dbo.ClientApplicationTextResource
		AFTER INSERT, UPDATE, DELETE
		AS
		BEGIN
			SET NOCOUNT ON;

			EXEC flat.RebuildTranslation;
		END
		'
	)
END
GO

IF OBJECT_ID(N'dbo.TR_Formality_RebuildFlatTranslation', N'TR') IS NULL
BEGIN
	EXEC
	(
		N'
		CREATE TRIGGER dbo.TR_Formality_RebuildFlatTranslation
		ON dbo.Formality
		AFTER INSERT, UPDATE, DELETE
		AS
		BEGIN
			SET NOCOUNT ON;

			EXEC flat.RebuildTranslation;
		END
		'
	)
END
GO

IF OBJECT_ID(N'dbo.TR_Presentation_RebuildFlatTranslation', N'TR') IS NULL
BEGIN
	EXEC
	(
		N'
		CREATE TRIGGER dbo.TR_Presentation_RebuildFlatTranslation
		ON dbo.Presentation
		AFTER INSERT, UPDATE, DELETE
		AS
		BEGIN
			SET NOCOUNT ON;

			EXEC flat.RebuildTranslation;
		END
		'
	)
END
GO

----------------------------------------------------------------------------------------------------------------------------------------------------------------
-- DEFAULTS
----------------------------------------------------------------------------------------------------------------------------------------------------------------

IF NOT EXISTS (SELECT 1 FROM dbo.Locale AS L WHERE L.LocaleKey = 'und')
	INSERT INTO dbo.Locale VALUES ('und', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Neutral', N'Neutral')
IF NOT EXISTS (SELECT 1 FROM dbo.Locale AS L WHERE L.LocaleKey = 'de')
	INSERT INTO dbo.Locale VALUES ('de', 'und', 'de', 'deu', NULL, 7, 'DEU', NULL, NULL, N'Deutsch', N'German')
IF NOT EXISTS (SELECT 1 FROM dbo.Locale AS L WHERE L.LocaleKey = 'de-DE')
	INSERT INTO dbo.Locale VALUES ('de-DE', 'de', 'de', 'deu', NULL, 1031, 'DEU', 'DE', 'DEU', N'Deutsch (Deutschland)', N'German (Germany)')
IF NOT EXISTS (SELECT 1 FROM dbo.Locale AS L WHERE L.LocaleKey = 'de-AT')
	INSERT INTO dbo.Locale VALUES ('de-AT', 'de', 'de', 'deu', NULL, 3079, 'DEA', 'AT', 'AUT', N'Deutsch (Österreich)', N'German (Austria)')
IF NOT EXISTS (SELECT 1 FROM dbo.Locale AS L WHERE L.LocaleKey = 'de-CH')
	INSERT INTO dbo.Locale VALUES ('de-CH', 'de', 'de', 'deu', NULL, 2055, 'DES', 'CH', 'CHE', N'Deutsch (Schweiz)', N'German (Switzerland)')
IF NOT EXISTS (SELECT 1 FROM dbo.Locale AS L WHERE L.LocaleKey = 'en')
	INSERT INTO dbo.Locale VALUES ('en', 'und', 'en', 'eng', NULL, 9, 'ENU', NULL, NULL, N'English', N'English')
IF NOT EXISTS (SELECT 1 FROM dbo.Locale AS L WHERE L.LocaleKey = 'en-GB')
	INSERT INTO dbo.Locale VALUES ('en-GB', 'en', 'en', 'eng', NULL, 2057, 'ENG', 'GB', 'GBR', N'English (United Kingdom)', N'English (United Kingdom)')
IF NOT EXISTS (SELECT 1 FROM dbo.Locale AS L WHERE L.LocaleKey = 'en-US')
	INSERT INTO dbo.Locale VALUES ('en-US', 'en', 'en', 'eng', NULL, 1033, 'ENU', 'US', 'USA', N'English (United States)', N'English (United States)')

IF NOT EXISTS (SELECT 1 FROM dbo.Formality AS F WHERE F.FormalityKey = 'Default')
	INSERT INTO dbo.Formality (FormalityKey, [Description]) VALUES ('Default', NULL)
IF NOT EXISTS (SELECT 1 FROM dbo.Formality AS F WHERE F.FormalityKey = 'Formal')
	INSERT INTO dbo.Formality (FormalityKey, [Description]) VALUES ('Formal', N'Formal form of address.')
IF NOT EXISTS (SELECT 1 FROM dbo.Formality AS F WHERE F.FormalityKey = 'Informal')
	INSERT INTO dbo.Formality (FormalityKey, [Description]) VALUES ('Informal', N'Informal form of address.')

IF NOT EXISTS (SELECT 1 FROM dbo.Presentation AS P WHERE P.PresentationKey = 'Default')
	INSERT INTO dbo.Presentation (PresentationKey, [Description]) VALUES ('Default', NULL)
IF NOT EXISTS (SELECT 1 FROM dbo.Presentation AS P WHERE P.PresentationKey = 'Capitalized')
	INSERT INTO dbo.Presentation (PresentationKey, [Description]) VALUES ('Capitalized', N'Capitalized form, for example for headers where required by the language.')
IF NOT EXISTS (SELECT 1 FROM dbo.Presentation AS P WHERE P.PresentationKey = 'Compact')
	INSERT INTO dbo.Presentation (PresentationKey, [Description]) VALUES ('Compact', N'Shortened or abbreviated form for space-constrained presentation.')
GO

EXEC flat.RebuildTranslation;
GO
