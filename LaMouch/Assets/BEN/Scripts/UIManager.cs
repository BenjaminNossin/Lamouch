using UnityEngine;
using System.Collections; 

public class UIManager : MonoBehaviour
{
    private GameObject arena;

    void Start()
    {     
        Debug.Log("PLACEMENT INDICATOR IS SET");
    }

    private void Update()
    {
#if !UNITY_EDITOR

        if (!TryGetTouchPosition(out Vector2 touchPosition))
        {
            return;
        }

        if (Input.GetTouch(0).phase == TouchPhase.Began)
        {
            ActivateArena(); 
        }
#else
        if (Input.GetKeyDown(KeyCode.C))
        {
            ActivateArena();
        }
#endif
    }

    public void ActivateArena()
    {
        // decide how much pillars to activate based on difficulty settings
        Instantiate(Resources.Load<GameObject>("Arena"), SpawnPoint.Transform.position, Quaternion.identity);
        StartCoroutine(SetArenaBool()); 

        Destroy(gameObject, 0.2f);
        Object self = gameObject;
        self = null;
    }

    IEnumerator SetArenaBool()
    {
        yield return new WaitForFixedUpdate(); 
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
}
