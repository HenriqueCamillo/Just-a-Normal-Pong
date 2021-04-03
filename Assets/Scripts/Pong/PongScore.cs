using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MyBox;

public class PongScore : MonoBehaviour
{
    public int p1, p2;
    [AutoProperty, SerializeField] TextMeshProUGUI score;

    private void Awake()
    {
        score.SetText($"{p1} x {p2}");
    }

    
    public void SetGoal(int player)
    {
        if (player == 1)
            p1++;
        else if (player == 2)
            p2++;

        score.SetText($"{p1} x {p2}");
    }
}
