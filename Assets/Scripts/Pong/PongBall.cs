using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

[RequireComponent(typeof(Rigidbody2D))]
public class PongBall : MonoBehaviour
{
    [AutoProperty, SerializeField, HideInInspector] Rigidbody2D rb;
    [SerializeField] PongScore scoreBoard;
    [Tag, SerializeField] string goal1Tag, goal2Tag;
    [SerializeField] float speed;
    private bool inGame;
    private Vector3 center;



    private void Awake()
    {
        center = this.transform.position;  

        if (scoreBoard == null)
            scoreBoard = FindObjectOfType<PongScore>();
    }

    public void Throw()
    {
        Vector2 rand = Random.insideUnitCircle.normalized;
        while (rand.x < 0.5f) 
            rand = Random.insideUnitCircle.normalized;

        rb.velocity = rand * speed;
        inGame = true;
    }

    private void Update()
    {
        if (!inGame && Input.GetKeyDown(KeyCode.Space))
            Throw();
    }

    private void Reset()
    {
        rb.velocity = Vector2.zero;
        this.transform.position = center;
        inGame = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(goal1Tag))
        {
            scoreBoard.SetGoal(1);
            Reset();
        }
        else if (other.CompareTag(goal2Tag))
        {
            scoreBoard.SetGoal(2);
            Reset();
        }
    }
}
