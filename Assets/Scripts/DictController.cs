using System;
using Unity.VisualScripting;
using UnityEngine;


public class DictController : MonoBehaviour
{
    MapKeeper mK = new MapKeeper();
    PosKeeper pK = new PosKeeper();
    DictKeeper dK = new DictKeeper();
    public Vector2 maxVal;
    public Vector2 minVal;



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
    public void manageDictionary(Vector2 incValue)//directorydeki bütün elemanları gezerek eşleşme arayacak
    {
        for (float i = minVal.x; i < maxVal.x; i = i + 0.5f)
        {
            for (float j = minVal.y; j < maxVal.y; j = j + 0.5f)
            {

                
            }
        }

    }


    //Bu fonksiyon içerisinde bütün mapi tarayarak patlama durumunda olanları patlamaları için bir değere atayarak (float olmalı(key değeri)) bir yere döndürerek patlatacaz.
    public bool findExplodables()
    {
        //Burada tüm mapi gezmek istediğimiz için belli bir aralıkta gezmeye karar vermiştik max ve min değerlerini bulduk şimdi onlara bakacam eğer doğruysa buraya çekip o aralığın içindeyken tüm keyleri gezdirerek sağda patlama ihtimali varmı ve alltta patlama ihtimali var mı ona baacam

        return false;
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

}


