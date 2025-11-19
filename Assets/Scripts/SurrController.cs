using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


public class SurrController : MonoBehaviour
{
    public MapKeeper mk = new MapKeeper();
    public string[,] mKC;
    //Collider2D[] hit0 = new Collider2D[10];
    public string[] _incInterractType = new string[18];
    public string createdName;
    public bool localSwipedOrHadTo;
    public Vector2[] dummyPos = new Vector2[5];

    public ObjectCreator objectCreator;

    private DictController _dictController;
    public Collider2D[] hit0 = new Collider2D[5];

    public ShiftController shiftController;


    //Dictionary<float, string> sub = new Dictionary<float, string>();

    void Start()
    {
        _dictController = FindFirstObjectByType<DictController>();
        shiftController = FindFirstObjectByType<ShiftController>();

        if (shiftController != null && _dictController != null)
        {
            shiftController.HizmetSinifiAtama(_dictController);
        }
        else
        {
            Debug.LogError("ShiftController or DictController not found in scene!");
        }
    }

    [Obsolete]
    public bool isSurExp(int incInteractTypeCLicked, int incInteractTypeOther, Vector2 incPosKeepClicked, Vector2 incPosKeepShifted)
    {

        /*Burada sadece objenin çevresinde bulunan diğer elemanlar patlamaya uygun mu onu kontrol edecez onun için bir yerde mapKeeperı lokal bir yere kopyalamalıyız.
        daha sonra kopyalanan bu mapKeeperZart'ı burada çağırarak kenar köşede bulunan elemanlar patlamaya uygun mu ona bakacaz*/
        mKC = mk.lvl1;


        return areTheySameH(incPosKeepClicked, incPosKeepShifted, incInteractTypeCLicked);


    }



    public bool areTheySameH(Vector2 worldPosClicked, Vector2 worldPosSwifted, int incInteractType)
    {
        Debug.Log("World Pos Clicked: " + worldPosClicked);
        Debug.Log("World Pos Swifted: " + worldPosSwifted);
        //rastgele atandı değişecek
        dummyPos[0].x = worldPosClicked.x - 0.5f;
        dummyPos[0].y = worldPosClicked.y;
        //dummyPos.y = worldPos.y + 0.5f;

        //sağ-sol kontrol

        _incInterractType[0] = _dictController.searchInDictionary(worldPosSwifted);
        _incInterractType[1] = _dictController.searchInDictionary(dummyPos[0]);
        dummyPos[1].x = dummyPos[0].x - 0.5f;
        dummyPos[1].y = dummyPos[0].y;
        _incInterractType[2] = _dictController.searchInDictionary(dummyPos[1]);

        // Check if all types are non-null and match
        if (!string.IsNullOrEmpty(_incInterractType[0]) &&
            !string.IsNullOrEmpty(_incInterractType[1]) &&
            _incInterractType[1] == _incInterractType[0])
        {


            if (!string.IsNullOrEmpty(_incInterractType[2]) && _incInterractType[2] == _incInterractType[0])
            {
                if (Physics2D.OverlapPoint(worldPosClicked) != null && Physics2D.OverlapPoint(dummyPos[0]) != null && Physics2D.OverlapPoint(dummyPos[1]) != null)
                {
                    hit0[0] = Physics2D.OverlapPoint(worldPosClicked);//Cliked object itself
                    Debug.Log(worldPosClicked);
                    hit0[1] = Physics2D.OverlapPoint(dummyPos[0]);
                    Debug.Log(dummyPos[0]);
                    hit0[2] = Physics2D.OverlapPoint(dummyPos[1]);
                    Debug.Log(dummyPos[1]);

                    string keepName1 = hit0[0].name;
                    string keepName2 = hit0[1].name;
                    string keepName3 = hit0[2].name;

                    // Store positions before destruction
                    Vector2 pos1 = new Vector2(hit0[0].transform.position.x, hit0[0].transform.position.y);
                    Vector2 pos2 = new Vector2(hit0[1].transform.position.x, hit0[1].transform.position.y);
                    Vector2 pos3 = new Vector2(hit0[2].transform.position.x, hit0[2].transform.position.y);

                    // Destroy the GameObjects
                    explodeFunc(hit0[0], hit0[1], hit0[2]);

                    // Clear destroyed objects from dictionaries
                    if (_dictController != null)
                    {
                        _dictController.removeExplodedPositions(pos1, pos2, pos3);
                    }

                    // Shift objects down and create new objects
                    if (shiftController != null && objectCreator != null)
                    {
                        // Shift and create new objects for each exploded position
                        Vector2 newPos1 = shiftController.findToShift(pos1, 0, true);
                        CreateNewObjectAtTop(newPos1);

                        Vector2 newPos2 = shiftController.findToShift(pos2, 0, true);
                        CreateNewObjectAtTop(newPos2);

                        Vector2 newPos3 = shiftController.findToShift(pos3, 0, true);
                        CreateNewObjectAtTop(newPos3);

                        // Check for new matches after refilling with a small delay
                        StartCoroutine(CheckForCascadingMatchesCoroutine());
                    }

                    return true; // Match found and explosion occurred
                }
                else
                {
                    return false;
                }


            }
            else
            {
                
                //Debug.Log("is NUll or Empty: " + !string.IsNullOrEmpty(_incInterractType[2]));
                Debug.Log("Is interact types same 2: " + _incInterractType[2]);
                Debug.Log("Is interact types same 0: " + _incInterractType[0]);

                Debug.Log("2 ve 0 eşit değil");
                return false; // No match
            }

        }
        else
        {
            Debug.Log("1 ve 0 eşit değil");
            return false; // No match
        }
    }

