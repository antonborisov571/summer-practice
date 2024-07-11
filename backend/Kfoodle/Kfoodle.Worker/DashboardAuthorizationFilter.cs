using Hangfire.Dashboard;

namespace Kfoodle.Worker;

/// <inheritdoc />
public class DashboardAuthorizationFilter : IDashboardAuthorizationFilter
{
    /// <inheritdoc />
    public bool Authorize(DashboardContext context) => true;
}