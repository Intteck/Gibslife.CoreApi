DELETE FROM [gibs-core-db].[policy].SMIs
DELETE FROM [gibs-core-db].[policy].Sections
DELETE FROM [gibs-core-db].[policy].Details

--ALTER TABLE [gibs-core-db].[policy].Details DROP FK_Details_Master_PolicyNo
--ALTER TABLE [gibs-core-db].[policy].Details DROP FK_Details_Notes_DebitNoteNo
--DROP INDEX IX_Details_DebitNoteNo ON [gibs-core-db].[policy].Details 


--Migrate Details
SET IDENTITY_INSERT [gibs-core-db].[policy].Details ON
INSERT
INTO	[gibs-core-db].[policy].Details(
		SN, DeclareNo, PolicyNo, DebitNoteNo, EndorseNo, EndorseTypeID, NaicomUID,

		--members
		ProductID, ProductName, CustomerID, CustomerName,
		BranchID,  BranchName,  PartyID,    PartyName, 
		--LeadPartyID, LeadPartyName, --MarketerID, MarketerName,
		ChannelID, ChannelName, SubChannelID, SubChannelName,

		--business
		CoPolicyNo, BizTypeID, SrcTypeID, ActTypeID,
		TransDate, StartDate, EndDate, CoverDays, StdCoverDays, CurrencyID,
		CurrencyRate, OurShareRate, --PremiumRate, CommRate, CommVatRate, 

		--premium
		GrossPremiumFx, GrossPremium, WholePremium, SharePremium, ProrataPremium, NetPremium, 
		SumInsuredFx, SumInsured, WholeSumInsured, ShareSumInsured,  
		
		--secure
		CreatedBy, CreatedUtc, LastModifiedBy, LastModifiedUtc,
		ApprovedBy, ApprovedUtc, Approval)

SELECT  d.DetailID, d.CertOrDocNo, d.PolicyNo, 
		NULLIF(NULLIF(d.DNCNNo, d.PolicyNo), ''), 
		ISNULL(NULLIF(d.EndorsementNo, d.PolicyNo), '<null-value>'), d.BizOption, p.Z_NAICOM_UID,

		--members
		SubRiskID, SubRisk, ISNULL(InsuredID, '<null-value>'), ISNULL(InsFullName, ISNULL(InsSurname, '<null-value>')),
		ISNULL(BranchID, '<null-value>'), Branch, ISNULL(PartyID, '<null-value>'), ISNULL(Party, '<null-value>'),
		--LeadID, Leader, --MktStaffID, MktStaff,
		InsStateID, Remarks, MktGroupID, MktGroup,

		--business
		NULLIF(d.CoPolicyNo, d.PolicyNo), 
		p.BizSource, ISNULL(p.SourceType,'<null-value>'), p.TrackID,
		d.EntryDate, d.StartDate, ISNULL(d.EndDate, GETDATE()), ISNULL(d.ProRataDays, 0), 365, ISNULL(d.ExCurrency, 'NGN'),
		ISNULL(d.ExRate, 1), d.ProportionRate, --ISNULL(d.PremiumRate, 0), 0, 0,

		--premium
		d.GrossPremiumFrgn, d.GrossPremium, d.GrossPremium, ISNULL(d.A2, d.GrossPremium), ISNULL(d.ProRataPremium, 0), d.NetAmount,
		d.SumInsuredFrgn, d.SumInsured, ISNULL(d.TotalRiskValue, d.SumInsured), d.SumInsured,
		
		--secure
		ISNULL(LOWER(d.SubmittedBy), '<null-value>'), d.SubmittedOn, LOWER(d.ModifiedBy), d.ModifiedOn,
		LOWER(d.Field42), NULL, d.Field40

FROM	[Gibs5db_CIP].dbo.PolicyDetails d
JOIN	[Gibs5db_CIP].dbo.Policies p ON d.PolicyNo = p.PolicyNo
--LEFT JOIN [Gibs5db_CIP].dbo.DNCNNotes n ON n.DNCNNo = d.DNCNNo

