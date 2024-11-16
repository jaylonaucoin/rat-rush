using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene loading

public class LevelChange : MonoBehaviour
{
    // This function will be called when the player enters the trigger collider
    private void OnTriggerEnter(Collider other)
    {
        // Check if the player is stepping on the object tagged as "Finish"
        if (other.CompareTag("Finish"))
        {
            // Load the next scene (you can replace this with a specific scene name or index)
            LoadNextLevel();
        }
    }

    void LoadNextLevel()
    {
        // You can load the next scene based on the build index
        int nextSceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings) // Ensure the next level exists
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("No more levels to load!");
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }
}