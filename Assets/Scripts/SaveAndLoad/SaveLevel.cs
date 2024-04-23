using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveLevel
{
    private static string SavePath => $"{Application.persistentDataPath}/level.txt";

    public static void Save(string level)
    {
        using (var fs = File.Open(SavePath, FileMode.Create))
        {
            var formatter = new BinaryFormatter();
            formatter.Serialize(fs, level);
        }
    }

    public static string Load()
    {
        if (!File.Exists(SavePath)) {
            return null;
        }

        using (FileStream stream = File.Open(SavePath, FileMode.Open))
        {
            var formatter = new BinaryFormatter();
            return (string)formatter.Deserialize(stream);
        }
    }
}
