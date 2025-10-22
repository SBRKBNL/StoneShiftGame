using UnityEngine;

public class MoveObject : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private bool retValue;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool MoveObjectFunc(GameObject incObj)
    {
        incObj.transform.position = new Vector3(2, 1, 0); ;
        Debug.Log("Object Possesion x:" + incObj.transform.position.x + "\n" + "Object Possesion y:" + incObj.transform.position.y);

        //obj.transform.position = new Vector3( i,  j, 0);

        return retValue;
    }
}
