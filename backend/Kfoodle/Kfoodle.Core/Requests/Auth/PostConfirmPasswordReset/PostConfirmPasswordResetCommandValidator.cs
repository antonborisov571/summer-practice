using Kfoodle.Core.Enums;
using FluentValidation;

namespace Kfoodle.Core.Requests.Auth.PostConfirmPasswordReset;

/// <summary>
/// Валидатор для <see cref="PostConfirmPasswordResetCommand"/>
/// </summary>
public class PostConfirmPasswordResetCommandValidator :
    AbstractValidator<PostConfirmPasswordResetCommand>
{
    /// <inheritdoc />
    public PostConfirmPasswordResetCommandValidator()
    {
        RuleFor(command => command.Email)
            .NotEmpty().WithMessage(AuthErrorMessages.EmptyField("Почта"));

        RuleFor(command => command.Email)
            .EmailAddress().WithMessage(AuthErrorMessages.InvalidEmailFormat);

        RuleFor(command => command.NewPassword)
            .NotEmpty().WithMessage(AuthErrorMessages.EmptyField("Новый пароль"));

        RuleFor(command => command.VerificationCodeFromUser)
            .NotEmpty().WithMessage(AuthErrorMessages.EmptyField("Код верификации"));
    }
}