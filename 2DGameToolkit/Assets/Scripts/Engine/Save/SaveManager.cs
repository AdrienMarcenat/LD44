
using UnityEngine;
using AnyObject = System.Object;

public class SaveManager : ISaveManager
{
    private const string m_SaveDirectory = "/Saves";

    public void SaveObject(AnyObject objectToSave, string filename)
    {
        string path = Application.streamingAssetsPath + m_SaveDirectory + filename;
        XMLSerializerHelper.Serialize(objectToSave, path);
    }

    public AnyObject GetSavedObject(System.Type type, string filename)
    {
        string path = Application.streamingAssetsPath + m_SaveDirectory + filename;
        return XMLSerializerHelper.Deserialize(path, type);
    }
}

