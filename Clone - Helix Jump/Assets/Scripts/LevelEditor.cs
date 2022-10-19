using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEditor : MonoBehaviour
{
    [SerializeField] private List<GameObject> _allLevel;
    public int CurrentLevelIndex = 1;
    [SerializeField] private int level;

    public static LevelEditor Instance;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        Instance = this;
        CurrentLevelIndex = PlayerPrefs.GetInt("currentLevelIndex");
        BallFollower.Instance.SetCameraBackroundColor(PlayerPrefs.GetInt("currentLevelIndex"));
        _allLevel[CurrentLevelIndex].SetActive(true);
    }

    public void NextLevel()
    {
        _allLevel[CurrentLevelIndex].SetActive(false);

        if (CurrentLevelIndex == _allLevel.Count - 1)
        {
            CurrentLevelIndex = -1;
        }

        CurrentLevelIndex++;
        _allLevel[CurrentLevelIndex].SetActive(true);
        SceneManager.LoadScene(0);
        PlayerPrefs.SetInt("currentLevelIndex", CurrentLevelIndex);
        BallFollower.Instance.SetCameraBackroundColor(PlayerPrefs.GetInt("currentLevelIndex"));
    }

    public void SelectLevel(int value)
    {
        _allLevel[CurrentLevelIndex].SetActive(false);
        CurrentLevelIndex = value;
        _allLevel[CurrentLevelIndex].SetActive(true);
        PlayerPrefs.SetInt("currentLevelIndex", CurrentLevelIndex);
        SceneManager.LoadScene(0);
    }

    public void values()
    {
        SelectLevel(level);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}