//using System;
//using System.Collections;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace Gibs.Domain.Entities
//{
//	public class PolicyCover
//	{
//		[Key]
//		public long PolicyCoverID { get; set; }

//		public string PolicySectionID { get; set; }
//		[Required]
//		public string PolicyNo { get; set; }

//		public string CoverID { get; set; }
//		[Required]
//		public string CoverName { get; set; }

//		public decimal Value { get; set; }

//		public decimal Rate { get; set; }

//		public decimal Premium { get; set; }

//		//all these below only apply to Fire
//		//
//		//Public Property Location As String
//		//Public Property FeaRate As Dtouble?
//		//Public Property FeaValue As Decimal
//		//Public Property NetPremium As Decimal

//		//Navigation Properties
//		public virtual PolicySection PolicySection { get; private set; }
//		public virtual ProductCover Cover { get; private set; }

//	}
//}
