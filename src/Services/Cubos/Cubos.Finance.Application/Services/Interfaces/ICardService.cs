using Cubos.Finance.Shared;

namespace Cubos.Finance.Application
{
    public interface ICardService
    {
        /// <summary>
        /// Cria um novo cartão vinculado a uma conta bancária.
        /// </summary>
        Task<CardResponse> CreateCardAsync(Guid bankAccountId, CardRequest request);

        /// <summary>
        /// Retorna todos os cartões associados a uma conta bancária.
        /// </summary>
        Task<List<CardResponse>> GetCardsAsync(Guid bankAccountId);

        /// <summary>
        /// Retorna os cartões vinculados a uma pessoa, com suporte à paginação e filtro.
        /// </summary>
        Task<QueryBaseResponse<CardResponse>> GetCardsFromPeopleAsync(Guid peopleId, CardPaginationRequest request);

    }
}
