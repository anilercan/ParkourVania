using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{
    int playerLives=100;
    int playerScore=0;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI endText;
    [SerializeField] public TextMeshProUGUI deathText;
    bool gameOver=false;
    //bool musicPlaying=false;
    void Awake()
    {
        //if (musicPlaying==false){
        int numGameSessions=FindObjectsOfType<GameSession>().Length;
        if (numGameSessions>1){
            Destroy(gameObject);
        }
        else{
            DontDestroyOnLoad(gameObject);
        }
        
    }
    void Start(){
        livesText.text="Lives: "+playerLives.ToString();
        scoreText.text="Score: "+playerScore.ToString();
    }
    public void ProcessPlayerDeath(){
        if (playerLives>1){
            deathText.gameObject.SetActive(true);
            TakeLife();
        }
        else{
            livesText.text="Lives: 0";
            ResetGameSession();
        }
    }
    public void AddToScore(){
        playerScore=playerScore+1;
        scoreText.text="Score: "+playerScore.ToString();
    }
    void TakeLife(){
        playerLives=playerLives-1;
        livesText.text="Lives: "+playerLives.ToString();
    }
    public void ResetGameSession(){
        gameOver=true;
        endText.text="Game Over!\nScore: "+playerScore.ToString();
        //FindObjectOfType<PlayerMovementScript>().bgmMusic.Stop();
        endText.gameObject.SetActive(true);
        StartCoroutine(ResetGame());
    }
    IEnumerator ResetGame(){
        yield return new WaitForSecondsRealtime(5);
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(0);
        Destroy(gameObject);        
    }
    public bool isGameOver(){
        return gameOver;
    }
}
