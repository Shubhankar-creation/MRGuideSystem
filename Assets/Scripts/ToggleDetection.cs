using UnityEngine;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARPlaneManager))]
public class ToggleDetection : MonoBehaviour
{
    ARPlaneManager planeManager;
    private void Awake()
    {
        planeManager = GetComponent<ARPlaneManager>();
    }

    public void planeDetectionToggle()
    {
        planeManager.enabled = !planeManager.enabled;

        if(planeManager.enabled)
        {
            changePlaneState(true);
        }
        else
        {
            changePlaneState(false);
        }
    }

    void changePlaneState(bool val)
    {
        foreach( var plane in planeManager.trackables)
        {
            plane.gameObject.SetActive(val);
        }
    }
}
