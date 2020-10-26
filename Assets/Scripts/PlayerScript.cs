using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public float speed;

    public Text countText;

    private int count;
    private int lives;
    public Text winText;
    public Text loseText;
    public Text livesText;
    public AudioClip musicClipOne;

    public AudioClip musicClipTwo;

    public AudioSource musicSource;


    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        count = 0;
        SetCountText ();
        lives = 3;
        SetLivesText ();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
       if(collision.collider.tag == "Coin")
        {
            count = count + 1;
            Destroy(collision.collider.gameObject);
            SetCountText ();
        }

        if(collision.collider.tag == "Enemy")
        {
            lives = lives - 1;
            Destroy(collision.collider.gameObject);
            SetLivesText ();
        }

    }
    void SetCountText ()
    {
        countText.text = "Score: " + count.ToString ();

        if (count == 4)
        {
            gameObject.transform.position = new Vector2(82,0);
        }

        if (count == 9)
        {
            winText.text = "You Win! Made by Regan Wheeless.";
            Destroy(gameObject);
            {
                    musicSource.clip = musicClipOne;
                    musicSource.Stop();

                    musicSource.clip = musicClipTwo;
                    musicSource.Play();

                }
        }
        
    }

    void SetLivesText ()
    {
        livesText.text = "Lives: " + lives.ToString ();

        if (lives <= 0)
        {
            loseText.text = "You Lose!";
            Destroy(gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground")
        {
            if(Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }
    }
}