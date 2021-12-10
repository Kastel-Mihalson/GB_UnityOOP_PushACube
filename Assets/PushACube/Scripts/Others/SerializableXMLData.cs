using System.IO;
using System.Xml.Serialization;

public class SerializableXMLData<T> : IData<T>
{
    private static XmlSerializer _xmlSerializer;

    public SerializableXMLData()
    {
        _xmlSerializer = new XmlSerializer(typeof(T));
    }

    public T Load(string path = null)
    {
        T result;

        if (!File.Exists(path)) return default;
        using (var fs = new FileStream(path, FileMode.Open))
        {
            result = (T)_xmlSerializer.Deserialize(fs);
        }

        return result;
    }

    public void Save(T data, string path = null)
    {
        if (data == null && !string.IsNullOrEmpty(path)) return;
        using (var fs = new FileStream(path, FileMode.Create))
        {
            _xmlSerializer.Serialize(fs, data);
        }
    }
}
