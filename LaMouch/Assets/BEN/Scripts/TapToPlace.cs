using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

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
        }
#else
        if (Input.GetTouch(0).phase == TouchPhase.Began) // m_aRRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon) && 
        {
            weaponBehaviour.Attack(); 
        }
#endif

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
