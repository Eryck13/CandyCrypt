using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;

public static class Save
{
    public static void SavePlayer (Player player)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/player.bomber";        //Application.persistent save for a standard file location on different OS'es
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    // public static Player LoadPlayer()  --> THIS DOESN"T WORK THE VOID HAVEN'T BEEN TESTED YET
    // LOAD PLAYER ERROR NEED TO BE FIXED OTHER THAN THAT WE HAVE A SERIALIZATION SYSTEM READY

    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.bomber";        //Application.persistent save for a standard file location on different OS'es
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save File not found in " + path);
            return null;
        }
    }
}