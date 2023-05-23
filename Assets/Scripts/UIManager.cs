using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static Action<bool> GameEndedEvent;

    [SerializeField] private TextMeshProUGUI movesText;
    [SerializeField] private TextMeshProUGUI goalText;
    private int moves;
    private int goal;
    [Space]
    [SerializeField] private RectTransform popup;
    [SerializeField] private TextMeshProUGUI resultMessage;


    private void OnEnable()
    {
        GameManager.StartGameEvent += Setup;
        TouchManager.MoveMadeEvent += UpdateUI;
    }

    private void OnDisable()
    {
        GameManager.StartGameEvent -= Setup;
        TouchManager.MoveMadeEvent -= UpdateUI;
    }

    public void Setup(int moves, int goal)
    {
        this.goal = goal;
        this.goalText.text = goal.ToString();
        this.moves = moves;
        this.movesText.text = moves.ToString();
    }

    public void UpdateUI(int amount)
    {
        StartCoroutine(Score(amount));
    }

    private IEnumerator Score(int amount)
    {
        moves--;
        movesText.text = (moves).ToString();
        LeanTween.scale(movesText.gameObject, Vector3.one * 2, 1.5f).setEase(LeanTweenType.punch);

        LeanTween.scale(goalText.gameObject, Vector3.one * 2f, 0.1f*amount).setEase(LeanTweenType.punch);
        for (int i = 0; i < amount; i++)
        {
            goal--;
            goalText.text = (goal).ToString();

            if(goal <= 0)
            {
                goal = 0;
                SendGameEndedEvent(true);
                break;
            }
            yield return new WaitForSeconds(0.05f);
        }
        if(moves <= 0 && goal > 0)
            SendGameEndedEvent(false);
    }

    private void SendGameEndedEvent(bool win)
    {
        popup.gameObject.SetActive(true);
        resultMessage.text = win ? "You WON!" : "All out<br>of moves...";
        GameEndedEvent?.Invoke(win);
    }

    public void Restart()
    {
        GameManager.Instance.Restart();
        popup.gameObject.SetActive(false);
    }
}
