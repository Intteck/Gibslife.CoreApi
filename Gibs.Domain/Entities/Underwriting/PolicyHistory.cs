
namespace Gibs.Domain.Entities
{
    public class PolicyHistory : ApprovalRecord
    {
        #pragma warning disable CS8618
        protected PolicyHistory() /*EfCore*/
        {
            Sections = new ReadOnlyList<PolicySection>();
            //Secure = new ApprovalRecord();
            //ExFields = new DbExtendedFields();
        }
        #pragma warning restore CS8618

        public PolicyHistory(ICodeNumberFactory codeFactory, 
                             Policy policy, BusinessBasics business,
                             EndorsementTypeEnum endorsementType,
                             IEnumerable<SectionRecord> sections)
        {
            if (sections is null || !sections.Any())
                throw new ArgumentNullException(nameof(sections), "Sections cannot be empty");

            var duplicates = sections.DuplicatesBy(x => x.SectionId);

            if (duplicates.Any())
                throw new ArgumentOutOfRangeException(nameof(sections),
                    $"Duplicate SectionIDs [{duplicates.First().SectionId}]");

            SerialNo = long.MaxValue;
            Policy = policy;
            PolicyNo = policy.PolicyNo;
            Endorsement = endorsementType;

            Members = policy.Members.ShallowCopy();
            Business = business.ShallowCopy();

            DeclareNo = codeFactory.CreateCodeNumber(CodeTypeEnum.DECLARATION_NO, endorsementType.ToString());
            EndorsementNo = codeFactory.CreateCodeNumber(CodeTypeEnum.ENDTNO, endorsementType.ToString()); 

            Sections = new ReadOnlyList<PolicySection>();

            foreach (var sec in sections)
            {
                var section = new PolicySection(codeFactory, this, sec);
                ((ICollection<PolicySection>)Sections).Add(section);
            }

            Premium = new BusinessPremium(Business, this);
        }

        public DebitNote CreateDebitNote(ICodeNumberFactory codeFactory)
        {
            if (DebitNoteNo != null)
                throw new Exception("This policy already has a debit note");

            //ExFields.Field50 = "COMPLETED"; //when should this be called??
            //Policy.Business.Approve(approver);

            DebitNote = new DebitNote(codeFactory, this/*, DebitNoteTypeEnum.NORMAL*/);
            ((ICollection<DebitNote>)Policy.DebitNotes).Add(DebitNote);

            DebitNoteNo = DebitNote.DebitNoteNo;
            return DebitNote;
        }
       
        internal void UpdateNaicom(NaicomRecord naicom)
        {
            ArgumentNullException.ThrowIfNull(naicom);

            Policy.UpdateNaicom(naicom);

            if (!string.IsNullOrEmpty(naicom.UniqueId))
            {
                NaicomId = naicom.UniqueId;
                return;
            }
            NaicomId = naicom.Status.ToString();
        }

        #region Public Properties        
        public long SerialNo { get; }
        public string PolicyNo { get; private set; }
        public string DeclareNo { get; private set; } //key aka declarationNo
        //public string ProductID { get; private set; }
        public string EndorsementNo { get; private set; }
        public EndorsementTypeEnum Endorsement { get; private set; }
        public BusinessMembers Members { get; protected set; }
        public BusinessBasics Business { get; private set; }
        public BusinessPremium Premium { get; private set; }

        public string? DebitNoteNo { get; private set; }
        public string? NaicomId { get; private set; }


        //Navigation Properties
        public virtual Policy Policy { get; private set; } 
        public virtual DebitNote? DebitNote { get; private set; } 
        public virtual ReadOnlyList<PolicySection> Sections { get; private set; }
        #endregion
    }
}
