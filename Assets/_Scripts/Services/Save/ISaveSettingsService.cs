public interface ISaveSettingsService
{
    public SettingsData LoadSettings();
    public void SaveSettings(SettingsData settings);
}
