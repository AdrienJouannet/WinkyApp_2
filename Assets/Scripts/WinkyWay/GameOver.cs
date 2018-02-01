using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

    private float time;
    public Text TimerScore;
    public Text highScore;
    public float highestScore;
    
    private void Start()
    {    
        time = 0;
        highestScore = PlayerPrefs.GetFloat("Highscore", highestScore);
    }

    void OnEnable()
    {
        time = Timer.timer;
        string minutes = Mathf.Floor(time / 59).ToString("00");
        string seconds = (time % 59).ToString("00");
        TimerScore.text = minutes + ":" + seconds;
        highestScore = PlayerPrefs.GetFloat("Highscore", highestScore);
        if (highestScore < time)
        {
            highestScore = time;
            PlayerPrefs.SetFloat("Highscore", time); 
            PlayerPrefs.Save();
        //    print("min = " + minutes + "sec  = " + seconds);
            highScore.text = minutes + ":" + seconds;
        }
        else
        {
            
            string min = Mathf.Floor(highestScore / 59).ToString("00");
            string sec = (highestScore % 59).ToString("00");
            highScore.text = min + ":" + sec;

        }
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}