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
    public GameObject[] gOArray = new GameObject[144]; // Doubled size for multiple explosion chains
    int arrayCounter;
    private System.Collections.Generic.HashSet<GameObject> processedObjects = new System.Collections.Generic.HashSet<GameObject>();

    public int GetArrayCounter()
    {
        return arrayCounter;
    }


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


    public void upgradeDictionaries(Vector2 incPos, Vector2 changedIncPos)//dictionarydeki eleman değerlerini güncelleyecek
    {
        Debug.Log("=== UPGRADING DICTIONARIES ===");
        Debug.Log("Moving from (" + changedIncPos.x + ", " + changedIncPos.y + ") to (" + incPos.x + ", " + incPos.y + ")");

        // Store what was at the old position
        string oldType = dK.Lvl1I0[(changedIncPos.x, changedIncPos.y)];
        GameObject oldObj = dK.Lvl1FFO[(changedIncPos.x, changedIncPos.y)];
        Debug.Log("Object being moved: Type=" + oldType + ", GameObject=" + (oldObj != null ? oldObj.name : "null"));

        // Move the object from changedIncPos to incPos
        dK.Lvl1I0[(incPos.x, incPos.y)] = dK.Lvl1I0[(changedIncPos.x, changedIncPos.y)];
        dK.Lvl1FFO[(incPos.x, incPos.y)] = dK.Lvl1FFO[(changedIncPos.x, changedIncPos.y)];

        // CRITICAL FIX: Clear the old position by setting it to null
        dK.Lvl1I0[(changedIncPos.x, changedIncPos.y)] = null;
        dK.Lvl1FFO[(changedIncPos.x, changedIncPos.y)] = null;

        Debug.Log("New position (" + incPos.x + ", " + incPos.y + "): Type=" + dK.Lvl1I0[(incPos.x, incPos.y)] + ", GameObject=" + (dK.Lvl1FFO[(incPos.x, incPos.y)] != null ? dK.Lvl1FFO[(incPos.x, incPos.y)].name : "null"));
        Debug.Log("Old position (" + changedIncPos.x + ", " + changedIncPos.y + "): Type=" + dK.Lvl1I0[(changedIncPos.x, changedIncPos.y)] + ", GameObject=" + (dK.Lvl1FFO[(changedIncPos.x, changedIncPos.y)] != null ? dK.Lvl1FFO[(changedIncPos.x, changedIncPos.y)].name : "null"));
        Debug.Log("=== DICTIONARY UPDATE COMPLETE ===\n");
    }

    public bool IfKeyExist((float, float) incFloatFloat)
    {
        if (dK.Lvl1I0.ContainsKey(incFloatFloat))
        {


            return true;
        }
        else
            return false;
    }

    // Debug method to print entire dictionary state
    /*public void PrintDictionaryState()
    {
        Debug.Log("\n========== DICTIONARY STATE ==========");
        Debug.Log("Min: (" + minVal.x + ", " + minVal.y + ") Max: (" + maxVal.x + ", " + maxVal.y + ")");

        for (float j = maxVal.y; j >= minVal.y; j = j - 0.5f)
        {
            string row = "Y=" + j + ": ";
            for (float i = minVal.x; i <= maxVal.x; i = i + 0.5f)
            {
                if (dK.Lvl1FFO.ContainsKey((i, j)) && dK.Lvl1FFO[(i, j)] != null)
                {
                    string type = dK.Lvl1I0[(i, j)];
                    GameObject obj = dK.Lvl1FFO[(i, j)];
                    ObjectScript script = obj.GetComponent<ObjectScript>();
                    row += "[" + type + ":" + (script != null ? script.interactType.ToString() : "?") + "] ";
                }
                else
                {
                    row += "[null] ";
                }
            }
            Debug.Log(row);
        }
        Debug.Log("======================================\n");
    }*/


    //Bu fonksiyon içerisinde bütün mapi tarayarak patlama durumunda olanları patlamaları için bir değere atayarak (float olmalı(key değeri)) bir yere döndürerek patlatacaz.
    public (bool, GameObject[]) findExplodables()
    {
        //Debug.Log("\n>>> SEARCHING FOR MATCHES <<<");
        //PrintDictionaryState();

        arrayCounter = 0;
        processedObjects.Clear();

        // Check horizontal matches (left to right)
        for (float j = minVal.y; j <= maxVal.y; j = j + 0.5f)
        {
            for (float i = minVal.x; i <= maxVal.x - 1.5f; i = i + 0.5f)
            {
                // Check if we have room in the array
                if (arrayCounter >= gOArray.Length - 3)
                {
                    Debug.LogWarning("gOArray is full, stopping search");
                    break;
                }

                if (dK.Lvl1FFO.ContainsKey((i, j)) && dK.Lvl1FFO[(i, j)] != null &&
                    dK.Lvl1FFO.ContainsKey((i + 0.5f, j)) && dK.Lvl1FFO[(i + 0.5f, j)] != null &&
                    dK.Lvl1FFO.ContainsKey((i + 1f, j)) && dK.Lvl1FFO[(i + 1f, j)] != null)
                {
                    GameObject obj1 = dK.Lvl1FFO[(i, j)];
                    GameObject obj2 = dK.Lvl1FFO[(i + 0.5f, j)];
                    GameObject obj3 = dK.Lvl1FFO[(i + 1f, j)];

                    ObjectScript script1 = obj1.GetComponent<ObjectScript>();
                    ObjectScript script2 = obj2.GetComponent<ObjectScript>();
                    ObjectScript script3 = obj3.GetComponent<ObjectScript>();

                    if (script1 != null && script2 != null && script3 != null &&
                        script1.interactType == script2.interactType &&
                        script2.interactType == script3.interactType &&
                        script1.objectType == 0 && script2.objectType == 0 && script3.objectType == 0)
                    {
                        Debug.Log("*** HORIZONTAL MATCH FOUND at (" + i + ", " + j + ") ***");
                        Debug.Log("  Objects: " + obj1.name + ", " + obj2.name + ", " + obj3.name);
                        Debug.Log("  Type: " + script1.interactType);

                        // Add all three objects with HashSet for better duplicate detection
                        if (processedObjects.Add(obj1) && arrayCounter < gOArray.Length)
                        {
                            gOArray[arrayCounter++] = obj1;
                        }
                        if (processedObjects.Add(obj2) && arrayCounter < gOArray.Length)
                        {
                            gOArray[arrayCounter++] = obj2;
                        }
                        if (processedObjects.Add(obj3) && arrayCounter < gOArray.Length)
                        {
                            gOArray[arrayCounter++] = obj3;
                        }
                    }
                    else
                    {
                        // Debug why match failed
                        if (script1 == null || script2 == null || script3 == null)
                        {
                            Debug.Log("Skipping (" + i + ", " + j + "): Missing scripts");
                        }
                        else if (script1.interactType != script2.interactType || script2.interactType != script3.interactType)
                        {
                            Debug.Log("Skipping (" + i + ", " + j + "): Types don't match (" + script1.interactType + ", " + script2.interactType + ", " + script3.interactType + ")");
                        }
                        else if (script1.objectType != 0 || script2.objectType != 0 || script3.objectType != 0)
                        {
                            Debug.Log("Skipping (" + i + ", " + j + "): Wrong objectType (" + script1.objectType + ", " + script2.objectType + ", " + script3.objectType + ")");
                        }
                    }
                }
            }
            if (arrayCounter >= gOArray.Length - 3)
            {
                break;
            }
        }

        // Check vertical matches (top to bottom)
        for (float i = minVal.x; i <= maxVal.x; i = i + 0.5f)
        {
            for (float j = maxVal.y; j >= minVal.y + 1.5f; j = j - 0.5f)
            {
                // Check if we have room in the array
                if (arrayCounter >= gOArray.Length - 3)
                {
                    Debug.LogWarning("gOArray is full, stopping search");
                    break;
                }

                if (dK.Lvl1FFO.ContainsKey((i, j)) && dK.Lvl1FFO[(i, j)] != null &&
                    dK.Lvl1FFO.ContainsKey((i, j - 0.5f)) && dK.Lvl1FFO[(i, j - 0.5f)] != null &&
                    dK.Lvl1FFO.ContainsKey((i, j - 1f)) && dK.Lvl1FFO[(i, j - 1f)] != null)
                {
                    GameObject obj1 = dK.Lvl1FFO[(i, j)];
                    GameObject obj2 = dK.Lvl1FFO[(i, j - 0.5f)];
                    GameObject obj3 = dK.Lvl1FFO[(i, j - 1f)];

                    ObjectScript script1 = obj1.GetComponent<ObjectScript>();
                    ObjectScript script2 = obj2.GetComponent<ObjectScript>();
                    ObjectScript script3 = obj3.GetComponent<ObjectScript>();

                    if (script1 != null && script2 != null && script3 != null &&
                        script1.interactType == script2.interactType &&
                        script2.interactType == script3.interactType &&
                        script1.objectType == 0 && script2.objectType == 0 && script3.objectType == 0)
                    {
                        Debug.Log("*** VERTICAL MATCH FOUND at (" + i + ", " + j + ") ***");
                        Debug.Log("  Objects: " + obj1.name + ", " + obj2.name + ", " + obj3.name);
                        Debug.Log("  Type: " + script1.interactType);

                        // Add all three objects with HashSet for better duplicate detection
                        if (processedObjects.Add(obj1) && arrayCounter < gOArray.Length)
                        {
                            gOArray[arrayCounter++] = obj1;
                        }
                        if (processedObjects.Add(obj2) && arrayCounter < gOArray.Length)
                        {
                            gOArray[arrayCounter++] = obj2;
                        }
                        if (processedObjects.Add(obj3) && arrayCounter < gOArray.Length)
                        {
                            gOArray[arrayCounter++] = obj3;
                        }
                    }
                }
            }
            if (arrayCounter >= gOArray.Length - 3)
            {
                break;
            }
        }

        bool foundMatches = arrayCounter > 0;
        Debug.Log(">>> MATCH SEARCH COMPLETE: Found " + arrayCounter + " objects to explode <<<\n");
        return (foundMatches, gOArray);
    }


    //In this function we check if the values are equal so we understand the dictionary upgraded or not.
    public  (bool, int) dictObjectAndIntValRet(Vector2 incChangedIncPos)
    {
        int retIntVal = -1;
        bool retBoolVal = false;

        // Check if the key exists and value is not null
        if (!dK.Lvl1I0.ContainsKey((incChangedIncPos.x, incChangedIncPos.y)) ||
            dK.Lvl1I0[(incChangedIncPos.x, incChangedIncPos.y)] == null)
        {
            return (false, -1);
        }

        retIntVal = int.Parse(dK.Lvl1I0[(incChangedIncPos.x, incChangedIncPos.y)].ToString());

        // Check if the GameObject exists
        if (!dK.Lvl1FFO.ContainsKey((incChangedIncPos.x, incChangedIncPos.y)) ||
            dK.Lvl1FFO[(incChangedIncPos.x, incChangedIncPos.y)] == null)
        {
            return (false, retIntVal);
        }

        GameObject comingVal = dK.Lvl1FFO[(incChangedIncPos.x, incChangedIncPos.y)];
        ObjectScript behaviour = comingVal.GetComponent<ObjectScript>();

        if (behaviour != null)
        {
            retBoolVal = (retIntVal == behaviour.interactType);
        }
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

    public void removeExplodedObjects(GameObject[] explodedObjects, int count)
    {
        for (int i = 0; i < count; i++)
        {
            if (explodedObjects[i] != null)
            {
                // Find and remove from dictionaries
                foreach (var key in new System.Collections.Generic.List<(float, float)>(dK.Lvl1FFO.Keys))
                {
                    if (dK.Lvl1FFO[key] == explodedObjects[i])
                    {
                        dK.Lvl1FFO[key] = null;
                        dK.Lvl1I0[key] = null;
                        Debug.Log("Removed exploded object at position: " + key);
                    }
                }

                // Destroy the actual GameObject
                Destroy(explodedObjects[i]);
                explodedObjects[i] = null;
            }
        }

        // Clear the entire array for next use
        for (int i = 0; i < gOArray.Length; i++)
        {
            gOArray[i] = null;
        }
    }

}


