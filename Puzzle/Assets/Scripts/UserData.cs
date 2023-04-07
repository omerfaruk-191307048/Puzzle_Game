using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using Saklambac.NetFramework.Database;

[System.Serializable]
public class User_Data
{
    public string ID { get; set; } // Id alani mutlaka olmalidir;
    public string UserName { get; set; }
    public int Score { get; set; }
}

public class UserData : MonoBehaviour
{
    public SaklambacDb<User_Data> Db = new SaklambacDb<User_Data>();

    public string Username;

    void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void SaveFile(int Score)
    {
        User_Data New_Data = new User_Data();

        New_Data.UserName = Username;
        New_Data.Score = Score;

        Db.Add(New_Data);

    }

    public void Set_Username(string new_Name)
    {
        Username = new_Name;
    }


}
