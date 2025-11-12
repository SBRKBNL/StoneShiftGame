using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem; // bu bulunduğu zaman eski tarzı kullanınca patlıyor.



public class SwipeDetection : MonoBehaviour
{

    private Vector3 startPos;
    private Vector3 touchStartPos;
    private bool isDragging = false;
    private int activeFingerId = -1;
    Transform objectTransform;
    public int whichWayDidISwipe;
    Collider2D hit;
    public SurrController surrController;
    //public List<ObjectEntity> objectList;
    

    private ExpController expController = new ExpController(); //Patlama olacak mı kontrol edecek bir sınıf bu sınıftaki fonksiyon bool bir değer dönecek.

    //[Header("Ayarlar")]
    
    public float dragThreshold = 0.4f;  // Sürükleme yönü algılama eşiği
    public float rayDistance = 1f;      // Komşu arama mesafesi 0.5olarak değiştirilmeli mi?
    public float returnSpeed = 10f;     // Geri dönme hızı

    private void Update()
    {
        // Eğer hiç dokunma yoksa çık
        if (Input.touchCount == 0) return;

        foreach (Touch touch in Input.touches)
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(touch.position);
            worldPos.z = 0;

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    // Dokunma başladığında collider'a temas ediliyor mu kontrol et
                    hit = Physics2D.OverlapPoint(worldPos);
                    //Debug.Log("hit pos: " + hit.transform == transform);
                    
                    if (hit != null)
                    {
                        objectTransform = hit.transform;
                        GameObject gO1 = hit.gameObject;
                        
                        //Debug.Log("Hit transform pos x" + gO1.transform.position.x);
                        //Gonderme islemini burada yapacaz.
                        
                        isDragging = true;
                        activeFingerId = touch.fingerId;
                        startPos = objectTransform.position;
                        touchStartPos = worldPos;
                    }
                    break;

                case TouchPhase.Moved:
                    if (isDragging && touch.fingerId == activeFingerId)
                    {
                        // Objeyi dokunma hareketiyle sürükle
                        Vector3 offset = worldPos - touchStartPos;
                        objectTransform.position = startPos + offset;
                        
                    }
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    if (isDragging && touch.fingerId == activeFingerId)
                    {
                        isDragging = false;
                        activeFingerId = -1;
                        
                        HandleRelease(worldPos);
                    }
                    break;
            }
        }
    }

    private void HandleRelease(Vector3 touchEndPos)
    {
        Vector3 dragVector = touchEndPos - touchStartPos;
        bool swapped = false;

        // Sürükleme yönünü belirle (x veya y ekseninde baskın olan)
        if (Mathf.Abs(dragVector.x) > Mathf.Abs(dragVector.y))
        {
            if (dragVector.x > dragThreshold)
            {
                whichWayDidISwipe = 1;
                swapped = TrySwapWithNeighbor(Vector2.right);
            }
            else if (dragVector.x < -dragThreshold)
            {
                whichWayDidISwipe = 2;
                swapped = TrySwapWithNeighbor(Vector2.left);
            }
        }
        else
        {
            if (dragVector.y > dragThreshold){

                whichWayDidISwipe = 3;
                swapped = TrySwapWithNeighbor(Vector2.up);
            
            }
                
            else if (dragVector.y < -dragThreshold){
                whichWayDidISwipe = 4;
                swapped = TrySwapWithNeighbor(Vector2.down);
            }
                
        }

        if (!swapped)
            StartCoroutine(SmoothReturn());
    }

    private IEnumerator SmoothReturn()
    {
        while (Vector3.Distance(objectTransform.position, startPos) > 0.01f)
        {
            objectTransform.position = Vector3.Lerp(objectTransform.position, startPos, Time.deltaTime * returnSpeed);
            yield return null;
        }
        objectTransform.position = startPos;
    }

    private bool TrySwapWithNeighbor(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(startPos, direction, rayDistance);
        if (hit.collider != null && hit.collider.gameObject != gameObject)
        {
            string dummyNameKeeper = objectTransform.name;
            //Dummyname keeperda ilk tıklanan obje var
            //Debug.Log("dummyNameKeeper: " + objectTransform.name);

            Transform other = hit.collider.transform;
            //other positionda 2. obje var
            Vector3 otherPos = other.position;
            //hit.transform.name = "deneme123";

            StartCoroutine(SwapSmooth(other, startPos, otherPos));
            
            objectTransform.name = other.name;

            Debug.Log("other name: " + other.name);
            other.name = dummyNameKeeper; 
            
            return true;
        }
        return false;
    }

    private IEnumerator SwapSmooth(Transform other, Vector3 myStart, Vector3 otherStart)
    {
        float t = 0f;
        float duration = 0.2f;
        Vector3 myTarget = otherStart;
        Vector3 otherTarget = myStart;

        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            objectTransform.position = Vector3.Lerp(myStart, myTarget, t);
            other.position = Vector3.Lerp(otherStart, otherTarget, t);
            yield return null;
        }

        objectTransform.position = myTarget;
        other.position = otherTarget;
        Debug.Log("Is boomable : "+expController.isBoomAble(surrController, objectTransform.gameObject, other.gameObject, whichWayDidISwipe));
    }

}
