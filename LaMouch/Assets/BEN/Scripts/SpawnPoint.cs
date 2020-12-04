using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public static Transform Transform; 

    void Update()
    {
        Transform = transform; 
    }
}
