using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ultranewcc : MonoBehaviour
{
    public float moveSpeed;
    public float jumpforce;

    private Vector3 moveDirection;
    public float gravityScale;
    public float rotateSpeed;
    float vertical;
    float horizontal;
    float inputAmount;
    Vector3 gravity;

    Rigidbody rb;

    private CharacterController CC;

    void Start()
    {
        CC = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {

        moveDirection = Vector3.zero;
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");

        Vector3 correctedVertical = vertical * Camera.main.transform.forward;
        Vector3 correctedHorizontal = horizontal * Camera.main.transform.right;

        Vector3 combinedInput = correctedHorizontal + correctedVertical;
        moveDirection = new Vector3((combinedInput).normalized.x, moveDirection.y, (combinedInput).normalized.z);

        rb.velocity = (moveDirection * moveSpeed * inputAmount) + gravity;


        float inputMagnitude = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
        inputAmount = Mathf.Clamp01(inputMagnitude);

        Quaternion rot = Quaternion.LookRotation(moveDirection);
        Quaternion targetRotation = Quaternion.Slerp(transform.rotation, rot, Time.fixedDeltaTime * inputAmount * rotateSpeed);

        transform.rotation = targetRotation;

        if (Input.GetButtonDown("Jump"))
        {
            moveDirection.y = jumpforce;
        }

        moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale* Time.deltaTime);
        gravity += Vector3.up * Physics.gravity.y * Time.fixedDeltaTime;
        CC.Move(moveDirection * Time.deltaTime);
    }

}