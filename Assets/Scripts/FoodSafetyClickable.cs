using UnityEngine;

public class FoodSafetyClickable : MonoBehaviour
{
    public string itemDescription;
    public bool isIssue;
    public int scoreDelta; // Adjusted for clarity
    private bool hasBeenCounted = false; // Track if scored

    private void Awake()
    {
        if (string.IsNullOrEmpty(itemDescription))
        {
            itemDescription = gameObject.name;
        }
    }

    public void OnMouseDown()
    {
        if (!GameManager.Instance.LevelCompleted) // Only allow clicks if the level is not completed
        {
            GameManager.Instance.ItemClicked(this);
            if (isIssue && !hasBeenCounted)
            {
                hasBeenCounted = true; // This prevents further scoring from this item
            }
        }
    }

    public bool HasBeenCounted()
    {
        return hasBeenCounted;
    }

    public void ResetItem() // Optional: For resetting item state in game logic
    {
        hasBeenCounted = false;
    }
}
