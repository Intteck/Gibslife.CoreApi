using System.ComponentModel.DataAnnotations;

namespace Gibs.Domain.Entities
{
    public class Period
    {
        [Key]
        public int Year { get; private set; } //eg. 2016, 2018
        [Required]
        public DateOnly StartDate { get; private set; }
        [Required]
        public DateOnly EndDate { get; private set; }
        [Required]
        public long LedgerIdStart { get; private set; }

        public long LedgerIdEnd { get; private set; }

        public bool IsActive { get; private set; }

        public string Remarks { get; private set; }

        protected Period() { /*EfCore*/ }

        internal Period(
            int year,
            DateOnly startDate,
            DateOnly endDate,
            long ledgerIdStart,
            string remarks) 
        {
            if (year < 1990 || year > 2100)
            {
                throw new ArgumentException("[year] must be between 1990 and 2100");
            }
            if (startDate >= endDate)
            {
                throw new ArgumentException("[endDate] must be later than [startDate]");
            }
            Year = year;
            StartDate = startDate;
            EndDate = endDate;
            LedgerIdStart = ledgerIdStart;
            Remarks = remarks;
        }
    }
}
