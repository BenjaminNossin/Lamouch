using UnityEngine;
using System.Collections; 

public class UIManager : MonoBehaviour
{
    private GameObject pillar;
    public static bool alive; 

    void Start()
    {
        
        pillar = Resources.Load<GameObject>("Pillar");
        Debug.Log("PLACEMENT INDICATOR IS SET");
        StartCoroutine(SetAliveState()); 
    }

    private void Update()
    {
#if !UNITY_EDITOR

        if (!TryGetTouchPosition(out Vector2 touchPosition))
        {
            return;
        }

        if (Input.GetTouch(0).phase == TouchPhase.Began && alive)
        {
            ActivatePillar(); 
        }
#else
        if (Input.GetKeyDown(KeyCode.C))
        {
            ActivatePillar();
        }
#endif
    }

    public void ActivatePillar()
    {
        // decide how much pillars to activate based on difficulty settings
        Instantiate(pillar, transform.position + new Vector3(0f, 1.35f, 0f), Quaternion.Euler(-90f, 0f, 0f));
        LevelManager.PillarCount++;
        Destroy(gameObject, 0.1f);
        Object self = gameObject;
        self = null;
    }

    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }

        touchPosition = default;
        return false;
    }

    IEnumerator SetAliveState()
    {
        yield return new WaitForSeconds(0.5f);
        alive = true; 
    }

    private void OnDestroy()
    {
        alive = false; 
    }
}
