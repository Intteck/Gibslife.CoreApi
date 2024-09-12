using System.ComponentModel.DataAnnotations;

namespace Gibs.Domain.Entities
{
    public class ComputeField : AuditRecord
	{
		private ComputeField() { }

		public ComputeField(ProductSection section, string fieldCode, FieldTypeEnum fieldType, string dbMapField, string description, Dictionary<string, string> valuePairs)
		{
            ArgumentNullException.ThrowIfNull(section);

            if (string.IsNullOrWhiteSpace(fieldCode))
				throw new ArgumentNullException(nameof(fieldCode));

			if (string.IsNullOrWhiteSpace(dbMapField))
				throw new ArgumentNullException(nameof(dbMapField));

			//if (fieldType == FieldTypeEnum.ENUM)
			//	UpdateEnumFieldType(valuePairs);

			SectionId = section.SectionId;
			ProductId = section.ProductId;
			FieldType = fieldType;
			FieldCode = fieldCode;
			DbMapField = dbMapField;
			Description = description;
			IsRequired = false;
			SerialNo = 0;
		}

		public void UpdateDbMapField(string dbMapField)
		{
			if (string.IsNullOrWhiteSpace(dbMapField))
				throw new ArgumentNullException(nameof(dbMapField));

			DbMapField = dbMapField;
		}

		public void UpdateDefault(object defaultValue)
        {
			DefaultValue = defaultValue.ToString();

        }


		[Key, Required]
		public string ProductId { get; private set; }
		[Key, Required]
		public string SectionId { get; private set; }
		[Key, Required]
		public string FieldCode { get; private set; }
		[Required]
		public FieldTypeEnum FieldType { get; private set; }
		[Required]
		public string DbMapField { get; private set; }

		public string? Description { get; set; }

		public bool IsRequired { get; set; }

		public string? DefaultValue { get; private set; }
		public string? MinimumValue { get; private set; }
		public string? MaximumValue { get; private set; }

		public string? GroupName { get; private set; }
		public int SerialNo { get; set; }

		public enum FieldTypeEnum
		{
			PREMIUM,
			LOADING,
			DISCOUNT,
			MULTIPLIER,
		}
	}
}
