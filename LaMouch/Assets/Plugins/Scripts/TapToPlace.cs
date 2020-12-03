using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;
using System.Collections;

[RequireComponent(typeof(ARRaycastManager))]
public class TapToPlace : MonoBehaviour
{
    public Weapon weaponBehaviour; 
    public GameObject placementIndicator;
    public GameObject weapon;

    private ARRaycastManager m_aRRaycastManager;
    private Pose PlacementPose;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private bool placementPoseIsValid = false;

    private void Awake()
    {
        m_aRRaycastManager = GetComponent<ARRaycastManager>(); 
    }

    void Update()
    {

#if !UNITY_EDITOR
        UpdatePlacementPose();
        UpdatePlacementIndicator(); 

        if (!TryGetTouchPosition(out Vector2 touchPosition))
        {
            return;
        }
#endif

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Space))
        {
            weaponBehaviour.Attack();

            weapon.transform.localScale = new Vector3(2f, 2f, 2f);
            StartCoroutine(ResetWeaponScale());
        }
#else
        if (Input.GetTouch(0).phase == TouchPhase.Began) // m_aRRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon) && 
        {
            weaponBehaviour.Attack(); 

            weapon.transform.localScale = new Vector3(2f, 2f, 2f);
            StartCoroutine(ResetWeaponScale()); 
        }
#endif

    }

    IEnumerator ResetWeaponScale()
    {
        yield return new WaitForSeconds(1f);
        weapon.transform.localScale = Vector3.one; 
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

    private void UpdatePlacementPose()
    {
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        m_aRRaycastManager.Raycast(screenCenter, hits, TrackableType.Planes);

        placementPoseIsValid = hits.Count > 0;
        if (placementPoseIsValid)
        {
            PlacementPose = hits[0].pose;

            var cameraForward = Camera.current.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 0f, cameraForward.z).normalized;
            Debug.Log($"camera current is : {Camera.current.name}");
            Debug.Log($"camera current is : {cameraBearing}");
            PlacementPose.rotation = Quaternion.LookRotation(cameraBearing);
        }
    }

    private void UpdatePlacementIndicator()
    {
        if (placementPoseIsValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(PlacementPose.position, PlacementPose.rotation);
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }
}
