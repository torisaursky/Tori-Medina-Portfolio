using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool gameOver = true;
    public bool isPaused = false;

    [SerializeField] private GameObject Hero = null;
    [SerializeField] private GameObject Paused;

    public int health = 10;
    public int lives = 3;

    private SpawnManager SM;
    private UIManager UI;

    // Start is called before the first frame update
    void Start()
    {
        SM = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        UI = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && !gameOver)
        {
            if (!isPaused)
            {
                Time.timeScale = 0;
                isPaused = true;
                Paused.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                isPaused = false;
                Paused.SetActive(false);
            } //end if
        } //end if
        
        if (gameOver)
        {
            health = 10;
            lives = 3;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Instantiate(Hero, new Vector3(0, -3.5f, 0), Quaternion.identity);
                Destroy(GameObject.FindWithTag("Death"));
                UI.HideTitle();
                UI.UpdateHealth(health);
                UI.UpdateHeroHealth(lives);
                UI.UpdateScore();
                gameOver = false;
                SM.spawnTime = 5.0f;
                SM.StartSpawnRoutines();
            } //end if
        }

        
    }

    public void Damage()
    {
        if (health > 1)
        {
            health--;
            UI.UpdateHealth(health);
        } else 
        {
        UI.UpdateScore();
        UI.ShowTitle();
        gameOver = true;
            Destroy(GameObject.FindWithTag("Player"));
        }
    }

    
}
 