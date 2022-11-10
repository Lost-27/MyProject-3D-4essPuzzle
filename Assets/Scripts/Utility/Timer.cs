using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private float _secondTime;
    [SerializeField] private Text _textDynamic;
    [SerializeField] private GameObject _board;
    [SerializeField] private GameObject _innerContainer;
    


    private void Update()
    {
        if (!_board.activeInHierarchy)
        {
            _innerContainer.SetActive(true);
            if (_secondTime > 0)
            {
                _secondTime -= Time.deltaTime;
            }
            else
            {
                _secondTime = 0;
            }

            DisplayTime(_secondTime);
        }
        
    }

    private void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        _textDynamic.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
