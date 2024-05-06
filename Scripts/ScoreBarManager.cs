using UnityEngine;
using UnityEngine.UI;

public class ScoreBarManager : MonoBehaviour
{
    public float startingFill = 0.5f; // Starting fill amount
    public float fillSpeed = 1f; // Speed of the fill transition
    public Image playerScoreBar;

    private float targetFill; // The target fill amount

    void Start()
    {
        // Set the initial fill amount
        playerScoreBar.fillAmount = startingFill;
        targetFill = startingFill;
    }

    void Update()
    {
        // Smoothly transition the fill amount towards the target fill amount
        playerScoreBar.fillAmount = Mathf.MoveTowards(playerScoreBar.fillAmount, targetFill, fillSpeed * Time.deltaTime);
    }

    // Method to update the score and adjust the fill amount accordingly
    public void UpdateScore(float newFill)
    {
        // Clamp the new fill value to be between 0 and 1
        newFill = Mathf.Clamp01(newFill);

        // Set the target fill amount for smooth transition
        targetFill = newFill;
    }
}
