using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parca : MonoBehaviour
{
    Vector2 DefaultTransform; //Baslangic pozisyonumuz
    Vector3 Yerlesecegi_Yer; //Box tag'iyle olan yerlerin pozisyonu
    public GameObject Yerlesecegi_Obje; //Parcanin yerlesecegi obje

    InGame InGameScript;

    public int Linked_List_Data = -1, Previos = -1, Next = -1;

    public bool IsHead = false;

    private void Awake()
    {
        DefaultTransform = transform.position; //Baslangic pozisyonumuzu tuttugumuz veri

        InGameScript = GameObject.FindWithTag("MainCamera").GetComponent<InGame>();
    }

    private void Start()
    {
        foreach (GameObject New_Object in GameObject.FindGameObjectsWithTag("Box")) //Box tagindaki tum objeleri bul
        {
            if (Linked_List_Data == New_Object.GetComponent<BoxScript>().data) //LinkedList datasi yerlesecegimiz yerin datasina esitse
            {
                Yerlesecegi_Yer = New_Object.transform.position; //Yerlesecegi pozisyon o parcanin pozisyonudur
                Yerlesecegi_Obje = New_Object.gameObject; //Yerlesecegi yer o parcanin kendisidir
            }
        }
    }

    private void Update()
    {
        //Eger parcayi yerlestirmediysek/parca dogru yerde degilse
        if (transform.position != Yerlesecegi_Yer) 
        {
            //Yerlesecegi yer ile parcanin yerinin arasindaki mesafeyi olcer
            float Mesafe = Vector3.Distance(Yerlesecegi_Yer, transform.position); //Distance: Iki parca arasi mesafeyi olcer

            if (Input.GetMouseButtonUp(0)) //GetMouseButtonUp : Tusa basildiginda. 0 olmasi sol click'i temsil eder
            {
                if ((int)Mesafe < 1) //Mesafe 1'den kucukse direkt pozisyona oturtur
                {
                    Set_New_Position();
                }
                else
                {
                    Set_Default_Position(); //Degilse mouse'u biraktigimizda parca eski pozisyonuna geri gelir
                }
            }
        }
    }

    void OnMouseDrag()
    {
        if(transform.position != Yerlesecegi_Yer) //Eger parca yerlesecegi yerde degilse parcami oynatabileyim
        {
            //kameramizdaki mouse'umuzun pozisyonunu gercek dunyaya uyarladik
            Vector3 Mouse_Position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Mouse_Position.z = 0; //Oyunumuz 2 boyutlu oldugu icin 
            gameObject.transform.position = Mouse_Position;

            Collider2D targetObject = Physics2D.OverlapPoint(Mouse_Position);
            if (targetObject) //Eger mouse'umun ucunda bir obje var ise
            {
                InGameScript.Selected_Object = targetObject.transform.gameObject; //Bu obje aslinda secilen (selected) objedir
            }
        }
    }

    void Set_New_Position()
    {
        transform.position = Yerlesecegi_Yer;
        Destroy(Yerlesecegi_Obje); //Yerlestigi zaman yerlesecegi yeri yok ederiz

        //Secilen obje eger bu obje ise
        if (InGameScript.Selected_Object == gameObject) 
        {
            InGameScript.Selected_Object = null; //Secilen objemizi bosa cikar

            InGameScript.Puan_Degistir(5);
        }
    }

    void Set_Default_Position()
    {
        if(InGameScript.Selected_Object == gameObject)
        {
            InGameScript.Selected_Object = null;

            InGameScript.Puan_Degistir(-10);
        }

        transform.position = DefaultTransform; //Eski yerine gore doner
    }

    public void SetData(int newValue, bool isHead)
    {
        Linked_List_Data = newValue;
        IsHead = isHead;

        if(!IsHead)
        {
            Previos = Linked_List_Data + 10;
            Next = Linked_List_Data - 10;
        }
        else
        {
            Previos = 0;
            Next = Linked_List_Data - 10;
        }
    }

}
