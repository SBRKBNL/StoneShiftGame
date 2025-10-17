using UnityEngine;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;

public class ObjectCreator : MonoBehaviour
{

    public Array[] objectData;
    public List<ObjectEntity> objectList;
    public List<ObjectEntity> objectType;
    public GameObject ObjectPrefab; // Prefab for the object to be created
    private int verticalHMT = 8; // Vertical index for objectData
    private int horizontalHMT = 8; // Horizontal index for objectData
    public int dummyNumber = 0; // Dummy number for testing purposes
    private Renderer rend;
    
    void Start()
    {
        CreateObjFunc(verticalHMT, horizontalHMT);
        //Debug.Log("ObjectCreator started and CreateObjFunc called.");
    }



    public void CreateObjFunc(int verticalHMT, int horizontalHMT)
    {
        int useVerticalHMT = verticalHMT/2;
        int useHorizontalHMT = horizontalHMT/2;
        {
            for (int i = -1 * useVerticalHMT; i < useVerticalHMT; i++)
            {

                /*GameObject obj = Instantiate(ObjectPrefab);
                obj.AddComponent<SpriteRenderer>().sprite = objectList[0].objectSprite;
                obj.transform.localScale = new Vector3(0.2f, 0.2f, 1f);
                obj.transform.position = new Vector3(5, 0, 0);*/
                for (int j = -1 * useHorizontalHMT; j < useVerticalHMT; j++)
                {
                    rend = GetComponent<Renderer>();
                    //Debug.Log("Creating object at position: " + i +" "+j);
                    RNG rng = new RNG();
                    int randomIndex = rng.RandomNumberGenerator(3);
                    GameObject obj = Instantiate(ObjectPrefab);
                    obj.SetActive(true);
                    obj.name = "Object_" + i + "_" + j;
                    int deneme = obj.GameObject.objectName;
                   
                    //obj.tag = "Object";

                    //obj.AddComponent<SpriteRenderer>().sprite = objectList[randomIndex].objectSprite;
                    //obj.GetComponent<SpriteRenderer>.sprite = objectList[0].objectSprite;
                    obj.GetComponent<SpriteRenderer>().sprite = objectList[randomIndex].objectSprite;
                    
                  
                    
                    
                    //obj.transform.localScale = new Vector3(0.2f, 0.2f, 1f);
                    obj.transform.position = new Vector3( i,  j, 0);

                }

            }
        }
        // This function is intended to create an instance of ObjectEntity


    }
}
