using Kfoodle.Core.Enums;
using FluentValidation;

namespace Kfoodle.Core.Requests.Auth.PostLogin;

/// <summary>
/// Валидатор для <see cref="PostLoginCommand"/>
/// </summary>
public class PostLoginCommandValidator : AbstractValidator<PostLoginCommand>
{
    /// <inheritdoc />
    public PostLoginCommandValidator()
    {
        RuleFor(command => command.Email)
            .NotEmpty().WithMessage(AuthErrorMessages.EmptyField("Почта"));

        RuleFor(command => command.Email)
            .EmailAddress().WithMessage(AuthErrorMessages.InvalidEmailFormat);
        
        RuleFor(command => command.Password)
            .NotEmpty().WithMessage(AuthErrorMessages.EmptyField("Пароль"));
    }
}