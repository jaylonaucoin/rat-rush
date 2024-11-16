using UnityEngine;
using UnityEngine.SceneManagement;


public class Death : MonoBehaviour
{
    public GameObject deathPanel;
    public AudioSource deathSound;
    public GameObject abs;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Show the death panel
            deathPanel.SetActive(true);
            
            AudioSource[] backgroundAudioSources = abs.GetComponents<AudioSource>();
            foreach (AudioSource source in backgroundAudioSources)
            {
                source.Stop();
            }
            
            deathSound.Play();

            // Reload the scene after a delay
            Invoke("ReloadScene", 2f);
        }
    }

    public void die()
    {
        // Show the death panel
        deathPanel.SetActive(true);
            
        AudioSource[] backgroundAudioSources = abs.GetComponents<AudioSource>();
        foreach (AudioSource source in backgroundAudioSources)
        {
            source.Stop();
        }
            
        deathSound.Play();

        // Reload the scene after a delay
        Invoke("ReloadScene", 2f);
    }

    void ReloadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
}