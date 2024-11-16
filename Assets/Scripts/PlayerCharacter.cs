using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class PlayerCharacter : MonoBehaviour
{
    private int maxHealth = 3;
    private int currentHealth;
    public Image healthBar;
    public GameObject abs;
    private Death deathScript;

    public void Start()
    {
        abs = GameObject.Find("Abstract");
        deathScript = abs.GetComponent<Death>();
        currentHealth = maxHealth;
    }
    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        float healthPercent = (float)currentHealth / maxHealth;
        healthBar.fillAmount = healthPercent;

        if (currentHealth <= 1)
        {
            deathScript.die();
        }
    }

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
