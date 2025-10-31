using System;
using JetBrains.Annotations;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


public class SurrController : MonoBehaviour
{
    public MapKeeper mk = new MapKeeper();
    public string[,] mKC;
    Collider2D[] hit0 = new Collider2D[10];
    public string createdName;

    public void isSurExp(int incInteractType, float xPosInc, float yPosInc)
    {
        /*Burada sadece objenin çevresinde bulunan diğer elemanlar patlamaya uygun mu onu kontrol edecez onun için bir yerde mapKeeperı lokal bir yere kopyalamalıyız.
        daha sonra kopyalanan bu mapKeeperZart'ı burada çağırarak kenar köşede bulunan elemanlar patlamaya uygun mu ona bakacaz*/
        mKC = mk.lvl1;
        Vector2 objectPos;
        objectPos.x = xPosInc;
        objectPos.y = yPosInc;
        areTheySameH(objectPos, incInteractType);

        
        //hit = Physics2D.OverlapPoint(worldPos);
        //Debug.Log("Game object name= " + mKC[1, 0]);

        /*if (GameObject.Find(findObject))
        {
            Debug.Log("Object Found:" + findObject);
        }
        else
            Debug.Log("Object Not Found" + findObject);
        */

    }



    public void areTheySameH(Vector2 worldPos, int incInteractType)
    {
        Vector2 dummyPos;
        //rastgele atandı değişecek
        dummyPos.x = worldPos.x - 0.5f;
        dummyPos.y = worldPos.y;
        //dummyPos.y = worldPos.y + 0.5f;

        //sağ-sol kontrol

        hit0[0] = Physics2D.OverlapPoint(worldPos);//Cliked object itself
        hit0[1] = Physics2D.OverlapPoint(dummyPos);
        ObjectScript behaviour = hit0[1].GetComponent<ObjectScript>();
        //Collider2D hitO0 = Physics2D.OverlapPoint(worldPos);
        //Collider2D hitO1 = Physics2D.OverlapPoint(dummyPos);        
        //Debug.Log(behaviour.interactType);
        //Debug.Log(incInteractType);
        //Debug.Log(behaviour.name);

        if (behaviour.interactType == incInteractType)
        {
            dummyPos.x = dummyPos.x - 0.5f;
            Collider2D hitO2 = Physics2D.OverlapPoint(dummyPos);
            ObjectScript behaviour2 = hitO2.GetComponent<ObjectScript>();
            dummyPos.x = dummyPos.x + 1.5f;
            Collider2D hitO3 = Physics2D.OverlapPoint(dummyPos);
            ObjectScript behaviour3 = hitO3.GetComponent<ObjectScript>();

            if (behaviour2.interactType == incInteractType)
            {
                explodeFunc(hit0[0], hit0[1], hitO2);

            }
            
            if (behaviour3.interactType == incInteractType)
            {
                explodeFunc(hit0[0], hit0[1], hitO3);

            }



            Debug.Log("Solda Patlamaya uygun");
            //Destroy(hit.gameObject);
        }
        else
            Debug.Log("Solda uygun değil");
        

        

        //Debug.Log("hittedobject: " + hit.name);
        

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
    
    public void floatCounter()
    {
        

    }
}
