using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

public class ABIEditorWindow : EditorWindow
{
    string inputValue = "Enter ABI here";
    string outputValue = "";


    [MenuItem("Tools/Format ABI")]
    static void Init()
    {
        ABIEditorWindow window = (ABIEditorWindow)EditorWindow.GetWindow(typeof(ABIEditorWindow));
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.Label("Modify ABI String", EditorStyles.boldLabel);
        GUILayout.Space(30f);

        GUILayout.Label("Input ABI");
        EditorStyles.textField.wordWrap = true; // This sets the wordwrap value of the property
        inputValue = EditorGUILayout.TextArea(inputValue, GUILayout.Height(80));
        GUILayout.Space(15f);

        GUILayout.Label("Output ABI");
        EditorStyles.textField.wordWrap = true; // This sets the wordwrap value of the property
        outputValue = EditorGUILayout.TextArea(outputValue, GUILayout.Height(80));
        GUILayout.Space(15f);

        if (GUILayout.Button("Convert Data"))
        {
            ConvertData();
        }
    }

    private void ConvertData()
    {
        string noCommas = EscapeCommas(inputValue);
        string finalString = ReplaceWhitespace(noCommas);

        outputValue = finalString;
    }

    /// <summary>
    /// Remove white spaces and create a single line.
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    private string ReplaceWhitespace(string str)
    {
        string pattern = "\\s+";
        string replacement = " ";
        Regex rx = new Regex(pattern);
        return rx.Replace(str, replacement);
    }

    /// <summary>
    /// Escape commas from strings
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    private string EscapeCommas(string str)
    {
        string newReplacement = @"\" + '"';
        string oldString = '"'.ToString();

        string replace = str.Replace(oldString, newReplacement);
        string singleLine = replace.Replace("\r\n", " ");

        return singleLine;
    }
}