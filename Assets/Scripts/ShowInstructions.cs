using UnityEngine;
using TMPro;

public class ShowInstructions : MonoBehaviour
{
    public float flashSpeed = 1.3f;  // Speed of the flash
    public float displayTime = 5f; // Time to display the flashing text
    private float timer;
    private bool isFadingIn = true; // Flag to control fading direction
    private TextMeshProUGUI instructionsText;

    void Start()
    {
        // Get the TextMeshProUGUI component directly from the attached GameObject
        instructionsText = GetComponent<TextMeshProUGUI>();
        instructionsText.gameObject.SetActive(true); // Show text at the start
        timer = displayTime;  // Set the display time
    }

    void Update()
    {
        timer -= Time.deltaTime;

        // Flashing (fading) logic
        Color textColor = instructionsText.color;

        if (isFadingIn)
        {
            textColor.a += flashSpeed * Time.deltaTime; // Fade in
            if (textColor.a >= 1f) // If fully visible, switch to fading out
            {
                textColor.a = 1f;
                isFadingIn = false;
            }
        }
        else
        {
            textColor.a -= flashSpeed * Time.deltaTime; // Fade out
            if (textColor.a <= 0f) // If fully transparent, switch to fading in
            {
                textColor.a = 0f;
                isFadingIn = true;
            }
        }

        instructionsText.color = textColor; // Apply the color change

        // Hide the text completely after the display time
        if (timer <= 0f)
        {
            instructionsText.gameObject.SetActive(false);
        }
    }
}