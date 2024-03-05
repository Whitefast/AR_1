using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


public class touchinput : MonoBehaviour
{
    [SerializeField] private TMP_Text debugtext;
    [SerializeField] private GameObject ballprefab;
    [SerializeField] private ARCameraManager arcamera;
    private ARRaycastManager arrcm;

    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    TrackableType trackableTypes = TrackableType.PlaneWithinPolygon;

    private void Strat()
    {
        arrcm = GetComponent<ARRaycastManager>();
    }

    public void singleTap(InputAction.CallbackContext ctx)
    {
        if (ctx.phase == InputActionPhase.Performed)
        {
            var touchPos = ctx.ReadValue<Vector2>();
            debugtext.text = touchPos.ToString();


            if(arrcm.Raycast(touchPos,hits,trackableTypes))
            {
                var ball = Instantiate(ballprefab, hits[0].pose.position, new Quaternion());
            }
        }
    }

    public void DoubleTap(InputAction.CallbackContext ctx)
    {
        if (ctx.phase == InputActionPhase.Performed)
        {
            debugtext.text = "Change Camera";

            if (arcamera.currentFacingDirection == CameraFacingDirection.World)
            {
                GetComponent<ARRaycastManager>().enabled = false;
                GetComponent<ARPlaneManager>().enabled = false;
                GetComponent<ARFaceManager>().enabled = true;
                arcamera.requestedFacingDirection = CameraFacingDirection.User;

            }
            else
            {
                GetComponent<ARRaycastManager>().enabled = true;
                GetComponent<ARPlaneManager>().enabled = true;
                GetComponent<ARFaceManager>().enabled = false;
                arcamera.requestedFacingDirection = CameraFacingDirection.World;
            }
        }
    }

}
