using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ShiftController : MonoBehaviour
{
    public Collider2D[] gO = new Collider2D[5];
    public float returnSpeed = 100f;
    private DictController dictController;

    // Method to assign DictController reference
    public void HizmetSinifiAtama(DictController dc)
    {
        dictController = dc;
    }

    // Updated method signature with all 3 parameters
    public Vector2 findToShift(Vector2 incComingPos, int depth, bool shouldUpdate)
    {
        if (dictController == null)
        {
            dictController = FindFirstObjectByType<DictController>();
        }

        Vector2 changedIncPos;
        changedIncPos.y = incComingPos.y + 0.5f;
        changedIncPos.x = incComingPos.x;

        // Check if there's an object above to shift down
        while (Physics2D.OverlapPoint(changedIncPos) != null)
        {
            Collider2D objCollider = Physics2D.OverlapPoint(changedIncPos);

            // Move the game object physically
            objCollider.gameObject.transform.position = new Vector2(incComingPos.x, incComingPos.y);

            // Update the GameObject name to reflect new position
            string newName = incComingPos.x.ToString("R") + "_" + incComingPos.y.ToString("R");
            objCollider.gameObject.name = newName;

            // Update dictionaries if requested
            if (shouldUpdate && dictController != null)
            {
                dictController.upgradeDictionaries(incComingPos, changedIncPos);
            }

            // Move up to check next position
            incComingPos.y += 0.5f;
            changedIncPos.y += 0.5f;
        }

        return incComingPos;
    }

    public void shiftAfterExplode()
    {
        // This method can be implemented later for additional shift logic after explosions
    }
}
