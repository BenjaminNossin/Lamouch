using System;
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject VFX; 

    [SerializeField, Range(5f, 15f)] private float speed = 10f;
    [SerializeField, Range(0.1f, 2f)] private float destroyDelay = 1f;

    public static Action OnHittingPillar;
    public static Action OnKillingFly; 

    private void Start()
    {
        VFX.SetActive(false); 
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
            Destroy(other.transform.parent.gameObject, 0.5f);
            Destroy(gameObject, 0.05f);

            OnKillingFly(); 
            Debug.Log("killing fly");
        }
        else if (other.CompareTag("Pillar"))
        { 
            Destroy(gameObject, 0.25f);

            VFX.SetActive(true);
            transform.DetachChildren();
            Destroy(VFX, 0.5f);

            OnHittingPillar(); 
            Debug.Log("detecting pillar"); 
        }
    }
}
