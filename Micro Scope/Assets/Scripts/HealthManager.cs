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

    public void restartTimer()
    {
        timer = 120f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.checkPause())
        {
            timer -= Time.deltaTime;
            healthBar.fillAmount = timer / 120f;

            if (timer <= 0)
            {

                gameManager.GameOver();

            }

            //Debug.Log(timer);
        }
    }
    
    }
