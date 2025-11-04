using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


public class SurrController : MonoBehaviour
{
    public MapKeeper mk = new MapKeeper();
    public string[,] mKC;
    //Collider2D[] hit0 = new Collider2D[10];
    private string[] _incInterractType = new string[18];
    public string createdName;
    public bool localSwipedOrHadTo;
    public Vector2[] dummyPos = new Vector2[5];
    
    private DictCreator dictCreator;
    public Collider2D[] hit0 = new Collider2D[5];
    
    //Dictionary<float, string> sub = new Dictionary<float, string>();

    [Obsolete]
    public void isSurExp(int incInteractTypeCLicked, int incInteractTypeOther, Vector2 incPosKeepClicked, Vector2 incPosKeepShifted)
    {
        dictCreator = FindObjectOfType<DictCreator>();
        /*Burada sadece objenin çevresinde bulunan diğer elemanlar patlamaya uygun mu onu kontrol edecez onun için bir yerde mapKeeperı lokal bir yere kopyalamalıyız.
        daha sonra kopyalanan bu mapKeeperZart'ı burada çağırarak kenar köşede bulunan elemanlar patlamaya uygun mu ona bakacaz*/
        mKC = mk.lvl1;
        
        
        areTheySameH(incPosKeepClicked, incPosKeepShifted, incInteractTypeCLicked);


    }



    public void areTheySameH(Vector2 worldPosClicked, Vector2 worldPosSwifted, int incInteractType)
    {
        
        //rastgele atandı değişecek
        dummyPos[0].x = worldPosClicked.x - 0.5f;
        dummyPos[0].y = worldPosClicked.y;
        //dummyPos.y = worldPos.y + 0.5f;

        //sağ-sol kontrol

        _incInterractType[0] = dictCreator.searchInDictionary(worldPosSwifted);
        Debug.Log("VAlue : " + _incInterractType[0]);
        //Burada tüm çevresel elemanlar kontrol edilecek gene ilk önce kaydırmanın yönüne karar verilmeli daha sonra ilk tıklanan elemanın çevresinden önce yatay kontrol daha sonra dikey ve aralar şeklinde kontrol sağlanır.
        _incInterractType[1] = dictCreator.searchInDictionary(dummyPos[0]);
        Debug.Log("VAlue : " + _incInterractType[1]);
        dummyPos[1].x = dummyPos[0].x - 0.5f;
        dummyPos[1].y = dummyPos[0].y;
        _incInterractType[2] = dictCreator.searchInDictionary(dummyPos[1]);
        Debug.Log("VAlue : " + _incInterractType[1]);
        



        //dK.lvl10.Keys.ElementAt(worldPos);


        /*hit0[0] = Physics2D.OverlapPoint(worldPos);//Cliked object itself
        hit0[1] = Physics2D.OverlapPoint(dummyPos);
        dummyPos.x = dummyPos.x - 0.5f;
        hit0[1] = Physics2D.OverlapPoint(dummyPos);
        ObjectScript behaviour = hit0[1].GetComponent<ObjectScript>();*/


        //Collider2D hitO0 = Physics2D.OverlapPoint(worldPos);
        //Collider2D hitO1 = Physics2D.OverlapPoint(dummyPos);        
        //Debug.Log(behaviour.interactType);
        //Debug.Log(incInteractType);
        //Debug.Log(behaviour.name);


        
        
        //ObjectScript behaviour2 = hit0[2].GetComponent<ObjectScript>();
        //dummyPos.x = dummyPos.x + 1.5f;
        
        
        
        
        


        //assignObjectBehaviours();//Tüm bu üstteki atamaları bu fonksiyonun içine taşıyarak tekte yapalım


        if (_incInterractType[1] == _incInterractType[0])
        {


            if (_incInterractType[2] == _incInterractType[0])
            {
                hit0[0] = Physics2D.OverlapPoint(worldPosClicked);//Cliked object itself
                hit0[1] = Physics2D.OverlapPoint(dummyPos[0]);

                hit0[2] = Physics2D.OverlapPoint(dummyPos[1]);
                explodeFunc(hit0[0], hit0[1], hit0[2]);

            }

            /*if (behaviour3.interactType == incInteractType)
            {
                explodeFunc(hit0[0], hit0[1], hit0[3]);

            }


        
            Debug.Log("Solda Patlamaya uygun");
            //Destroy(hit.gameObject);
        }else if (behaviour3.interactType == incInteractType)
        {
            
            explodeFunc(hit0[0], hit0[3], hit0[4]);

        }
        else
            Debug.Log("Solda uygun değil");
        

        

        //Debug.Log("hittedobject: " + hit.name);
        
    */
        }
    }

    public void explodeFunc(Collider2D hitO0,Collider2D hitO1,Collider2D hitO2)
    {           
                Destroy(hitO0.gameObject);
                Destroy(hitO1.gameObject);
                Destroy(hitO2.gameObject);
        
    }

    public void areTheySameV(Vector2 worldPos, int incObjectType)
    {
        Vector2 dummyPos;
        //rastgele atandı değişecek
        dummyPos.x = worldPos.x - 0.5f;
        dummyPos.y = worldPos.y + 0.5f;

        //sağ-sol kontrol



        Collider2D hit = Physics2D.OverlapPoint(worldPos);
        ObjectScript behaviour = hit.GetComponent<ObjectScript>();
        if (behaviour.objectType == incObjectType)
        {
            //Debug.Log("Patlamaya uygun");
            //Destroy(hit.gameObject);
        }
        

    }


    public void createName(string incName)//, float xPos, float yPos
    {
        //Debug.Log("hitname7" + incName[7]);
        //Debug.Log("hitname9" + incName[9]);
        //char to string yapmayı öğren
        createdName = incName;
        int dummyKeeper1 = incName[7] - '0';
        dummyKeeper1 = dummyKeeper1 + 5;

        //string dummyKeeper = (char)dummyKeeper1;

        createdName.Replace(createdName[7], (char)dummyKeeper1);
        //Debug.Log("createdName = " + createdName);



    }

    /*public void assignObjectBehaviours()
    {

        
        hit0[2] = Physics2D.OverlapPoint(dummyPos);
        if (!hit0[2])
        {
            behaviour2 = hit0[2].GetComponent<ObjectScript>();
            dummyPos.x = dummyPos.x + 1.5f;

        }
        else
            Debug.Log("Hit 2 NUll");
        hit0[3] = Physics2D.OverlapPoint(dummyPos);

        if (!hit0[3])
        {
            behaviour3 = hit0[3].GetComponent<ObjectScript>();
            dummyPos.x = dummyPos.x + 2f;

        }
        else
            Debug.Log("Hit 3 NUll");
        
        hit0[4] = Physics2D.OverlapPoint(dummyPos);

        if (!hit0[4])
        {
            behaviour4 = hit0[4].GetComponent<ObjectScript>();
            dummyPos.x = dummyPos.x + 2f;

        }   
        else
            Debug.Log("Hit 4 NUll");

    }
    
    public void dontKnowWhatToDo()
    {
        



    }
    
    public void floatCounter()
    {*/
        

    
}
