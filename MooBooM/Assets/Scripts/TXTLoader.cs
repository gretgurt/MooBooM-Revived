using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;
using UnityEngine;

public class TXTLoader
{

    private TextAsset txtFile;
    private char lineSeparator = '\n';
    private string[] fieldSeparator = {"\",\""};
    private char surroundEntry = '"';
    
    public void LoadTXT()
    {
        txtFile = Resources.Load<TextAsset>("localisation");
    }

    public Dictionary<string, string> GetDictionaryValues(string attributeId)
    {
        Dictionary<string, string> dictionary = new Dictionary<string, string>();

        string[] lines = txtFile.text.Split(lineSeparator);

        int attributeIndex = -1;

        string[] headers = lines[0].Split(fieldSeparator, System.StringSplitOptions.None);
        
        for(int i = 0; i < headers.Length; i++)
        {
            if (headers[i].Contains(attributeId))
            {
                attributeIndex = i;
                break;
            }
        }
        Regex TXTParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");

        for(int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];

            string[] fields = TXTParser.Split(line);

            for (int y = 0; y < fields.Length; y++)
            {
                fields[y] = fields[y].TrimStart(' ', surroundEntry);
                fields[y] = fields[y].Replace("\"", ""); 
            }

            if(fields.Length > attributeIndex)
            {
                var key = fields[0];

                if (dictionary.ContainsKey(key))
                {
                    continue;
                }

                var value = fields[attributeIndex];

                dictionary.Add(key, value);
            }
        }
        return dictionary;
    }

}
