using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;

    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("Missing UIManager");

            return _instance;
        }
    }
    [SerializeField]
    private Text _coinText;

    [SerializeField]
    private Text _livesText;

    [SerializeField]
    private Text _elevetorText;

    [SerializeField]
    private Text _deathText;

    //[SerializeField]
    //private GameObject _pauseMenu;

    private void Awake()
    {
        _instance = this;
    }

    public void RestartGame()
    {
        //SceneManager.LoadScene("Loading");
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void TogglePause()
    {
        Time.timeScale = 1f - Time.timeScale;
        //_pauseMenu.SetActive(!_pauseMenu.activeInHierarchy);
    }

    public void UpdateCoinText(int val)
    {
        _coinText.text = "x " + val.ToString("000");
    }

    public void UpdateLivesText(int val)
    {
        _livesText.text = "x " + val.ToString("00");
    }

    public void UpdateElevatorText(bool isOk)
    {
        _elevetorText.text = isOk ?
        @"Press 'E' to call the Elevator" :
        "Not enough coins to call the Elevator!";
    }

    public void SetElevatorText(string s)
    {
        _elevetorText.text = s;
    }

    private IEnumerator DeathDisplayRoutine()
    {
        Color textColor = _deathText.color;
        for (float a = 1; a >= 0; a -= 0.02f)
        {
            Color newColor = new Color(textColor.r, textColor.g, textColor.b, a);
            _deathText.color = newColor;
            yield return new WaitForSeconds(0.025f);
        }
    }
}
