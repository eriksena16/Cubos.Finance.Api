namespace Cubos.Finance.External
{
    public interface IComplianceFacade
    {
        Task<bool> IsDocumentValidAsync(string document);
    }

}
