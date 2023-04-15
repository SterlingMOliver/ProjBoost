using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour

{
    [SerializeField] AudioClip winLevel;
    [SerializeField] AudioClip loseLevel;
    AudioSource audioSource;
    Rigidbody rb;

    bool isTransitioning = false; 
    void Start() 
     {
         audioSource = GetComponent<AudioSource>();
     }
     void OnCollisionEnter(Collision other) 
    {
        if (isTransitioning) { return; }


       switch (other.gameObject.tag)
       {
        case "Friendly":
            Debug.Log("You're touching a Friendly object!");
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
        //TODO:
        //add Crash particles
        isTransitioning = true;
        GetComponent<Movement>().enabled = false;
        audioSource.Stop();
        audioSource.PlayOneShot(loseLevel);
        Invoke("ReloadLevel", reloadTime);
    }

    void EndLevel()
    {
        //TODO:
        //add Success particles
        isTransitioning = true;
        GetComponent<Movement>().enabled = false;
        audioSource.Stop();
        audioSource.PlayOneShot(winLevel);
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
