using Kfoodle.Contracts.Requests.Test.GetCatalog;
using MediatR;

namespace Kfoodle.Core.Requests.Test.GetCatalog;

/// <summary>
/// Запрос на получение каталога
/// </summary>
public class GetCatalogQuery : IRequest<GetCatalogResponse>;