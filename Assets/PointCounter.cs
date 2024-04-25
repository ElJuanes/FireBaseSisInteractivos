using UnityEngine;

public class PointCounter : MonoBehaviour
{
    private ScoreDisplay scoreDisplay;
    private int points = 0;
    private int numTurns = 0;
    private float startTime = 0f;
    private bool counting = false;
    private bool canClick = true;

    void Start()
    {
        scoreDisplay = GameObject.FindObjectOfType<ScoreDisplay>();
    }
    public void ActivarScript()
     {
            // Activa este script
            this.enabled = true;
            Debug.Log("El script ha sido activado.");
     }
    public void DesactivarScript()
    {
        
        this.enabled = false;
        Debug.Log("El script ha sido desactivado.");
    }
    void Update()
    {
        if (canClick && Input.GetMouseButtonDown(0))
        {
            if (!counting)
            {
                startTime = Time.time;
                counting = true;
            }
            else
            {
                if (Time.time - startTime < 10f && points < 100)
                {
                    points++;
                    Debug.Log("Puntos: " + points);
                    scoreDisplay.UpdateScoreText(points);
                }
                else
                {
                    counting = false;
                    canClick = false;
                    Invoke("EnableClick", 2f); // Permitir clics nuevamente después de 2 segundos
                    Debug.Log("Tiempo Finalizado");
                    Debug.Log(numTurns + "Vuelta: Reiniciando...");
                    startTime = Time.time;
                    points = 0;
                    numTurns = 0;
                }
            }
        }
    }

    void EnableClick()
    {
        canClick = true;
    }
}

   /* void Update()
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
                if (Time.time - startTime < 10f && points < 100)
                {
                    points++;
                    Debug.Log("Puntos: " + points);
                }

                else
                {
                    Debug.Log("TiempoFinalizado");
                    if (Time.time - startTime >= 10f || points >= 100)
                    {
                        numTurns++;
                    }
                    startTime = Time.time;
                    points = 0;
                    Debug.Log(numTurns + "Vuelta: Reinciando");
                }
            }
        }
    }*/
