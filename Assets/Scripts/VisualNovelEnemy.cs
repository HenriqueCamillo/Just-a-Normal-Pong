using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using MyBox;

public class VisualNovelEnemy : MonoBehaviour
{
    [SerializeField, MinMaxRange(1f, 20f)] RangedInt coughFrequency;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Invoke(nameof(Cough), Random.Range(coughFrequency.Min, coughFrequency.Max));
    }

    void Cough()
    {
        animator.SetTrigger("Cough");
        Invoke(nameof(Cough), Random.Range(coughFrequency.Min, coughFrequency.Max));
    }
}
