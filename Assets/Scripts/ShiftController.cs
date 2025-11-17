using System;

using Unity.VisualScripting;
using UnityEngine;

public class ShiftController
{
    public Collider2D[] gO = new Collider2D[5];
    float t = 0f;
    public float returnSpeed = 100f;
    private DictController _dictController;
    public ObjectCreator objCreator;
    //public int _counter = 0;
    

    public Vector2 findToShift(Vector2 incComingPos, int counter, bool checkForExplosions = false)
    {
        //dictController = FindObjectOfType<DictController>();
        Vector2 changedIncPos;

        changedIncPos.y = incComingPos.y + 0.5f;
        changedIncPos.x = incComingPos.x;

        while (Physics2D.OverlapPoint(changedIncPos) != null)
        {
            float t = 0f;
            float duration = 0.5f;


            gO[0] = Physics2D.OverlapPoint(changedIncPos);
            //Debug.Log("Şu anda buradasınız: " + gO[0].name);
            while (t < 2f)
            {
                t += Time.deltaTime / duration;
                gO[0].gameObject.transform.position = Vector3.Lerp(gO[0].gameObject.transform.position, incComingPos, t);



            }
            //Burada atama yapılmıyor değişiklikler dictionarytlere atanmıyor

            ObjectScript behaviour = gO[0].GetComponent<ObjectScript>();
            _dictController.upgradeDictionaries(incComingPos, changedIncPos);
            if (_dictController.dictObjectAndIntValRet(changedIncPos).Item1 == true && _dictController.dictObjectAndIntValRet(changedIncPos).Item2 == behaviour.interactType)
            {

                Debug.Log("Tüm değerler eşit");


            }
            else
            {
                Debug.Log("değerler " + _dictController.dictObjectAndIntValRet(changedIncPos).Item1 + " \ndictController item 2" + _dictController.dictObjectAndIntValRet(changedIncPos).Item2 + " \ndictController interacttype2 " + behaviour.interactType);
            }
            //calling find explodables here is making the explode process slower so I must carry it to another position.
            //on this position ı didnt upgraded the map values so it is normal not to find explodables
            // DO NOT call findExplodables here - it causes array to fill up!
            changedIncPos.y = changedIncPos.y + 0.5f;
            incComingPos.y = incComingPos.y + 0.5f;





        }

        // Only call findExplodables if explicitly requested (to avoid multiple calls)
        if (checkForExplosions)
        {
            var result = _dictController.findExplodables();
            if (result.Item1)
            {
                // If matches found, remove them
                int count = _dictController.GetArrayCounter();
                _dictController.removeExplodedObjects(result.Item2, count);
                Debug.Log("Found and removed " + count + " exploded objects");
            }
        }
        if(Physics2D.OverlapPoint(changedIncPos) != null &&  counter == 0){
                objCreator.continueGame(incComingPos);//Burada find to shiftten gelen değerleri doğrudan continue game içine vererek orada oluşturulacak olan yeni taşların pozisyonuna karar verdik.
                counter ++;
        }
                    //_dictController.manageDictionaries(worldPosClicked);

                    

        return incComingPos;



    }
    
    public void HizmetSinifiAtama(DictController dictController)
    {
        _dictController = dictController;
    }
    

    public void shiftAfterExplode()
    {




    }
    
    


}
