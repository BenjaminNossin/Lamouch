using UnityEngine;

public enum State { None, Idle, Rush }

// run around pillars for x seconds before rushing to player with x speed
public class EnemyAI : MonoBehaviour
{
    [Range(1f, 40)] public byte speed = 20;
    public State AIState;

    private Transform pillar;
    // run around
    // rush speed

    private void Start()
    {
        pillar = GameObject.Find("Cursed_Pilier").transform; // :D
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.RotateAround(pillar.position, Vector3.up, speed * Time.fixedDeltaTime);
        AIState = State.Idle;

        transform.Rotate(Vector3.up, -2.5f * Time.fixedDeltaTime); 
    }
}
