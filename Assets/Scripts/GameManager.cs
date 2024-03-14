using UnityEngine;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public static GameManager Instance;

    [Header("Camera Settings")]
    [SerializeField] private Vector3[] cameraPositions; // Array of camera positions for each level
    [SerializeField] private Camera mainCamera; // Reference to the main camera

    [Header("Game Settings")]
    [SerializeField] private int totalIssuesToFind = 4; // Set this based on how many issues there are to find in the scene
    [SerializeField] private int startingScore = 0;


    [Header("UI Elements")]
    [SerializeField] private float transitionDuration = 2f;
    [SerializeField] private GameObject SuccessPanel, feedbackPanel, transitionPanel;
    //[SerializeField] private TMP_Text levelText;
    //[SerializeField] private TMP_Text scoreText;
    //[SerializeField] private TMP_Text feedbackText;
    [SerializeField] private TMP_Text safetyDescription;
    //[SerializeField] private TMP_Text issuesFoundText;

    private bool isLeft = true;
    private int score = 0, issuesFound = 0, currentSettingIndex = 0;
    public bool LevelCompleted { get; private set; } = false; // Track if the current level is completed



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        score = startingScore;
        feedbackPanel.SetActive(false);
        SuccessPanel.SetActive(false);
        transitionPanel.SetActive(false);
        Debug.Log(GameManager.Instance);
    }

    private void Update(){
        move();
    }

    public void ItemClicked(FoodSafetyClickable item)
    {
        if (!LevelCompleted)
        {
            if (feedbackPanel == null || safetyDescription == null)
            {
                Debug.LogWarning("Some UI elements are missing");
            }
            // Show item description regardless of its counted state
            //feedbackText.text = item.itemName;
            safetyDescription.text = item.isSafe ? item.safeDescription : item.unsafeDescription;

            if (!item.HasBeenCounted())
            {
                // Update score and issues found only if the item hasn't been counted yet
                score += item.scoreDelta;
                //scoreText.text = "Score: " + score.ToString();

                if (item.isSafe)
                {
                    issuesFound++;
                    // Check if all issues have been found after processing the clicked item
                    // commented out for playtest FLAG
                    //CheckForLevelCompletion();a
                }
                //issuesFoundText.text = "Issues found: " + issuesFound;
            }

            feedbackPanel.SetActive(true);
            Debug.Log(item.transform.position);
            Debug.Log(item.transform.localPosition);

            Vector3 position = mainCamera.WorldToScreenPoint(item.transform.localPosition);

            position += new Vector3(0, 50, 0);
            feedbackPanel.transform.position = position;


            //feedbackPanel.transform.localPosition = item.transform.localPosition;
        }
    }

    private void CheckForLevelCompletion()
    {
        if (issuesFound >= totalIssuesToFind)
        {
            LevelCompleted = true;
            StartCoroutine(TransitionToNextSetting());
        }
    }

    private IEnumerator TransitionToNextSetting()
    {

        currentSettingIndex++;
        if (currentSettingIndex < cameraPositions.Length)
        {
            transitionPanel.SetActive(true); // Show transition panel
            yield return new WaitForSeconds(transitionDuration); // Wait for the duration of the transition
            mainCamera.transform.position = cameraPositions[currentSettingIndex];
            //levelText.text = "Lv." + (currentSettingIndex + 1).ToString();
            ResetLevel();
        }
        else
        {
            transitionPanel.SetActive(false);
            SuccessPanel.SetActive(true);
            Debug.Log("Congratulations! You've completed all the levels!");
            // Handle game completion logic here
        }

        transitionPanel.SetActive(false); // Hide transition panel after moving
    }
    private void ResetLevel()
    {
        LevelCompleted = false; // Reset level completion status
        issuesFound = 0;
        //issuesFoundText.text = "Issues found: " + issuesFound;
    }

    private void move(){
        if (isLeft&&Input.GetKeyDown(KeyCode.LeftArrow)){
            isLeft = false;
             Vector3 tempTrans = mainCamera.GetComponent<Transform>().position;
             tempTrans.x = 22;
             mainCamera.GetComponent<Transform>().position = tempTrans;
        }
        else if (!isLeft&&Input.GetKeyDown(KeyCode.RightArrow)){
            isLeft = true;
            Vector3 tempTrans = mainCamera.GetComponent<Transform>().position;
            tempTrans.x = 0;
            mainCamera.GetComponent<Transform>().position = tempTrans;
        }
    }
}