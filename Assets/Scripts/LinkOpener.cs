using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class LinkOpener : MonoBehaviour, IPointerClickHandler
{
    public string URL; // The URL to open

    void Start()
    {
        // Optionally, set the text from here if needed
        TMP_Text textComponent = GetComponentInChildren<TMP_Text>();
        if (textComponent != null)
        {
            // Set your link text here, if you want it to be dynamic
            textComponent.text = "<color=blue><u>Click to check more details</u></color>"; // Example text
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Application.OpenURL(URL);
    }
}
