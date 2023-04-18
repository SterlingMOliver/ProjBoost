using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour

{
    [SerializeField] AudioClip winLevel;
    [SerializeField] AudioClip loseLevel;

    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    AudioSource audioSource;
    Rigidbody rb;
    bool triggersDisabled = false;
    bool isTransitioning = false; 
    void Start() 
     {
         audioSource = GetComponent<AudioSource>();
     }
     void Update()
    {
        RespondToDebugKeys();

    }

    void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            triggersDisabled = !triggersDisabled;
            Debug.Log("Triggers disabled = " + triggersDisabled);
        }
    }

    void OnCollisionEnter(Collision other) 
    {
        if (isTransitioning || triggersDisabled) { return; }


       switch (other.gameObject.tag)
       {
        case "Friendly":
            Debug.Log("You're touching a Friendly object!");
            break;
        case "Finish":
            WinLevel();
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
        crashParticles.Play();
        isTransitioning = true;
        GetComponent<Movement>().enabled = false;
        audioSource.Stop();
        audioSource.PlayOneShot(loseLevel);
        Invoke("ReloadLevel", reloadTime);
    }

    void WinLevel()
    {
        successParticles.Play();
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

    //Debug keys
    }
    


    }
