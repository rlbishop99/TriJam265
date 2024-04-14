using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusController : MonoBehaviour
{

    private GameManager gameManager;

    public float speed;
    public float rotationSpeed;
    public float spin;
    private bool hasMoved;
    private bool QTE;
    
    private Vector3 dir;
    private Vector3 rotationVel = new Vector3(0,0,60);

    private Rigidbody rb;

    Quaternion rationChange;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        speed = Random.Range(.01f, .02f);
        rotationSpeed = Random.Range(1, 5);
        spin = Random.RandomRange(0, 2);
        rb = gameObject.GetComponent<Rigidbody>();
        QTE = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.checkPause())
        {
            
            switch (spin) {
                case 0:
                    rationChange = Quaternion.Euler(rotationVel * Time.deltaTime * rotationSpeed);
                    break;
                case 1:
                    rationChange = Quaternion.Euler(rotationVel * Time.deltaTime * -rotationSpeed);
                    break;
            }

            if (hasMoved == false && QTE == false)
            {
                dir = new Vector2(Random.Range(-10, 10), Random.Range(-10, 10));
                hasMoved = true;
            }

            if (hasMoved == true)
            {
                rb.AddForce(dir * speed);
                rb.MoveRotation(rb.rotation * rationChange);
            }
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }

    private void OnCollisionEnter(Collision other) {
        hasMoved = false;
    }

    private void OnMouseDown() {
        if (!GameManager.Instance.checkPause())
        {
            if (gameManager.playingQTE == false && gameManager.CheckVirus(gameObject)){
                rb.velocity = Vector3.zero;
            }
        }
    }

    public void correctVirus(string output){

        switch(output){
            case "yes":
                QTE = true;
                hasMoved = false;
                rb.velocity = Vector3.zero;
                break;
            case "no":
                break;
        }
    }
}
