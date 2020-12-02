using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField, Range(0.5f, 5f)] private float speed = 2f;
    [SerializeField, Range(0.25f, 5f)] private float destroyDelay = 2f;

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
            Destroy(gameObject, 0.1f); 
        }
    }
}
