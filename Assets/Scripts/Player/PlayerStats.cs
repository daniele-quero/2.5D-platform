using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStats : MonoBehaviour
{
    private int _coins = 0;
    private int _lives = 2;

    private void Start()
    {
        UIManager.Instance.UpdateLivesText(_lives);
        UIManager.Instance.UpdateCoinText(_coins);
    }

    public int Coins { get => _coins;}
    public int Lives { get => _lives; set => _lives = value; }

    public void AddCoin(int c)
    {
        _coins += c;
        UIManager.Instance.UpdateCoinText(_coins);
    }

    public void AddLife(int l)
    {
        _lives += l;
        UIManager.Instance.UpdateLivesText(_lives);
    }
}
