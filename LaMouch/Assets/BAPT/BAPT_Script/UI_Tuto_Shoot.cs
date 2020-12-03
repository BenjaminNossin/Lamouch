using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Tuto_Shoot : MonoBehaviour
{
    public GameObject TouchScreenSprite;
    public GameObject TextShoot;

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(DesactivateStrite());
    }

    IEnumerator DesactivateStrite()
    {
        yield return new WaitForSeconds(3f);
        TouchScreenSprite.SetActive(false);
        TextShoot.SetActive(false);
    }
}
