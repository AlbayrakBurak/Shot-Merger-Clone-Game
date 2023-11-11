using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance => _instance ?? (_instance = FindObjectOfType<GameManager>());

    private bool _isGameStarted = false;
    private bool _isGameOver = false;

    [SerializeField] private GameObject _gameStartUI, _failedPanel, _successPanel;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool IsGameStarted()
    {
        return _isGameStarted;
    }

    public bool IsGameOver()
    {
        return _isGameOver;
    }

    public void OnGameStart()
    {
        _isGameStarted = true;
        DeactivateGameStartUI();
    }

    public void OnGameFailed()
    {
        _isGameOver = true;
        ActivatePanel(_failedPanel);
    }

    public void OnGameFinished()
    {
        _isGameOver = true;
        ActivatePanel(_successPanel);
    }

    private void DeactivateGameStartUI()
    {
        _gameStartUI.SetActive(false);
    }

    private void ActivatePanel(GameObject panel)
    {       
        panel.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
