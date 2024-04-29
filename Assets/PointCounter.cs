using UnityEngine;
using TMPro;
public class PointCounter : MonoBehaviour
{
    private ScoreDisplay scoreDisplay;
    private int points = 0;
    private int numTurns = 0;
    private float startTime = 0f;
    private bool counting = false;
    private bool canClick = true;
    private static FirebaseManager firebaseManager;

    private string nombreUsuario;
    private bool estaJugando;
    public static PointCounter instance;

    void Start()
    {
        if (scoreDisplay == null)
        {
            scoreDisplay = GameObject.FindObjectOfType<ScoreDisplay>(); // Use FindObjectOfType if not directly referenced
        }
        scoreDisplay = GameObject.FindObjectOfType<ScoreDisplay>();
        GameObject uiObject = GameObject.Find("UI");
        if (uiObject == null)
        {
            Debug.LogError("No se encontró el objeto 'UI' en la escena.");
            return;
        }
        firebaseManager = uiObject.GetComponentInChildren<FirebaseManager>();
        if (firebaseManager == null)
        {
            Debug.LogError("No se encontró un componente FirebaseManager en el objeto 'UI'.");
            return;
        }

        // Obtener la referencia a ScoreDisplay
        scoreDisplay = FindObjectOfType<ScoreDisplay>();
    }
    private void Awake()
    {
        // Asignar esta instancia al inicio del juego
        instance = this;
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
                    Invoke("EnableClick", 2f);
                    Debug.Log("Tiempo Finalizado");
                    UpdateScoreInFirebase(points);
                    Debug.Log(numTurns + "Vuelta: Reiniciando...");
                    startTime = Time.time;
                    points = 0;
                    numTurns = 0;
                }
                if (scoreDisplay != null) // Check if 'scoreDisplay' is not null before accessing it
                {
                    scoreDisplay.UpdateScoreText(points);
                }
                else
                {
                    Debug.LogError("ScoreDisplay reference is null. Please ensure it is set correctly.");
                }
            }

        }
    }
    void UpdateScoreInFirebase(int score)
    {
        // Asegúrate de tener una referencia a la base de datos en tu FirebaseManager
        // Supongamos que tienes una referencia llamada "dbReference"
        if (FirebaseManager.instance != null && FirebaseManager.instance.dbReference != null && FirebaseManager.instance.user != null)
        {
            // Guarda la puntuación del usuario en la base de datos
            FirebaseManager.instance.dbReference.Child("users").Child(FirebaseManager.instance.user.UserId).Child("score").SetValueAsync(score)
                .ContinueWith(task =>
                {
                    if (task.IsFaulted || task.IsCanceled)
                    {
                        // Maneja cualquier error aquí
                        Debug.LogError("Error al guardar la puntuación en Firebase: " + task.Exception);
                    }
                    else
                    {
                        // La puntuación se guardó correctamente
                        Debug.Log("Puntuación guardada en Firebase: " + score);
                    }
                });
        }
        else
        {
            Debug.LogWarning("No se pudo guardar la puntuación en Firebase: referencia nula");
        }
    }

    public int GetPoints()
    {
        return points;
    }

    void EnableClick()
    {
        canClick = true;
    }
    public void UpdatePoints(int newPoints)
    {
        points = newPoints;
    }
    public void ActualizarNombreUsuario(string nombre)
    {
        nombreUsuario = nombre;
        Debug.Log("Nombre de usuario actualizado: " + nombreUsuario);
    }
    public void ActualizarEstadoJugador(bool estado)
    {
        estaJugando = estado;
        Debug.Log("Estado del jugador actualizado: " + estaJugando);
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
