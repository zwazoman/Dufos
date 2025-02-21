using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.Tilemaps;
using UnityEngine;

public class SavedDataCenter : MonoBehaviour
{
    #region Singleton
    private static SavedDataCenter instance;

    public static SavedDataCenter Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("Save Data Center");
                instance = go.AddComponent<SavedDataCenter>();
            }
            return instance;
        }
    }
    #endregion

    public SaveData Data = new SaveData();
       
    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            Data.CurrentMap = 6;
            Data.ClearedCampsCount = 8;

            Save();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            TryLoadSave();

            print(Data.CurrentMap);
            print(Data.ClearedCampsCount);
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            EraseSave();
        }
    }

    
    public void Save()
    {
        string json = JsonUtility.ToJson(Data);

        File.WriteAllText(Application.persistentDataPath + "/SavedData.json", json);
    }


    public SaveData TryLoadSave()
    {
        string saveFilePath = Application.persistentDataPath + "/SavedData.json";

        SaveData loadData = new SaveData();

        if (File.Exists(saveFilePath))
            loadData = JsonUtility.FromJson<SaveData>(File.ReadAllText(saveFilePath));
        
        Data = loadData;

        return Data;
    }

    public void EraseSave()
    {
        string saveFilePath = Application.persistentDataPath + "/SavedData.json";

        if (File.Exists(saveFilePath))
            File.Delete(saveFilePath);
    }

}

public struct SaveData
{
    public int CurrentMap;
    public int ClearedCampsCount;
}
