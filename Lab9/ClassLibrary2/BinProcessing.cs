using System.Text;
using System.Text.Encodings.Web;
using ProtoBuf;

public class BinProcessing<T>
{
    public BinProcessing() { }
    public static Stream Write(List<T> Ts)
    {
        MemoryStream stream = new MemoryStream();
        // Если не поставить true, StreamWriter не дописывает последние строчки файла.
        Serializer.Serialize(stream, Ts);
        //newStream.Position = 0;
        string str = Convert.ToBase64String(stream.GetBuffer(),
        0, (int)stream.Length);
        MemoryStream newStream = new();
        StreamWriter writer = new(newStream);
        writer.AutoFlush = true;
        writer.Write(str);
        newStream.Position = 0;
        return newStream;
    }
    public static List<T> Read(Stream stream)
    {
        try
        {
            StreamReader streamReader = new(stream);
            string str = streamReader.ReadToEnd();
            MemoryStream newStream = new(Convert.FromBase64String(str));
            return Serializer.Deserialize<List<T>>(newStream);
        }
        catch (Exception ex)
        {
            throw ex;
            //throw new ArgumentException("Unable to process Binary");
        }
    }
}
