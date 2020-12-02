using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform firepoint;
    [SerializeField, Range(0f, 1f)] private float delay = 0.75f;

    private bool canShoot = true; 

    public void Attack()
    {
        if (canShoot)
        {
            Instantiate(bullet, firepoint.position, firepoint.rotation);
            StartCoroutine(Cooldown()); 
        }
    }

    IEnumerator Cooldown()
    {
        canShoot = false; 

        yield return new WaitForSeconds(delay);
        canShoot = true; 
    }
}
