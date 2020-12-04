using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField, Range(5f, 15f)] private float speed = 10f;
    [SerializeField, Range(0.1f, 2f)] private float destroyDelay = 1f;

    public static Action OnHittingPillar;
    public static Action OnKillingFly; 

    private void Start()
    {
        Destroy(gameObject, destroyDelay);
    }

    void FixedUpdate()
    {
        transform.Translate(Vector3.forward.normalized * Time.fixedDeltaTime * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.transform.parent.gameObject);
            Destroy(gameObject, 0.05f);

            OnKillingFly(); 
            Debug.Log("killing fly");
        }
        else if (other.CompareTag("Pillar"))
        { 
            Destroy(gameObject, 0.05f);

            OnHittingPillar(); 
            Debug.Log("detecting pillar"); 
        }
    }
}
