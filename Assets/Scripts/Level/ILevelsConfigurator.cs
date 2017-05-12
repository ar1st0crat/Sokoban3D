public interface ILevelsConfigurator
{
    /// <summary>
    /// Get level #idx
    /// </summary>
    Level Get(int idx);

    /// <summary>
    /// Get total number of available levels
    /// </summary>
    int Count { get; }
}
