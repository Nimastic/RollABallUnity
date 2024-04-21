using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public Text countText;
    public Text winText;

    private Rigidbody rb;
    private int count;
    private int totalPickups;

    private bool jumping = false;

    // This runs once on Start
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        // * For UI and collection
        count = 0;
        updateScore();
        winText.text = "";
        checkTotalPickups();

    }

    // Called just before each physics update
    // Suitable for code involving physics behaviour
    void FixedUpdate()
    {
        
         //* This adds jump functions
        if (Input.GetKey("space") && jumping == false)
        {
            rb.AddForce(Vector3.up * 500);
            jumping = true;
        }


        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
    }
    
     // This is for collision detection
    // Runs on collision
    void OnTriggerEnter(Collider other)
    {
        // Checks if the gameObject belongs to the tag to be compared against
        // Note: Remember to assign the desired gameObject the tag!
        if (other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            count++;
            updateScore();
            checkEnd();
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Floor")
        {
            jumping = false;
        }

        if (col.gameObject.name == "DeathZone")
        {
            transform.position = new Vector3(0.0f, 0.5f, 0.0f);
            SceneManager.LoadScene(0);
        }

        if (col.gameObject.layer == 8)
        {
            // do something when ball hits some object with layer #8
        }
    }
    
    
    //This is for UI
    void updateScore()
    {
        countText.text = "Count: " + count.ToString();
    }

    void checkEnd()
    {
        if (count >= totalPickups)
        {
            countText.text = "";
            winText.text = "You Won!";
        }
    }
    
     //* This is for checking total collection
    void checkTotalPickups()
    {
        totalPickups = GameObject.FindGameObjectsWithTag("Pickup").Length;
    }
   
}