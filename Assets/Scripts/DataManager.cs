using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    // переделать в приватные поля
    public static string Name = "";
    public static int Level;
    public float DisappearingTime;

    private string DataPath;

    public void DataSave()
    {
        Data data = new Data(Name, Level, DisappearingTime);
        File.WriteAllText(DataPath, JsonUtility.ToJson(data));
    }

    public void DataLoad()
    {
        Data data = JsonUtility.FromJson<Data>(File.ReadAllText(DataPath));
        Name = data.name;
        Level = data.level;
        DisappearingTime = data.disappearingTime;
    }
    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
            DataSave();
    }

    private void OnApplicationQuit()
    {
        DataSave();
    }

    private void Awake()
    {
        Instance = this;
        DataPath = Path.Combine(Application.persistentDataPath + "/Data.json");
        if (!File.Exists(DataPath))
        {
            DataSave();
            return;
        }
        DataLoad();
    }
}
