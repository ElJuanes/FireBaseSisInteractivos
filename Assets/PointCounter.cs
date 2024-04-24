using UnityEngine;

public class PointCounter : MonoBehaviour
{
    private int points = 0;
    private float startTime = 0f;
    private bool counting = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!counting)
            {
                startTime = Time.time;
                counting = true;
            }
            else
            {
                if (Time.time - startTime < 10f && points < 30)
                {
                    points++;
                    Debug.Log("Puntos: " + points);
                }
                else
                {
                    // Restablecer el tiempo y el contador de puntos
                    startTime = Time.time;
                    points = 0;
                    Debug.Log("Límite de tiempo alcanzado o máximo de puntos alcanzado. Reiniciando...");
                }
            }
        }
    }
}