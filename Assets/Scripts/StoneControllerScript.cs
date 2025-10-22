using UnityEngine;

public class StoneControllerScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    MapKeeper mK = new MapKeeper(); //Calls a new Map keeper
    WhichIsWhich whichIsWhich = new WhichIsWhich(); //Calls a new WhichIsWhich
    private int verticalHMT; // Vertical index for objectData
    private int horizontalHMT; // Horizontal index for objectData
    public ObjectCreator objectCreator;
    void Start()
    {

        horizontalHMT = mK.lvl1.GetLength(0);
        verticalHMT = mK.lvl1.GetLength(1);
        objectCreator.CreateObjFunc(verticalHMT, horizontalHMT);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
