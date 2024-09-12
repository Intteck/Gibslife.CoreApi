DELETE FROM [gibs-core-db].[policy].[Master]

--Migrate Master
SET IDENTITY_INSERT [gibs-core-db].[policy].[Master] ON
INSERT
INTO	[gibs-core-db].[policy].[Master](
		SN, PolicyNo, 

		--insured
		FullName, [Address], Email, Phone, PhoneAlt, 

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
		CreatedBy, CreatedUtc, LastModifiedBy, LastModifiedUtc)

SELECT  p.PolicyID, p.PolicyNo, 
		p.InsFullName, p.InsAddress, LOWER(p.InsEmail), p.InsMobilePhone, p.InsMobilePhone2,

		--members
		p.SubRiskID, p.SubRisk, ISNULL(p.InsuredID, '<null-value>'), ISNULL(p.InsFullName, ISNULL(p.InsSurname, '<null-value>')),
		ISNULL(p.BranchID, '<null-value>'), p.Branch, ISNULL(p.PartyID, '<null-value>'), ISNULL(p.Party, '<null-value>'),
		--p.LeadID, p.Leader, --MktStaffID, MktStaff,
		p.InsStateID, p.Remarks, p.MktGroupID, p.MktGroup,
		 
		--business
		NULLIF(p.CoPolicyNo, p.PolicyNo), 
		p.BizSource, ISNULL(p.SourceType,'<null-value>'), p.TrackID,
		p.TransDate, p.StartDate, ISNULL(p.EndDate, GETDATE()), ISNULL(p.ProRataDays, 0), 365, ISNULL(p.ExCurrency, 'NGN'),
		ISNULL(p.ExRate, 1), p.ProportionRate, --ISNULL(p.PremiumRate, 0), 0, 0,

		--premium
		p.GrossPremiumFrgn, p.GrossPremium, p.GrossPremium, ISNULL(p.A2, p.GrossPremium), ISNULL(p.ProRataPremium, 0), ISNULL(p.A3, 0),
		p.SumInsuredFrgn, p.SumInsured, p.SumInsured, ISNULL(p.A1, 0),

		--secure
		ISNULL(LOWER(p.SubmittedBy), '<null-value>'), ISNULL(p.SubmittedOn, GETDATE()), LOWER(p.ModifiedBy), p.ModifiedOn

FROM	[Gibs5db_CIP].dbo.Policies p

--Exclude 5 Policy duplicates
WHERE	PolicyNo IN (SELECT PolicyNo
					 FROM [Gibs5db_CIP].[dbo].[Policies]
					 GROUP BY PolicyNo
					 HAVING COUNT(*) = 1) 
AND		BizSource IN ('DIRECT', 'ACCEPTED', 'CO-INSURANCE LEAD', 'FAC-IN')
AND		GrossPremium IS NOT NULL
SET IDENTITY_INSERT [gibs-core-db].[policy].[Master] OFF



--SELECT * FROM [Gibs5db_CIP].[dbo].[Policies]
--WHERE BizSource NOT IN ('DIRECT', 'ACCEPTED', 'CO-INSURANCE LEAD', 'FAC-IN') 
