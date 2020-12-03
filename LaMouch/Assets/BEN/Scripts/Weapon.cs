using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform firepoint;
    [SerializeField] private Transform firepointEnd;
    [SerializeField, Range(0f, 1f)] private float delay = 0.75f;
    [SerializeField, Range(2f, 5f)] private float rayDistance = 3.5f;
    [SerializeField] LayerMask obstacleMask;
    [SerializeField] LayerMask enemyMask; 

    private bool canShoot = true;
    private bool flyDetected;
    private bool obstacleDetected; 
    private readonly int 
        enemyMaskindex = 10,
        obstacleMaskIndex = 11; 

    [Header("-- DEBUG --")]
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject nullIndicator;

    private void FixedUpdate()
    {
        CastRay(); 
    }


    public void Attack()
    {
        if (canShoot)
        {
            Instantiate(bullet, firepoint.position, firepoint.rotation);
            StartCoroutine(Cooldown()); 
        }
    }

    public void CastRay()
    {
        flyDetected = Physics.Linecast(firepoint.position, firepointEnd.position, enemyMask); 
        obstacleDetected = Physics.Linecast(firepoint.position, firepointEnd.position, obstacleMask);

        Debug.DrawLine(firepoint.position, firepointEnd.position, Color.red); 

        if (flyDetected && !obstacleDetected)
        {
            target.transform.localPosition = firepointEnd.localPosition - new Vector3(0.8f, 0f, 0f);
            target.SetActive(true);
            nullIndicator.SetActive(false);
        }
        else if (obstacleDetected)
        {
            nullIndicator.transform.localPosition = firepointEnd.localPosition - new Vector3(3f, 0f, 0f); 
            target.SetActive(false);
            nullIndicator.SetActive(true);
        }
        else
        {
            target.SetActive(false);
            nullIndicator.SetActive(false);
        }

    }

    IEnumerator Cooldown()
    {
        canShoot = false; 

        yield return new WaitForSeconds(delay);
        canShoot = true; 
    }
}
