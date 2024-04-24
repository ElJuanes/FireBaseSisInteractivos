using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Snake : MonoBehaviour
{
    public float moveSpeed;
    private Rigidbody rb;

    [SerializeField] LoseManager Lose;
    

    private List<Transform> _SnakeSpawn;
    public Transform snakePrefab;

    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        _SnakeSpawn = new List<Transform>();
        _SnakeSpawn.Add(this.transform);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            rb.velocity = new Vector3(0, moveSpeed, 0);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            rb.velocity = new Vector3(0, -moveSpeed, 0);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            rb.velocity = new Vector3(moveSpeed, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            rb.velocity = new Vector3(-moveSpeed, 0, 0);
        }
    }

    private void FixedUpdate()
    {
        for (int i = _SnakeSpawn.Count - 1; i > 0; i--)
        {
            _SnakeSpawn[i].position = _SnakeSpawn[i - 1].position;
        }
    }

    private void grow()
    {
        Transform snakeSpawn = Instantiate(this.snakePrefab);
        snakeSpawn.position = _SnakeSpawn[_SnakeSpawn.Count - 1].position;

        _SnakeSpawn.Add(snakeSpawn);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Food")
        {
            grow();
        }

        if (collision.gameObject.tag == "Wall")
        {
            Lose.Fabiola();
            

        }
    }


}
