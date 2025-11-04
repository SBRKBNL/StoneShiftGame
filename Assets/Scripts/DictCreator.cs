using Unity.VisualScripting;
using UnityEngine;


public class DictCreator : MonoBehaviour
{
    MapKeeper mK = new MapKeeper();
    PosKeeper pK = new PosKeeper();
    DictKeeper dK = new DictKeeper();

    public bool createDictionaryFunc(int horizontalHMT, int verticalHMT)
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

    public string searchInDictionary(Vector2 worldPos)
    {
        //if (string.IsNullOrEmpty(dK.Lvl1I0[(worldPos.x, worldPos.y)]))
        //{

        string value = dK.Lvl1I0[(worldPos.x, worldPos.y)];

        return value;
        //}
        //return "zortt";
    }

}


