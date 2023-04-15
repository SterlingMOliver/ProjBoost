using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour

{
    AudioSource audioSource;
     void OnCollisionEnter(Collision other) 
     
    {
       switch (other.gameObject.tag)
       {
        case "Friendly":
            Debug.Log("You're touching a Friendly object!");
            break;
        case "Fuel":
            Debug.Log("You're touching Fuel!");
            break;
        case "Finish":
            EndLevel();
            Debug.Log("You've reached the end!");
            break;
        default:
            StartCrashSequence();
            break;
       }     
    }
[SerializeField] float reloadTime = 1f;
[SerializeField] float nextLevelTime = 1f;
    void StartCrashSequence()
    {
        audioSource = GetComponent<AudioSource>();
        GetComponent<Movement>().enabled = false;
        audioSource.Stop();
        Invoke("ReloadLevel", reloadTime);
    }

    void EndLevel()
    {
        audioSource = GetComponent<AudioSource>();
        GetComponent<Movement>().enabled = false;
        audioSource.Stop();
        Invoke("LoadNextLevel", nextLevelTime);
    }
    void ReloadLevel() 
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
