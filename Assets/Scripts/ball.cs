using UnityEngine;

public class ball : MonoBehaviour
{
    [SerializeField] float velocidadInicial = 5f;
    [SerializeField] float incrementoVelocidad = 1.1f;
    [SerializeField] float velocidadMaxima = 10f;

    [SerializeField] private Transform player1Transform;
    [SerializeField] private Transform player2Transform;
    [SerializeField] private Transform BallTransform;

    private Rigidbody2D rb;
    private float velocidadActual;
    private int direccionActualX;

    private Vector3 player1StartPos;
    private Vector3 player2StartPos;
    private Vector3 ballStartPos;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        velocidadActual = velocidadInicial;

        // Guardar posiciones iniciales
        player1StartPos = player1Transform.position;
        player2StartPos = player2Transform.position;
        ballStartPos = BallTransform.position;

        Invoke("LanzarPelota", 1f);
    }

    void LanzarPelota()
    {
        direccionActualX = Random.value < 0.5f ? -1 : 1;
        Vector2 direccion = new Vector2(direccionActualX, Random.Range(-0.5f, 0.5f)).normalized;
        rb.velocity = direccion * velocidadActual;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Raqueta"))
        {
            float y = (transform.position.y - collision.transform.position.y) / collision.collider.bounds.size.y;
            y = Mathf.Clamp(y, -0.75f, 0.75f);

            Vector2 nuevaDireccion = new Vector2(rb.velocity.x > 0 ? 1 : -1, y).normalized;

            velocidadActual = Mathf.Min(velocidadActual + incrementoVelocidad, velocidadMaxima);
            rb.velocity = nuevaDireccion * velocidadActual;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Colisión con: " + collision.gameObject.tag);

        if (collision.CompareTag("LeftWall"))
        {
            GameManager.Instance.player1score();
        }
        else if (collision.CompareTag("RightWall"))
        {
            GameManager.Instance.player2score();
        }

        GameManager.Instance.ResetGame();
        ResetGame();
        Invoke("LanzarPelota", 1f);
    }

    public void ResetGame()
    {
        player1Transform.position = player1StartPos;
        player2Transform.position = player2StartPos;
        BallTransform.position = ballStartPos;
        rb.velocity = Vector2.zero;
        velocidadActual = velocidadInicial;
    }
}