    public void explodeFunc(Collider2D hitO0,Collider2D hitO1,Collider2D hitO2)
    {
        Destroy(hitO0.gameObject);
        //Debug.Log("GameObject1 Pos: " + hitO0.transform.position);
        Destroy(hitO1.gameObject);
        //Debug.Log("GameObject2 Pos: " + hitO1.transform.position);
        Destroy(hitO2.gameObject);
        //Debug.Log("GameObject3 Pos: " + hitO2.transform.position);
    }

    // Creates a new object at the top position after shifting
    private void CreateNewObjectAtTop(Vector2 topPosition)
    {
        if (objectCreator == null || _dictController == null)
        {
            return;
        }

        // Create new object at the top position
        GameObject newObj = objectCreator.continueGame(topPosition);

        if (newObj != null)
        {
            // Update the dictionaries with the new object
            _dictController.createObjectDictionaryFunc(topPosition.x, topPosition.y, newObj);
        }
    }

    // Coroutine version with delay for cascading match check
    private IEnumerator CheckForCascadingMatchesCoroutine()
    {
        // Wait a frame to ensure all objects are positioned
        yield return new WaitForEndOfFrame();

        if (_dictController == null)
        {
            yield break;
        }

        // Use the existing findExplodables method to scan the entire board
        (bool foundMatches, GameObject[] matchedObjects) = _dictController.findExplodables();

        if (foundMatches)
        {
            int matchCount = _dictController.GetArrayCounter();

            // Get the positions before destroying
            Vector2[] positions = new Vector2[matchCount];
            for (int i = 0; i < matchCount; i++)
            {
                if (matchedObjects[i] != null)
                {
                    positions[i] = new Vector2(matchedObjects[i].transform.position.x, matchedObjects[i].transform.position.y);
                }
            }

            // Destroy matched objects and remove from dictionaries
            _dictController.removeExplodedObjects(matchedObjects, matchCount);

            // Shift and refill for each exploded position
            if (shiftController != null && objectCreator != null)
            {
                for (int i = 0; i < matchCount; i++)
                {
                    if (positions[i] != Vector2.zero)
                    {
                        Vector2 newPos = shiftController.findToShift(positions[i], 0, true);
                        CreateNewObjectAtTop(newPos);
                    }
                }

                // Recursively check for more matches
                yield return StartCoroutine(CheckForCascadingMatchesCoroutine());
            }
        }
    }



}
