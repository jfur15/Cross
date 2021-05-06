using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using UnityEngine;

public static class ScoreboardControllerScript
{
    //Show up to 8 entries
    public static int entryCount = 8;

    static List<Tuple<DateTime, int>> scores;
    static StorageHandler sh = new StorageHandler();
    static string filename = "scores";
    // Start is called before the first frame update
    static ScoreboardControllerScript()
    {
        //if loaddata = null, do savedata on empty
        //do loaddata, then savedata
    }
    public static void ClearScores()
    {
        sh.SaveData(new List<Tuple<DateTime, int>>(), filename);
    }


    public static void NewScore(int newScore)
    {

        object varr = sh.LoadData(filename);

        if (varr == null)
        {
            sh.SaveData(new List<Tuple<DateTime, int>>(), filename);
        }

        scores = (List<Tuple<DateTime, int>>)sh.LoadData(filename);
        scores.Add(new Tuple<DateTime, int>(DateTime.Now, newScore));
        sh.SaveData(scores, filename);

    }
    public static List<Tuple<DateTime, int>> GetScores()
    {

        object varr = sh.LoadData(filename);

        if (varr == null)
        {
            sh.SaveData(new List<Tuple<DateTime, int>>(), filename);
        }

        scores = (List<Tuple<DateTime, int>>)varr;
        return scores.OrderByDescending(o => o.Item2).Take(entryCount).ToList();

    }
}
public class StorageHandler
{
    /// <summary>
    /// Serialize an object to the devices File System.
    /// </summary>
    /// <param name="objectToSave">The Object that will be Serialized.</param>
    /// <param name="fileName">Name of the file to be Serialized.</param>
    public void SaveData(object objectToSave, string fileName)
    {
        // Add the File Path together with the files name and extension.
        // We will use .bin to represent that this is a Binary file.
        string FullFilePath = Application.persistentDataPath + "/" + fileName + ".bin";
        // We must create a new Formattwr to Serialize with.
        BinaryFormatter Formatter = new BinaryFormatter();
        // Create a streaming path to our new file location.
        FileStream fileStream = new FileStream(FullFilePath, FileMode.Create);
        // Serialize the objedt to the File Stream
        Formatter.Serialize(fileStream, objectToSave);
        // FInally Close the FileStream and let the rest wrap itself up.
        fileStream.Close();
    }
    /// <summary>
    /// Deserialize an object from the FileSystem.
    /// </summary>
    /// <param name="fileName">Name of the file to deserialize.</param>
    /// <returns>Deserialized Object</returns>
    public object LoadData(string fileName)
    {
        string FullFilePath = Application.persistentDataPath + "/" + fileName + ".bin";
        // Check if our file exists, if it does not, just return a null object.
        if (File.Exists(FullFilePath))
        {
            BinaryFormatter Formatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(FullFilePath, FileMode.Open);
            object obj = Formatter.Deserialize(fileStream);
            fileStream.Close();
            // Return the uncast untyped object.
            return obj;
        }
        else
        {
            return null;
        }
    }
}
