
using System.Text.Json;
using System.Xml.Serialization;
using ProtoBuf;
using System.IO;



namespace Serializers
{
    public abstract class MySerializer
    {
        public abstract void Write<GameCatalog>(GameCatalog obj, string filePath);
        public abstract GameCatalog Read<GameCatalog>(string filePath);
    }
    class MyJsonSerializer : MySerializer
    {
        public override void Write<GameCatalog>(GameCatalog obj, string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                JsonSerializer.Serialize(fs, obj);
            }
        }
        public override GameCatalog Read<GameCatalog>(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                return JsonSerializer.Deserialize<GameCatalog>(fs);
            }
        }
    }
    class MyXmlSerializer : MySerializer
    {
        public override void Write<GameCatalog>(GameCatalog obj, string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                new XmlSerializer(typeof(GameCatalog)).Serialize(fs, obj);
            }
        }
        public override GameCatalog Read<GameCatalog>(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                return (GameCatalog)new XmlSerializer(typeof(GameCatalog)).Deserialize(fs);
            }
        }
    }
    class MyBinSerializer : MySerializer
    {
        public override void Write<GameCatalog>(GameCatalog obj, string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                Serializer.Serialize(fs, obj);
            }
        }
        public override GameCatalog Read<GameCatalog>(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                return Serializer.Deserialize<GameCatalog>(fs);
            }
        }

    }
}

