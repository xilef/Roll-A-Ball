using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Text scoreText;
    public Text winText;
    public Text accelText;

    private Rigidbody rb;
    private int score;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        score = 0;
        updateScore();
        winText.text = "";
        accelText.text = "";
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
       /* float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");*/
        float moveHorizontal = 0.0f;
        float moveVertical = 0.0f;

        //Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        Vector3 newMovement = Vector3.zero;

        int x = 0;
        accelText.text = "";
        while (x < Input.accelerationEventCount)
        {
            AccelerationEvent evt = Input.GetAccelerationEvent(x);
            accelText.text = accelText.text + evt.acceleration.ToString();
            accelText.text = accelText.text + evt.deltaTime.ToString();

            moveHorizontal += (evt.acceleration).x;
            moveVertical += (evt.acceleration).y;

            ++x;
        }

        newMovement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        //accelText.text = newMovement.ToString();

        rb.AddForce(newMovement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            score++;
            updateScore();
        }
    }

    void updateScore()
    {
        scoreText.text = "Score: " + score.ToString();

        if (score >= 7)
        {
            winText.text = "You Win!";
        }
    }
}
