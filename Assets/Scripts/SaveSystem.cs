using System;
using UnityEngine;
using System.IO;
using System.Numerics;
using System.Runtime.Serialization.Formatters.Binary;
using Game_Classes;


public static class SaveSystem 
{
    
    //Метод сейва
    public static void save(GameLogic gameLogic)
    {
        BinaryFormatter formatter = new BinaryFormatter();// то что переводит нашь сейв в бин файл и обратно
        string path = Application.persistentDataPath + "/Save.game"; // путь до сейва
        FileStream stream = new FileStream(path, FileMode.Create);// создаем файл для сейва
        // запись сейва в созданый файл
        formatter.Serialize(stream, new Save(gameLogic.Buildings, gameLogic.Money, DateTime.Now, 
            gameLogic.GetComponent<UpgradeHandClick>().HandClicker, gameLogic.TensPower));
        stream.Close(); // закрываем файл
    }
    // загрузка
    public static Save load()
    {
        string path = Application.persistentDataPath + "/Save.game"; // путь до сейва
        // Debug.Log(path);
        //если сейв есть то загружаем его если нет посылаем нулл
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();// то что переводит нашь сейв в бин файл и обратно
            FileStream stream = new FileStream(path, FileMode.Open);// открываем файл с сейвом
            Save data = formatter.Deserialize(stream) as Save;// переводим бин код в класс сейва
            stream.Close(); // закрываем файл
            return data; // возвращаем наш класс сейва
        }
        else
        {
            return null;
        }

    }
}