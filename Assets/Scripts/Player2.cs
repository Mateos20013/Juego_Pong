using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour
{
    [SerializeField]
    float velocidadJugador;

    private Rigidbody2D rb;
    private Vector2 direccionJugador;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float direccionY = Input.GetAxisRaw("Vertical1");
        direccionJugador = new Vector2(0, direccionY).normalized;
    }

    void FixedUpdate()
    {
        rb.velocity = direccionJugador * velocidadJugador;
    }
}
