namespace TypeLibrary.Data.Contracts;

public interface IApplicationSettingsRepository
{
    string ApplicationSemanticUrl { get; }
    string ApplicationUrl { get; }
}