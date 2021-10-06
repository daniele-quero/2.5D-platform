using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerStats : MonoBehaviour
{
    private int _coins = 0;
    [SerializeField]
    private int _lives = 2;

    private void Start()
    {
        UIManager.Instance.UpdateLivesText(_lives);
        UIManager.Instance.UpdateCoinText(_coins);
    }

    public int Coins { get => _coins; }
    public int Lives { get => _lives; set => _lives = value; }

    public void AddCoin(int c)
    {
        _coins += c;
        UIManager.Instance.UpdateCoinText(_coins);
    }

    public void AddLife(int l)
    {
        _lives += l;
        if (_lives > -1)
        {
            UIManager.Instance.UpdateLivesText(_lives);
            GetComponent<PlayerMovement>().Respawn();
        }
        else
            SceneManager.LoadScene(0);
    }
}
