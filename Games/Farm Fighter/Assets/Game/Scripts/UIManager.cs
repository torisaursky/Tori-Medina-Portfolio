using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField] private GameObject Title = null;

    [SerializeField] private Sprite[] HealthImages = null;
    [SerializeField] private Image HealthBar = null;
    [SerializeField] private Sprite[] HeroHealthImages = null;
    [SerializeField] private Image HeroHealth = null;

    [SerializeField] private Text ScoreTracker;
    [SerializeField] private Text HighScoreTracker;

    private int score = 0;
    private int highScore = 0;

    private GameManager GM;

    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        HighScoreTracker.text = " High: " + highScore;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HideTitle()
    {
        Title.SetActive(false);
    } //end HideTitle

    public void ShowTitle()
    {
        Title.SetActive(true);
    }

    public void UpdateHealth(int count)
    {
        HealthBar.sprite = HealthImages[count];
    } //end UpdateHealth

    public void UpdateHeroHealth(int count)
    {
        HeroHealth.sprite = HeroHealthImages[count];
    } //end UpdateHeroHealth

    public void UpdateScore()
    {
        if (!GM.gameOver)
        {
            score += 10;
        } else {
            score = 0;
        } //end if
        ScoreTracker.text = " Score: " + score;

        if (score > highScore)
        {
            highScore = score;

            PlayerPrefs.SetInt("HighScore", highScore);
        }

        HighScoreTracker.text = " High: " + highScore;
    } //end UpdateScore

}
