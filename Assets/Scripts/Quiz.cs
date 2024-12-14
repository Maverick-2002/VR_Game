using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Quiz : MonoBehaviour
{
    public GameObject[] questionPanels;      // Array of all question panels, including the starting panel
    public TextMeshProUGUI feedbackText;     // TMP element for feedback messages
    private int currentPanelIndex = 0;       // Index of the currently active panel
    private bool quizStarted = false;        // Boolean to check if the quiz has started

    void Start()
    {
        // Deactivate all panels initially, then activate the first one (the starting panel)
        foreach (GameObject panel in questionPanels)
        {
            panel.SetActive(false);
        }

        if (questionPanels.Length > 0)
        {
            questionPanels[0].SetActive(true); // Show the starting panel first
        }

        feedbackText.text = ""; // Clear feedback at the start
    }

    // This method will be called when the "Next" button is clicked on the starting panel
    public void StartQuiz()
    {
        questionPanels[0].SetActive(false); // Show the starting panel first

        if (!quizStarted)
        {
            quizStarted = true; // Mark the quiz as started
            currentPanelIndex = 1; // Skip the starting panel and show the first question
            ShowPanel(currentPanelIndex); // Show the first question panel
        }
    }

    // Method to handle answer selection with parameters for correct and selected answers
    public void OnAnswerSelected(int correctAnswerIndex, int selectedAnswerIndex)
    {
        if (selectedAnswerIndex == correctAnswerIndex)
        {
            feedbackText.text = "Correct!";
            feedbackText.color = Color.green; // Set the text color to green for correct answer
        }
        else
        {
            feedbackText.text = "Wrong Answer!";
            feedbackText.color = Color.red; // Set the text color to red for wrong answer
        }

        StartCoroutine(ShowNextPanel());
    }

    // Method to show the next panel after feedback
    private IEnumerator ShowNextPanel()
    {
        yield return new WaitForSeconds(2); // Wait for feedback to display

        feedbackText.text = ""; // Clear feedback for the next question

        // Hide the current panel
        if (currentPanelIndex < questionPanels.Length)
        {
            questionPanels[currentPanelIndex].SetActive(false);
        }

        // Show the next panel
        currentPanelIndex++;
        if (currentPanelIndex < questionPanels.Length)
        {
            ShowPanel(currentPanelIndex);
        }
        else
        {
            Debug.Log("Quiz Completed!");
            // Optionally, add end-of-quiz logic here (e.g., show score, restart button, etc.)
        }
    }

    // Helper method to show a panel based on its index
    private void ShowPanel(int index)
    {
        if (index < questionPanels.Length)
        {
            questionPanels[index].SetActive(true); // Activate the panel at the given index
        }
    }

    // Wrapper methods for each answer option (to be connected in the Unity UI button OnClick)
    public void SelectOption1() { OnAnswerSelected(correctAnswerIndex: 0, selectedAnswerIndex: 0); }
    public void SelectOption2() { OnAnswerSelected(correctAnswerIndex: 0, selectedAnswerIndex: 1); }
    public void SelectOption3() { OnAnswerSelected(correctAnswerIndex: 0, selectedAnswerIndex: 2); }
    public void SelectOption4() { OnAnswerSelected(correctAnswerIndex: 0, selectedAnswerIndex: 3); }
}
