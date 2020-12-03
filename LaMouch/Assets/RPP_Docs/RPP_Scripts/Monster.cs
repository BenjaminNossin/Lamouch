using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] Transform monsterObject;
    [SerializeField] List <Transform> hidingSpots;
    bool isWaiting, canBeKilled;
    float hidingCooldown, timeBeforeAttack;
    int nextHidingSpot;   
    
    void Start()
    {
        monsterObject = this.transform;
        isWaiting = true;
        canBeKilled = false;
    }

   
    void Update()
    {
        StartCoroutine(Attaque());
        if (isWaiting)
        {
            isWaiting = false;
            StartCoroutine(ChangeHiding());
        }    
    }

    IEnumerator ChangeHiding()
    {
        nextHidingSpot = Random.Range(0, hidingSpots.Count); 
        monsterObject.position = hidingSpots[nextHidingSpot].position;
        hidingCooldown = Random.Range(3f, 8f);
        yield return new WaitForSeconds(hidingCooldown);
        isWaiting = true;
    }

    IEnumerator Attaque()
    {
        timeBeforeAttack = Random.Range(60f, 90f);
        yield return new WaitForSeconds(timeBeforeAttack);
        //Attack the player
        canBeKilled = true;
    }
}
