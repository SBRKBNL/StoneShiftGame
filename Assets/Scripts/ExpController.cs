using Unity.VisualScripting;
using UnityEngine;

public class ExpController
{
    public SurrController surrController = new SurrController();

    public bool isBoomAble(GameObject gO)
    {
        //Debug.Log(gO.GetComponent<GameObject>().value);
        //gO.GetComponent(objectType);
        surrController.willItExplode(gO);

        return true;
    }

     
}
