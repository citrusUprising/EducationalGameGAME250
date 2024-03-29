using UnityEngine;
using TMPro; // For TextMeshPro
using System.Collections.Generic;
using System;
using UnityEngine.EventSystems; // Needed for IPointerClickHandler

[System.Serializable]
public class ChecklistItem
{
    public string name;
    public string content;
    public string safeDescription;
    public string unsafeDescription;
    public string verdictFalseNeg;
    public string verdictFalsePos;
    public int score;
    public string safeImage;
    public string unsafeImage;
    public float[] position;
    public string link;
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
    [SerializeField] private GameObject checklistLinkPrefab;
    [SerializeField] private GameObject checklistHeaderPrefab;
    [SerializeField] private Dictionary<string, GameObject> itemPrefabs; // Holds item name to prefab mapping
    [SerializeField] private Transform itemsParent;
    [SerializeField] private string subfolderPath = "Prefabs/checkItems";
    [SerializeField] private GameObject notAllFilledWarning;
    public CheckBox[] checkBoxes;
    private bool isAllCorrect = false;

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
                    CheckBox checkBox = uiItem.GetComponent<CheckBox>();
                    // assign the item's description to the clickable component
                    if (clickable != null && checkBox != null)
                    {
                        clickable.itemName = item.name;
                        clickable.isSafe = UnityEngine.Random.Range(0, 2) == 0; // Randomly set the safety state
                        clickable.safeDescription = item.safeDescription;
                        clickable.unsafeDescription = item.unsafeDescription;
                        clickable.scoreDelta = item.score * (clickable.isSafe ? 1 : -1);
                        clickable.position = new Vector3(item.position[0], item.position[1], item.position[2]);
                        // get the srpite render from the prefab
                        SpriteRenderer spriteRenderer = prefab.GetComponent<SpriteRenderer>();

                        // assign correct checking fields to the checkBox component
                        checkBox.correctAnswer = clickable.isSafe;
                        checkBox.verdictFalseNeg = item.verdictFalseNeg;
                        checkBox.verdictFalsePos = item.verdictFalsePos;
                        // change the sprite to item.safeImage if the item is safe, item.unsafeImage otherwise
                        if (clickable.isSafe)
                        {
                            spriteRenderer.sprite = Resources.Load<GameObject>(item.safeImage).GetComponent<SpriteRenderer>().sprite;
                        }
                        else
                        {
                            spriteRenderer.sprite = Resources.Load<GameObject>(item.unsafeImage).GetComponent<SpriteRenderer>().sprite;
                        }
                        float width = 1;
                        float height = 1;
                        // get width and height of the sprite
                        try
                        {
                            width = spriteRenderer.sprite.bounds.size.x;
                            height = spriteRenderer.sprite.bounds.size.y;

                        }
                        catch (Exception e)
                        {
                            Debug.Log("Error getting sprite bounds for " + item.name + "isSafe: " + clickable.isSafe);
                            Debug.Log(e);
                        }
                        // set the scale of the collider's size to the sprite's size
                        BoxCollider2D collider = prefab.GetComponent<BoxCollider2D>();
                        collider.size = new Vector2(width, height);
                        GameObject gameItem = Instantiate(prefab, clickable.position, Quaternion.identity, itemsParent);
                    }
                    // add external link to the clickable component
                    if (!string.IsNullOrEmpty(item.link))
                    {
                        CreateLink(item.link, uiItem.transform);
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

    void CreateLink(string URL, Transform parentItem)
    {
        GameObject linkItem = Instantiate(checklistLinkPrefab, parentItem);
        LinkOpener linkInstance = linkItem.GetComponent<LinkOpener>();
        linkInstance.URL = URL;
        // Optionally set parent for UI elements
        // linkInstance.transform.SetParent(parentTransform, false);
    }

    public void submitListNshow()
    {
        notAllFilledWarning.SetActive(false);
        checkBoxes = FindObjectsOfType<CheckBox>();
        foreach (CheckBox checkBox in checkBoxes)
        {
            if (!checkBox.isFilledIn)
            {
                Debug.Log("Not all filled");
                notAllFilledWarning.SetActive(true);
                return;
            }
        }
        foreach (CheckBox checkBox in checkBoxes)
        {
            bool result = checkBox.trySubmit();
            isAllCorrect = isAllCorrect && result;
        }
        if (isAllCorrect)
        {
            Debug.Log("All correct");
        }else{
            Debug.Log("Not all correct");
        }
    }

}