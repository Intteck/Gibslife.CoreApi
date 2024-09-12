DELETE FROM [gibs-core-db].[agency].Rates
DELETE FROM [gibs-core-db].[agency].Parties
DELETE FROM [gibs-core-db].[agency].Customers

PRINT 'Delete Completed'

--Migrate Customers
INSERT
INTO	[gibs-core-db].[agency].Customers(
		CustomerID, CustomerName, CustomerTypeID, BirthDate,
		Title, LastName, FirstName, OtherNames, Industry, RiskProfileID, 
		CreatedBy, CreatedUtc, LastModifiedBy, LastModifiedUtc, 		
		Email, Phone, PhoneAlt, 
		[Address], CityLGA, StateID, Country, ContactPerson,
		TaxNumber, KycNumber, KycTypeID, KycIssueDate, KycExpiryDate)		

SELECT  InsuredID, ISNULL(ISNULL(FullName, Surname), '<null-value>'), InsuredType, DOB,
		Field2, Surname, FirstName, OtherNames, Occupation, [Profile],
		LOWER(SubmittedBy), SubmittedOn, LOWER(ModifiedBy), ModifiedOn,  
		ISNULL(LOWER(Email), '<null-value>'), MobilePhone, LandPhone, 
		[Address], Field5, Remarks, Field4, Contperson, 
		Field9, MeansIDNo, MeansID, ExtraDate1, ExtraDate2
FROM	[Gibs5db_CIP].dbo.InsuredClients 

--Migrate Parties
INSERT
INTO	[gibs-core-db].[agency].Parties(
		PartyID, PartyName, PartyTypeID, BirthDate, 
		CreatedBy, CreatedUtc, LastModifiedBy, LastModifiedUtc, 		 
		Email, Phone, PhoneAlt, [Address], CityLGA, StateID, Country, 
		CommTypeID, ContactPerson, TaxNumber, 
		KycNumber, KycTypeID, KycIssueDate, KycExpiryDate) --KycTypeID, CommTypeID, PartnerTypeID

SELECT  UPPER(PartyID), Party, UPPER(PartyType), NULL, 
		LOWER(SubmittedBy), SubmittedOn, LOWER(ModifiedBy), ModifiedOn,
		Lower(Email), mobilePhone, LandPhone, [Address], Remarks, StateID, 'Nigeria',
		ComRate, [Description], NULL,
		InsContact, FinContact, StartDate, ExpiryDate
FROM	[Gibs5db_CIP].dbo.Parties 
WHERE	PartyType IN (SELECT PartyTypeID FROM [gibs-core-db].[agency].PartyTypes)

--Migrate Rates
INSERT
INTO	[gibs-core-db].[agency].Rates(
		RiskOptionID, PartyOptionID, 
		CommRate, VatRate, Remarks,
		CreatedBy, CreatedUtc, LastModifiedBy, LastModifiedUtc)

SELECT  SubRiskID, ISNULL(CommType, ''), 
		ComRate, VatRate, SubRisk,
		'<null-value>', GETDATE(), NULL, NULL 
FROM	[Gibs5db_CIP].dbo.PartyRates
