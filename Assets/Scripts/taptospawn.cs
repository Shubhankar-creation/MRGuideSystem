using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class taptospawn : MonoBehaviour
{
    [SerializeField] private GameObject spawnGO;
    private ARRaycastManager aRRaycastManager;

    private Vector2 touchPosition;
    private GameObject instanceGO;

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
            instanceGO = Instantiate(spawnGO, new Vector3(hitPos.position.x, hitPos.position.y, hitPos.position.z), hitPos.rotation);
        }
    }

}
