using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gibs.Domain.Entities
{

	public class Company
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public string CompanyID { get; set; }

		public string CompanyName { get; set; }

		public string CompanyType { get; set; }

		public string Address { get; set; }

		public string MobilePhone { get; set; }

		public string LandPhone { get; set; }

		public string Email { get; set; }

		public string Remarks { get; set; }

		public byte Deleted { get; set; }

		public byte Active { get; set; }

		public string SubmittedBy { get; set; }

		public DateTime SubmittedOn { get; set; }

		public string ModifiedBy { get; set; }

		public DateTime ModifiedOn { get; set; }
	}
}
