using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class CloudAnchorManager : MonoBehaviour
{
    [SerializeField] private GameObject spawnGO;
    private ARRaycastManager aRRaycastManager;
    private ARPlaneManager planeManager;
    private ARAnchorManager anchorManager;

    private Vector2 touchPosition;
    private GameObject instanceGO;

    private ARAnchor _anchor = null;

    private List<ARRaycastHit> hits = new List<ARRaycastHit>();


    private void Awake()
    {
        aRRaycastManager = GetComponent<ARRaycastManager>();
    }

    bool tryGetTouchPos(out Vector2 touchPosition)
    {
        if(Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }
        else
        {
            touchPosition = default;
            return false;
        }
    }

    void Update()
    {
        if(!tryGetTouchPos(out touchPosition))
        {
            return;
        }
        if(aRRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPos = hits[0].pose;
            ARPlane plane = planeManager.GetPlane(hits[0].trackableId);

            _anchor = anchorManager.AttachAnchor(plane, hitPos);

            if(_anchor != null)
                instanceGO = Instantiate(spawnGO, _anchor.transform);
        }
    }
}
