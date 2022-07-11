using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Kimeria.Nyx.Tools.Localisation
{
    [System.Serializable]
    public class LocalizationItem
    {
        public string key;
        public string value;
    }

    [System.Serializable]
    public class LocalizationData
    {
        public string lang = "fr";
        public LocalizationItem[] items;
        public Dictionary<string, string> localizedText = new Dictionary<string, string>();

        void AddFromText(string txt, char lineSplitChar ='\n', char keySplitChar = '=')
        {
            string[] entries = txt.Split(lineSplitChar);
            for (int i = 0; i < entries.Length; i++)
            {
                if (entries[i].StartsWith("//")) continue;
                string[] entry = entries[i].Split(new char[] { keySplitChar }, 2);
                if (entry.Length == 2)
                {
                    localizedText.Add(entry[0], entry[1]);
                }
            }
        }

        public bool LoadTextFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                string content = File.ReadAllText(filePath);
                localizedText = new Dictionary<string, string>();
                AddFromText(content);
                Debug.Log($"Data loaded, dictionary contains: {localizedText.Count} entries");
                return true;
            }
            else
            {
                Debug.LogError($"Cannot find localization file at {filePath}");
                return false;
            }
        }

        public void SaveToTextFile(string fileName)
        {
            string filePath = Path.Combine(Application.streamingAssetsPath, fileName);
            string entries = "";
            foreach (var entry in localizedText)
            {
                entries += entry.Key + "=" + entry.Value + "\n";
            }
            File.WriteAllText(filePath, entries);
        }
    }
}
