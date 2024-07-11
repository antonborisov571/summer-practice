namespace Kfoodle.Worker.Workers;

/// <summary>
/// Background-служба
/// </summary>
public interface IWorker
{
    /// <summary>
    /// Запустить службу
    /// </summary>
    /// <returns></returns>
    public Task RunAsync();
}