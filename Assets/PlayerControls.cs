using System.Collections;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D m_rigidbody;
    public float moveSpeed = 100f;
    public float ballSpeed = 100f;
    public bool isGrouded = false;
    public GameObject ball;

    protected bool hasJumped = false;
    protected bool hasFired = false;

    // Update is called once per frame
    void Update()
    {
        //Get The Input
        float xInput = Input.GetAxisRaw("Horizontal");

        //Check for Animation
        if(xInput != 0)
        {
            animator.SetBool("Run", true);
        }
        else if (xInput == 0)
        {
            animator.SetBool("Run", false);
        }

        //Check for Flip
        if(xInput < 0f)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if(xInput > 0f)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        //Movement
        m_rigidbody.AddForce(Vector2.right * xInput * Time.deltaTime * moveSpeed * 100f);

        //Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrouded)
        {
            animator.SetBool("Jump", true);

            if (hasJumped)
            {
                m_rigidbody.AddForce(Vector2.up * Time.deltaTime * 10000f * moveSpeed);
            }
            else
            {
                m_rigidbody.AddForce(Vector2.up * Time.deltaTime * 4000f * moveSpeed);
            }

            //Fix the Jump Cold Start Bug
            isGrouded = false;
            hasJumped = true;
        }

        //Ball throw
        if (Input.GetButtonDown("Fire1"))
        {
            //Get current position
            Vector3 currentPos = transform.position;

            //Instantiate Ball
            GameObject currentBall = Instantiate(ball, currentPos, Quaternion.identity);

            //Add Force so that it moves in a parabolic motion
            if(xInput >= 0f)
            {
                if (!hasFired)
                {
                    currentBall.GetComponent<Rigidbody2D>().AddForce(Vector2.right * Time.deltaTime * 40f * ballSpeed);
                    currentBall.GetComponent<Rigidbody2D>().AddForce(Vector2.up * Time.deltaTime * 200f * ballSpeed);
                    currentBall.GetComponent<Rigidbody2D>().AddForce(Vector2.right * Time.deltaTime * 60f * ballSpeed);
                }

                else if (hasFired)
                {
                    currentBall.GetComponent<Rigidbody2D>().AddForce(Vector2.right * Time.deltaTime * 80f * ballSpeed);
                    currentBall.GetComponent<Rigidbody2D>().AddForce(Vector2.up * Time.deltaTime * 800f * ballSpeed);
                    currentBall.GetComponent<Rigidbody2D>().AddForce(Vector2.right * Time.deltaTime * 120f * ballSpeed);
                }
            }

            else if (xInput < 0f || GetComponent<SpriteRenderer>().flipX == true)
            {
                Debug.Log("Yas");
                if (!hasFired)
                {
                    currentBall.GetComponent<Rigidbody2D>().AddForce(Vector2.left * Time.deltaTime * 40f * ballSpeed);
                    currentBall.GetComponent<Rigidbody2D>().AddForce(Vector2.up * Time.deltaTime * 200f * ballSpeed);
                    currentBall.GetComponent<Rigidbody2D>().AddForce(Vector2.left * Time.deltaTime * 60f * ballSpeed);
                }

                else if (hasFired)
                {
                    currentBall.GetComponent<Rigidbody2D>().AddForce(Vector2.left * Time.deltaTime * 80f * ballSpeed);
                    currentBall.GetComponent<Rigidbody2D>().AddForce(Vector2.up * Time.deltaTime * 800f * ballSpeed);
                    currentBall.GetComponent<Rigidbody2D>().AddForce(Vector2.left * Time.deltaTime * 120f * ballSpeed);
                }
            }

            //FIX BUGS
            hasFired = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrouded = true;
            animator.SetBool("Jump", false);
        }
    }
}
