using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Food : MonoBehaviour
{
  
    [SerializeField] ScoreManager Score;

    public BoxCollider foodSpawn;

    public float score;
    public TextMeshProUGUI ScoreText;

    private void Start()
    {
        RandomPose();
    }

    private void Update()
    {
        ScoreText.text = "" + score;
    }

    private void RandomPose()
    {
        Bounds bounds = this.foodSpawn.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            RandomPose();
            score += 1;
            Score.AddScore();
        }
    }
}
