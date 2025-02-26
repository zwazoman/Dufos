using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PnjSaveHandler : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _pnjs = new();

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
        if (!File.Exists(Application.dataPath + "/PnjSaveFile.json"))
        {
            File.Create(Application.dataPath + "/PnjSaveFile.json");
            return;
        }

    }

    private void Start()
    {
        if (_pnjs.Count > 0)
        {
            _json = File.ReadAllText(Application.dataPath + "/PnjSaveFile.json");

            if (_json == "")
            {
                for (int i = 0; i < _pnjs.Count; i++)
                {
                    obj.PnjActive.Add(true);
                }

                _json = JsonUtility.ToJson(obj);
                print("reset");

                File.WriteAllText(Application.dataPath + "/PnjSaveFile.json", _json);
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
        if(File.Exists(Application.dataPath + "/PnjSaveFile.json"))
        {
            File.Delete(Application.dataPath + "/PnjSaveFile.json");
            File.Delete(Application.dataPath + "/PnjSaveFile.json.meta");
        }
    }

    // On réecrit les variables à chaque appel de la méthode, autant dans l'UI que dans le fichier de sauvegarde.
    public void ApplySave()
    {
        print("save");
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
        File.WriteAllText(Application.dataPath + "/PnjSaveFile.json", _json);
    }
}
