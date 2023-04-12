using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using CodeX.LinkedList;

public class InGame : MonoBehaviour
{
    LinkedList Parcalar = new LinkedList();

    int Puan = 0;
    int HamleSayisi = 0;

    [HideInInspector] public GameObject Selected_Object; //Secili obje

    [HideInInspector] public List<string> Puzzle; //Yerlestirilecek parcalar

    [HideInInspector] public List<Vector2> Default_Positions; //Yerlestirilecek parcalarin pozisyonlari

    //Default_position'dan objenin pozisyonunu cekince freeSpawnPoints'e atiyoruz
    List<Vector2> freeSpawnPoints; //Karistirma islemi icin gerekli

    UserData Data_a; //Objeyi bulmasi icin

    bool OyunBasladi = false; //Oyunun baslayip baslamadigini tutan bool

    private void Awake() //Oyun baslamadan hemen once calisir
    {
        //Puzzle parcalarinin isimlerini listeye ekleriz
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
            //Instantiate: Objeyi sahneye spawn eder/gonderir.
            GameObject Instance = Instantiate(Resources.Load(Puzzle[a].ToString(), typeof(GameObject))) as GameObject;
            //Eger bunu yapmazsak obje sahneye gonderildiginde isminin yanina (clone) tagi ekler 
            Instance.name = Puzzle[a].ToString();

            //GetInstanceID(): Objenin unity'nin icerisindeki id'sini dondurur 
            Parcalar.InsertFirst(Instance.GetInstanceID()); 
            Parca_Data deger = Parcalar.GetElement(Instance.GetInstanceID());

            
            Instance.gameObject.GetComponent<Parca>().SetData(deger.Data, deger.IsHead);

            foreach (GameObject New_Object in GameObject.FindGameObjectsWithTag("Box")) //Box tagindaki tum objeleri bul
            {
                if (Instance.name == New_Object.name) //Parcanin adi Box tagindaki herhangi bir parcanin adi ile ayni ise
                {
                    New_Object.GetComponent<BoxScript>().SetData(Instance.GetComponent<Parca>().Linked_List_Data);
                }
            }


            //Sahneye gonderilen objelerin default pozisyonlari alinip listeye eklenir
            //Karistirma icin gerekli-
            Default_Positions.Add(new Vector2(Instance.transform.position.x, Instance.transform.position.y));
        }
        //Tum islemler bittiginde pozisyonlari karistirma islemi icin ayri bir datada tutuyoruz 
        freeSpawnPoints = new List<Vector2>(Default_Positions);
        //Datayi sahnede bulur
        Data_a = GameObject.FindWithTag("Data").GetComponent<UserData>(); 
    }

    private void Update()
    {
        if(Selected_Object != null) //Objeyi sectiginde oyunu baslatir. Tutmamizin sebebi oyun basladiginda karistirma islemi olmaz
        {
            OyunBasladi = true;
        }
    }

    private void OnGUI()
    {
        GUILayout.Label("Puan Durumu: " + Puan);
        GUILayout.Label("Hamle Sayisi: " + HamleSayisi);

        if (!OyunBasladi)
        {
            if (GUILayout.Button("Karistir", GUILayout.Width(75), GUILayout.Height(50)))
            {
                GameObject[] Puzzle_Objects = GameObject.FindGameObjectsWithTag("Puzzle");

                for (int g = 0; g < Puzzle_Objects.Length; g++)
                {
                    Destroy(Puzzle_Objects[g]); //Sahnedeki puzzle parcalarini sildik
                }

                freeSpawnPoints.Clear(); //Yedek olarak tuttugumuz listenin icini temizledik
                freeSpawnPoints = new List<Vector2>(Default_Positions); //Default pozisyonlarimizi tekrar listeye ekliyoruz

                for (int b = 0; b < Puzzle.Count; b++)
                {
                    GameObject Instance = Resources.Load(Puzzle[b].ToString()) as GameObject;

                    GameObject New_Instance = Instantiate(Instance, GetPos(), Quaternion.identity) as GameObject;
                    New_Instance.name = Puzzle[b].ToString();
                }
            }
        }

        if(GUILayout.Button("Skoru Kaydet"))
        {
            Data_a.SaveFile(Puan, HamleSayisi);
        }
    }

    public void Puan_Degistir(int Degisim)
    {
        Puan += Degisim;
        HamleSayisi++;
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
