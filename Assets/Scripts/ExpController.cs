using Unity.VisualScripting;
using UnityEngine;

public class ExpController
{

   

    
    public bool isBoomAble(SurrController surrController, GameObject clickedObject, GameObject otherObject, int whichWayDidISwipe)
    {
        /*Kendime Düşünceler*/
        /* Bu class swipeDetection içerisinde çağırılıyor.eğer bir obje hareket ettirilirse çağırılıyor.
        Burada ne kontrol edilmeli:
        - Öncelikle objenin türü öğrenilmeli bunun sonucunda da obje hareket ettirilebilir mi dönmeli
        - Objenin türü patlamaya uygunsa daha sonra objenin yaptığı hareket sonucu patlama olacak mı o sorgulanmalı.
        - Sorgulama sonucu da uygunsa obje patlatılır
        - Eğer uygun değilse swipe gerçekleşmez*/
        ObjectScript behaviourClicked = clickedObject.GetComponent<ObjectScript>();
        ObjectScript behaviourOther = otherObject.GetComponent<ObjectScript>();
        int localObjectType = behaviourClicked.objectType;
        int localInteractTypeClicked = behaviourClicked.interactType;
        int localInteractTypeOther = behaviourOther.interactType;

        ObjectScript behaviourLetted = otherObject.GetComponent<ObjectScript>();
        int localObjectTypeLetted = behaviourLetted.interactType;


        //ilk once uzerine tıklanan gameObject bir ana oyun elemanı mı onu kontrol etmeliyiz, daha sonra patlama olacak mı olmayacak mı on bakacağız.
        if (localObjectType == 0 || localObjectType == 1)
        {
            Vector2 posKeepClicked, posKeepSwifted;
            posKeepClicked.x = clickedObject.transform.position.x;
            posKeepClicked.y = clickedObject.transform.position.y;
            posKeepSwifted.x = otherObject.transform.position.x;
            posKeepSwifted.y = otherObject.transform.position.y;

            bool foundMatch = surrController.isSurExp(localInteractTypeClicked, localInteractTypeOther, posKeepClicked, posKeepSwifted);

            return foundMatch; // Return true only if a match was found
        }
        else // Buradan false dönmesi durumunda yer değiştirme fonksiyonunu kapatmalıyım.
        {
            return false;
        }
        //Patlamaları burada kontrol edecez, patlama kontrolü nerede başlar? ilk aşamada başlamayacak. ilk hareketten sonra başlamalı
        //Debug.Log(gO.GetComponent<GameObject>().value);
        //gO.GetComponent(objectType);


        //return true;
    }

     
}
