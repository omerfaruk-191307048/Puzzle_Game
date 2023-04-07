using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parca : MonoBehaviour
{
    Vector2 DefaultTransform;
    Vector3 Yerlesecegi_Yer;
    public GameObject Yerlesecegi_Obje;

    InGame InGameScript;

    private void Awake()
    {
        DefaultTransform = transform.position;

        InGameScript = GameObject.FindWithTag("MainCamera").GetComponent<InGame>();
    }

    private void Start()
    {
        foreach (GameObject New_Object in GameObject.FindGameObjectsWithTag("Box"))
        {
            if (gameObject.name == New_Object.name)
            {
                Yerlesecegi_Yer = New_Object.transform.position;
                Yerlesecegi_Obje = New_Object.gameObject;
            }
        }
    }

    private void Update()
    {
        if (transform.position != Yerlesecegi_Yer)
        {
            float Mesafe = Vector3.Distance(Yerlesecegi_Yer, transform.position);

            if (Input.GetMouseButtonUp(0)) 
            {
                if ((int)Mesafe < 1)
                {
                    Set_New_Position();
                }
                else
                {
                    Set_Default_Position();
                }
            }
        }
    }

    void OnMouseDrag()
    {
        if(transform.position != Yerlesecegi_Yer)
        {
            //kameramizdaki mouse'umuzun pozisyonunu gercek dunyaya uyarladik
            Vector3 Mouse_Position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Mouse_Position.z = 0;
            gameObject.transform.position = Mouse_Position;

            Collider2D targetObject = Physics2D.OverlapPoint(Mouse_Position);
            if (targetObject)
            {
                InGameScript.Selected_Object = targetObject.transform.gameObject;
            }
        }
    }

    void Set_New_Position()
    {
        transform.position = Yerlesecegi_Yer;
        Destroy(Yerlesecegi_Obje);

        if (InGameScript.Selected_Object == gameObject)
        {
            InGameScript.Selected_Object = null;

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

        transform.position = DefaultTransform;
    }

}
