using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

using Saklambac;

using System.Linq;

public class MainMenu : MonoBehaviour
{
    enum Windows { Login, MainMenuRect, HighestScoreRect };
    Windows Active_Window = Windows.Login;

    Vector2 Score_Scroll = Vector2.zero;

    UserData Data;

    string UserName = "";
    int Score = 0;

    private void Start()
    {
        Data = GameObject.FindWithTag("Data").GetComponent<UserData>();
    }

    private void OnGUI()
    {
        if(Active_Window == Windows.Login)
        {
            LoginGUI();
        }
        else if (Active_Window == Windows.MainMenuRect)
        {
            MainMenuGUI();
        }
        else
        {
            HighestScoreGUI();
        }
    }

    void LoginGUI()
    {
        GUILayout.BeginArea(new Rect(Screen.width / 2 - 165, Screen.height / 2 - 150, 325, 150), "Giris", GUI.skin.GetStyle("Window"));

        GUILayout.Box("Isminiz: ", GUILayout.Height(20));
        UserName = GUILayout.TextField(UserName, GUILayout.Height(30));

        if (GUILayout.Button("Giris Yap", GUILayout.Height(50)))
        {
            Active_Window = Windows.MainMenuRect;
            Data.Set_Username(UserName);
        }
        GUILayout.EndArea();
    }

    void MainMenuGUI()
    {
        GUILayout.BeginArea(new Rect(Screen.width / 2 - 165, Screen.height / 2 - 150, 325, 200), "ANA MENU", GUI.skin.GetStyle("Window"));

        if (GUILayout.Button("Oyuna Basla", GUILayout.Height(50)))
        {
            SceneManager.LoadScene("InGame");
        }
        if (GUILayout.Button("Skor Tablosu", GUILayout.Height(50)))
        {
            Active_Window = Windows.HighestScoreRect;
        }
        if (GUILayout.Button("Cikis", GUILayout.Height(50)))
        {

        }

        GUILayout.EndArea();
    }

    void HighestScoreGUI()
    {
        GUILayout.BeginArea(new Rect(Screen.width / 2 - 165, Screen.height / 2 - 150, 325, 300), "ANA MENU", GUI.skin.GetStyle("Window"));

        GUILayout.Box("Username" + "-" + "Score");

        Score_Scroll = GUILayout.BeginScrollView(Score_Scroll, false, true);

        List<User_Data> New_List = Data.Db.GetAll();

        var Sirali_Liste = New_List.OrderBy(b => b.Score).Reverse();

        foreach (var Kisi in Sirali_Liste)
        {
            GUILayout.Box("Isim: " + Kisi.UserName + " Score: " + Kisi.Score);
        }

        /*
        for (int a = 0; a < New_List.Count; a++)
        {
            GUILayout.Box("Isim: " + New_List[a].UserName + " Score: " + New_List[a].Score);
        }
        */

        GUILayout.EndScrollView();

        if (GUILayout.Button("Geri", GUILayout.Height(50)))
        {
            Active_Window = Windows.MainMenuRect;
        }
        GUILayout.EndArea();
    }


    

}
