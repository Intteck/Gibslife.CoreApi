USE [gibs-core-db]

ALTER TABLE [gibs-core-db].[policy].[Master] DROP CONSTRAINT IF EXISTS chk_ActTypeID
ALTER TABLE [gibs-core-db].[policy].[Master] DROP CONSTRAINT IF EXISTS chk_BizTypeID
ALTER TABLE [gibs-core-db].[policy].[Master] DROP CONSTRAINT IF EXISTS chk_SrcTypeID

ALTER TABLE [gibs-core-db].[policy].[Master] ADD CONSTRAINT chk_ActTypeID CHECK (ActTypeID IN (0, 100))
--ALTER TABLE [gibs-core-db].[policy].[Master] ADD CONSTRAINT chk_BizTypeID CHECK (BizTypeID IN ('Smith', 'Anderson', 'Jones'))
--ALTER TABLE [gibs-core-db].[policy].[Master] ADD CONSTRAINT chk_SrcTypeID CHECK (SrcTypeID IN ('Smith', 'Anderson', 'Jones'))

DELETE FROM [gibs-core-db].[account].FxCurrencies
DELETE FROM [gibs-core-db].[agency].PartyTypes

INSERT INTO [gibs-core-db].[account].FxCurrencies(
			FxCurrencyID, Symbol, FxCurrencyName, LastRate, Active)
VALUES ('NGN', '₦', 'Naira', 1, 1),
	   ('USD', '$', 'US Dollars', 1, 1),
	   ('EUR', '€', 'Euros', 1, 1),	   
	   ('GBP', '£', 'Pounds', 1, 1),
	   ('YEN', '¥', 'Yen', 1, 1);

INSERT INTO [gibs-core-db].[agency].PartyTypes(
			CategoryID, PartyTypeID, PartyTypeName)
VALUES 
       ('A', 'BR', 'BROKER'),
	   ('A', 'AG', 'AGENT'),
	   ('A', 'SG', 'STAFF AGENT'),
	   ('A', 'ASG','ASSOCIATE STAFF'),
	   ('A', 'OA', 'OUTSOURCED AGENT'),
	   ('A', 'DI', 'DIRECT INDIVIDUAL'),
       ('A', 'DC', 'DIRECT CORPORATE'),
	   ('A', 'FP', 'FINANCIAL PLANNER'),
	   ('A', 'IN', 'INSURANCE COMPANY'),
	   ('A', 'RB', 'RE-INSURANCE BROKER'),
	   ('A', 'RI', 'RE-INSURANCE COMPANY'),
       ('A', 'BAS','BANCASSURANCE ASSOCIATE'),
	   ('A', 'FI', 'FACULTATIVE PARTICIPANT'),

       ('B', 'BA', 'BANK'),
	   ('B', 'BN', 'BANK NON-AGENT'),
	   ('B', 'NB', 'NON-BANK INSTITUTION'),
	   ('B', 'FA', 'FACILITATOR-CRM/MARKETER'),
	   ('B', 'SP', 'SUPERINTENDENT'),
	   ('B', 'RA', 'RECOVERY AGENT'),
	   ('B', 'SA', 'SALVAGE'),
	   ('B', 'OT', 'OTHERS'),

	   ('C', 'GA', 'GARAGE'),
	   ('C', 'EN', 'ENGINEER'),
	   ('C', 'AD', 'ADVOCATE/LAWYER'),
	   ('C', 'MC', 'MEDICAL CONSULTANT'),
	   ('C', 'LA', 'LOSS ADJUSTER'),
	   ('C', 'SU', 'SURVEYOR');