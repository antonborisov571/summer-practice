﻿using FluentValidation;

namespace Kfoodle.Core.Requests.Account.PatchUpdateUserInfo;

/// <summary>
/// Валидатор для <see cref="PatchUpdateUserInfoCommand"/>
/// </summary>
public class PatchUpdateUserInfoCommandValidator : AbstractValidator<PatchUpdateUserInfoCommand>
{
    /// <inheritdoc />
    public PatchUpdateUserInfoCommandValidator()
    {
    }
}