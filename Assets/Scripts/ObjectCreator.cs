using UnityEngine;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;

public class ObjectCreator : MonoBehaviour
{

    public Array[] objectData;
    public List<ObjectEntity> objectList;
    public GameObject ObjectPrefab; // Prefab for the object to be created
    private int verticalHMT = 9; // Vertical index for objectData
    private int horizontalHMT = 8; // Horizontal index for objectData
    public int dummyNumber = 0; // Dummy number for testing purposes
    private Renderer rend;
    private MapKeeper mapKeeper;

    


    void Start()
    {
        CreateObjFunc(verticalHMT, horizontalHMT);
        //Debug.Log("ObjectCreator started and CreateObjFunc called.");
    }




    public void CreateObjFunc(int verticalHMT, int horizontalHMT)
    {
        Debug.Log("verticalHMT" + verticalHMT);
        Debug.Log("horizontalHMT" + horizontalHMT);
        float useVerticalHMT = verticalHMT / 2;
        Debug.Log("userVerticalHMT" + useVerticalHMT);
        float useHorizontalHMT = horizontalHMT / 2;
        Debug.Log("useHorizontalHMT" + useHorizontalHMT);
        float useVerticalHMTS;
        float useHorizontalHMTS;
        Debug.Log(mapKeeper.lvl1[0, 1]);

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
                rend = GetComponent<Renderer>();
                //Debug.Log("Creating object at position: " + i +" "+j);
                RNG rng = new RNG();
                int randomIndex = rng.RandomNumberGenerator(3);
                GameObject obj = Instantiate(ObjectPrefab);
                obj.SetActive(true);
                obj.name = "Object_" + i + "_" + j;

                // Objeye bağlı script'e eriş
                ObjectScript behaviour = obj.GetComponent<ObjectScript>();
                behaviour.objectType = objectList[randomIndex].objectType;
                behaviour.interactType = objectList[randomIndex].interactType;
                //int deneme = obj.GameObject.objectName;

                //obj.tag = "Object";

                //obj.AddComponent<SpriteRenderer>().sprite = objectList[randomIndex].objectSprite;
                //obj.GetComponent<SpriteRenderer>.sprite = objectList[0].objectSprite;
                obj.GetComponent<SpriteRenderer>().sprite = objectList[randomIndex].objectSprite;
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
