using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using TMPro;
public class face : MonoBehaviour
{
    [SerializeField] private ARCameraManager arcamera;
    [SerializeField] private ARFaceManager arface;
    [SerializeField] private TMP_Text debugtext;


    private void OnEnable() => arface.facesChanged += onFaceChanged;
    private void OnDisable() => arface.facesChanged -= onFaceChanged;
    private List<ARFace> faces = new List<ARFace>();

    private void onFaceChanged(ARFacesChangedEventArgs eventArgs)
    {
        foreach (var newFace in eventArgs.added)
        {
            faces.Add(newFace);
        }

        foreach (var lostFace in eventArgs.removed)
        {
            faces.Remove(lostFace);
        }
    }

    void Update()
    {
        if(arcamera.currentFacingDirection == CameraFacingDirection.User)
        {
            if (faces.Count > 0)
            {
                Vector3 loverlippos = faces[0].vertices[14];
                debugtext.text = loverlippos.ToString("F3");
            }

        }
            
    }
}
