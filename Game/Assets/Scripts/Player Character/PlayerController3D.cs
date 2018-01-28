using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController3D : MonoBehaviour
{

    [SerializeField]
    private Rigidbody rigidbody3D;

    [SerializeField]
    private Animator animator;

    /*[SerializeField]
    [Range(1.5f, 100f)]
    private float sprintSpeed = 15f;*/

    [SerializeField]
    [Range(1f, 20f)]
    private float forwardSpeed = 11f;

    [SerializeField]
    [Range(0.1f, 20f)]
    private float backWardSpeed = 6f;

    [SerializeField]
    [Range(0.01f, 20f)]
    private float rotateSpeed = 0.02f;

    /*[SerializeField]
    private ForceMode moveForceMode;*/

    private Vector3 playerDirection = Vector3.zero;

    private Vector3 m_Move;



    [SerializeField]
    private Kolvi kolvi;
    [SerializeField]
    [Range(0.1f, 1f)]
    private float whackMinInterval = 0.7f;

    private float whackTimer;

    [SerializeField]
    [Range(0.1f, 1f)]
    private float hideAfterWhack = 0.6f;
    private float hideAfterTimer = 0f;
    [SerializeField]
    [Range(0.1f, 1f)]
    private float showKolviInterval = 0.2f;
    private float showKolviTimer = 0f;

    // Use this for initialization
    void Start()
    {
        whackTimer = whackMinInterval;
    }

    // Update is called once per frame
    void Update()
    {
        if (showKolviTimer > 0f)
        {
            showKolviTimer -= Time.deltaTime;
            if (showKolviTimer <= 0f)
            {
                kolvi.gameObject.SetActive(true);
                kolvi.Whack();
            }
        }
        if (hideAfterTimer > 0f)
        {
            hideAfterTimer -= Time.deltaTime;
            if (hideAfterTimer <= 0f)
            {
                //kolvi.gameObject.SetActive(false);
            }
        }
        if (whackTimer > whackMinInterval)
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                showKolviTimer = showKolviInterval;
                animator.SetTrigger("Whack");
                whackTimer = 0f;
                
                hideAfterTimer = hideAfterWhack;
            }
        }
        else
        {
            whackTimer += Time.deltaTime;
        }
    }


    private void FixedUpdate()
    {
        // read inputs
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (Mathf.Abs(h) > 0.05f || Mathf.Abs(v) > 0.05f)
        {
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }
        m_Move = v * forwardSpeed * Vector3.forward + h * forwardSpeed * Vector3.right;

        // pass all parameters to the character control script
        //if (m_Move.magnitude > 1f) m_Move.Normalize();
        /*m_Move = transform.InverseTransformDirection(m_Move);

        m_Move = Vector3.ProjectOnPlane(m_Move, Vector3.up);
        float m_TurnAmount = Mathf.Atan2(m_Move.x, m_Move.z);
        float m_ForwardAmount = m_Move.z;*/

        //rigidbody3D.velocity = new Vector3(rigidbody3D.velocity.x, 0f, rigidbody3D.velocity.z);
        rigidbody3D.velocity = m_Move;
        if (m_Move.magnitude > 0.05f)
        {
            transform.forward = m_Move;
        }
    }


}
