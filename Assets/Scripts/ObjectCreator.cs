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
    public MapKeeper mapKeeper = new MapKeeper();
    public WhichIsWhich whichIsWhich = new WhichIsWhich();

    


    void Start()
    {
        //horizontalHMT = mapKeeper.lvl1.GetLength(0);
        //verticalHMT = mapKeeper.lvl1.GetLength(1);
        //CreateObjFunc(verticalHMT, horizontalHMT);
        //Debug.Log("ObjectCreator started and CreateObjFunc called.");
    }




    public void CreateObjFunc(int verticalHMT, int horizontalHMT)
    {
        //Debug.Log("verticalHMT" + verticalHMT);
        //Debug.Log("horizontalHMT" + horizontalHMT);
        float useVerticalHMT = verticalHMT / 2;
        //Debug.Log("userVerticalHMT" + useVerticalHMT);
        float useHorizontalHMT = horizontalHMT / 2;
        //Debug.Log("useHorizontalHMT" + useHorizontalHMT);
        float useVerticalHMTS;
        float useHorizontalHMTS;
        //Debug.Log(mapKeeper.lvl1[0, 1]);
        //Debug.Log(mapKeeper.lvl1.GetLength(0)); // 1. map için 8 döndü
        //Debug.Log(mapKeeper.lvl1.GetLength(1)); //1. map için 9 döndü
        

        //Burayı bir fonksiyona çevir
        if (verticalHMT % 2 == 1)
        {

            useVerticalHMTS = useVerticalHMT + 1;
        }
        else
        {
            useVerticalHMTS = useVerticalHMT;

        }

        if (horizontalHMT % 2 == 1)
        {

            useHorizontalHMTS = useHorizontalHMT + 1;
        }
        else
        {
            useHorizontalHMTS = useHorizontalHMT;

        }

        

        for (float i = -1 * useVerticalHMT; i < useVerticalHMTS; i++)
        {

            /*GameObject obj = Instantiate(ObjectPrefab);
            obj.AddComponent<SpriteRenderer>().sprite = objectList[0].objectSprite;
            obj.transform.localScale = new Vector3(0.2f, 0.2f, 1f);
            obj.transform.position = new Vector3(5, 0, 0);*/
            for (float j = -1 * useHorizontalHMT; j < useHorizontalHMTS; j++)
            {
                //Debug.Log("Creating object at position: " + i +" "+j);
                RNG rng = new RNG();
                int randomIndex = rng.RandomNumberGenerator(3);
                GameObject obj = Instantiate(ObjectPrefab);
                obj.SetActive(true);
                obj.name = "Object_" + i + "_" + j;

                // Objeye bağlı script'e eriş
                ObjectScript behaviour = obj.GetComponent<ObjectScript>();
                behaviour.objectType = objectList[whichIsWhich.wIWF(horizontalHMT, verticalHMT)].objectType; // i yi ve j yi buradan yollamadığım sürece hep baştan başlatacak ve yalnızca ilk değeri alacak.
                //Debug.Log("whichiswhichdonen" + whichIsWhich.wIWF(horizontalHMT, verticalHMT));
                behaviour.interactType = objectList[whichIsWhich.wIWF(horizontalHMT, verticalHMT)].interactType;
                //int deneme = obj.GameObject.objectName;

                //obj.tag = "Object";

                //obj.AddComponent<SpriteRenderer>().sprite = objectList[randomIndex].objectSprite;
                //obj.GetComponent<SpriteRenderer>.sprite = objectList[0].objectSprite;
                obj.GetComponent<SpriteRenderer>().sprite = objectList[whichIsWhich.wIWF(horizontalHMT, verticalHMT)].objectSprite;
                //obj.objectType = objectList[randomIndex].objectType;
                //obj.GetComponent<ObjectData>().objectType = objectList[randomIndex].objectType;





                //obj.transform.localScale = new Vector3(0.2f, 0.2f, 1f);
                float ix = i * 0.5f;
                float jy = j * 0.5f;
                obj.transform.position = new Vector3(ix, jy, 0);

            }

        }

        // This function is intended to create an instance of ObjectEntity


    }
}
