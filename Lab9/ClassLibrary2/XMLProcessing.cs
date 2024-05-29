using System.Text;
using System.Text.Encodings.Web;
using System.Xml.Serialization;

public class XMLProcessing<T>
{
    public XMLProcessing() { }
    public static Stream Write(List<T> Ts)
    {
        MemoryStream newStream = new MemoryStream();
        StreamWriter writer = new StreamWriter(newStream);
        writer.AutoFlush = true;
        // Если не поставить true, StreamWriter не дописывает последние строчки файла.
        XmlSerializer serializer = new(typeof(List<T>));
        serializer.Serialize(writer, Ts);
        newStream.Position = 0;
        return newStream;
    }
    public static List<T> Read(Stream stream)
    {
        XmlSerializer serializer = new(typeof(List<T>));
        try
        {
            return (List<T>)serializer.Deserialize(stream);
        }
        catch (Exception ex)
        {
            throw new ArgumentException("Unable to process XML");
        }
    }
}
