using System;
using System.Runtime.CompilerServices;
using NUnit.Framework.Constraints;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Composites;


public class DictController : MonoBehaviour
{
    MapKeeper mK = new MapKeeper();
    PosKeeper pK = new PosKeeper();
    DictKeeper dK = new DictKeeper();
    public Vector2 maxVal;
    public Vector2 minVal;
    public GameObject[] gOArray;
    int arrayCounter;


    public bool createStringDictionaryFunc(int horizontalHMT, int verticalHMT)
    {
        for (int i = 0; i < horizontalHMT; i++)
        {

            for (int j = 0; j < verticalHMT; j++)
            {
                //if(pK.lvl1[i, j].Item1 != null && mK.lvl1[i, j] != null)
                if (dK.Lvl1I0.ContainsKey(pK.lvl1[i, j]))
                {
                    // Anahtar mevcutsa, değeri değiştir.
                    dK.Lvl1I0[pK.lvl1[i, j]] = mK.lvl1[i, j];

                }
                else
                {
                    // Anahtar mevcut değilse, hiçbir şey yapma (veya farklı bir işlem yap).
                    dK.Lvl1I0.Add(pK.lvl1[i, j], mK.lvl1[i, j]);
                }

                //Debug.Log("Deger Atanmis");

            }

        }


        /*if (dC.lvl10.ContainsKey((-2f, 2f))) {

            Debug.Log("Deger Atanmis");

         }*/


        return true;
    }

    public void assignMinMax(Vector2 incMaxVal, Vector2 incMinVal)
    {

        minVal = incMinVal;
        maxVal = incMaxVal;
        //Debug.Log("MinVal: " + minVal +"MaxVal" + maxVal);


    }



    public string searchInDictionary(Vector2 worldPos)
    {
        //if (string.IsNullOrEmpty(dK.Lvl1I0[(worldPos.x, worldPos.y)]))
        //{

        string value = dK.Lvl1I0[(worldPos.x, worldPos.y)];

        return value;
        //}
        //return "zortt";
    }
    /*BUrası yanlış oldu düzelt*/


    public void upgradeDictionaries(Vector2 incPos, Vector2 changedIncPos)//directorydeki bütün elemanları gezerek eşleşme arayacak
    {
        dK.Lvl1I0[(incPos.x, incPos.y)] = dK.Lvl1I0[(changedIncPos.x, changedIncPos.y)];
        dK.Lvl1FFO[(incPos.x, incPos.y)] = dK.Lvl1FFO[(changedIncPos.x, changedIncPos.y)];
        //Debug.Log("Hello World");

    }


    
    public (bool, GameObject[]) findExplodables()
    {

        for (float i = minVal.x; i < maxVal.x ; i = i + 0.5f)
        {
            for (float j = minVal.y; j < maxVal.y ; j = j + 0.5f)
            {
                
                if (dK.Lvl1FFO.ContainsKey((i, j)) && dK.Lvl1FFO.ContainsKey((i + 0.5f, j)) && dK.Lvl1FFO[(i, j)] != null && dK.Lvl1FFO[(i + 0.5f, j)] != null)
                {
                    if (dK.Lvl1FFO[(i, j)] == dK.Lvl1FFO[(i + 0.5f, j)])
                    {
                        if (dK.Lvl1FFO.ContainsKey((i + 1f, j)) && dK.Lvl1FFO[(i + 1f, j)] != null && (dK.Lvl1FFO[(i + 0.5f, j)] == dK.Lvl1FFO[(i + 1f, j)]))
                        {
                            gOArray[arrayCounter] = dK.Lvl1FFO[(i, j)];
                            arrayCounter++;
                            Debug.Log("Patladı, " + i + " " + j);
                            //Patlat

                        }

                    }
                    else if (dK.Lvl1FFO.ContainsKey((i, j - 0.5f)) && dK.Lvl1FFO[(i, j - 0.5f)] != null && dK.Lvl1FFO[(i, j)] == dK.Lvl1FFO[(i, j - 0.5f)])
                    {
                        if (dK.Lvl1FFO[(i, j)] == dK.Lvl1FFO[(i + 0.5f, j - 0.5f)])
                        {
                            gOArray[arrayCounter] = dK.Lvl1FFO[(i, j)];
                            Debug.Log("Patladı, " + i + " " + j);
                            arrayCounter++;

                        }
                    }
                    else{

                    Debug.Log("Patlamaya uygun yok iç üst");

                    }
                }else if(dK.Lvl1FFO.ContainsKey((i, j)) && dK.Lvl1FFO.ContainsKey((i, j - 0.5f)) && dK.Lvl1FFO[(i, j)] != null && dK.Lvl1FFO[(i, j - 0.5f)] != null)
                {
                    if (dK.Lvl1FFO[(i, j)] == dK.Lvl1FFO[(i, j - 0.5f)])
                    {
                        if (dK.Lvl1FFO.ContainsKey((i, j - 1f)) && dK.Lvl1FFO[(i, j - 1f)] != null && (dK.Lvl1FFO[(i, j - 0.5f)] == dK.Lvl1FFO[(i, j - 1f)]))
                        {
                            gOArray[arrayCounter] = dK.Lvl1FFO[(i, j)];
                            Debug.Log("Patladı, " + i + " " + j);
                            arrayCounter++;
                            //Patlat

                        }

                    }
                    else if (dK.Lvl1FFO.ContainsKey((i, j - 0.5f)) && dK.Lvl1FFO[(i, j - 0.5f)] != null && dK.Lvl1FFO[(i, j)] == dK.Lvl1FFO[(i, j - 0.5f)])
                    {
                        if (dK.Lvl1FFO[(i, j)] == dK.Lvl1FFO[(i + 0.5f, j - 0.5f)])
                        {
                            gOArray[arrayCounter] = dK.Lvl1FFO[(i, j)];
                            Debug.Log("Patladı, " + i + " " + j);
                            arrayCounter++;

                        }
                    }
                    else{

                    Debug.Log("Patlamaya uygun yok iç alt");

                }



                }
                else{

                    Debug.Log("Patlamaya uygun yok Üst if");

                }
            }
            
        }

        arrayCounter = 0;
        return (true, gOArray);
    }


