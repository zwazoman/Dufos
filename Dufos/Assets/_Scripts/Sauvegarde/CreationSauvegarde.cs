using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class CreationSauvegarde : MonoBehaviour
{
    static string[] _saveFileName = { "File1" };
    static int _currentSaveFile;
    private bool _didEnterZone = false;

    public void OnEnterZone()
    {
        if (_didEnterZone == false)
        {
            XmlWriterSettings settings = new XmlWriterSettings()
            {
                NewLineOnAttributes = true,
                Indent = true,
            };

            XmlWriter writer = XmlWriter.Create(Application.dataPath.Replace("\\", "/") + "/_SaveFiles/" + _saveFileName[_currentSaveFile], settings);
            writer.WriteStartDocument();
            writer.WriteStartElement("Data");
            Debug.Log(writer.ToString());
            Debug.Log(Application.persistentDataPath);
        }
        else
        {
            Debug.Log("Already Saved");
        }
        
    }
}
