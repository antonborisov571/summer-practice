using Kfoodle.Contracts.Requests.Test.GetPassedTests;
using Kfoodle.Contracts.Requests.Test.GetTestResult;
using MediatR;

namespace Kfoodle.Core.Requests.Test.GetPassedTests;

/// <summary>
/// Запрос на получение пройденных тестов
/// </summary>
public class GetPassedTestsQuery : IRequest<GetPassedTestsResponse>;