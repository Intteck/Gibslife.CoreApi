using Gibs.Domain.Entities;

namespace Gibs.Api.Contracts.V1
{
    public class CreateMarineClauseRequest: UpdateMarineClauseRequest
    {
        public required string ClauseID { get; init; }
    }
    public class UpdateMarineClauseRequest
    {
        public string? CategoryID { get; init; }
        public string? Details { get; init; }
        public string? ICC { get; init; }
    }
    public class MarineClauseResponse(MarineClause clause)  
    {
        public string ClauseID { get; } = clause.Id;
        public string? CategoryID { get; } = clause.Category;
        public string? Details { get; } = clause.Details;
        public string? ICC { get; } = clause.ICC;
    }


    public class CreateMotorClauseRequest : UpdateMotorClauseRequest
    {
        public required string ClauseID { get; init; }
    }
    public class UpdateMotorClauseRequest
    {
        public string? ProductID { get; set; }
        public string? CoverTypeID { get; set; }
        public string? EntitledToA { get; set; }
        public string? EntitledToB { get; set; }
        public string? LimitationA { get; set; }
        public string? LimitationB { get; set; }
        public string? VehicleUsage { get; set; }
    }
    public class MotorClauseResponse(MotorClause clause)  
    {
        public string ClauseID { get; } = clause.Id;
        public string? ProductID { get; } = clause.ProductId;
        public string? CoverTypeID { get; } = clause.Category;
        public string? EntitledToA { get; } = clause.EntitledToA;
        public string? EntitledToB { get; } = clause.EntitledToB;
        public string? LimitationA { get; } = clause.LimitationA;
        public string? LimitationB { get; } = clause.LimitationB;
        public string? VehicleUsage { get; } = clause.VehicleUsage;
    }


    public class CreateMemoClauseRequest : UpdateMemoClauseRequest
    {
        public required string ClauseID { get; init; }
    }
    public class UpdateMemoClauseRequest
    {
        public string? ProductID { get; set; }
        public string? CategoryID { get; set; }
        public string? HeaderText { get; set; }
        public string? Document1 { get; set; }
        public string? Document2 { get; set; }
    }
    public class MemoClauseResponse(MemoClause clause)  
    {
        public string ClauseID { get; } = clause.Id;
        public string? ProductID { get; } = clause.ProductId;
        public string? CategoryID { get; } = clause.Category;
        public string? HeaderText { get; } = clause.HeaderText;
        public string? Document1 { get; } = clause.Document1;
        public string? Document2 { get; } = clause.Document2;
    }
}
