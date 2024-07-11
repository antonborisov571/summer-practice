﻿using Kfoodle.Core.Abstractions.Services;
using Kfoodle.Core.Entities;

namespace Kfoodle.Core.Services;

/// <inheritdoc />
public class AvatarService(ISftpService sftpService) : IAvatarService
{
    /// <inheritdoc />
    public async Task<string?> GetAvatar(User user, CancellationToken cancellationToken = default)
    {
        if (user.Avatar != null)
        {
            using var memoryStream = new MemoryStream();
            var avatar = await sftpService.DownloadFileAsync(user.Avatar, cancellationToken);

            if (avatar != null)
                await avatar.Content.CopyToAsync(memoryStream, cancellationToken);

            return Convert.ToBase64String(memoryStream.ToArray());
        }

        return null;
    }
}