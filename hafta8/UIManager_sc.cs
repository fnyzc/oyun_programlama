using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager_sc : MonoBehaviour
{
    [SerializeField]
    TMP_Text scoreText;
    [SerializeField]
    TMP_Text gameOverText;

    [SerializeField]
    Image liveImg;

    [SerializeField]
    Sprite[] liveSprites;

    [SerializeField]
    TMP_Text restartText;

    GameManager_sc gameManager_sc;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreText.text = "Score: 0";
        liveImg.sprite = liveSprites[3];
        gameOverText.gameObject.SetActive(false);
        gameManager_sc = GameObject.Find("GameManager").GetComponent<GameManager_sc>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore(int score)
    {
        scoreText.text = "Score: " + score;
    }

    public void UpdateLives(int currentLives)
    {
        liveImg.sprite = liveSprites[currentLives];
        if(currentLives == 0)
        {
            GameOverSequence();
        }
    }
    IEnumerator GameOverFlickerRoutine()
    {
        while (true)
        {
            gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }

    void GameOverSequence()
    {
        if(gameManager_sc != null)
        {
            gameManager_sc.GameOver();
        }
        else
        {
            Debug.LogError("UIManager_sc:: GameOverSequence, gameManager_sc is NULL");
        }
        gameOverText.gameObject.SetActive(true);
        restartText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlickerRoutine());
    }
}
