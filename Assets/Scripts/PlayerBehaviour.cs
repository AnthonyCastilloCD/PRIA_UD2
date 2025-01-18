using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public float moveSpeed = 10f;
    //Movement, Camera Controls, and Collisions
    public float rotateSpeed = 75f;
    private float vInput;
    private float hInput;
    //Component Rigidbody
    private Rigidbody _rb;
    //Variable Jump
    public float JumpVelocity = 5f;
    private bool _isJumping;
    //Variable Detect Ground
    public float DistanceToGround = 0.1f;
    public LayerMask GroundLayer;
    private CapsuleCollider _col;
    //Variable Shoot
    public GameObject Bullet;
    public float BulletSpeed = 100f;
    private bool _isShooting;

    //Lose HP
    private GameBehaviour _gameManager;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _col = GetComponent<CapsuleCollider>();
        _gameManager = GameObject.Find("Game Manager").GetComponent<GameBehaviour>();
    }

    void Update()
    {
        vInput = Input.GetAxis("Vertical") * moveSpeed;
        hInput = Input.GetAxis("Horizontal") * rotateSpeed;

        _isJumping |= Input.GetKeyDown(KeyCode.J);

        _isShooting |= Input.GetKeyDown(KeyCode.Space);

        /*
        this.transform.Translate(Vector3.forward * vInput *
        Time.deltaTime);
        this.transform.Rotate(Vector3.up * hInput * Time.deltaTime);
        */
    }

    void FixedUpdate()
    {
        Vector3 rotation = Vector3.up * hInput;

        Quaternion angleRot = Quaternion.Euler(rotation * Time.fixedDeltaTime);

        _rb.MovePosition(this.transform.position + this.transform.forward * vInput *
        Time.fixedDeltaTime);

        _rb.MoveRotation(_rb.rotation * angleRot);

        if (_isJumping && isGrounded())
        {
            _rb.AddForce(Vector3.up * JumpVelocity, ForceMode.Impulse);
        }
        _isJumping = false;

        if (_isShooting)
        {
            GameObject newBullet = Instantiate(Bullet,
            this.transform.position + new Vector3(0, 0, 1),
            this.transform.rotation);

            Rigidbody BulletRB = newBullet.GetComponent<Rigidbody>();

            BulletRB.velocity = this.transform.forward * BulletSpeed;
        }
        _isShooting = false;
    }

    private bool isGrounded()
    {
        Vector3 capsuleBottom = new Vector3(_col.bounds.center.x,
        _col.bounds.min.y, _col.bounds.center.z);

        bool grounded = Physics.CheckCapsule(_col.bounds.center,
        capsuleBottom, DistanceToGround, GroundLayer,
        QueryTriggerInteraction.Ignore);
        
        return grounded;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Enemy")
        {
            _gameManager.HP -= 1;
        }
    }

}
