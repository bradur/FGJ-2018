using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kolvi : MonoBehaviour
{


    [SerializeField]
    private Animator animator;


    // Use this for initialization
    void Start()
    {
    }

    public void Whack()
    {
        animator.SetTrigger("Whack");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter (Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().GetHit();
        }
    }
}
