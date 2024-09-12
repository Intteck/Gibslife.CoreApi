DELETE FROM [gibs-core-db].[agency].Notes

--Migrate Debit Notes
INSERT
INTO	[gibs-core-db].[agency].Notes(
		NoteNo, DeclarationNo, NoteTypeID, BranchID,
		PolicyNo, Narration, PartyID, PartyName, 
		FxCurrencyId, FxRate, Amount, Approval,
		CreatedBy, CreatedUtc, ApprovedBy, ApprovedUtc, 
		Z_NAICOM_UID, Z_NAICOM_SENT_ON, Z_NAICOM_STATUS, Z_NAICOM_ERROR, Z_NAICOM_JSON)

SELECT  n.DNCNNo, n.refDNCNNo, n.NoteType, ISNULL(n.BranchID, '<null-value>'),
		ISNULL(n.PolicyNo, '<null-value>'), n.Narration, ISNULL(n.PartyID, '<null-value>'), ISNULL(n.Party, '<null-value>'),
		ISNULL(n.ExCurrency, 'NGN'), ISNULL(n.ExRate, 0), ISNULL(n.NetAmount, 0), n.Approval, 
		ISNULL(LOWER(n.SubmittedBy), '<null-value>'), ISNULL(n.SubmittedOn, GETDATE()), LOWER(n.ApprovedBy), n.ApprovedOn, 
		n.Z_NAICOM_UID, n.Z_NAICOM_SENT_ON, n.Z_NAICOM_STATUS, n.Z_NAICOM_ERROR, n.Z_NAICOM_JSON

FROM	[Gibs5db_CIP].dbo.DNCNNotes n
JOIN	[gibs-core-db].[policy].[Master] p ON p.PolicyNo = n.PolicyNo
WHERE	NoteType = 'DN' AND  n.DNCNNo IS NOT NULL
--Exclude 5 Policy duplicates
AND 	n.DNCNNo IN   (SELECT DNCNNo
					   FROM	[Gibs5db_CIP].dbo.DNCNNotes n
					   GROUP BY DNCNNo
					   HAVING COUNT(*) = 1) 
AND 	p.PolicyNo IN (SELECT PolicyNo
					   FROM [Gibs5db_CIP].[dbo].[Policies]
					   GROUP BY PolicyNo
					   HAVING COUNT(*) = 1) 
