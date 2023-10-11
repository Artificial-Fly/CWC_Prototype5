using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    private int score =0;
    private int lives=3;
    public bool isGameOver = true;
    public TextMeshProUGUI scoreText, livesText, gameOverText;
    public Button restartButton;
    public GameObject titleScreen;
    public List<GameObject> targets;
    
    float spawnRate = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if(lives<1){
            GameOver();
        }
    }
    public void UpdateLives(int livesToRed){
        if(!isGameOver){
            lives += livesToRed;
            livesText.text = "Lives\n"+lives;
            if(lives<2){
                livesText.color = new Color32(247,46,46, 255);
            }else{
                livesText.color = new Color32(241,232,224, 255);
            }
        }
    }
    public void UpdateScore(int scoreToAdd){
        if(!isGameOver){
            score += scoreToAdd;
            scoreText.text = "Score\n"+score;
            if(score>200){
                scoreText.color = new Color32(241,227,132, 255);
            }else{
                scoreText.color = new Color32(241,232,224, 255);
            }
        }
    }

    IEnumerator SpawnTarget(int difficulty){
        spawnRate /= difficulty;
        while(!isGameOver){
            yield return new WaitForSeconds(spawnRate);
            int index = 0;
            if(difficulty==1){index = Random.Range(0, targets.Count-2);}else if(difficulty==2){index = Random.Range(0, targets.Count-1);}else{index = Random.Range(0, targets.Count);}
            Instantiate(targets[index]);
            
        }
    }

    void GameOver(){
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        isGameOver = true;
    }
    public void RestartGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty){
        titleScreen.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(true);
        livesText.gameObject.SetActive(true);
        isGameOver = false;
        score = 0;
        lives = 3;
        StartCoroutine(SpawnTarget(difficulty));
        UpdateScore(0);
        UpdateLives(0);
    }
}
