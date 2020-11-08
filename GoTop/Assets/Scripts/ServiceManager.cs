using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ServiceManager : MonoBehaviour
{
    #region Singleton
    public static ServiceManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    #endregion

    void Start()
    {
        Time.timeScale = 1;

        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            PlayerPrefs.SetInt(GamePrefs.LastLevelPlayed.ToString(), SceneManager.GetActiveScene().buildIndex);
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().buildIndex + GamePrefs.LevelPlayed.ToString(), 1);
        }
            
    }

    public void Restart()
    {
        ChangeLvl(SceneManager.GetActiveScene().buildIndex);
    }

    public void EndLevel()
    {
        ChangeLvl(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ChangeLvl(int lvl)
    {
        SceneManager.LoadScene(lvl);
    }

    public void Quit()
    {
        Debug.Log("QUITED");
        Application.Quit();
    }

    public void ResetProgress()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("deleted");
    }
}

public enum Scenes
{
    MainMenu,
    JustIntroduction1,
    BigPlay2,
    TryNotToDie3
}

public enum GamePrefs
{
    LastLevelPlayed,
    LevelPlayed,
    Coins
}
