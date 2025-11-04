using UnityEngine;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;
using Unity.Collections;

public class ObjectCreator : MonoBehaviour
{

    public List<ObjectEntity> objectList;
    public GameObject ObjectPrefab; // Prefab for the object to be created
    //private int verticalHMT; // Vertical index for objectData
    //private int horizontalHMT; // Horizontal index for objectData
    //public int dummyNumber = 0; // Dummy number for testing purposes
    //public MapKeeper mapKeeper = new MapKeeper();
    public WhichIsWhich whichIsWhich = new WhichIsWhich();
    public (int, float, float) wIWS; //WhichIsWhichStorage




    void Start()
    {
        //horizontalHMT = mapKeeper.lvl1.GetLength(0);
        //verticalHMT = mapKeeper.lvl1.GetLength(1);
        //CreateObjFunc(verticalHMT, horizontalHMT);
        //Debug.Log("ObjectCreator started and CreateObjFunc called.");
        
    }




    public void CreateObjFunc(int verticalHMT, int horizontalHMT, float horizontStart, float verticalStart)
    {


        for (int i = 0; i < verticalHMT; i++)
        {

            for (int j = 0; j < horizontalHMT; j++)
            {
                /*RNG rng = new RNG();
                int randomIndex = rng.RandomNumberGenerator(3);*/
                

                GameObject obj = Instantiate(ObjectPrefab);
                obj.SetActive(true);
                obj.name = "Object_" + i + "_" + j;
                wIWS = whichIsWhich.wIWF(i, j);
                // Objeye bağlı script'e eriş
                ObjectScript behaviour = obj.GetComponent<ObjectScript>();
                //3 kere çağırılmasına sebep olan yer
                behaviour.objectType = objectList[wIWS.Item1].objectType; // i yi ve j yi buradan yollamadığım sürece hep baştan başlatacak ve yalnızca ilk değeri alacak.
                behaviour.interactType = objectList[wIWS.Item1].interactType;
                obj.GetComponent<SpriteRenderer>().sprite = objectList[wIWS.Item1].objectSprite;



                //obj.AddComponent<SpriteRenderer>().sprite = objectList[randomIndex].objectSprite;
                //obj.GetComponent<SpriteRenderer>.sprite = objectList[0].objectSprite;
                //obj.objectType = objectList[randomIndex].objectType;
                //obj.GetComponent<ObjectData>().objectType = objectList[randomIndex].objectType;


                //obj.transform.localScale = new Vector3(0.2f, 0.2f, 1f);
                obj.transform.position = new Vector3(wIWS.Item2, wIWS.Item3, 0);

            }

        }
        whichIsWhich.PrintCounterValues();

    }
}