--Exclude 36 PolicyDetails duplicates
WHERE	d.GrossPremium IS NOT NULL AND d.SubmittedOn IS NOT NULL
AND		d.CertOrDocNo IN (SELECT CertOrDocNo
						  FROM [Gibs5db_CIP].[dbo].PolicyDetails
						  GROUP BY CertOrDocNo
						  HAVING COUNT(*) = 1) 
--Exclude 5 Policy duplicates
AND 	p.PolicyNo IN (SELECT PolicyNo
					   FROM [Gibs5db_CIP].[dbo].[Policies]
					   GROUP BY PolicyNo
					   HAVING COUNT(*) = 1) 
SET IDENTITY_INSERT [gibs-core-db].[policy].Details OFF

/*
-- Migrate Sections
INSERT
INTO	[gibs-core-db].[policy].Sections(
		SN, DetailNo, SectionID, CertificateNo, SumInsured, Premium, 
		A1,  A2,  A3,  A4,  A5,  A6,  A7,  A8,  A9,  A10,
		A11, A12, A13, A14, A15, A16, A17, A18, A19, A20, 
		A21, A22, A23, A24, A25, A26, A27, A28, A29, A30,
		A31, A32, A33, A34, A35, A36, A37, A38, A39, A40, 
		A41, A42, A43, A44, A45, A46, A47, A48, A49, A50,
		Field1,  Field2,  Field3,  Field4,  Field5, 
		Field6,  Field7,  Field8,  Field9,  Field10, 
		Field11, Field12, Field13, Field14, Field15,
		Field16, Field17, Field18, Field19, Field20,
		Field21, Field22, Field23, Field24, Field25, 
		Field26, Field27, Field28, Field29, Field30, 
		CreatedBy, CreatedUtc, LastModifiedBy, LastModifiedUtc)

SELECT  DetailID, 'detailNo', SectionID, '', 'certificateNo', RisKSum, SectionPremium, 
		A1,  A2,  A3,  A4,  A5,  A6,  A7,  A8,  A9,  A10,
		A11, A12, A13, A14, A15, A16, A17, A18, A19, A20, 
		A21, A22, A23, A24, A25, A26, A27, A28, A29, A30,
		A31, A32, A33, A34, A35, A36, A37, A38, A39, A40, 
		A41, A42, A43, A44, A45, A46, A47, A48, A49, A50,
		Field1,  Field2,  Field3,  Field4,  Field5, 
		Field6,  Field7,  Field8,  Field9,  Field10, 
		Field11, Field12, Field13, Field14, Field15,
		Field16, Field17, Field18, Field19, Field20,
		Field21, Field22, Field23, Field24, Field25, 
		Field26, Field27, Field28, Field29, Field30, 
		LOWER(SubmitBy), SubmitOn, LOWER(ModifiedBy), ModifiedOn
FROM	[Gibs5db_CIP].dbo.PolicyTempDetails

-- Migrate SMIs
INSERT 
INTO	[gibs-core-db].[policy].SMIs(

		SectionID, SMIID, DetailNo, SN,
		SMIName, [Description], PremiumRate, TotalSumInsured,
		TotalPremium, ShareSumInsured, SharePremium, PolicyNo,
		CreatedBy, CreatedUtc, LastModifiedBy, LastModifiedUtc)

SELECT  Section, SMICode, p.DetailID, '',
		p.SMIDDetail, ContentDesc, d.PremiumRate, d.SumInsureD,
		d.NetAmount, 'd.', d.ProportionRate, p.PolicyNo,
		LOWER(SubmitBy), SubmitOn, LOWER(p.ModifiedBy), p.ModifiedOn

FROM	[Gibs5db_CIP].dbo.PolicyPremDetails p
JOIN	[Gibs5db_CIP].dbo.PolicyDetails d ON p.DetailID = d.DetailID

*/