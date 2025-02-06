using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OuchieMenu : MonoBehaviour
{
    [SerializeField] private KeyCode returnKey;
    public GameObject ouchUI;
    public static bool GameIsPaused = false;
    // Start is called before the first frame update
    void Start()
    {
        ouchUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(returnKey))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void Resume()
    {
        ouchUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    public void Pause()
    {
        ouchUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Resume();
    }
    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Resume();
    }
}
