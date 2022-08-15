using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    #region Variables
    [SerializeField] private int _score;
    #endregion
    #region Properties
    public int Score { get => _score; private set => _score = value; }
    #endregion
    
    public void IncreaseScore(int amount)
    {
        Score += amount;
    }
}
