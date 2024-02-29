using UnityEngine;
using UnityEngine.UI; // Make sure to include this if you're manipulating UI elements

public class PanelController : MonoBehaviour
{
    public GameObject panel; // Assign your panel here through the inspector
    private bool isCollapsed = true;
    void Start()
    {
        if (panel != null)
        {
            panel.SetActive(!isCollapsed); // Set the panel active state based on the collapsed state
        }
    }

    // This method toggles the panel's collapsed state
    public void TogglePanel()
    {
        if (panel != null)
        {
            isCollapsed = !isCollapsed; // Toggle the state

            // Set the panel active state based on the collapsed state
            panel.SetActive(!isCollapsed);
        }
    }
}
