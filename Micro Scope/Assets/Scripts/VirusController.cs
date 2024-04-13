using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusController : MonoBehaviour
{

    private GameManager gameManager;

    public float speed;
    private bool hasMoved;
    private bool QTE;
    
    private Vector3 dir;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        speed = Random.Range(.01f, .02f);
        rb = gameObject.GetComponent<Rigidbody>();
        QTE = false;

    }

    // Update is called once per frame
    void Update()
    {
        if(hasMoved == false && QTE == false) {
            dir = new Vector2(Random.Range(-10, 10), Random.Range(-10, 10));
            hasMoved = true;
        }

        if(hasMoved == true) {
            rb.AddForce(dir * speed);
        }
    }

    private void OnCollisionEnter(Collision other) {
        hasMoved = false;
    }

    private void OnMouseDown() {
        gameManager.CheckVirus(gameObject);
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
