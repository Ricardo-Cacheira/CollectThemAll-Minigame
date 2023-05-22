using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI movesText;
    [SerializeField] private TextMeshProUGUI goalText;
    private int moves;
    private int goal;

    private void Start() {
        Setup(15,60);
    }

    public void Setup(int moves, int goal)
    {
        this.goal = goal;
        this.goalText.text = goal.ToString();
        this.moves = moves;
        this.movesText.text = moves.ToString();
    }

    [ContextMenu("TestUI")]
    public void Test()
    {
        UpdateUI(30);
    }

    public void UpdateUI(int amount)
    {
        StartCoroutine(Score(amount));
    }

    private IEnumerator Score(int amount)
    {
        moves--;
        movesText.text = (moves).ToString();
        //TODO Tween Animation

        for (int i = 0; i < amount; i++)
        {
            goal--;
            goalText.text = (goal).ToString();
            //TODO Tween Animation
            yield return new WaitForSeconds(0.1f);
        }
    }

}
