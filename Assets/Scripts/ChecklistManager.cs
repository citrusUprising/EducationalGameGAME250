using UnityEngine;
using TMPro; // For TextMeshPro
using System.Collections.Generic;
using System;

[System.Serializable]
public class ChecklistItem
{
    public string name;
    public string content;
    public string safeDescription;
    public string unsafeDescription;
    public int score;
    public string safeImage;
    public string unsafeImage;
    public int[] position;
}


[System.Serializable]
public class Section
{
    public string name;
    public ChecklistItem[] items;
}

[System.Serializable]
public class ChecklistSections
{
    public Section[] sections;
}

public class ChecklistManager : MonoBehaviour
{
    [SerializeField] private TextAsset checklistJson; // Assign in the inspector
    [SerializeField] private ChecklistSections checklistData;
    [SerializeField] private Transform checklistUIParent;
    [SerializeField] private GameObject checklistItemPrefab;
    [SerializeField] private GameObject checklistHeaderPrefab;
    [SerializeField] private Dictionary<string, GameObject> itemPrefabs; // Holds item name to prefab mapping
    [SerializeField] private Transform itemsParent;
    [SerializeField] private string subfolderPath = "Prefabs/checkItems";

    void Start()
    {
        itemPrefabs = new Dictionary<string, GameObject>();
        LoadItemPrefabs();
        LoadChecklistItems();
    }

    void LoadItemPrefabs()
    {
        foreach (var item in Resources.LoadAll<GameObject>(subfolderPath))
        {
            if (!itemPrefabs.ContainsKey(item.name))
            {
                itemPrefabs.Add(item.name, item);
            }
        }
    }

    void LoadChecklistItems()
    {
        checklistData = JsonUtility.FromJson<ChecklistSections>(checklistJson.text);
        DisplayChecklistItems();
    }

    void DisplayChecklistItems()
    {
        foreach (var section in checklistData.sections)
        {
            //create a header for each section
            CreateSectionHeader(section.name);
            // Debug.Log("Images");
            // Debug.Log(Resources.LoadAll<GameObject>("Images"));
            foreach (var item in section.items)
            {
                if (itemPrefabs.TryGetValue(item.name, out GameObject prefab))
                {
                    // Instantiate the UI element as a child of the 'Content' GameObject
                    GameObject uiItem = Instantiate(checklistItemPrefab, checklistUIParent);
                    // Set the text of the UI element to the content of the item
                    TMP_Text textComponent = uiItem.GetComponentInChildren<TMP_Text>();
                    if (textComponent != null) textComponent.text = item.content;
                    // Instantiate the food safety item in the scene
                    FoodSafetyClickable clickable = prefab.GetComponent<FoodSafetyClickable>();
                    // assign the item's description to the clickable component
                    if (clickable != null)
                    {
                        clickable.itemName = item.name;
                        clickable.isSafe = UnityEngine.Random.Range(0, 2) == 0; // Randomly set the safety state
                        clickable.safeDescription = item.safeDescription;
                        clickable.unsafeDescription = item.unsafeDescription;
                        clickable.scoreDelta = item.score * (clickable.isSafe ? 1 : -1);
                        clickable.position = new Vector3(item.position[0], item.position[1], item.position[2]);
                        // get the srpite render from the prefab
                        SpriteRenderer spriteRenderer = prefab.GetComponent<SpriteRenderer>();
                        // change the sprite to item.safeImage if the item is safe, item.unsafeImage otherwise
                        if (clickable.isSafe)
                        {
                            Debug.Log(item.safeImage);
                            Debug.Log(Resources.Load<GameObject>(item.safeImage));
                            spriteRenderer.sprite = Resources.Load<GameObject>(item.safeImage).GetComponent<SpriteRenderer>().sprite;
                            
                        }
                        else
                        {
                            Debug.Log(item.unsafeImage);
                            Debug.Log(Resources.Load<GameObject>(item.unsafeImage));
                            spriteRenderer.sprite = Resources.Load<GameObject>(item.unsafeImage).GetComponent<SpriteRenderer>().sprite;
                        }
                        GameObject gameItem = Instantiate(prefab, clickable.position, Quaternion.identity, itemsParent);
                        // Debug.Log($"Setting {item.name} description to {item.safeDescription}");
                    }
                }
                else
                {
                    // Debug.LogWarning($"Prefab for {item.name} not found.");
                }
            }
        }
    }

    void CreateSectionHeader(string sectionName)
    {
        // This function creates a UI element that serves as a header for each section
        GameObject header = Instantiate(checklistHeaderPrefab, checklistUIParent);
        TMP_Text headerTextComponent = header.GetComponentInChildren<TMP_Text>();
        if (headerTextComponent != null) headerTextComponent.text = sectionName;
    }
}
