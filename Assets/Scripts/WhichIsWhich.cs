using System.Net.Http;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

public class WhichIsWhich
{
    MapKeeper mK = new MapKeeper(); //Creats map keeper referance.
    PosKeeper pK = new PosKeeper();
    int anaTasCounter = 0;
    int engelTasCounter = 0;
    int jokerTasCounter = 0;
    public int wIWF(int incX, int incY)
    {

        char rastgeleDeneme = mK.lvl1[incX, incY][0];
        

        int retVal = mK.lvl1[incX, incY][1] - '0';


        switch (rastgeleDeneme)
        {
            case '0':
                //Debug.Log("Ana Tas");
                anaTasCounter++;

                //Debug.Log("donen" + retVal);

                return retVal;
            case '1':
                //Debug.Log("Engel Tas");
                engelTasCounter++;
                //Debug.Log("donen" + retVal);

                return retVal + 4;
            case '2':
                Debug.Log("Joker Tas");
                jokerTasCounter++;
                //Debug.Log("donen" + retVal);

                return retVal + 10;
            default:
                Debug.Log("İkisi de gerceklesmedi");
                break;
        }

        return -1;
    }

    //Burada bir sorun var counter *3 olarak gidiyor 1 artması gereken yerde 3 artıyor
    public void PrintCounterValues()
    {

        Debug.Log("Anatas Counter Values = " + anaTasCounter);
        Debug.Log("Engel Counter Values = " + engelTasCounter);
        Debug.Log("Joker Counter Values = " + jokerTasCounter);

    }
}




/*public class WhichIsWhich
{
    MapKeeper mK = new MapKeeper(); //Creats map keeper referance.
    
    public int wIWF(int xMaxVal, int yMaxVal)
    {
        for (int i = 0; i < xMaxVal; i++)//In this for structure we will travel arround all the map valeus which stocked on the arrays.
        {

            for (int j = 0; j < yMaxVal; j++)
            {
                char rastgeleDeneme = mK.lvl1[i, j][0];
                //int retVal = (int)char.GetNumericValue(mK.lvl1[i, j][1]); -> Bu sistem işe yaramadı başka bir şey deneyeceğim
                int retVal = mK.lvl1[i, j][1] - '0';
                
                //int intValue = ch - '0';
                Debug.Log("donen" + retVal);
                switch (rastgeleDeneme)
                {
                    case '0':
                        Debug.Log("Ana Tas");
                        

                        //Debug.Log("donen" + retVal);
                        
                        return retVal;
                    case '1':
                        Debug.Log("Engel Tas");
                        //Debug.Log("donen" + retVal);
                        
                        return retVal + 4;
                    case '2':
                        Debug.Log("Joker Tas");
                        //Debug.Log("donen" + retVal);

                        return retVal + 10;
                    default:
                        Debug.Log("İkisi de gerceklesmedi");
                        break;
                }

            }
            


        }
        return -1;
    }
}*/
