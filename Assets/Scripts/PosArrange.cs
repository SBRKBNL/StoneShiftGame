using UnityEditor;
using UnityEngine;

public class PosArrange
{

    //Buralar biraz karıştı bir daha düşün. Öncelikle wordde planla.

    /*public (int, int) PosArrangeFunc(int x, int y)
    {
        (int, int) posVal;
        

        /*Debug.Log("donen deger: " + DecideLine(x, y));

        for (int i = -1 * userverticalhmt; i < userverticalhmt; i++)
        {
            for (int j = -1 * userhorizontalhmt; j < userhorizontalhmt; j++)
        }


        return posVal;
    }*/

    //Burada ne hesaplanıyor ? 
    // -1 * useVerticalHmt çarp burası dizime başlanacak ilk konumu veriyor oradan verticalHmtye kadar diz
    public (float, float) DecideLine(int verticalHMT, int horizontalHMT)
    {
        float useVerticalHMT = verticalHMT / 2;
        //Debug.Log("userVerticalHMT" + useVerticalHMT);
        float useHorizontalHMT = horizontalHMT / 2;
        //Debug.Log("useHorizontalHMT" + useHorizontalHMT);
        //float useVerticalHMTS;
        //float useHorizontalHMTS;
        (float, float) lineVal;

        if (verticalHMT % 2 == 1)
        {

            lineVal.Item1 = useVerticalHMT + 1;
        }
        else
        {
            lineVal.Item1 = useVerticalHMT;

        }

        if (horizontalHMT % 2 == 1)
        {

            lineVal.Item2 = useHorizontalHMT + 1;
        }
        else
        {
            lineVal.Item2 = useHorizontalHMT;

        }
        /*lineVal.Item1 = -1 * lineVal.Item1;
        lineVal.Item2 = -1 * lineVal.Item2;*/

        return lineVal;
    }
}
