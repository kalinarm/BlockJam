using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace Kimeria.Nyx.Tools.Localisation
{
    public class LocalizationManager : MonoBehaviour
    {
        [SerializeField] string currentLanguage = "fr";
        [SerializeField] bool logMissingKeys = true;

        private bool isLanguageLoaded = false;
        private Dictionary<string, string> missingStrings = new Dictionary<string, string>();

        LocalizationData locData = null;

        public Dictionary<string, string> LocalizedText { get => locData.localizedText; }

        #region singleton
        private static LocalizationManager instance;
        public static LocalizationManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = GameObject.FindObjectOfType<LocalizationManager>();
                }
                if (instance == null)
                {
                    instance = new GameObject("Localization").AddComponent<LocalizationManager>();
                }
                if (!instance.isLanguageLoaded)
                {
                    SwitchLanguage(instance.currentLanguage);
                    //instance.LoadLocalizedTextTXT("Lang/french.txt");
                    //instance.LoadLocalizedTextJSON("Lang/french.json");
                    //instance.exportStringAsTxt("Lang/french.txt");
                }
                return instance;
            }
        }
        #endregion

        #region unity
        void Awake()
        {
            //DontDestroyOnLoad(gameObject);
        }

        void OnDestroy()
        {
            if (logMissingKeys && missingStrings.Count > 0)
            {
                string filePath = Path.Combine(Application.streamingAssetsPath, currentLanguage + "_loc_missing.txt");
                string entries = "";
                foreach (var entry in missingStrings)
                {
                    entries += entry.Key + "\n";
                }
                File.WriteAllText(filePath, entries);
            }
        }
        #endregion

        #region public static
        public static void SwitchLanguage(string newLang)
        {
            instance.currentLanguage = newLang;
            instance.LoadLocalizedTextTXT();
        }

        public static string Localize(string str)
        {
            return Instance.GetLocalizedValue(str);
        }
        #endregion

        #region core
        public static string GetPath(string lang)
        {
            return Path.Combine(Application.streamingAssetsPath, "Lang", lang + ".txt");
        }

        public void LoadLocalizedTextTXT()
        {
            locData = new LocalizationData();
            if (locData.LoadTextFile(GetPath(currentLanguage)))
            {
                isLanguageLoaded = true;
            }
        }
        public void ExportStringAsTxt()
        {
            if (locData == null) return;
            locData.SaveToTextFile(GetPath(currentLanguage));
        }

        public string GetLocalizedValue(string key)
        {
            string result = key;
            if (LocalizedText.ContainsKey(key))
            {
                result = LocalizedText[key];
            }
            else
            {
                if (logMissingKeys && !missingStrings.ContainsKey(key))
                {
                    missingStrings.Add(key, key);
                }
                Debug.LogWarning("localization : key does not exist : " + key);
            }
            return result;
        }
        #endregion
    }
}