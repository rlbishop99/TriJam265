using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{

    private GameManager gameManager;

    public Image healthBar;

    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        timer = 120f;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        healthBar.fillAmount = timer/120f;

        if(timer <= 0){

            gameManager.GameOver();

        }

        Debug.Log(timer);
    }
    
    }