    //Bu fonksiyon içerisinde bütün mapi tarayarak patlama durumunda olanları patlamaları için bir değere atayarak (float olmalı(key değeri)) bir yere döndürerek patlatacaz.
    public  (bool, int) dictObjectAndIntValRet(Vector2 incChangedIncPos)
    {
        int retIntVal;
        bool retBoolVal;

        retIntVal = int.Parse(dK.Lvl1I0[(incChangedIncPos.x, incChangedIncPos.y)].ToString());


        GameObject comingVal = dK.Lvl1FFO[(incChangedIncPos.x, incChangedIncPos.y)];
        ObjectScript behaviour = comingVal.GetComponent<ObjectScript>();

        retBoolVal = (retIntVal == behaviour.interactType);
        //int.Parse(mK.lvl1[incX, incY][1].ToString());
        //Burada tüm mapi gezmek istediğimiz için belli bir aralıkta gezmeye karar vermiştik max ve min değerlerini bulduk şimdi onlara bakacam eğer doğruysa buraya çekip o aralığın içindeyken tüm keyleri gezdirerek sağda patlama ihtimali varmı ve alltta patlama ihtimali var mı ona baacam

        return (retBoolVal, retIntVal);
    }


    public void createObjectDictionaryFunc(float incX, float incY, GameObject gO)
    {
        //Buraya name.x_name.y gibi bir formatta string dosyası gelecek bunu parse ile _ öncesi ve sonrası olarak ayırmam lazım bunu nasıl yaparım
        string posKeeperName = gO.name;
        float floatDeger1 = 0.0f;
        float floatDeger2 = 0.0f;

        string[] parcalar = posKeeperName.Split(new char[] { '_' }, 2, StringSplitOptions.RemoveEmptyEntries);
        float.TryParse(parcalar[0], out floatDeger1);
        float.TryParse(parcalar[1], out floatDeger2);

        if (dK.Lvl1FFO.ContainsKey((incX, incY)))
        {
            // Anahtar mevcutsa, değeri değiştir.
            dK.Lvl1FFO[(incX, incY)] = gO;

        }
        else
        {
            // Anahtar mevcut değilse, hiçbir şey yapma (veya farklı bir işlem yap).
            dK.Lvl1FFO.Add((incX, incY), gO);
        }
    }

    public bool upgradeObjectDictionary(Vector2 incPos)
    {


        return false;
    }

}


