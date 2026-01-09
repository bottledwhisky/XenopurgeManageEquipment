using HarmonyLib;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Settings;

namespace XenopurgeManageEquipment
{
    public static class ModLocalization
    {
        private static string _currentLanguage = "en";

        private static readonly Dictionary<string, Dictionary<string, string>> _translations = I18nData._translations;

        public static void SetLanguage(string languageCode)
        {
            if (string.IsNullOrEmpty(languageCode))
            {
                languageCode = "en";
            }

            // Normalize language code
            string normalizedCode = languageCode.ToLower();

            if (_translations.ContainsKey(normalizedCode))
            {
                _currentLanguage = normalizedCode;
                MelonLogger.Msg($"Mod language set to: {normalizedCode}");
            }
            else
            {
                // Try without region code (e.g., "en-US" -> "en")
                string baseCode = normalizedCode.Split('-')[0];
                if (_translations.ContainsKey(baseCode))
                {
                    _currentLanguage = baseCode;
                    MelonLogger.Msg($"Mod language set to: {baseCode} (from {normalizedCode})");
                }
                else
                {
                    _currentLanguage = "en";
                    MelonLogger.Msg($"Language '{normalizedCode}' not supported, using English");
                }
            }
        }

        public static string Get(string key, params object[] args)
        {
            if (_translations.ContainsKey(_currentLanguage) &&
                _translations[_currentLanguage].ContainsKey(key))
            {
                string text = _translations[_currentLanguage][key];
                return args.Length > 0 ? string.Format(text, args) : text;
            }

            // Fallback to English
            if (_currentLanguage != "en" &&
                _translations.ContainsKey("en") &&
                _translations["en"].ContainsKey(key))
            {
                string text = _translations["en"][key];
                return args.Length > 0 ? string.Format(text, args) : text;
            }

            return key;
        }
    }

    // Patch SettingsManager to detect language changes
    [HarmonyPatch(typeof(SettingsManager))]
    public class SettingsManager_Patches
    {
        // Hook into LoadPlayerPrefs to get initial language
        [HarmonyPatch("LoadPlayerPrefs")]
        [HarmonyPostfix]
        public static void LoadPlayerPrefs_Postfix(SettingsManager __instance)
        {
            try
            {
                if (__instance.HasLoadedSettings)
                {
                    string currentLanguage = __instance.Language;
                    ModLocalization.SetLanguage(currentLanguage);
                    MelonLogger.Msg($"Detected initial game language: {currentLanguage}");
                }
            }
            catch (Exception ex)
            {
                MelonLogger.Error($"Error in LoadPlayerPrefs patch: {ex}");
            }
        }

        // Hook into SetLanguage to detect language changes
        [HarmonyPatch("SetLanguage")]
        [HarmonyPostfix]
        public static void SetLanguage_Postfix(SettingsManager __instance, string value)
        {
            try
            {
                ModLocalization.SetLanguage(value);
                MelonLogger.Msg($"Language changed to: {value}");
            }
            catch (Exception ex)
            {
                MelonLogger.Error($"Error in SetLanguage patch: {ex}");
            }
        }
    }

}
