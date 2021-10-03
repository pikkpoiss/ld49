using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TextFileReader
{
    public static string ReadFileAsString(string filename) {
        string path = string.Format("Assets/Resources/{0}", filename);
        StreamReader reader = new StreamReader(path);
        string output = reader.ReadToEnd();
        reader.Close();
        return output;
    }
}
