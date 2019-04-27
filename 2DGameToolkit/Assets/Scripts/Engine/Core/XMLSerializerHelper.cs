using System.IO;
using System.Xml.Serialization;

public class XMLSerializerHelper
{
    public static void Serialize(object item, string path)
    {
        XmlSerializer serializer = new XmlSerializer(item.GetType());
        StreamWriter writer = new StreamWriter(path);
        serializer.Serialize(writer.BaseStream, item);
        writer.Close();
    }

    public static T Deserialize<T>(string path)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(T));
        StreamReader reader = new StreamReader(path);
        T deserialized = (T)serializer.Deserialize(reader.BaseStream);
        reader.Close();
        return deserialized;
    }

    public static object Deserialize(string path, System.Type type)
    {
        XmlSerializer serializer = new XmlSerializer(type);
        StreamReader reader = new StreamReader(path);
        object deserialized = serializer.Deserialize(reader.BaseStream);
        reader.Close();
        return deserialized;
    }
}