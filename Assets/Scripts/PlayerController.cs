using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    private Rigidbody myRigitBody;

    private Vector3 moveInput;
    private Vector3 moveVelocity;

    private Camera mainCamera;

    // Start is called before the first frame update
    private void Start()
    {
        myRigitBody = GetComponent<Rigidbody>();
        mainCamera = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    private void Update()
    {
        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput * moveSpeed;

        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.red);

            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }
    }

    private void FixedUpdate()
    {
        myRigitBody.velocity = moveVelocity;
    }
}