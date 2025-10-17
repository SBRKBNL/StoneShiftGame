using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class ObjectScript : MonoBehaviour
{
    private Vector3 startPos;
    private Vector3 touchStartPos;
    private bool isDragging = false;
    private int activeFingerId = -1;
    //public List<ObjectEntity> objectList;
    public int objectType; 
    public int interactType;
    private ExpController expController = new ExpController(); //Patlama olacak mı kontrol edecek bir sınıf bu sınıftaki fonksiyon bool bir değer dönecek.

    //[Header("Ayarlar")]
    public float dragThreshold = 0.4f;  // Sürükleme yönü algılama eşiği
    public float rayDistance = 1f;      // Komşu arama mesafesi
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
                    Collider2D hit = Physics2D.OverlapPoint(worldPos);
                    if (hit != null && hit.transform == transform)
                    {
                        //Gonderme islemini burada yapacaz.
                        Debug.Log(expController.isBoomAble(hit.gameObject));
                        isDragging = true;
                        activeFingerId = touch.fingerId;
                        startPos = transform.position;
                        touchStartPos = worldPos;
                    }
                    break;

                case TouchPhase.Moved:
                    if (isDragging && touch.fingerId == activeFingerId)
                    {
                        // Objeyi dokunma hareketiyle sürükle
                        Vector3 offset = worldPos - touchStartPos;
                        transform.position = startPos + offset;
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
                swapped = TrySwapWithNeighbor(Vector2.right);
            else if (dragVector.x < -dragThreshold)
                swapped = TrySwapWithNeighbor(Vector2.left);
        }
        else
        {
            if (dragVector.y > dragThreshold)
                swapped = TrySwapWithNeighbor(Vector2.up);
            else if (dragVector.y < -dragThreshold)
                swapped = TrySwapWithNeighbor(Vector2.down);
        }

        if (!swapped)
            StartCoroutine(SmoothReturn());
    }

    private IEnumerator SmoothReturn()
    {
        while (Vector3.Distance(transform.position, startPos) > 0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, startPos, Time.deltaTime * returnSpeed);
            yield return null;
        }
        transform.position = startPos;
    }

    private bool TrySwapWithNeighbor(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(startPos, direction, rayDistance);
        if (hit.collider != null && hit.collider.gameObject != gameObject)
        {
            Transform other = hit.collider.transform;
            Vector3 otherPos = other.position;
            StartCoroutine(SwapSmooth(other, startPos, otherPos));
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
            transform.position = Vector3.Lerp(myStart, myTarget, t);
            other.position = Vector3.Lerp(otherStart, otherTarget, t);
            yield return null;
        }

        transform.position = myTarget;
        other.position = otherTarget;
    }
}
