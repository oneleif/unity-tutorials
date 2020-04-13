using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed;
    Rigidbody myRigidbody;

    bool didLose;
    public int score;

    private void Start()
    {
        playerSpeed = 0.1f;
        myRigidbody = GetComponent<Rigidbody>();
        didLose = false;
        score = 0;
    }

    void Update()
    {
        HandleLoss();
        UpdateRotation();
        UpdatePosition();
    }

    void HandleLoss()
    {
        if (didLose)
        {
            // Reset player's position
            myRigidbody.isKinematic = true;
            transform.position = Vector3.zero;
            myRigidbody.isKinematic = false;

            // Reset player's rotation
            transform.eulerAngles = Vector3.zero;

            // Reset player's speed
            playerSpeed = 0.05f;

            // Now that we are rest, we are no longer losing
            didLose = false;
            score = 0;
        }
    }
    
    void UpdateRotation()
    {
        // Access the current mouse input this frame
        float mouseXMovement = Input.GetAxis("Mouse X"); //between -1 and 1
        float mouseYMovement = Input.GetAxis("Mouse Y");

        // Turn the mouse movement into a 3d rotation
        Vector3 rotationChange = new Vector3(-mouseYMovement, mouseXMovement, 0);
        // Update my objects rotation
        transform.eulerAngles += rotationChange;

        //Debug.Log("Rotation: " + rotationChange);
    }

    void UpdatePosition()
    {
        Vector3 changeInPosition = transform.forward * playerSpeed;
        Vector3 newPosition = transform.position + changeInPosition;
        myRigidbody.MovePosition(newPosition);
        //Debug.Log("myForwardVector: " + transform.forward);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("you hit something fool");
        if(collision.gameObject.tag == "Wall")
        {
            Lose();
        }else if(collision.gameObject.tag == "Food")
        {
            EatFood(collision.gameObject);
        }
    }

    void Lose()
    {
        didLose = true;
    }

    void EatFood(GameObject foodWeCollidedWith)
    {
        RandomlyPlace(foodWeCollidedWith);
        score++;
        StartCoroutine(UpdateSpeed());
    }

    IEnumerator UpdateSpeed()
    {
        yield return new WaitForSeconds(1);
        playerSpeed *= 2;
    }

    void RandomlyPlace(GameObject foodWeCollidedWith)
    {
        int randomX = Random.Range(-17, 17);
        int randomY = Random.Range(-17, 17);
        int randomZ = Random.Range(-17, 17);
        foodWeCollidedWith.transform.position = new Vector3(randomX, randomY, randomZ);
    }
}
