using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Saklambac.NetFramework.Database;

[System.Serializable]
public class User_Data //datanin kendisi
{
    public string ID { get; set; } // Id alani mutlaka olmalidir;
    public string UserName { get; set; }
    public int Score { get; set; }
    public int Hamle { get; set; }
}

public class UserData : MonoBehaviour
{
    public SaklambacDb<User_Data> Db = new SaklambacDb<User_Data>(); //saklambacin kullanimi bu sekilde

    public string Username;

    void Start()
    {
        DontDestroyOnLoad(this); //sahneler arasi geciste objeyi sahnede tutar, objeyi yok etmez
    }

    public void SaveFile(int Score, int Hamle_Say)
    {
        User_Data New_Data = new User_Data(); //verilerin, kayitlarin tutuldugu yeni data

        New_Data.UserName = Username;
        New_Data.Score = Score;
        New_Data.Hamle = Hamle_Say;

        Db.Add(New_Data); //txt'ye burada ekler

    }

    public void Set_Username(string new_Name)
    {
        Username = new_Name;
    }
}
