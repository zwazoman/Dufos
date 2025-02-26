using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PnjSaveHandler : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _pnjs = new();
    [SerializeField]
    private string _saveName;

    private Data obj;
    private string _json = "";

    // Stocker les valeurs de la classe à sauvegarder (ne pas oublier le seriazable).
    [Serializable]
    public class Data
    {
        public List<bool> PnjActive = new();
    }

    private void Awake()
    {
        obj = new();

        // Si la file n'existe pas, on l'a créer en lui assignant un chemin.
        if (!File.Exists(Application.dataPath + "/" + _saveName + ".json"))
        {
            File.Create(Application.dataPath + "/" + _saveName + ".json");
            return;
        }

    }

    private void Start()
    {
        if (_pnjs.Count > 0)
        {
            _json = File.ReadAllText(Application.dataPath + "/" + _saveName + ".json");

            if (_json == "")
            {
                for (int i = 0; i < _pnjs.Count; i++)
                {
                    obj.PnjActive.Add(true);
                }

                _json = JsonUtility.ToJson(obj);

                File.WriteAllText(Application.dataPath + "/" + _saveName + ".json", _json);
            }

            else if (_json != "")
            {
                obj = JsonUtility.FromJson<Data>(_json);

                for(int i = 0; i < _pnjs.Count; i++)
                {
                    if (!obj.PnjActive[i])
                    {
                        _pnjs[i].SetActive(obj.PnjActive[i]);
                    }
                }
            }
        }
    }

    public void EraseSave()
    {
        if(File.Exists(Application.dataPath + "/" + "Zone1" + ".json"))
        {
            File.Delete(Application.dataPath + "/" + "Zone1" + ".json");
            File.Delete(Application.dataPath + "/" + "Zone1" + ".json.meta");
        }

        if (File.Exists(Application.dataPath + "/" + "Zone2" + ".json"))
        {
            File.Delete(Application.dataPath + "/" + "Zone2" + ".json");
            File.Delete(Application.dataPath + "/" + "Zone2" + ".json.meta");
        }

        if (File.Exists(Application.dataPath + "/" + "Zone3" + ".json"))
        {
            File.Delete(Application.dataPath + "/" + "Zone3" + ".json");
            File.Delete(Application.dataPath + "/" + "Zone3" + ".json.meta");
        }
    }

    // On réecrit les variables à chaque appel de la méthode, autant dans l'UI que dans le fichier de sauvegarde.
    public void ApplySave()
    {
        SavedDataCenter.Instance.Save();

        if(obj != null && obj.PnjActive != null)
        {
            obj.PnjActive.Clear();
        }

        for (int i = 0; i < _pnjs.Count; i++)
        {
            obj.PnjActive.Add(_pnjs[i].activeInHierarchy);
        }

        _json = JsonUtility.ToJson(obj);
        File.WriteAllText(Application.dataPath + "/" + _saveName + ".json", _json);
    }
}
