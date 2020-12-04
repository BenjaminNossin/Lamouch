using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems; 

public class LevelManager : MonoBehaviour
{
    [Header("Spawning")]
    [SerializeField] private GameObject entityToSpawn;
    [SerializeField, Range(0f, 0.2f)] private float spawnProbability = 0.6f;
    [SerializeField, Range(3f, 20f)] private float spawnCooldown = 10f;
    [SerializeField] private List<Transform> spawnPoints_Low = new List<Transform>();
    [SerializeField] private List<Transform> spawnPoints_Middle = new List<Transform>();
    [SerializeField] private List<Transform> spawnPoints_High = new List<Transform>();
    private bool spawn = true; 
    private bool gameStarted; 

    // audio
    public AudioMixer gameplayAudioMixer; 
    public static AudioMixer GameplayAudioMixer;

    // need reference of spawned object ? 

    /* [Header("Image Tracking")] 
    public RuntimeReferenceImageLibrary library;  
    private XRImageTrackingSubsystem subsystem; */

    private void Start()
    {
        GameplayAudioMixer = gameplayAudioMixer;
        StartCoroutine(SetInitialSpawn());
        /* subsystem.imageLibrary = library;
        subsystem.Start(); */
    }

    void Update() 
    {
        if (spawn && gameStarted)
        {
            for (int i = 0; i < spawnPoints_Low.Count; i++)
            {
                float selector_low = Random.Range(0f, 1f);
                float selector_medium = Random.Range(0f, 1f);
                float selector_high = Random.Range(0f, 1f);

                // low
                if (selector_low <= spawnProbability)
                {
                    Instantiate(entityToSpawn, spawnPoints_Low[i].position, Quaternion.Euler(0f, spawnPoints_Low[i].rotation.eulerAngles.y - 180f, 0f)); 
                }

                // medium
                if (selector_medium <= spawnProbability)
                {
                    Instantiate(entityToSpawn, spawnPoints_Middle[i].position, Quaternion.Euler(0f, spawnPoints_Middle[i].rotation.eulerAngles.y - 180f, 0f));
                }

                // high
                if (selector_high <= spawnProbability)
                {
                    Instantiate(entityToSpawn, spawnPoints_High[i].position, Quaternion.Euler(0f, spawnPoints_High[i].rotation.eulerAngles.y - 180f, 0f));
                }
            }

            StartCoroutine(SpawnCooldown());
        }
    }

    private IEnumerator SetInitialSpawn()
    {
        yield return new WaitForSeconds(3f);
        gameStarted = true; 
    }

    private IEnumerator SpawnCooldown()
    {
        spawn = false; 

        yield return new WaitForSeconds(spawnCooldown);
        spawn = true; 
    }
}
