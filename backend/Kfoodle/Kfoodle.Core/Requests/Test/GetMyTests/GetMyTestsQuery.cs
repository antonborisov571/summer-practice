using Kfoodle.Contracts.Requests.Test.GetMyTests;
using MediatR;

namespace Kfoodle.Core.Requests.Test.GetMyTests;

/// <summary>
/// Запрос на получение моих тестов
/// </summary>
public class GetMyTestsQuery : IRequest<GetMyTestsResponse>;