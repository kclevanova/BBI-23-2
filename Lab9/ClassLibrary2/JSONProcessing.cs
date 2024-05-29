using System.Text;
using System.Text.Encodings.Web;
using Newtonsoft.Json;
public class JSONProcessing<T>
{
    public JSONProcessing() { }
    public static Stream Write(List<T> Ts)
    {
        MemoryStream newStream = new MemoryStream();
        StreamWriter writer = new StreamWriter(newStream);
        writer.AutoFlush = true;
        // Если не поставить true, StreamWriter не дописывает последние строчки файла.
        JsonSerializer serializer = new();
        serializer.NullValueHandling = NullValueHandling.Ignore;
        serializer.TypeNameHandling = TypeNameHandling.Auto;
        serializer.Formatting = Formatting.Indented;
        serializer.Serialize(writer, Ts);
        newStream.Position = 0;
        return newStream;
    }
    public static List<T> Read(Stream stream)
    {

        JsonSerializerSettings settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto,
            NullValueHandling = NullValueHandling.Ignore,

        };
        StreamReader reader = new StreamReader(stream);
        string streamData = reader.ReadToEnd();
        try
        {
            return JsonConvert.DeserializeObject<List<T>>(streamData, settings);
        }
        catch (Exception ex)
        {
            throw new ArgumentException("Unable to process JSON");
        }
    }
}
