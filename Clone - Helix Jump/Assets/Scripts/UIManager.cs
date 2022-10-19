using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    #region ScreensAndButton
    [Header("Screens And Button")]
    [SerializeField] private GameObject _finishScreen;
    [SerializeField] private GameObject _restartLevelButton;
    [SerializeField] private GameObject _nextLevelButton;
    #endregion
    [Space]

    #region  ScoreEvent
    [Header("Score")]
    [Space]
    [SerializeField] private TextMeshProUGUI _score;
    [SerializeField] private TextMeshProUGUI _scoreAnim;
    [SerializeField] private TextMeshProUGUI _bestScore;
    private int _scoreValue;
    #endregion

    [Space]
    [SerializeField] private Canvas _canvas;
    [SerializeField] private TextMeshProUGUI _fpsCounter;
    [SerializeField] private TextMeshProUGUI _levelCounter;
    private int _fpsf;
    private int _levelcount;

    public static UIManager Instance;
    [SerializeField] private LevelEditor _leveleditor;

    private void Awake()
    {
        LevelEditor.Instance = _leveleditor;
        Instance = this;
    }

    private void Start()
    {
        _levelcount += _leveleditor.CurrentLevelIndex + 1;
        _levelCounter.text = "Level " + _levelcount;
        _bestScore.text = "BEST :" + PlayerPrefs.GetInt("BestScore");
    }

    void Update()
    {
        FpsCounter();
        SetBestScoreAndScore();
    }

    private void FpsCounter()
    {
        _fpsf = (int)(1f / Time.unscaledDeltaTime);
        _fpsCounter.SetText($"Fps : {_fpsf}");
    }

    public void RestartGame()
    {
        _leveleditor.RestartGame();
    }

    public void NextLevel()
    {
        _leveleditor.NextLevel();
    }

    public void SetScore(int value)
    {
        _scoreValue += value;
        ScoreAnimInstantate(value);
    }

    private void SetBestScoreAndScore()
    {
        _score.text = $" {_scoreValue}";

        if (_scoreValue > PlayerPrefs.GetInt("BestScore"))
        {
            PlayerPrefs.SetInt("BestScore", _scoreValue);
            int bestScore = PlayerPrefs.GetInt("BestScore");
            _bestScore.text = "BEST :" + bestScore.ToString();
        }
    }

    public void FinishScreen(bool isDie)
    {
        if (isDie)
        {
            _finishScreen.SetActive(true);
            _nextLevelButton.SetActive(true);
        }
        else
        {
            _finishScreen.SetActive(true);
            _restartLevelButton.SetActive(true);
        }
    }

    private void ScoreAnimInstantate(int value)
    {

        TextMeshProUGUI scoreAnimPrefab = Instantiate(_scoreAnim);
        scoreAnimPrefab.text = "+" + value;
        scoreAnimPrefab.transform.SetParent(_canvas.transform);
        scoreAnimPrefab.transform.localPosition = new Vector3(45, 0, 0);
        StartCoroutine(CO_ScoreAnimSetActive(scoreAnimPrefab));
    }

    IEnumerator CO_ScoreAnimSetActive(TextMeshProUGUI currentanim)
    {
        yield return new WaitForSeconds(0.4f);
        currentanim.gameObject.SetActive(false);
    }
}