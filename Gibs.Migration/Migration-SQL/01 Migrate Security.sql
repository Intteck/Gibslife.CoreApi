DELETE FROM [gibs-core-db].[security].AuditLog
DELETE FROM [gibs-core-db].[security].Roles
DELETE FROM [gibs-core-db].[security].Users

PRINT 'Delete Completed'

--Migrate Users
INSERT
INTO	[gibs-core-db].[security].Users (
		UserID, [Password], FirstName, LastName,
		Email, Phone, [Address], Active, 
		PwdExpiryDate, LastLoginDate, ApiKey, 
		CreatedBy, CreatedUtc, LastModifiedBy, LastModifiedUtc) 
SELECT	LOWER(Username), [Password], UPPER(FirstName), UPPER(LastName), 
		LOWER(Email), Phone, [Address], Active, 
		PwdExpiry, LastLoginDate, NEWID(), 
		LOWER(SubmittedBy), SubmittedOn, LOWER(ModifiedBy), ModifiedOn
FROM	[Gibs5db_CIP].dbo.Users

--Migrate Roles
INSERT
INTO	[gibs-core-db].[security].Roles(
		RoleID, RoleName, 
		CreatedBy, CreatedUtc, LastModifiedBy, LastModifiedUtc) 
SELECT	RoleID, UPPER(RoleName), 
		LOWER(SubmittedBy), SubmittedOn, LOWER(ModifiedBy), ModifiedOn
FROM	[Gibs5db_CIP].dbo.Roles

--Migrate Auditlogs
SET IDENTITY_INSERT [gibs-core-db].[security].AuditLog ON
INSERT
INTO	[gibs-core-db].[security].AuditLog(
		AuditLogID, [ActionID], Category, ModuleID, 
		[Description], CreatedUtc, CreatedBy, CreatedIP) 
SELECT	AuditLogID, LogType, ISNULL(Category, '<null-value>'), [Source], 
		[Description], SubmittedOn, SubmittedBy, '192.168.0.1'
FROM	[Gibs5db_CIP].dbo.AuditLogs

SET IDENTITY_INSERT [gibs-core-db].[security].AuditLog OFF
