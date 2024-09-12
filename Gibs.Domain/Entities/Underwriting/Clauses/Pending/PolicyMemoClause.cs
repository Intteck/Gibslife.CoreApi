using System.ComponentModel.DataAnnotations;

namespace Gibs.Domain.Entities
{

    public class PolicyMemoClause
	{
		[Key]
		public long PolicyMemoClauseID { get; set; }

		public string PolicyNo { get; set; }

		public byte Category { get; set; }

		public string HeaderText { get; set; }

		public string Details { get; set; }

	}
}
