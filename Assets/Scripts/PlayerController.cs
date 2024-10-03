using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro; 


public class PlayerController : MonoBehaviour
{
    public float speed = 0; 
    public TextMeshProUGUI countText; 
    public GameObject winTextObject; 

    private Rigidbody rb;
    private int count; 
    private float movementx;
    private float movementy; 

    // Start is called before the first frame update
    void Start()
    {
       rb = GetComponent<Rigidbody>();
       count = 0;

       SetCountText ();
       winTextObject.SetActive(false);

    }

    void OnMove(InputValue movementValue)
    {
            Vector2 movementVector = movementValue.Get<Vector2>();
            movementx = movementVector.x;
            movementy = movementVector.y;

    }

    void SetCountText ()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 8)
        {
            winTextObject.SetActive(true);
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
        }

    }

     void FixedUpdate()
     {
         Vector3 movement = new Vector3(movementx, 0.0f, movementy);
         rb.AddForce(movement * speed);
     }

     private void OnCollisionEnter(Collision collision)
     {
         if (collision.gameObject.CompareTag("Enemy"))
         {
             Destroy(gameObject);

             winTextObject.gameObject.SetActive(true);
             winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
         }
     }

     private void OnTriggerEnter(Collider other)
     {
         if(other.gameObject.CompareTag("PickUp"))
         {
             other.gameObject.SetActive(false);
             count = count + 1;

             SetCountText();
         }
     }

}