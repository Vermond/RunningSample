using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour {

    Rigidbody2D rb;
    bool isGrounded;
    Vector3 jump;
    float jumpForce = 2.0f;
    float moveForce = 1.0f;
    Animator animator;

    // Use this for initialization
    void Start ()
    {
        isGrounded = true;
        rb = GetComponent<Rigidbody2D>();
        jump = new Vector3(0.0f, 10.0f, 0.0f);

        animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //좌우 이동 (기본)
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * moveForce;

        //애니메이션 방향 설정
        if (x > 0)
        {
            //원래 방향
            transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.Translate(x, 0, 0);

            animator.SetBool("bIsRun", true);
        }
        else if (x < 0)
        {
            //반대 방향
            transform.rotation = Quaternion.Euler(0, 180, 0);
            transform.Translate(-x, 0, 0);

            animator.SetBool("bIsRun", true);
        }
        else
        {
            animator.SetBool("bIsRun", false);
        }

        //점프
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(jump * jumpForce);
        }
        animator.SetFloat("ySpeed", rb.velocity.y);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        isGrounded = true;
        animator.SetBool("bJumping", false);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
        animator.SetBool("bJumping", true);
    }
}
