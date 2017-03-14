using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterLevelController : MonoBehaviour
{

    private float StartingLevel = 200f;
    public Image Water;
    private float currentLevel;
    private bool isEmpty;
    private bool isFull;

    public float CurrentLevel { get { return currentLevel; } }
    public bool IsEmpty { get { return isEmpty; } }
    public bool IsFull { get { return isFull; } }

    //Initialize the variables.
    void Start()
    {
        isEmpty = false;
        isFull = true;
        currentLevel = StartingLevel;
        Water.fillAmount = 1f;
    }
    void Update()
    {
        currentLevel += Time.deltaTime * 2;
        Water.fillAmount = currentLevel / StartingLevel;
        isEmpty = false;
    }

    public bool DecrementElement(int amount)
    {
        if (IsEmpty)
            return isEmpty;

        // Decrement the element by the amount specified but make sure it stays between the min and max.
        currentLevel -= amount;
        currentLevel = Mathf.Clamp(currentLevel, 0f, StartingLevel);

        // Set the element to show the normalised amount.
        Water.fillAmount = currentLevel / StartingLevel;

        // If the current amount is approximately equal to zero
        if (Mathf.Abs(currentLevel) < float.Epsilon)
        {
            isEmpty = true;
        }
        isFull = false;
        return isEmpty;
    }

    public bool IncrementElement(int amount)
    {
        if (IsFull)
            return isFull;

        // Increment the element by the amount specified but make sure it stays between the min and max.
        currentLevel += amount;
        currentLevel = Mathf.Clamp(currentLevel, 0f, StartingLevel);

        // Set the element to show the normalised amount.
        Water.fillAmount = currentLevel / StartingLevel;

        // If the current amount is equal to 100
        if (currentLevel == StartingLevel)
        {
            isFull = true;
        }
        return isFull;

    }

}
