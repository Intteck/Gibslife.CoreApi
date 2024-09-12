using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

namespace Gibs.Domain.Entities
{
    public class Policy : AuditRecord
    {
#pragma warning disable CS8618
        protected Policy() /*EfCore*/
        {
            //Secure = new SecureDetails("E-CHANNEL");
            Histories = new ReadOnlyList<PolicyHistory>();
            DebitNotes = new ReadOnlyList<DebitNote>();
            Insured = new InsuredFields(null);
        }
#pragma warning restore CS8618

        public Policy(ICodeNumberFactory codeFactory,
                      BusinessBasics business,
                      Product product,
                      Customer customer,
                      Branch branch,
                      Party party,
                      //Party? leadParty,
                      Marketer? marketer, 
                      SalesChannel channel,
                      SalesSubChannel subChannel,
                      IEnumerable<SectionRecord> sections)
        {
            ArgumentNullException.ThrowIfNull(codeFactory);
            ArgumentNullException.ThrowIfNull(business);
            ArgumentNullException.ThrowIfNull(sections);

            if (!sections.Any())
                throw new ArgumentNullException(nameof(sections), "Sections cannot be empty");

            Members = new BusinessMembers(product, customer, branch,
                         party, channel, subChannel, marketer);

            PolicyNo = codeFactory.CreateCodeNumber(CodeTypeEnum.POLICY, business.SourceType.ToString());

            Product = product;
            Customer = customer;

            Business = business.ShallowCopy();
            Histories = new ReadOnlyList<PolicyHistory>();
            DebitNotes = new ReadOnlyList<DebitNote>();
            Insured = new InsuredFields(Customer);

            DoNew(codeFactory, business, sections);
        }

        [MemberNotNull(nameof(Premium))]
        private void DoNew(ICodeNumberFactory codeFactory,
                           BusinessBasics business,
                           IEnumerable<SectionRecord> sections)
        {
            if (Histories.Count != 0)
                throw new ArgumentException("You cannot do NEW on an existing policy");

            //Business = business.ShallowCopy();
            var ph = new PolicyHistory(codeFactory, this, business, EndorsementTypeEnum.NEW, sections);
            AppendHistoryAndCalculate(ph);
        }

        public PolicyHistory DoRenew(ICodeNumberFactory codeFactory,
                                     BusinessBasics renewBusiness,
                                     IEnumerable<SectionRecord>? sections = null)
        {
            if (Histories.Count == 0)
                throw new ArgumentException("You cannot do RENEW on a fresh policy");

            //if (renewBusiness.StartDate < Business.EndDate)
            //    throw new ArgumentOutOfRangeException(nameof(renewBusiness.StartDate),
            //        $"Your Renewal StartDate[{renewBusiness.StartDate}] " +
            //        $"cannot be before the previous EndDate[{Business.EndDate}]");

            sections ??= GetCurrentHistory().Sections.Select(SectionRecord.CopyFrom);

            //Business = renewBusiness.ShallowCopy();
            var ph = new PolicyHistory(codeFactory, this, renewBusiness, EndorsementTypeEnum.RENEWAL, sections);
            AppendHistoryAndCalculate(ph);
            return ph;
        }

        public PolicyHistory DoEndorsement(ICodeNumberFactory codeFactory,
                                           BusinessBasics endorsementBusiness, 
                                           EndorsementTypeEnum endorsement,
                                           IEnumerable<SectionRecord> sections)
        {
            if (Histories.Count == 0)
                throw new ArgumentException("You cannot do Endorsement on a fresh policy");

            if (endorsement == EndorsementTypeEnum.NEW ||
                endorsement == EndorsementTypeEnum.RENEWAL)
                throw new ArgumentOutOfRangeException(nameof(endorsement),
                    "NEW, RENEW cannot be done as Endorsement");

            var ph = new PolicyHistory(codeFactory, this, endorsementBusiness, endorsement, sections);
            AppendHistoryAndCalculate(ph);
            return ph;
        }

        private ReadOnlyCollection<PolicyHistory> GetActiveHistories()
        {
            EndorsementTypeEnum[] startsFilter =
            {
                EndorsementTypeEnum.NEW,
                EndorsementTypeEnum.RENEWAL,
            };

            EndorsementTypeEnum[] continueFilter =
            {
                EndorsementTypeEnum.ADDITIONAL,
                EndorsementTypeEnum.EXTENSION,
                EndorsementTypeEnum.REDUCTION
            };

            // check if Histories have only one NEW & RENEW
            var starts = Histories.Where(x => startsFilter.Contains(x.Endorsement))
                                  .OrderByDescending(x => x.SerialNo).ToList();

            var continues = Histories.Where(x => continueFilter.Contains(x.Endorsement))
                                     .OrderByDescending(x => x.SerialNo).ToList();
            if (starts.Count != 0)
                continues.Insert(0, starts.First());

            return continues.AsReadOnly();
        }

        public PolicyHistory GetCurrentHistory()
        {
            var activeHistories = GetActiveHistories();

            if (activeHistories.Count > 0)
                return activeHistories.First();

            //only NIL, EXPIRIED, CANCELLED, RETURN, REVERSAL 
            if (Histories.Count > 0)
                return Histories.OrderByDescending(x => x.SerialNo).First();

            throw new InvalidOperationException("Catastrophic failure! Policy has no History. Restore the Database");
        }

        [MemberNotNull(nameof(Premium))]
        private void AppendHistoryAndCalculate(PolicyHistory ph)
        {
            ((ICollection<PolicyHistory>)Histories).Add(ph);
            //we need the Premium values from the PolicyHistory
            Premium = new BusinessPremium(Business, ph);
        }

        internal void UpdateNaicom(NaicomRecord naicom)
        {
            ArgumentNullException.ThrowIfNull(naicom);

            if (!string.IsNullOrEmpty(naicom.UniqueId))
            {
                NaicomId = naicom.UniqueId;
                return;
            }
            NaicomId = naicom.Status.ToString();
        }

        #region Public Properties
        public long SerialNo { get; }
        public string PolicyNo { get; }
        public InsuredFields Insured { get; }
        public string? NaicomId { get; private set; }

        public BusinessMembers Members { get; protected set; }
        public BusinessBasics Business { get; private set; }
        public BusinessPremium Premium { get; private set; }

        // Navigation Properties
        public virtual Product Product { get; private set; }
        public virtual Customer Customer { get; protected set; }
        public virtual ReadOnlyList<PolicyHistory> Histories { get; private set; }
        public virtual ReadOnlyList<DebitNote> DebitNotes { get; private set; }
        #endregion

        public class InsuredFields  
        {
            public string? FullName { get; init; }
            public string? Address { get; init; }
            public string? Email { get; init; }
            public string? Phone { get; init; }
            public string? PhoneAlt { get; init; }

            public InsuredFields() { }
        
            public InsuredFields(Customer? customer)
            {
                if (customer == null) return;

                FullName = customer.FullName;
                Address = customer.Address;
                Email = customer.Email;
                Phone = customer.Phone;
                PhoneAlt = customer.PhoneAlt;
            }
        }
    }
}
