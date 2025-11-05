using System;

using Unity.VisualScripting;
using UnityEngine;

public class ShiftController
{
    public Collider2D[] gO = new Collider2D[5];
    float t = 0f;
    public float returnSpeed = 100f;

    public void findToShift(Vector2 incComingPos)
    {
        Vector2 changedIncPos;

        changedIncPos.y = incComingPos.y + 0.5f;
        changedIncPos.x = incComingPos.x;

        while(Physics2D.OverlapPoint(changedIncPos) != null)
        {
            float t = 0f;
            float duration = 0.5f;
            

            gO[0] = Physics2D.OverlapPoint(changedIncPos);
            Debug.Log("Şu anda buradasınız: " + gO[0].name);
            while (t < 2f)
            {
                t += Time.deltaTime / duration;
                gO[0].gameObject.transform.position = Vector3.Lerp(gO[0].gameObject.transform.position, incComingPos, t);
                


            }
            changedIncPos.y = changedIncPos.y + 0.5f;
            incComingPos.y = incComingPos.y + 0.5f;
            
            


        }




    }
    

    public void shiftAfterExplode()
    {




    }
    
    


}
