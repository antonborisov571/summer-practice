using AutoMapper;
using Kfoodle.Contracts.Requests.Test.GetEditQuestions;
using Kfoodle.Contracts.Requests.Test.GetEditTest;
using Kfoodle.Contracts.Requests.Test.GetStartTest;
using Kfoodle.Contracts.Requests.Test.GetTest;
using Kfoodle.Core.Entities;
using Choice = Kfoodle.Core.Entities.Choice;
using Question = Kfoodle.Core.Entities.Question;

namespace Kfoodle.Core.Profiles;

/// <summary>
/// Для маппинга
/// </summary>
public class AppMappingProfile : Profile
{
    /// <summary>
    /// Конструктор
    /// </summary>
    public AppMappingProfile()
    {
        // GetEditTest
        CreateMap<Test, GetEditTestResponse>();
        
        // GetEditQuestions
        CreateMap<Test, GetEditQuestionsResponse>();

        CreateMap<Question, Kfoodle.Contracts.Requests.Test.GetEditQuestions.Question>();

        CreateMap<Choice, Kfoodle.Contracts.Requests.Test.GetEditQuestions.Choice>();
        
        // GetTest
        CreateMap<Test, GetTestResponse>();

        CreateMap<Question, QuestionItem>();

        CreateMap<Choice, ChoiceItem>();
        
        // GetTest
        CreateMap<Test, GetStartTestResponse>();
    }
}