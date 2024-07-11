using Kfoodle.Contracts.Requests.Test.PatchUpdateTest;
using MediatR;

namespace Kfoodle.Core.Requests.Test.PatchUpdateTest;

/// <summary>
/// Конструктор
/// </summary>
/// <param name="request"></param>
public class PatchUpdateTestCommand(PatchUpdateTestRequest request) : PatchUpdateTestRequest(request), IRequest;