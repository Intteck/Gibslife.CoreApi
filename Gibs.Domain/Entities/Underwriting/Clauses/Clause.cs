namespace Gibs.Domain.Entities
{
    public abstract class Clause
	{
		public required string Id { get; set; }
        //public required string TypeId { get; set; }
        public string? ProductId { get; set; }
		public string? Category { get; set; }
	}

	public class MemoClause : Clause
	{
		public string? HeaderText { get; set; }
        public string? Document1 { get; set; }
        public string? Document2 { get; set; }
    }

    public class MarineClause : Clause
	{
        public string? Details { get; set; }
        public string? ICC { get; set; }
    }

    public class MotorClause : Clause
    {
        public string? EntitledToA { get; set; }
        public string? EntitledToB { get; set; }
        public string? LimitationA { get; set; }
        public string? LimitationB { get; set; }
        public string? VehicleUsage { get; set; }
    }

    //Public Enum MemoClauseCategoryEnum As Byte
    //    STANDARD   = 0
    //    [OPTIONAL] = 1
    //End Enum

}

