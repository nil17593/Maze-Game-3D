using System.IO;
using UnityEngine;

public class JsonManager : Singleton<JsonManager>
{
    [SerializeField] private string savePath = "checkpointData.json"; // File path resides here

    public void SaveData<T>(T data)
    {
        string jsonData = JsonUtility.ToJson(data);
        File.WriteAllText(savePath, jsonData);
    }

    public T LoadData<T>()
    {
        if (File.Exists(savePath))
        {
            string jsonData = File.ReadAllText(savePath);
            return JsonUtility.FromJson<T>(jsonData);
        }
        else
        {
            Debug.LogError("No saved data found.");
            return default;
        }
    }

    public void DeleteData()
    {
        if (File.Exists(savePath))
        {
            File.Delete(savePath);
        }
        else
        {
            Debug.Log("No data to delete.");
        }
    }
}
