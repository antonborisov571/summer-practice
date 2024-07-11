using Kfoodle.Contracts.Requests.Test.PostAddQuestions;
using MediatR;

namespace Kfoodle.Core.Requests.Test.PostAddQuestions;

/// <summary>
/// Запрос на добавление вопросов
/// </summary>
public class PostAddQuestionsCommand : IRequest
{
     /// <summary>
     /// Конструктор
     /// </summary>
     /// <param name="id"></param>
     /// <param name="request"></param>
     public PostAddQuestionsCommand(Guid id, PostAddQuestionsRequest request)
     {
          TestId = id;
          SourceTestId = request.SourceTestId;
     }
     
     /// <summary>
     /// Id теста
     /// </summary>
     public Guid TestId { get; set; }
     
     /// <summary>
     /// Id теста для добавления
     /// </summary>
     public Guid SourceTestId { get; set; }
}