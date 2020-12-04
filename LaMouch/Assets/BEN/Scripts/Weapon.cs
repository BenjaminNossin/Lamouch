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

    public Sound shootSound; 

    [Header("-- DEBUG --")]
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject nullIndicator;

    private void Start()
    {
        shootSound.source.outputAudioMixerGroup = shootSound.group;
    }

    private void FixedUpdate()
    {
        CastRay(); 
    }

    public void Attack()
    {
        if (canShoot)
        {
            Instantiate(bullet, firepoint.position, firepoint.rotation);

            shootSound.source.PlayOneShot(shootSound.clip);

            StartCoroutine(ModifyWeaponRotation()); 
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

    IEnumerator ModifyWeaponRotation()
    {
        yield return new WaitForFixedUpdate(); 

        // play animation instead
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,
                                              transform.rotation.eulerAngles.y,
                                              transform.rotation.eulerAngles.z + 23f);

        yield return new WaitForSeconds(0.1f);
        // animation placeholder
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,
                                      transform.rotation.eulerAngles.y,
                                      transform.rotation.eulerAngles.z - 23f);
    }

    IEnumerator Cooldown()
    {
        canShoot = false; 

        yield return new WaitForSeconds(delay);
        canShoot = true; 
    }
}
