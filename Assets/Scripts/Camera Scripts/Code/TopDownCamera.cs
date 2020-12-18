using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCamera : MonoBehaviour
{
    #region Variables

    public Transform target;
    public float height = 10f;
    public float distance = 20f;
    public float angle = 45f;
    public float smoothSpeed = 0.5f;

    private Vector3 refVelocity;

    #endregion Variables

    #region Main Methods

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        HandleCamera();
    }

    #endregion Main Methods

    #region Helper Methods

    protected virtual void HandleCamera()
    {
        if (!target)
        {
            return;
        }

        //Build world postion vector
        Vector3 worldPostion = (Vector3.forward * -distance) + (Vector3.up * height);
        //Debug.DrawLine(target.position, worldPostion, Color.blue);

        //build rotated vector
        Vector3 rotatedVector = Quaternion.AngleAxis(angle, Vector3.up) * worldPostion;
        //Debug.DrawLine(target.position, rotatedVector, Color.green);

        //move our piostion
        Vector3 flatTargetPostion = target.position;
        flatTargetPostion.y = 0f;
        Vector3 finalPostion = flatTargetPostion + rotatedVector;
        //Debug.DrawLine(target.position, finalPostion, Color.black);

        transform.position = Vector3.SmoothDamp(transform.position, finalPostion, ref refVelocity, smoothSpeed);
        //transform.position = finalPostion;
        transform.LookAt(flatTargetPostion);
    }

    private void OnDrawGizmos()
    {
        if (target)
        {
            Gizmos.DrawLine(transform.position, target.position);
            Gizmos.DrawSphere(target.position, 1.5f);
        }
        Gizmos.DrawSphere(transform.position, 1.5f);
    }

    #endregion Helper Methods
}