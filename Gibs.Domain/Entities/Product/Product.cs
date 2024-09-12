using System.ComponentModel.DataAnnotations;

namespace Gibs.Domain.Entities
{
    public class Product : AuditRecord
	{
		#pragma warning disable CS8618
		private Product() { }
		#pragma warning restore CS8618

        public Product(string id, MidClass? midClass, Class @class, 
			string name, string? altName, string? naicomCode)
        {
            ClassId = @class.Id;

            if (midClass != null)
			{
                MidClassId = midClass.Id;
            }

            Id = id;
            Name = name;
            AltName = altName;
            NaicomCode = naicomCode;
        }

        public void UpdateProduct(string classId, string? midClassId, 
			string name, string? altName, string? naicomCode)
        {
            ClassId = classId;
            MidClassId = midClassId;
            Name = name;
            AltName = altName;
            NaicomCode = naicomCode;
        }

        public IEnumerable<ProductField> GetAllFields()
		{
			if (_fields == null)
			{
				if (_fieldsCache == null)
					throw new InvalidOperationException("Products FieldsCache was not set");

				_fields = _fieldsCache.Where(x => ProductField.WhereProduct(x, this));
			}

			return _fields;
		}
		public IEnumerable<ProductField> GetVisibleFields()
		{
			return GetAllFields().Where(x => x.IsHidden == false);
		}

		public static void SetRatesCache(IEnumerable<CommRate> rates)
		{
			if (_ratesCache != null)
				throw new InvalidOperationException("Commission RatesCache is already set");

			_ratesCache = rates;
		}

		public static void SetFieldsCache(IEnumerable<ProductField> fields)
		{
			if (_fieldsCache != null)
				throw new InvalidOperationException("Product FieldsCache is already set");

			_fieldsCache = fields;
		}

		public void SetNextClaimNo(long nextNumber)
		{
			AutoNumNextClaimNo = nextNumber;
		}

		public void SetNextNotifyNo(long nextNumber)
		{
			AutoNumNextNotifyNo = nextNumber;
		}

		public CommRate GetCommissionRate(Party party, string[] allPartyTypes)
		{
			if (_ratesCache == null)
				throw new InvalidOperationException("Commission RatesCache was not set");

			//var partyTypes = party.TypeId.ToGibsDbStrings();

			var riskOptions = new List<string?> { Id, MidClassId, ClassId };
			var partyOptions = new List<string?> { party.Id };

			partyOptions.AddRange(allPartyTypes);
			partyOptions.Add(string.Empty); 

			foreach (var agentOption in partyOptions)
			{
				foreach (var riskOption in riskOptions)
				{
					var rate = _ratesCache.FirstOrDefault(x => x.RiskOptionId == riskOption
                                                            && x.PartyOptionId == agentOption);
					if (rate != null) return rate;
				}
			}

			throw new Exception($"Commission Rates was not setup " +
				$"for Agent {party.Id} or AgentType {party.Type}, " +
				$"for either Product [{Id}] or MidClass [{MidClassId}] or Class [{ClassId}]");
		}

        //Fields
        private static IEnumerable<CommRate>? _ratesCache;
		private static IEnumerable<ProductField>? _fieldsCache;
		private IEnumerable<ProductField>? _fields;

		//Properties
		public string Id { get; private set; }
		public string? MidClassId { get; private set; }
		public string ClassId { get; private set; }
		[Required]
		public string Name { get; private set; }
		public string? AltName { get; private set; }
		public string? NaicomCode { get; private set; }


		public long AutoNumNextClaimNo { get; private set; }
		public long AutoNumNextNotifyNo { get; private set; }


		//Navigation Properties
		public ReadOnlyList<ProductSection> Sections { get; init; } = [];
		//public ReadOnlyList<ProductCover> Covers { get; init; }
		//public ReadOnlyList<ProductSMI> SMIs { get; init; }
	}
}
