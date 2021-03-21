using UnityEngine;
using UnityEngine.SceneManagement;


public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float nextLeveldelay = 1f;
    [SerializeField] float reloadLeveldelay = 1f;
    [SerializeField] AudioClip ExplosionSound;
    [SerializeField] AudioClip SuccessSound;
    [SerializeField] ParticleSystem ExplosionParticles;
    [SerializeField] ParticleSystem SuccessParticles;

    AudioSource audioSource;

    bool isTransitioning = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        RespondToDegugKeys();
    }

    void RespondToDegugKeys()
    {
        if (Input.GetKey("l"))
        {
            LoadNextLevel();
        }
        if (Input.GetKey("c"))
        {
            isTransitioning = !isTransitioning;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning) { return; }

        switch (other.gameObject.tag) 
        {
            case "Finish":
                StartSuccessSequence();
                break;
            case "Friendly":
                Debug.Log("Colide with Friendley");
                break;
            case "Fuel":
                Debug.Log("Picked up Fuel");
                break;
            default:
                StartCrashSequence();
                break;
        }

    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        ExplosionParticles.Play();
        audioSource.Stop();
        audioSource.PlayOneShot(ExplosionSound);
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", reloadLeveldelay);
    }
    
    void StartSuccessSequence()
    {
        isTransitioning = true;
        SuccessParticles.Play();
        audioSource.Stop();
        audioSource.PlayOneShot(SuccessSound);
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", nextLeveldelay);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        currentSceneIndex++;
        if (currentSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            currentSceneIndex = 0;
        }
        SceneManager.LoadScene(currentSceneIndex);
    }
}
