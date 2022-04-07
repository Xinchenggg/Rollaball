using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

// PlayerInput component will notify PlayerController script of action happening 
public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public GameObject loseTextObject;
    public GameObject infoTextObject;

    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;

        SetCountText();
        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);
        infoTextObject.SetActive(true);
        Destroy(infoTextObject, 2);
    }

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void FreezePlayer()
    {
        rb.isKinematic = true;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if(count >= 7) {
            winTextObject.SetActive(true);
            FreezePlayer();
        }
    }

    // FixedUpdate is called just before performing any physics calculation
    // Physics code stay here
    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);
    }

    // Update is called once per frame, before rendering the frame
    void Update()
    {
        
    }

    // OnTriggerEnter is called when the object first touches a trigger collider
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PickUp")) {
            other.gameObject.SetActive(false);
            count += 1;

            SetCountText();
        }
        if(other.gameObject.CompareTag("NoPickUp")) {
            other.gameObject.SetActive(false);
            loseTextObject.SetActive(true);
            FreezePlayer();
        }
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }
}
