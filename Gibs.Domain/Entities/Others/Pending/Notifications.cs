using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gibs.Domain.Entities
{

    public enum NotificationTypeEnum
	{
		EMAIL,
		TEXT
	}

	public class NotificationEvent
	{
		[Key]
		public string EventId { get; set; }
		[Required]
		public string Description { get; set; }

	}

	public class NotificationSetting : AuditRecord
	{
		[Key]
		public string EventId { get; set; }
		[Key]
		public NotificationTypeEnum NotificationType { get; set; }

		public string TemplateText { get; set; }

		public string HeaderText { get; set; }

		public bool IsActive { get; set; }

	}

	public class NotificationGroup : AuditRecord
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public string GroupId { get; set; }
		[Required]
		public string GroupName { get; set; }
		[Required]
		public string GroupData { get; set; }
		[Required]
		public string Count { get; set; }

	}

	public class NotificationHistory : AuditRecord
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public string NotificationHistoryId { get; set; }

		public string EventId { get; set; }

		public NotificationTypeEnum NotificationType { get; set; }

		public DateTime StartDate { get; set; }

		public DateTime EndDate { get; set; }

		public string Destination { get; set; }

		public string MessageText { get; set; }

		public string HeaderText { get; set; }

		public byte Status { get; set; }

	}
}
