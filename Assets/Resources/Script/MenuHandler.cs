using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour
{
    public static MenuHandler instance;
    public GameObject[] GameArea;
    [Header("MainMenu")]
    public GameObject mainMenu;
    public GameObject ContinueButton;
    [Header("Settings")]
    public GameObject settings;
    public GameObject homeButton;
    public GameState currentChapter;
    public enum GameState
    {
        MainMenu,
        Chapter_1,
        Chapter_2,
        Chapter_3,
        Chapter_4,
        Chapter_5
    }

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        // GameData.instance.ratingObject.SetActive(false);
        Debug.Log(CheckExistingSaves());
        if (CheckExistingSaves())
        {
            // If there are existing saves, show the continue button
            ContinueButton.SetActive(true);
        }
        else
        {
            // If no saves exist, hide the continue button
            ContinueButton.SetActive(false);
        }
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            Settings();
        }
    }

    private bool CheckExistingSaves()
    {
        Debug.Log(PlayerPrefs.GetInt("CurrentChapter"));
        return PlayerPrefs.HasKey("CurrentChapter");
    }
    public void StartGame()
    {
        StartCoroutine(LoadChapter(GameState.Chapter_1));
    }

    public void ContinueGame()
    {
        int currentChapter = PlayerPrefs.GetInt("CurrentChapter", 0);
        StartCoroutine(LoadChapter((GameState)currentChapter));
    }

    public void Settings()
    {
        if (settings.activeSelf)
        {
            settings.SetActive(false);
            Time.timeScale = 1f;
        }
        else
        {
            settings.SetActive(true);
            if ((int)currentChapter != 0) homeButton.SetActive(true);
            else homeButton.SetActive(false);
            Time.timeScale = 0f;
        }
    }

    public void BackToMainMenu()
    {
        foreach (GameObject area in GameArea)
        {
            if (area.activeSelf)
            {
                area.GetComponent<FirstChapter>()?.StopAllCoroutines();
                area.GetComponent<SecondChapter>()?.StopAllCoroutines();
                area.GetComponent<ThirdChapter>()?.StopAllCoroutines();
                area.GetComponent<FourthChapter>()?.StopAllCoroutines();
                area.GetComponent<FifthChapter>()?.StopAllCoroutines();
            }
        }
        Time.timeScale = 1f;
        settings.SetActive(false);
        StartCoroutine(LoadChapter(GameState.MainMenu));
        AudioHandler.instance.PlayMusic("MainMenu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public IEnumerator LoadChapter(GameState chapter)
    {
        currentChapter = chapter;
        TransitionManager.instance.FadeOut();
        yield return new WaitForSeconds(1f);
        // Deactivate all game areas
        foreach (GameObject area in GameArea)
        {
            area.SetActive(false);
        }
        // Activate the selected chapter's game area
        switch (chapter)
        {
            case GameState.MainMenu:
                GameArea[0].SetActive(true);
                break;
            case GameState.Chapter_1:
                GameArea[1].SetActive(true);
                break;
            case GameState.Chapter_2:
                GameArea[2].SetActive(true);
                break;
            case GameState.Chapter_3:
                GameArea[3].SetActive(true);
                break;
            case GameState.Chapter_4:
                GameArea[4].SetActive(true);
                break;
            case GameState.Chapter_5:
                GameArea[5].SetActive(true);
                break;
            default:
                Debug.LogError("Invalid chapter selected.");
                break;
        }
        PlayerPrefs.SetInt("CurrentChapter", (int)chapter);
        Debug.Log("Loading Chapter " + (int)chapter);
        TransitionManager.instance.FadeIn();
        yield return new WaitForSeconds(1f);
    }
}
