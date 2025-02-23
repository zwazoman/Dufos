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

    // Stocker les valeurs de la classe à sauvegarder (ne pas oublier le seriazable).
    [Serializable]
    public class Data
    {
        public List<bool> PnjActive = new();
        public List<Vector3> PnjPos = new();
    }

    private void Awake()
    {
        obj = new();

        // Si la file n'existe pas, on l'a créer en lui assignant un chemin.
        if (!File.Exists(Application.dataPath + "PnjSaveFile.json"))
        {
            File.Create(Application.dataPath + "PnjSaveFile.json");
            return;
        }

        // S'il n'est pas vide, on assigne les variables contenues dans la file aux variables internes.
        _json = File.ReadAllText(Application.dataPath + "PnjSaveFile.json");

        obj = JsonUtility.FromJson<Data>(_json);

        for (int i = 0; i < _pnjs.Count; i++)
        {
            _pnjs[i].SetActive(obj.PnjActive[i]);
        }
    }

    // On réecrit les variables à chaque appel de la méthode, autant dans l'UI que dans le fichier de sauvegarde.
    public void ApplySave()
    {
        if (obj.PnjPos.Count != 0)
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

        File.WriteAllText(Application.dataPath + "PnjSaveFile.json", _json);
    }
}
