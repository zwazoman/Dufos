using UnityEngine;
using System.Xml;
using TMPro;

public class CreationSauvegarde : MonoBehaviour
{
    static string[] _saveFileName = { "File1" };
    static int _currentSaveFile;
    static int _numberSave = 0;
    [SerializeField] private TMP_Text _uISaves;

    private void Awake()
    {
        /*
        LoadGame();
        */
        //_uISaves.text = $"Save : {_numberSave}";
    }

    public void Saving()
    {
        _numberSave += 1;
        XmlWriterSettings settings = new XmlWriterSettings
        {
            NewLineOnAttributes = true,
            Indent = true,
        };

        XmlWriter writer = XmlWriter.Create(Application.persistentDataPath + _saveFileName[_currentSaveFile] + ".xml", settings);
        writer.WriteStartDocument();
        writer.WriteStartElement("Data");

        /*
        Debug.Log(writer.ToString());
        Debug.Log(_saveFileName[_currentSaveFile]);
        Debug.Log(Application.persistentDataPath);
        */

        WriteXmlString(writer, "ZoneCurrentlyOn", SaveManager.instance.ZoneName);
        //WriteXmlFloat(writer, "Saves", _numberSave);
        WriteXmlFloat(writer, "FightsWon", SaveManager.instance.FightsWon);
        WriteXmlVector3D(writer, "PlayerPosition", SaveManager.instance.PlayerPosition);

        //_uISaves.text = $"Save : {_numberSave}";

        writer.WriteEndElement();
        writer.WriteEndDocument();
        writer.Close();
    }

    static void WriteXmlString(XmlWriter _writer, string _key, string _value)
    {
        _writer.WriteStartElement(_key);
        _writer.WriteString(_value);
        _writer.WriteEndElement();
    }

    static void WriteXmlFloat(XmlWriter _writer, string _key, float _value)
    {
        _writer.WriteStartElement(_key);
        _writer.WriteValue(_value);
        _writer.WriteEndElement();
    }

    static void WriteXmlVector3D(XmlWriter _writer, string _key, Vector3 _value)
    {
        _writer.WriteStartElement(_key);
        _writer.WriteValue(_value);
        _writer.WriteEndElement();
    }

    public static void LoadGame()
    {
        XmlDocument saveFile = new XmlDocument();
        if (!System.IO.File.Exists(Application.persistentDataPath + _saveFileName[_currentSaveFile] + ".xml"))
        {
            //NoSaveFound();
            return;
        }
        saveFile.LoadXml(System.IO.File.ReadAllText(Application.persistentDataPath + _saveFileName[_currentSaveFile] + ".xml"));

        string key;
        string value;
        foreach (XmlNode node in saveFile.ChildNodes[1])
        {
            key = node.Name;
            value = node.InnerText;

            switch (key)
            {
                case "Test":

                    break;
                case "Saves":
                    _numberSave = int.Parse(value);
                    break;
                case "FightsWon":

                    break;
            }
        }
        /*
        switch (key)
        {

        Pour les string :
        case "Name":
            _name = value;
            break;

        Pour les int :
        case "Pressed":
            _numberSavePressed = int.Parse(value);
            break;

        Pour les float :
        case "Time":
            _timePlayed.TimePlaying = float.Parse(value, CultureInfo.InvariantCulture);
            break;
        }
        */
    }
}
