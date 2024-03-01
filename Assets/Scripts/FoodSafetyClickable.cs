using UnityEngine;

public class FoodSafetyClickable : MonoBehaviour
{
    public string itemName;

    [HideInInspector]
    public string safeDescription;
    
    [HideInInspector]
    public string unsafeDescription;
    [HideInInspector]
    public Vector3 position;
    public bool isSafe;
    public int scoreDelta; // Adjusted for clarity
    private bool hasBeenCounted = false; // Track if scored

    private void Awake()
    {
        if (string.IsNullOrEmpty(itemName))
        {
            itemName = gameObject.name;
            if(isSafe){
                //this.GetComponent<SpriteRenderer>().sprite = ;
            }else{
                //this.GetComponent<SpriteRenderer>().sprite = ;
            }
        }
    }

    public void OnMouseDown()
    {
        if (!GameManager.Instance.LevelCompleted) // Only allow clicks if the level is not completed
        {
            GameManager.Instance.ItemClicked(this);
            if (isSafe && !hasBeenCounted)
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
