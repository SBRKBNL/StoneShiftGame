using UnityEngine;

public class TouchedObject : MonoBehaviour
{

    Vector3 touchPosWorld;
    bool retValue;
    public MoveObject moveObject;

    //Change me to change the touch phase used.
    TouchPhase touchPhase = TouchPhase.Ended;

    void Update()
    {
        /*if (Input.touchCount > 0 && Input.GetTouch(0).phase == touchPhase) {
            //We transform the touch position into word space from screen space and store it.
            touchPosWorld = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);

            Vector2 touchPosWorld2D = new Vector2(touchPosWorld.x, touchPosWorld.y);

            //We now raycast with this information. If we have hit something we can process it.
            RaycastHit2D hitInformation = Physics2D.Raycast(touchPosWorld2D, Camera.main.transform.forward);

            if (hitInformation.collider != null) {
                //We should have hit something with a 2D Physics collider!
                GameObject touchedObject = hitInformation.transform.gameObject;
                //touchedObject should be the object someone touched.
                Debug.Log("Touched " + touchedObject.transform.name);
                moveObject.MoveObjectFunc(touchedObject);
            }
        }*/

    }
    public bool TouchObjectFunc(Vector2 initPosition)
    {

        //Vector2 touchPosWorld2D = new Vector2(touchPosWorld.x, touchPosWorld.y);
        //Debug.Log("initposx" + initPosition.x + "\ninitposy" + initPosition.y);
        RaycastHit2D hit = Physics2D.Raycast(initPosition, Vector2.zero);
        //RaycastHit2D hitInformation = Physics2D.Raycast(initPosition, Camera.main.transform.forward);


        if (hit.collider != null)
        {
            //We should have hit something with a 2D Physics collider!
            GameObject touchedObject = hit.transform.gameObject;
            //touchedObject should be the object someone touched.
            //Debug.Log("Touched " + touchedObject.transform.name);
            moveObject.MoveObjectFunc(touchedObject);
        }
        else
        {
            //Debug.Log("ObjectNUll");

        }


        return retValue;
    }

}
