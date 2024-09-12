using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gibs.Domain.Entities
{
    public class PolicyMarineClause
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public long PolicyMarineClauseID { get; set; }

		public string PolicyNo { get; set; }

		public byte Category { get; set; }

		public string SerialNo { get; set; }

		public string HeaderText { get; set; }

		public string Details { get; set; }
	}
}
