using UnityEngine;

public class StoneControllerScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    MapKeeper mK = new MapKeeper(); //Calls a new Map keeper
    WhichIsWhich whichIsWhich = new WhichIsWhich(); //Calls a new WhichIsWhich
    private int verticalHMT; // Vertical index for objectData
    private int horizontalHMT; // Horizontal index for objectData
    public ObjectCreator objectCreator;
    public PosArrange posArrange = new PosArrange();
    public (float, float) gelenDeger;
    void Start()
    {

        horizontalHMT = mK.lvl1.GetLength(0);
        //Debug.Log("horizontalHMT= " + horizontalHMT);
        verticalHMT = mK.lvl1.GetLength(1);
        //Debug.Log("verticalHMT= " + verticalHMT);
        gelenDeger = posArrange.DecideLine(horizontalHMT, verticalHMT);
        //Debug.Log("Gelen Deger Item1= " + gelenDeger.Item1 + "Gelen Deger Item2 = " + gelenDeger.Item2);
        
        objectCreator.CreateObjFunc(horizontalHMT, verticalHMT, gelenDeger.Item1, gelenDeger.Item2); // horizontal hmt yatay sıraları tutarken vertical dikey sıraları tuutyor.
                                                                 //Burada ilk aşamada y değerini veriyoruz daha sonra x değerini gönderiyoruz

    }

    // Update is called once per frame
    void Update()
    {

    }
}
