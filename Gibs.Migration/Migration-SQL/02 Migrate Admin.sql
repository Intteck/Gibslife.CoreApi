DELETE FROM [gibs-core-db].[common].SubChannels
DELETE FROM [gibs-core-db].[common].Channels
DELETE FROM [gibs-core-db].[common].Branches
DELETE FROM [gibs-core-db].[common].Regions
DELETE FROM [gibs-core-db].[common].Settings
DELETE FROM [gibs-core-db].[agency].Marketers

PRINT 'Delete Completed'

--Migrate Marketers
INSERT
INTO	[gibs-core-db].[agency].Marketers(
		MarketerID, ChannelID, 
		SubChannelID, FullName, Active,
		CreatedBy, CreatedUtc, LastModifiedBy, LastModifiedUtc) 
SELECT	MktStaffID, mktUnitID,
		MktGrpID, StaffName, ISNULL(Active,1),
		ISNULL(SubmittedBy, '<null-user>'), ISNULL(SubmittedOn, GETDATE()), ModifiedBy, ModifiedOn
FROM	[Gibs5db_CIP].dbo.MktStaffs

--Migrate Settings
INSERT
INTO	[gibs-core-db].[common].Settings(
		SettingID, [Value], [SettingName], DataTypeID,
		MinValue, MaxValue, DefValue, CreatedBy, CreatedUtc) 
SELECT	Setting, [Value], Setting, 'STRING', 
		'', '', '', '<null-value>', GETDATE()
FROM	[Gibs5db_CIP].dbo.Settings

--Migrate Regions
INSERT
INTO	[gibs-core-db].[common].Regions(
		RegionID, RegionName, AltName,
		CreatedBy, CreatedUtc, LastModifiedBy, LastModifiedUtc) 
SELECT	RegionID, Region, Remarks, 
		SubmittedBy, SubmittedOn, ModifiedBy, ModifiedOn
FROM	[Gibs5db_CIP].dbo.Regions

--Migrate Branches
INSERT
INTO	[gibs-core-db].[common].Branches(
		BranchID, RegionID, AltName, StateID, 
		BranchName, [Address], Phone, Tag, 
		CreatedBy, CreatedUtc, LastModifiedBy, LastModifiedUtc) 
SELECT	BranchID, RegionID, Manager, StateID, 
		[Description], [Address], MobilePhone, BranchID2, 
		ISNULL(SubmittedBy, '<null-value>'), ISNULL(SubmittedOn, GETDATE()), ModifiedBy, ModifiedOn	
FROM	[Gibs5db_CIP].dbo.Branches 
WHERE	RegionID != '014' --error entry

--Migrate Channels
INSERT
INTO	[gibs-core-db].[common].Channels(
		ChannelID, BranchID, ChannelName) 
SELECT	ChannelID, ISNULL(NULLIF(NULLIF([Description], 'NULL'), ''),'100') A, Channels
FROM	[Gibs5db_CIP].dbo.MktChannels 
WHERE	[Description] NOT IN ('405') --MISSING 405 BRANCH IN SOURCE TABLE

--Migrate SubChannels, 
INSERT
INTO	[gibs-core-db].[common].SubChannels(
		ChannelID, SubChannelID, SubChannelName, AltName) 
SELECT	G.BranchID, G.MktGroupID, G.GroupName, G.[Description]
FROM	[Gibs5db_CIP].dbo.MktGroups G
INNER JOIN [gibs-core-db].[common].Channels C
		ON G.BranchID = C.ChannelID

--