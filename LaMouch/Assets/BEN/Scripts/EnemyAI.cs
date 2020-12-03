using UnityEngine;
using System.Collections;
using System.Collections.Generic; 

public enum State { None, Idle, Charging, Rush }

// run around pillars for x seconds before rushing to player with x speed
public class EnemyAI : MonoBehaviour
{
    [Range(1, 40)] public byte rotationSpeed = 20;
    [Range(1, 10)] public byte attackSpeed = 2;
    [Range(1, 5)] public byte chargingStateMinDelay = 5;
    [Range(2, 15)] public byte chargingStateMaxDelay = 10;
    public LayerMask playerLayer;
    public LayerMask obstacleLayer;

    public State AIState;
    private float attackDelay;
    private float attackTimer; 

    private Transform pillar;
    private Transform target;

    private bool rush;
    private bool newRotationIsSet;

    public Sound flyingSound; 

    // run around
    // rush speed 

    private void Start()
    {
        pillar = GameObject.Find("Cursed_Pilier").transform; // :D
        target = GameObject.Find("AR Camera").transform; // :D

        attackDelay = Random.Range(chargingStateMinDelay, chargingStateMaxDelay);

        flyingSound.source.outputAudioMixerGroup = flyingSound.group;
        flyingSound.source.PlayOneShot(flyingSound.clip);
        

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (AIState != State.Rush)
            IdleState();
        else
            AttackState(); 
    }

    private void IdleState()
    {
        if (AIState != State.Charging)
        {
            AIState = State.Idle;
            attackTimer += Time.fixedDeltaTime;
        }

        transform.RotateAround(pillar.position, Vector3.up, rotationSpeed * Time.fixedDeltaTime);
        transform.Rotate(Vector3.up, -2.5f * Time.fixedDeltaTime);

        if (!Physics.Raycast(transform.position, target.position - transform.position, 30f, obstacleLayer))
            Debug.DrawRay(transform.position, target.position - transform.position, Color.green);
        else
            Debug.DrawRay(transform.position, target.position - transform.position, Color.red);


        if (attackTimer >= attackDelay)
        {
            AIState = State.Charging;

            if (Physics.Raycast(transform.position, target.position - transform.position, 30f, playerLayer)
                && !Physics.Raycast(transform.position, target.position - transform.position, 30f, obstacleLayer))
            {
                StartCoroutine(TransitionToRushState());               
            }
        }
    }

    private IEnumerator TransitionToRushState()
    {
        yield return new WaitForSeconds(0.5f); 
        AIState = State.Rush;
        StartCoroutine(SetAttackMovement()); 
    }

    private IEnumerator SetAttackMovement()
    {
        yield return new WaitForSeconds(1f);
        rush = true;
        LevelManager.GameplayAudioMixer.SetFloat("Fly_Move_Pitch", 2f); 
    }

    private void AttackState()
    {
        Debug.DrawRay(transform.position, target.position - transform.position, Color.blue);

        if (!newRotationIsSet)
        {
            newRotationIsSet = true;
            transform.LookAt(target.position - transform.position);
            transform.rotation = Quaternion.Euler(-transform.rotation.eulerAngles.x,
                                                  transform.rotation.eulerAngles.y - 180f, 
                                                  transform.rotation.eulerAngles.z);
        }

        if (rush)
            transform.Translate(Vector3.back.normalized * Time.fixedDeltaTime * attackSpeed, Space.Self);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            Destroy(gameObject); // placeholder
            Debug.Log("-- HIT --"); 
            // NEED FEEDBACK PASS
        }
    }
}
