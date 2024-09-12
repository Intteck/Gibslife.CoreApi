DELETE FROM [gibs-core-db].[product].SMIs
DELETE FROM [gibs-core-db].[product].Sections
DELETE FROM [gibs-core-db].[product].Fields
DELETE FROM [gibs-core-db].[product].[Master]
DELETE FROM [gibs-core-db].[product].MidRisks
DELETE FROM [gibs-core-db].[product].Risks

PRINT 'Delete Completed'

--Migrate Risks
INSERT INTO [gibs-core-db].[product].Risks(
			RiskID, RiskName) 
SELECT		RiskID, Risk
FROM		[Gibs5db_CIP].dbo.Risks

--Migrate MidRisks
INSERT INTO [gibs-core-db].[product].MidRisks(
			RiskID, MidRiskID, MidRiskName) 
SELECT		RiskID, MidRiskID, MidRisk
FROM		[Gibs5db_CIP].dbo.MidRisks

--Migrate Product
INSERT INTO [gibs-core-db].[product].[Master](
			ProductID, MidRiskID, RiskID, 
			ProductName, AltName, NaicomCode,
			AutoNumNextClaimNo, AutoNumNextNotifyNo,
			CreatedBy, CreatedUtc, LastModifiedBy, LastModifiedUtc) 
SELECT		S.SubRiskID, M.MidRiskID, S.RiskID, 
			S.SubRisk, S.[Description], S.InsuranceTypeID, 0, 0, 
			S.SubmittedBy, S.SubmittedOn, S.ModifiedBy, S.ModifiedOn 
FROM		[Gibs5db_CIP].dbo.SubRisks S
LEFT JOIN	[Gibs5db_CIP].dbo.MidRisks M ON S.MidRiskID = M.MidRiskID

--Migrate Section
INSERT INTO [gibs-core-db].[product].Sections(
			ProductID, SectionID, SectionName) 
SELECT		S.SubRiskID, C.SectionCode, C.SectionName 
FROM		[Gibs5db_CIP].dbo.SubRiskSections C
INNER JOIN	[Gibs5db_CIP].dbo.SubRisks S ON S.SubRiskID = C.SubRiskID

--Migrate SMIs
INSERT INTO [gibs-core-db].[product].SMIs(
			ProductID, SectionID, SmiID, SmiName) 
SELECT		M.SubRiskID, M.SectionCode, M.SMICode, MAX(M.SMIDetails)   
FROM		[Gibs5db_CIP].[dbo].[SubRiskSMIs] M
INNER JOIN	[gibs-core-db].[product].Sections S ON M.SectionCode = S.SectionID AND M.SubRiskID = S.ProductID
GROUP BY	SubRiskID, SectionCode, SMICode


--Migrate Field
INSERT INTO [gibs-core-db].[product].Fields(
			CodeID, CodeTypeID, FieldID, DataTypeID,
			DbSectionField, DbHistoryField,
			[Description], IsParent, IsRequired,
			DefValue, MinValue, MaxValue, [Group], [Serial], 
			CreatedBy, CreatedUtc, LastModifiedBy, LastModifiedUtc) 
SELECT		CodeID, CodeType, FieldCode, FieldType, 
			DbSectionField, DbHistoryField,
			[Description], IsParent, IsRequired, 
			DefValue, MinValue, MaxValue, [Group], [Serial], 
			SubmittedBy, SubmittedOn, ModifiedBy, ModifiedOn 
FROM		[Gibs5db_CIP].dbo.SubRiskFieldsV2

