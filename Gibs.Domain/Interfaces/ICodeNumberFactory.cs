namespace Gibs.Domain.Entities
{
    public interface ICodeNumberFactory
    {
        string CreateCodeNumber(CodeTypeEnum codeType, string? bizSource = null);
    }
}