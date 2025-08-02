namespace Cubos.Finance.Domain
{
    public interface IPeopleRepository
    {
        Task<People> CreateAsync(People request);
        Task<People> GetByDocumentAsync(string document);
        Task<bool> HasPeopleAsync(string document);
    }

}
