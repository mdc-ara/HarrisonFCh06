using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float rotateSpeed = 75f;
    public float jumpVelocity = 5f;

    // 1
    public float distanceToGround = 0.1f;
    // 2
    public LayerMask groundLayer;

    private float vInput;
    private float hInput;
    private Rigidbody _rb;

    // 3
    private CapsuleCollider _col;

    // Start is called before the first frame update
    void Start()
    {
        // 3
        _rb = GetComponent<Rigidbody>();
        // 4
        _col = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        vInput = Input.GetAxis("Vertical") * moveSpeed;
        hInput = Input.GetAxis("Horizontal") * rotateSpeed;

        /*
        this.transform.Translate(Vector3.forward * vInput * Time.deltaTime);
        this.transform.Rotate(Vector3.up * hInput * Time.deltaTime);
        */
    }
    //1
    void FixedUpdate() {
        // 2
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            // 3
            _rb.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
        }

        Vector3 rotation = Vector3.up * hInput;
        Quaternion angleRot = Quaternion.Euler(rotation *  Time.fixedDeltaTime);
        _rb.MovePosition(this.transform.position + this.transform.forward* vInput * Time.fixedDeltaTime);
        _rb.MoveRotation(_rb.rotation* angleRot);
    }
    private bool IsGrounded()
    {
        Vector3 capsuleBottom = new Vector3(_col.bounds.center.x, _col.bounds.min.y, _col.bounds.center.z);
        bool grounded = Physics.CheckCapsule(_col.bounds.center, capsuleBottom, distanceToGround, groundLayer, QueryTriggerInteraction.Ignore);

        return grounded;
    }
}
