// Helpers/Settings.cs
using Refractored.Xam.Settings;
using Refractored.Xam.Settings.Abstractions;

namespace FstyleLotteryXamarin.Helpers
{
  /// <summary>
  /// This is the Settings static class that can be used in your Core solution or in any
  /// of your client applications. All settings are laid out the same exact way with getters
  /// and setters. 
  /// </summary>
  public static class Settings
  {
    private static ISettings AppSettings
    {
      get
      {
        return CrossSettings.Current;
      }
    }

    #region Setting Constants

    private const string SettingsKey = "settings_key";
    private static readonly string SettingsDefault = string.Empty;

    #endregion


    public static string TextDataSettings
    {
        get
        {
            return AppSettings.GetValueOrDefault(SettingsKey, SettingsDefault);
        }
        set
        {
            AppSettings.AddOrUpdateValue(SettingsKey, value);
        }
    }

    public static T GetValue<T>(T defaultValue, [System.Runtime.CompilerServices.CallerMemberName] string key = "")
    {
        return AppSettings.GetValueOrDefault(key, defaultValue);
    }

    public static void SetValue<T>(T newValue, [System.Runtime.CompilerServices.CallerMemberName] string key = "")
    {
        AppSettings.AddOrUpdateValue(key, newValue);
    }

  }
}