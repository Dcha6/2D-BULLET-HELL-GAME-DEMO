using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public bool isGrounded;
    public bool isJumping;
    public float moveSpeed = 5f;
    public float jumpHeight = 10f;
    public int jumps = 0;
    public int maxJumps = 2;
    public bool ExtraJump;

    private bool facingRight;
    public Transform respawnPoint;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        FlipChar(horizontal);
    }
    void Update()
    {
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpHeight), ForceMode2D.Impulse);
            jumps = maxJumps;
        }
        if (Input.GetButtonDown("Jump") && (jumps < maxJumps))
        {
            Jump();
        }
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * moveSpeed;
    }

    void Jump()
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * jumpHeight, ForceMode2D.Impulse);
        jumps = jumps + 1;
    }




    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "spikes")
        {
            Scene scene;
            scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }

        if (other.tag == "ExtraJump")
        {
            jumps = 0;
            Destroy(other.gameObject);
        }
    }


    private void FlipChar(float horizontal)
    {
        if (horizontal > 0 && facingRight || horizontal < 0 && !facingRight)
        {
            facingRight = !facingRight;
            transform.Rotate(0f, 180f, 0f);
        }
    }
}
