using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class PnjSaveHandler : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _pnjs = new();

    private Data obj;
    private string _json;
    private int _index = -1;

    // Stocker les valeurs de la classe à sauvegarder (ne pas oublier le seriazable).
    [Serializable]
    public class Data
    {
        public List<bool> PnjActive = new();
        public List<Vector3> PnjPos = new();
    }

    private void Awake()
    {
        if (_pnjs != null && _pnjs.Count != 0)
        {
            obj = new();

            // Si la file n'existe pas, on l'a créer en lui assignant un chemin.
            if (!File.Exists(Application.dataPath + "/PnjSaveFile.json"))
            {
                File.Create(Application.dataPath + "/PnjSaveFile.json");
                return;
            }

            LoadSave();
        }

    }

    public void LoadSave()
    {
        // S'il n'est pas vide, on assigne les variables contenues dans la file aux variables internes.
        if (File.Exists(Application.dataPath + "/PnjSaveFile.json"))
        {
            ApplySave();
            _json = File.ReadAllText(Application.dataPath + "/PnjSaveFile.json");
            obj = JsonUtility.FromJson<Data>(_json);

            if (obj != null)
            {
                for (int i = 0; i < _pnjs.Count; i++)
                {
                    if (_pnjs[i] != null && _pnjs[i].activeInHierarchy)
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
        }
    }

    // On réecrit les variables à chaque appel de la méthode, autant dans l'UI que dans le fichier de sauvegarde.
    public void ApplySave()
    {
        if (obj != null && obj.PnjPos.Count != 0)
        {
            obj.PnjPos.Clear();
            obj.PnjActive.Clear();
        }

        for (int i = 0; i < _pnjs.Count; i++)
        {
            obj.PnjPos.Add(_pnjs[i].transform.position);
            obj.PnjActive.Add(_pnjs[i].activeInHierarchy);
        }

        _json = JsonUtility.ToJson(obj);

        File.WriteAllText(Application.dataPath + "/PnjSaveFile.json", _json);
    }
}
