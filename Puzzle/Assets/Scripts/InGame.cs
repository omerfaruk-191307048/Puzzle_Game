using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class InGame : MonoBehaviour
{
    int Puan = 0;

    [HideInInspector] public GameObject Selected_Object;

    public List<string> Puzzle;

    public List<Vector2> Default_Positions;

    List<Vector2> freeSpawnPoints;

    UserData Data_a;

    private void Awake()
    {
        Puzzle.Add("1");
        Puzzle.Add("2");
        Puzzle.Add("3");
        Puzzle.Add("4");
        Puzzle.Add("5");
        Puzzle.Add("6");
        Puzzle.Add("7");
        Puzzle.Add("8");
        Puzzle.Add("9");
        Puzzle.Add("10");
        Puzzle.Add("11");
        Puzzle.Add("12");
        Puzzle.Add("13");
        Puzzle.Add("14");
        Puzzle.Add("15");
        Puzzle.Add("16");
    }

    private void Start()
    {
        for (int a = 0; a < Puzzle.Count; a++)
        {
            GameObject Instance = Instantiate(Resources.Load(Puzzle[a].ToString(), typeof(GameObject))) as GameObject;
            Instance.name = Puzzle[a].ToString();

            Default_Positions.Add(new Vector2(Instance.transform.position.x, Instance.transform.position.y));
        }

        freeSpawnPoints = new List<Vector2>(Default_Positions);

        Data_a = GameObject.FindWithTag("Data").GetComponent<UserData>();
    }

    private void OnGUI()
    {
        GUILayout.Label("Puan Durumu: " + Puan);

        if (GUILayout.Button("Karistir", GUILayout.Width(75), GUILayout.Height(50)))
        {
            GameObject[] Puzzle_Objects = GameObject.FindGameObjectsWithTag("Puzzle");
            for (int g = 0; g < Puzzle_Objects.Length; g++)
                Destroy(Puzzle_Objects[g]);

            freeSpawnPoints.Clear();
            freeSpawnPoints = new List<Vector2>(Default_Positions);

            for (int b = 0; b < Puzzle.Count; b++)
            {
                GameObject Instance = Resources.Load(Puzzle[b].ToString()) as GameObject;

                GameObject New_Instance = Instantiate(Instance, GetPos(), Quaternion.identity) as GameObject;
                New_Instance.name = Puzzle[b].ToString();
            }
        }

        if(GUILayout.Button("Skoru Kaydet"))
        {
            Data_a.SaveFile(Puan);
        }
    }

    public void Puan_Degistir(int Degisim)
    {
        Puan += Degisim;
    }

    Vector2 GetPos()
    {
        if (freeSpawnPoints.Count <= 0)
        {
            return new Vector2(0, 0); // Spawn Point Kalmadý (null)
        }

        int index = Random.Range(0, freeSpawnPoints.Count);

        Vector2 New_Position = freeSpawnPoints[index];

        freeSpawnPoints.RemoveAt(index); // Spawn Pointleri geçici listeden kaldýrýyoruz

        return New_Position;
    }
}
