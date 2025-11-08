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
    RNG rng = new RNG();
    private Vector2 _maxVal;
    private Vector2 _minVal;
    public DictController dC;


    public (bool, Vector2, Vector2) CreateObjFunc(int verticalHMT, int horizontalHMT, float horizontStart, float verticalStart)
    {


        for (int i = 0; i < verticalHMT; i++)
        {

            for (int j = 0; j < horizontalHMT; j++)
            {
                /*RNG rng = new RNG();
                int randomIndex = rng.RandomNumberGenerator(3);*/


                GameObject obj = Instantiate(ObjectPrefab);
                obj.SetActive(true);
                wIWS = whichIsWhich.wIWF(i, j);
                //wIWS' dene gelen en büyük ve en küçük değeri bulamlıyım. Bunun için de bir fonksiyon tanımlayacağım. Bunu bulmak için de rastgele sırala değil de sıralı hazırlanmış mapler kullanılmasını beklerim mi?
                /*if(i == verticalHMT -1 && j == horizontalHMT - 1)    
                    FindMinAndMaxPoints(wIWS.Item2, wIWS.Item3);
                else*/
                FindMinAndMaxPoints(wIWS.Item2, wIWS.Item3);





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
                string wIWSItem2String = wIWS.Item2.ToString("R");
                string wIWSItem3String = wIWS.Item3.ToString("R");

                obj.name = wIWSItem2String + "_" + wIWSItem3String;

                dC.createObjectDictionaryFunc(wIWS.Item2, wIWS.Item3, obj);

            }

        }
        whichIsWhich.PrintCounterValues();
        
        return (true, _maxVal, _minVal);
    }

    public void continueGame(Vector2 incCreatePos)//inc create pos olarak en üstü verip oradan düşürebilirim
    {
        int randomIndex = rng.RandomNumberGenerator(3);
        GameObject obj = Instantiate(ObjectPrefab);
        obj.SetActive(true);
        //direk yerine geleceği objenin adı gelir ve eşlenir

        // Objeye bağlı script'e eriş
        ObjectScript behaviour = obj.GetComponent<ObjectScript>();
        //3 kere çağırılmasına sebep olan yer
        behaviour.objectType = objectList[randomIndex].objectType; // i yi ve j yi buradan yollamadığım sürece hep baştan başlatacak ve yalnızca ilk değeri alacak.
        behaviour.interactType = objectList[randomIndex].interactType;
        obj.GetComponent<SpriteRenderer>().sprite = objectList[randomIndex].objectSprite;
        obj.transform.position = new Vector3(incCreatePos.x, incCreatePos.y, 0);
        string incCreatePosXString = incCreatePos.x.ToString("R");
        string incCreatePosYString = incCreatePos.y.ToString("R");
        obj.name = incCreatePosXString + "_" + incCreatePosYString;
    }

    
    
    public void FindMinAndMaxPoints(float incValX, float incValY)
    {


        if (incValX < _minVal.x && incValY < _minVal.y)
        {
            _minVal.x = incValX;
            _minVal.y = incValY;

        }else if(incValX > _maxVal.x && incValY > _maxVal.y)
        {
            
            _maxVal.x = incValX;
            _maxVal.y = incValY;

        }

        

    }
}
