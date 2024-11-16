using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    
    private bool isPaused = false;
    public GameObject abs;
    private List<AudioSource> playingSources = new List<AudioSource>();

    public void Update()
    {
        // Check if ESC key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
    
    public void PauseGame()
    {
        // Get all audio sources in "abs" and pause them if they are playing
        AudioSource[] backgroundAudioSources = abs.GetComponents<AudioSource>();
        playingSources.Clear();  // Clear the list before adding currently playing sources

        foreach (AudioSource source in backgroundAudioSources)
        {
            if (source.isPlaying)
            {
                playingSources.Add(source);  // Add the playing source to the list
                source.Pause();
            }
        }

        pauseMenu.SetActive(true);
        isPaused = true;
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;

        // Unpause all sources that were playing before pausing
        foreach (AudioSource source in playingSources)
        {
            source.UnPause();
        }
        
        playingSources.Clear();  // Clear the list after unpausing
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(0);
    }
}