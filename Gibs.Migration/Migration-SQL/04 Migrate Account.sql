DELETE FROM [gibs-core-db].[account].Accounts
DELETE FROM [gibs-core-db].[account].ControlAccounts

PRINT 'Delete Completed'

--Migrate ControlAccounts
INSERT
INTO	[gibs-core-db].[account].ControlAccounts(
		ControlID, ControlName, CategoryID, EntryType, Remarks,
		CreatedBy, CreatedUtc, LastModifiedBy, LastModifiedUtc) 
SELECT	Code, AccountType, LedgerGroup, ISNULL(MainGrp4, '<null-value>'), Tag4,
		'<null-value>', GETDATE(), NULL, NULL 
FROM [Gibs5db_CIP].dbo.AccountType

--Migrate Accounts
INSERT
INTO	[gibs-core-db].[account].Accounts(
		AccountID, AccountName, ControlID, Remarks,
		CreatedBy, CreatedUtc, 
		LastModifiedBy, LastModifiedUtc) 
SELECT	AccountID, AccountName, ControlAcctID, Remark7,
		ISNULL(LOWER(SubmittedBy), '<null-value>'), ISNULL(SubmittedOn, GETDATE()), 
		LOWER(ModifiedBy), ModifiedOn 
FROM [Gibs5db_CIP].dbo.Accounts
