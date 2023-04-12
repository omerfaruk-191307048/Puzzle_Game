using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //Sahneler arasi gecisi saglayan kutuphane
using Saklambac; //txt de veri yonetmeyi saglar
using System.Linq; //siralama saglar (highest score icin)

public class MainMenu : MonoBehaviour
{
    enum Windows { Login, MainMenuRect, HighestScoreRect };
    Windows Active_Window = Windows.Login;

    Vector2 Score_Scroll = Vector2.zero;

    UserData Data;

    string UserName = "";
    int Score = 0;

    private void Start() //sahne basladiginda
    {
        Data = GameObject.FindWithTag("Data").GetComponent<UserData>(); //UserData'yi data tagiyla sahnede bulur ve ceker
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
        //giris ekrani ayari
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
        //Somut olarak olusturdugumuz kisim
        GUILayout.BeginArea(new Rect(Screen.width / 2 - 165, Screen.height / 2 - 150, 325, 200), "ANA MENU", GUI.skin.GetStyle("Window"));

        if (GUILayout.Button("Oyuna Basla", GUILayout.Height(50)))
        {
            SceneManager.LoadScene("InGame"); //Oyun sahnemize gecer
        }
        if (GUILayout.Button("Skor Tablosu", GUILayout.Height(50))) 
        {
            Active_Window = Windows.HighestScoreRect; //Skor tablosuna gecis yapar
        }
        if (GUILayout.Button("Cikis", GUILayout.Height(50)))
        {
            Application.Quit();
        }

        GUILayout.EndArea();
    }

    void HighestScoreGUI()
    {
        GUILayout.BeginArea(new Rect(Screen.width / 2 - 165, Screen.height / 2 - 150, 325, 300), "ANA MENU", GUI.skin.GetStyle("Window"));

        GUILayout.Box("Username" + "-" + "Score");

        Score_Scroll = GUILayout.BeginScrollView(Score_Scroll, false, true); //skorlarin kaydirmasini yapabilmek icin

        List<User_Data> New_List = Data.Db.GetAll();

        var Sirali_Liste = New_List.OrderBy(b => b.Score).Reverse(); //en yuksekten en asagiya sirala

        foreach (var Kisi in Sirali_Liste)
        {
            GUILayout.Box("Isim: " + Kisi.UserName + " Score: " + Kisi.Score + " Hamle Sayisi: " + Kisi.Hamle);
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
