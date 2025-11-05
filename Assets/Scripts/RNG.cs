using UnityEngine;

public class RNG
{
    public int RandomNumberGenerator(int divNum)
    {
        int randomIndex = Random.Range(0, divNum);
        randomIndex = randomIndex % divNum; // Ensure the index is within bounds
        Debug.Log(randomIndex);

        return randomIndex;

    }
}
