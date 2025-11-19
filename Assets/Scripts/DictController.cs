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
        // Check if the key exists in the dictionary
        if (!dK.Lvl1I0.ContainsKey((worldPos.x, worldPos.y)))
        {
            return null;
        }

        string value = dK.Lvl1I0[(worldPos.x, worldPos.y)];

        // Return null if the value is null (position has been cleared)
        if (string.IsNullOrEmpty(value))
        {
            return null;
        }

        return value;
    }
    /*BUrası yanlış oldu düzelt*/


    public void upgradeDictionaries(Vector2 incPos, Vector2 changedIncPos)//dictionarydeki eleman değerlerini güncelleyecek
    {
        // Move the object from changedIncPos to incPos
        dK.Lvl1I0[(incPos.x, incPos.y)] = dK.Lvl1I0[(changedIncPos.x, changedIncPos.y)];
        dK.Lvl1FFO[(incPos.x, incPos.y)] = dK.Lvl1FFO[(changedIncPos.x, changedIncPos.y)];

        // Clear the old position by setting it to null
        dK.Lvl1I0[(changedIncPos.x, changedIncPos.y)] = null;
        dK.Lvl1FFO[(changedIncPos.x, changedIncPos.y)] = null;
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
                    // Removed excessive debug logging for failed matches
                    // Uncomment below for detailed debugging if needed
                    /*else
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
                    }*/
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

        // Get the object's interact type
        ObjectScript objectScript = gO.GetComponent<ObjectScript>();
        string interactTypeStr = objectScript != null ? objectScript.interactType.ToString() : "unknown";

        // Update or add to Lvl1FFO (GameObject dictionary)
        if (dK.Lvl1FFO.ContainsKey((incX, incY)))
        {
            dK.Lvl1FFO[(incX, incY)] = gO;
        }
        else
        {
            dK.Lvl1FFO.Add((incX, incY), gO);
        }

        // CRITICAL FIX: Update or add to Lvl1I0 (type string dictionary)
        if (dK.Lvl1I0.ContainsKey((incX, incY)))
        {
            dK.Lvl1I0[(incX, incY)] = interactTypeStr;
        }
        else
        {
            dK.Lvl1I0.Add((incX, incY), interactTypeStr);
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

    // Remove objects at specific positions from dictionaries
    public void removeExplodedPositions(Vector2 pos1, Vector2 pos2, Vector2 pos3)
    {
        // Clear position 1
        if (dK.Lvl1I0.ContainsKey((pos1.x, pos1.y)))
        {
            dK.Lvl1I0[(pos1.x, pos1.y)] = null;
            dK.Lvl1FFO[(pos1.x, pos1.y)] = null;
        }

        // Clear position 2
        if (dK.Lvl1I0.ContainsKey((pos2.x, pos2.y)))
        {
            dK.Lvl1I0[(pos2.x, pos2.y)] = null;
            dK.Lvl1FFO[(pos2.x, pos2.y)] = null;
        }

        // Clear position 3
        if (dK.Lvl1I0.ContainsKey((pos3.x, pos3.y)))
        {
            dK.Lvl1I0[(pos3.x, pos3.y)] = null;
            dK.Lvl1FFO[(pos3.x, pos3.y)] = null;
        }
    }

}


