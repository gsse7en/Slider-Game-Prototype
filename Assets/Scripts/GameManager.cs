using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public GameObject gameOverText;
    public GameObject titleScreen;
    public GameObject pauseScreen;
    public bool isGameActive;
    private int score;
    private int lives = 3;
    private float spawnRate = 1.0f;
    int layerMask;

    void Start()
    {
        
    }
    public void StartGame(int difficulty)
    {
        spawnRate /= difficulty;
        titleScreen.gameObject.SetActive(false);
        isGameActive = true;
        StartCoroutine(SpawnTarget());
        UpdateScore(0);
        lives++;
        layerMask = 1 << 6;
        UpdateLives();
    }
    public void GameOver()
    {
        isGameActive = false;
        gameOverText.SetActive(true);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void PauseGame()
    {
        Time.timeScale = 0f;
        pauseScreen.gameObject.SetActive(true);
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pauseScreen.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            PauseGame();
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(ray, out hit,  Mathf.Infinity, layerMask))
        {
            if (Input.GetMouseButton(0))
            {
                Target target = hit.transform.gameObject.GetComponent<Target>();
                target.Destroy();
            }
            if (Input.touchCount > 0)
            {
                Target target = hit.transform.gameObject.GetComponent<Target>();
                target.Destroy();
            }
        }
    }
    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }
    public void UpdateLives()
    {
        if (isGameActive)
        {
            lives--;
            livesText.text = "Lives: " + lives;
            if (lives == 0) GameOver();
        }
    }
}
