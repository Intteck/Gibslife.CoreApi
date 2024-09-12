using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gibs.Domain.Entities
{
    public class JournalDetail
    {
        [Key, Column(Order = 0)]
        public string JournalID { get; private set; }
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
        public virtual Journal Journal { get; private set; }

        public virtual Account Account { get; private set; }

        protected JournalDetail() { /*EfCore*/ }

        internal JournalDetail(
            string journalID,
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
            JournalID = journalID;
            AccountID = accountID;
            Narration = narration;
            Reference = reference;
            Credit = credit;
            Debit = debit;
        }
    }
}
