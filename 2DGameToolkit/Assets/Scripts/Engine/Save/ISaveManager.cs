
using AnyObject = System.Object;

public interface ISaveManager
{
    void SaveObject(AnyObject objectToSave, string filename);
    AnyObject GetSavedObject(System.Type type, string filename);
}

public class SaveManagerProxy : UniqueProxy<ISaveManager>
{ }
