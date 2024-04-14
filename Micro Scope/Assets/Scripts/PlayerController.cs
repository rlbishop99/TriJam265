using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public AudioSource scroll;
    private bool scrollplaying = false;

    private GameManager gameManager;
    public float speed;
    private float x;
    private float y;
    private float xmin = -10f;
    private float xmax = 10f;
    private float ymin = -10f;
    private float ymax = 10f;

    private float xPos;
    private float yPos;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        scroll = GameManager.Instance.scroll;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.checkPause())
        {
            xPos = transform.position.x;
            yPos = transform.position.y;
            CheckBounds(xPos, yPos);

            if (gameManager.acceptInput == true)
            {
                x = Input.GetAxis("Horizontal");
                y = Input.GetAxis("Vertical");
                speed = 5;

                if ((x != 0 || y != 0) && !scrollplaying)
                {
                    scroll.Play();
                    scrollplaying = true;
                }
                else
                {
                    scroll.Stop();
                    scrollplaying = false;
                }
            }
            else
            {
                speed = 0;
                scroll.Stop();
                scrollplaying = false;
            }

            Vector3 dir = new Vector3(x, y, 0);

            transform.Translate(dir * speed * Time.deltaTime);
        }
    }

    void CheckBounds(float x, float y) {

        if(x < xmin){

            if(y > ymax){
                transform.position = new Vector3(xmin, ymax, -10);
            }
            else{
                transform.position = new Vector3(xmin, y, -10);
            }
        }
        
        if(x > xmax){

            if(y > ymax) {
                transform.position = new Vector3(xmax, ymax, -10);
            }
            else {
                transform.position = new Vector3(xmax, y, -10);
            }
        }
        
        if(y < ymin){

            if(x > xmax){
                transform.position = new Vector3(xmax, ymin, -10);
            } else if(x < xmin){
                transform.position = new Vector3(xmin, ymin, -10);
            } else{
                transform.position = new Vector3(x, ymin, -10);
            }
        }
        
        if(y > ymax){

            if(x < xmin){
                transform.position = new Vector3(xmin, ymax, -10);
            } else if(x > xmax) {
                transform.position = new Vector3(xmax, ymax, -10);
            } else{
                transform.position = new Vector3(x, ymax, -10);
            }
        }
    }
}
