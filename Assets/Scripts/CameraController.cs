using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float lookSensitivity;
    [SerializeField] private float smoothing;

    private GameObject player;
    private Vector2 smoothedVeloc;
    private Vector2 currentlookpos;

    private void Start()
    {
        player = transform.parent.gameObject;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        RotateCamera();
    }

    private void RotateCamera()
    {
        Vector2 inputVal = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        inputVal = Vector2.Scale(inputVal, new Vector2(lookSensitivity * smoothing, lookSensitivity * smoothing));
        
        smoothedVeloc.x = Mathf.Lerp(smoothedVeloc.x, inputVal.x, 1f / smoothing);
        smoothedVeloc.y = Mathf.Lerp(smoothedVeloc.y, inputVal.y, 1f / smoothing);

        currentlookpos += smoothedVeloc;

        transform.localRotation = Quaternion.AngleAxis(-currentlookpos.y, Vector3.right);
        player.transform.localRotation = Quaternion.AngleAxis(currentlookpos.x, player.transform.up);
    }

}
