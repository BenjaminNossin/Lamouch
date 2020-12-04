using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Audio;
using System;

public enum Difficulty { Easy, Medium, Hard }

public class LevelManager : MonoBehaviour
{
    [Header("Spawning")]
    [SerializeField] private GameObject entityToSpawn;
    [SerializeField, Range(0f, 0.2f)] private float spawnProbability = 0.08f;
    [SerializeField, Range(3f, 20f)] private float spawnCooldown = 10f;
    [SerializeField] private List<Transform> spawnPoints_Low = new List<Transform>();
    [SerializeField] private List<Transform> spawnPoints_Middle = new List<Transform>();
    [SerializeField] private List<Transform> spawnPoints_High = new List<Transform>();
    private bool spawn = true; 
    private bool gameStarted; 

    // audio
    public AudioMixer gameplayAudioMixer; 
    public static AudioMixer GameplayAudioMixer;
    public Sound spawnSound;

    // need reference of spawned object ? 

    /* [Header("Image Tracking")] 
    public RuntimeReferenceImageLibrary library;  
    private XRImageTrackingSubsystem subsystem; */

    private Difficulty gameplayDifficulty;
    public static Action<int> OnReachingNewDifficulty; 

    private void Start()
    {
        gameplayDifficulty = Difficulty.Easy;
        GameplayAudioMixer = gameplayAudioMixer;
        spawnSound.source.outputAudioMixerGroup = spawnSound.group;
        StartCoroutine(SetInitialSpawn());
    }

    void Update() 
    {

        if (Time.time >= 15f && gameplayDifficulty == Difficulty.Easy)
        {
            gameplayDifficulty = Difficulty.Medium; 
            spawnProbability = 0.125f;
            OnReachingNewDifficulty(3); 
        }

        if (Time.time >= 25f && gameplayDifficulty == Difficulty.Medium)
        {
            gameplayDifficulty = Difficulty.Hard;
            spawnProbability = 0.2f;
            OnReachingNewDifficulty(10);
        }

        if (spawn && gameStarted && Time.time >= 5f)
        {
            for (int i = 0; i < spawnPoints_Low.Count; i++)
            {
                float selector_low = UnityEngine.Random.Range(0f, 1f);
                float selector_medium = UnityEngine.Random.Range(0f, 1f);
                float selector_high = UnityEngine.Random.Range(0f, 1f);

                // low
                if (selector_low <= spawnProbability)
                {
                    spawnSound.source.PlayOneShot(spawnSound.clip); 
                    Instantiate(entityToSpawn, spawnPoints_Low[i].position, Quaternion.Euler(0f, spawnPoints_Low[i].rotation.eulerAngles.y - 180f, 0f)); 
                }

                // medium
                if (selector_medium <= spawnProbability)
                {
                    spawnSound.source.PlayOneShot(spawnSound.clip);
                    Instantiate(entityToSpawn, spawnPoints_Middle[i].position, Quaternion.Euler(0f, spawnPoints_Middle[i].rotation.eulerAngles.y - 180f, 0f));
                }

                // high
                if (selector_high <= spawnProbability)
                {
                    spawnSound.source.PlayOneShot(spawnSound.clip);
                    Instantiate(entityToSpawn, spawnPoints_High[i].position, Quaternion.Euler(0f, spawnPoints_High[i].rotation.eulerAngles.y - 180f, 0f));
                }
            }

            StartCoroutine(SpawnCooldown());
        }
    }

    private IEnumerator SetInitialSpawn()
    {
        yield return new WaitForSeconds(5f);
        Instantiate(entityToSpawn, spawnPoints_Low[0].position, Quaternion.Euler(0f, spawnPoints_Low[0].rotation.eulerAngles.y - 180f, 0f));
        gameStarted = true;
    }

    private IEnumerator SpawnCooldown()
    {
        spawn = false; 

        yield return new WaitForSeconds(spawnCooldown);
        spawn = true; 
    }
}
