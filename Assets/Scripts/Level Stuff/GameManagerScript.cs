using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public SpriteRenderer playersr;
    public Playermovement playermovement;
    public AudioSource BGM;


    public GameObject gameOverUI;
    public GameObject PauseMenu;
    public GameObject youWinUI;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOverUI.activeInHierarchy)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined;
        }
        if (youWinUI.activeInHierarchy)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void gameOver()
    {
        gameOverUI.SetActive(true);
        BGM.Stop();
    }
    public void youWin()
    {
        youWinUI.SetActive(true);
        Time.timeScale = 0f;
        playersr.enabled = false;
        playermovement.enabled = false;
    }

    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void mainMenu()
    {
        SceneManager.LoadScene("level/Menu");
        Debug.Log("Mein Menu");
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
