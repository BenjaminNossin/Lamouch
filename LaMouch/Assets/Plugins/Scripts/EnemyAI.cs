using UnityEngine;

public enum State { None, Idle, Rush }

// run around pillars for x seconds before rushing to player with x speed
public class EnemyAI : MonoBehaviour
{
    [Range(1f, 40)] public byte speed = 20; 

    // run around
    // rush speed
    public State AIState;
    public Transform pillar; 

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.RotateAround(pillar.position, Vector3.up, speed * Time.fixedDeltaTime); 
    }
}
