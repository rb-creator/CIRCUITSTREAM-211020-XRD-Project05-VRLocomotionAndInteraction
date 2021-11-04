using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRLocomotion : MonoBehaviour
{
    public Transform XRRig;
    public Transform head;
    
    public bool canSmoothMove = false;
    public float moveSpeed = 20;

    public bool canSmoothRotate = false;
    public float rotateSpeed = 20;

    public bool canTeleport = false;
    public Transform teleportingHand;
    public LineRenderer line;

    private string verticalAxis;
    private string horizontalAxis;
    private string triggerButton;


    private void Awake()
    {
        verticalAxis = "XRI_Left_Primary2DAxis_Vertical";
        horizontalAxis = "XRI_Left_Primary2DAxis_Horizontal";
        triggerButton = "XRI_Left_TriggerButton";

    }



    void Start()
    {
        
    }

    void Update()
    {
        var verticalValue = Input.GetAxis(verticalAxis);
        var horizontalValue = Input.GetAxis(horizontalAxis);


        if (canSmoothMove)
            SmoothMove(verticalValue);

        if (canSmoothRotate)
            SmoothRotate(horizontalValue);

        if (canTeleport)
            Teleport();
    }

    void SmoothMove(float axisValue)
    {
        Vector3 lookDirection = new Vector3(head.forward.x, 0, head.forward.z);
        lookDirection.Normalize();

        XRRig.position += Time.deltaTime * lookDirection * axisValue * moveSpeed * -1;
    }

    void SmoothRotate(float axisValue)
    {
        XRRig.Rotate(Vector3.up, rotateSpeed * Time.deltaTime * axisValue);

    }

    void Teleport()
    {
        Ray ray = new Ray(teleportingHand.position, teleportingHand.forward);

        if(Physics.Raycast(ray, out RaycastHit hit))
        {
            line.enabled = true;

            line.SetPosition(0, teleportingHand.position);
            line.SetPosition(1, hit.point);

            if (Input.GetButtonDown(triggerButton))
            {
                XRRig.position = hit.point;
            }
        }
        else
        {
            line.enabled = false;
        }

    }

}
