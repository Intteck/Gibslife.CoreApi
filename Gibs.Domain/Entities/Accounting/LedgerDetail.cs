using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gibs.Domain.Entities
{
    public class LedgerDetail
    {
        [Key, Column(Order = 0)]
        public string LedgerID { get; private set; }
        [Key, Column(Order = 1)]
        public string AccountID { get; private set; }
        [Required]
        public string Narration { get; private set; }
        [Required]
        public decimal Debit { get; private set; }
        [Required]
        public decimal Credit { get; private set; }

        public string Reference { get; private set; }

        //Navigation Properties
        public virtual Ledger Ledger { get; private set; }

        public virtual Account Account { get; private set; }

        protected LedgerDetail() { /*EfCore*/ }

        internal LedgerDetail(
            string ledgerID,
            string accountID,
            string narration,
            decimal debit,
            decimal credit,
            string reference)
        {
            if (debit < 0 || credit < 0)
            {
                throw new ArgumentException("Negative values are not allowed");
            }
            if (debit == 0 && credit == 0)
            {
                throw new ArgumentException("Both [Debit] and [Credit] cannot be zero");
            }
            if (debit > 0 && credit > 0)
            {
                throw new ArgumentException("Both [Debit] and [Credit] cannot have values");
            }
            LedgerID = ledgerID;
            AccountID = accountID;
            Narration = narration;
            Reference = reference;
            Credit = credit;
            Debit = debit;
        }
    }
}
