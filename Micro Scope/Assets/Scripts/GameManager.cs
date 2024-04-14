using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    [SerializeField] 
    public GameObject[] viruses;
    public GameObject[] directions;
    public GameObject QTEIndicator;
    private int QTEcount = 0;
    public TextMeshProUGUI scoreText;
    private int scoreCount;
    private GameObject currentVirus;

    public Camera gameCamera;
    public AudioSource audioSource;
    public AudioClip[] sounds;
    public AudioSource music;
    public AudioSource scroll;

    private bool timeToSpawn = false;
    private bool currentVirusHasSpawned = false;

    public bool playingQTE = false;
    public bool acceptInput;

    private bool isGamePause = false;

    public GameObject virusSprite;
    public GameObject pauseMenu;
    public GameObject gameOverMenu;
    public TextMeshProUGUI finalScoreText;

    private AudioManager audioManager;

    private void Awake() {
        audioManager = AudioManager.Instance;
        if (Instance == null) {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        } else if(Instance != this) {
            Destroy(gameObject);
        }

        acceptInput = true;
        audioManager.destoryObjects();
        DestroyObject(audioManager);
    }


    // Start is called before the first frame update
    void Start()
    {
        scoreCount = 0;


        currentVirus = viruses[Random.Range(0, viruses.Length)];
        Debug.Log(currentVirus.name);
        timeToSpawn = true;
    }

    public void Restart()
    {
        if(playingQTE){
            EndQTE();
            playingQTE = false;
        }

        scoreCount = 0;
        scoreText.text = scoreCount.ToString();

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Virus");

        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }

        Destroy(GameObject.FindGameObjectWithTag("killer"));

        currentVirus = viruses[Random.Range(0, viruses.Length)];
        currentVirusHasSpawned = false;

        timeToSpawn = true;
        acceptInput = true;

        pauseGame();
    }

    public void playAgain()
    {
        if(playingQTE){
            EndQTE();
            playingQTE = false;
        }

        scoreCount = 0;
        scoreText.text = scoreCount.ToString();

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Virus");

        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }

        Destroy(GameObject.FindGameObjectWithTag("killer"));

        currentVirus = viruses[Random.Range(0, viruses.Length)];
        currentVirusHasSpawned = false;

        timeToSpawn = true;
        acceptInput = true;

        isGamePause = false;
        gameOverMenu.SetActive(isGamePause);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGamePause)
        {
            if (timeToSpawn == true)
            {
                SpawnViruses();
            }

            if (playingQTE == true)
            {
                PlayQTE();
            }

            //GetClosest();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseGame();
        }
    }

    void SpawnViruses(){

        for(int i = 0; i < 125; i++){
            GameObject virusToSpawn = viruses[Random.Range(0, viruses.Length)];
            virusToSpawn.gameObject.tag = "Virus";

            if(virusToSpawn == currentVirus && currentVirusHasSpawned != true){
                currentVirusHasSpawned = true;
                virusToSpawn.gameObject.tag = "killer";
                virusSprite.GetComponent<SpriteRenderer>().sprite = currentVirus.GetComponent<SpriteRenderer>().sprite;
            } else {
                while(virusToSpawn == currentVirus) {
                    virusToSpawn = viruses[Random.Range(0, viruses.Length)];
                }
            }
            
            Instantiate(virusToSpawn, new Vector3(Random.Range(-8, 8), Random.Range(-8, 8), 5), Quaternion.identity);
        }

        timeToSpawn = false;

    }

    public bool CheckVirus(GameObject clickedVirus) {

        if(clickedVirus.tag != "killer"){
            clickedVirus.GetComponent<VirusController>().correctVirus("no");
            audioSource.PlayOneShot(sounds[3]);
            return false;
        } else{
            clickedVirus.GetComponent<VirusController>().correctVirus("yes");
            StartQTE(clickedVirus);
            return true;
        }

    }

    public void StartQTE(GameObject virus){
        audioSource.PlayOneShot(sounds[1]);
        gameCamera.transform.position = new Vector3(0, 0, -10);
        Debug.Log("QTE Started!");

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Virus");
        
        foreach(GameObject enemy in enemies){
            if(enemy == virus){
                continue;
            } else {
                Destroy(enemy);
            }

            virus.transform.position = new Vector3(0, 0, 5);
        }

        Instantiate(directions[Random.Range(0, directions.Length)], QTEIndicator.transform);
        QTEcount = 0;
        playingQTE = true;
        acceptInput = false;

    }

    void PlayQTE(){

        if(QTEcount >= 5){
            playingQTE = false;
            EndQTE();
            return;
        }

        GameObject newDirection;

        if(Input.GetKeyDown(KeyCode.UpArrow) && QTEIndicator.transform.GetChild(0).tag == "UpArr"){
            QTEcount++;
            audioSource.PlayOneShot(sounds[0]);
            newDirection = directions[Random.Range(0,4)];

            while(QTEIndicator.transform.GetChild(0).CompareTag(newDirection.transform.tag))
            {
                newDirection = directions[Random.Range(0,4)];
            }

            Destroy(QTEIndicator.transform.GetChild(0).gameObject);
            Instantiate(newDirection, QTEIndicator.transform);
        }

        if(Input.GetKeyDown(KeyCode.RightArrow) && QTEIndicator.transform.GetChild(0).tag == "RightArr"){
            QTEcount++;
            audioSource.PlayOneShot(sounds[0]);
            newDirection = directions[Random.Range(0,4)];

            while(QTEIndicator.transform.GetChild(0).CompareTag(newDirection.transform.tag))
            {
                newDirection = directions[Random.Range(0,4)];
            }

            Destroy(QTEIndicator.transform.GetChild(0).gameObject);
            Instantiate(newDirection, QTEIndicator.transform);
        }

        if(Input.GetKeyDown(KeyCode.DownArrow) && QTEIndicator.transform.GetChild(0).tag == "DownArr"){
            QTEcount++;
            audioSource.PlayOneShot(sounds[0]);
            newDirection = directions[Random.Range(0,4)];

            while(QTEIndicator.transform.GetChild(0).CompareTag(newDirection.transform.tag))
            {
                newDirection = directions[Random.Range(0,4)];
            }

            Destroy(QTEIndicator.transform.GetChild(0).gameObject);
            Instantiate(newDirection, QTEIndicator.transform);
        }

        if(Input.GetKeyDown(KeyCode.LeftArrow) && QTEIndicator.transform.GetChild(0).tag == "LeftArr"){
            QTEcount++;
            audioSource.PlayOneShot(sounds[0]);
            newDirection = directions[Random.Range(0,4)];

            while(QTEIndicator.transform.GetChild(0).CompareTag(newDirection.transform.tag))
            {
                newDirection = directions[Random.Range(0,4)];
            }

            Destroy(QTEIndicator.transform.GetChild(0).gameObject);
            Instantiate(newDirection, QTEIndicator.transform);
        }

    }

    void EndQTE() {

        audioSource.PlayOneShot(sounds[2]);
        Destroy(QTEIndicator.transform.GetChild(0).gameObject);
        Destroy(GameObject.FindGameObjectWithTag("killer"));

        scoreCount++;
        scoreText.text = scoreCount.ToString();

        currentVirus = viruses[Random.Range(0, viruses.Length)];
        currentVirusHasSpawned = false;

        timeToSpawn = true;
        acceptInput = true;

    }

    public void GameOver(){

        //Time.timeScale = 0.0f;
        isGamePause = !isGamePause;
        gameOverMenu.SetActive(isGamePause);
        finalScoreText.text = "Score: " + scoreCount.ToString();
    }

    public bool checkPause()
    {
        return isGamePause;
    }

    public void pauseGame()
    {
        isGamePause = !isGamePause;
        pauseMenu.SetActive(isGamePause);
    }

    public void destoryGameObject()
    {
        Destroy(gameObject);
        DestroyObject(music);
    }
}
