using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverHandler : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Button continueButton;
    [SerializeField] private TMP_Text gameOverText;
    [SerializeField] private ScoreSystem scoreSystem;
    [SerializeField] private GameObject gameOverDisplay;
    [SerializeField] private AsteroidSpawner asteroidSpawner;

    public void EndGame()
    {
        asteroidSpawner.enabled = false;

        int finalScore = scoreSystem.EndTimer();

        gameOverText.text = $"Your Score: {finalScore}";
                
        gameOverDisplay.gameObject.SetActive(true);
    }
    public void PlayAgain()
    {
        SceneManager.LoadScene(1);
    }

    public void ContinueButton()
    {
        AdManager.Instance.ShowAd(this);
        continueButton.interactable = false;
    }

    public void ReturnMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ContinueGame()
    {
        // this all happens after the player finishes the AD

        scoreSystem.StartTimer(); // start the timer

        player.transform.position = Vector3.zero; // place the player in the middle
        player.SetActive(true); // reenable
        player.GetComponent<Rigidbody>().velocity = Vector3.zero; // reset velocity

        asteroidSpawner.enabled = true; // start spawning asteroids again

        gameOverDisplay.gameObject.SetActive(false); // hide the UI


    }
}
