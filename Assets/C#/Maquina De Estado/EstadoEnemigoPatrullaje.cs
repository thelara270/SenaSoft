using UnityEngine;

// Clase que representa el estado de patrullaje de un enemigo
public class EstadoEnemigoPatrullaje : EstadoEnemigo
{
    // Constructor que recibe el enemigo y lo pasa al constructor de la clase base
    public EstadoEnemigoPatrullaje(ControladorEnemigo enemigo) : base(enemigo) { }

    // Metodo que se llama cuando el enemigo entra en este estado
    public override void Enter()
    {
        // Cambia el parametro del Animator para activar la animación de caminar
        enemigo.animator.SetInteger("Estado", 1); // Animación de caminar
    }

    // Metodo que se ejecuta cada frame mientras el enemigo está patrullando
    public override void Update()
    {
        // Si no hay puntos de patrulla, salir del método
        if (enemigo.puntosPatrulla.Length == 0) return;

        // Obtiene el punto actual al que debe patrullar
        Transform destino = enemigo.puntosPatrulla[enemigo.indicePatrulla];

        // Calcula la dirección hacia el punto de patrulla
        Vector3 direccion = (destino.position - enemigo.transform.position).normalized;
        direccion.y = 0f; // Se asegura de no mirar hacia arriba o abajo y solo rotación horizontal

        // Rota al enemigo suavemente hacia la dirección del punto
        if (direccion != Vector3.zero)
        {
            Quaternion rotacionDeseada = Quaternion.LookRotation(direccion); // Dirección deseada
            enemigo.transform.rotation = Quaternion.Slerp(
                enemigo.transform.rotation,     // Rotación actual
                rotacionDeseada,                // Rotación deseada
                Time.deltaTime * 3f             // Velocidad de rotación
            );
        }

        // Mueve al enemigo hacia el punto de patrulla
        enemigo.transform.position = Vector3.MoveTowards(
            enemigo.transform.position,       // Posición actual
            destino.position,                 // Posición destino
            enemigo.velocidad * Time.deltaTime // Velocidad de movimiento
        );

        // Si está suficientemente cerca al destino, pasa al siguiente punto
        if (Vector3.Distance(enemigo.transform.position, destino.position) < 0.1f)
        {
            enemigo.indicePatrulla = (enemigo.indicePatrulla + 1) % enemigo.puntosPatrulla.Length;
            // Reinicia a 0 si llega al final (patrulla en bucle)
        }

        // Si el jugador está dentro del rango de visión, cambia al estado de persecución
        if (Vector3.Distance(enemigo.transform.position, enemigo.jugador.position) < enemigo.rangoVision)
        {
            enemigo.ChangeState(enemigo.GetChaseState());
        }
    }

    // Método llamado al salir de este estado
    public override void Exit() { }
}