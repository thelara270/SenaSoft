using UnityEngine;

// Clase que representa el estado de patrullaje de un enemigo
public class EstadoEnemigoPatrullaje : EstadoEnemigo
{
    // Constructor que recibe el enemigo y lo pasa al constructor de la clase base
    public EstadoEnemigoPatrullaje(ControladorEnemigo enemigo) : base(enemigo) { }

    // Metodo que se llama cuando el enemigo entra en este estado
    public override void Enter()
    {
        // Cambia el parametro del Animator para activar la animaci�n de caminar
        enemigo.animator.SetInteger("Estado", 1); // Animaci�n de caminar
    }

    // Metodo que se ejecuta cada frame mientras el enemigo est� patrullando
    public override void Update()
    {
        // Si no hay puntos de patrulla, salir del m�todo
        if (enemigo.puntosPatrulla.Length == 0) return;

        // Obtiene el punto actual al que debe patrullar
        Transform destino = enemigo.puntosPatrulla[enemigo.indicePatrulla];

        // Calcula la direcci�n hacia el punto de patrulla
        Vector3 direccion = (destino.position - enemigo.transform.position).normalized;
        direccion.y = 0f; // Se asegura de no mirar hacia arriba o abajo y solo rotaci�n horizontal

        // Rota al enemigo suavemente hacia la direcci�n del punto
        if (direccion != Vector3.zero)
        {
            Quaternion rotacionDeseada = Quaternion.LookRotation(direccion); // Direcci�n deseada
            enemigo.transform.rotation = Quaternion.Slerp(
                enemigo.transform.rotation,     // Rotaci�n actual
                rotacionDeseada,                // Rotaci�n deseada
                Time.deltaTime * 3f             // Velocidad de rotaci�n
            );
        }

        // Mueve al enemigo hacia el punto de patrulla
        enemigo.transform.position = Vector3.MoveTowards(
            enemigo.transform.position,       // Posici�n actual
            destino.position,                 // Posici�n destino
            enemigo.velocidad * Time.deltaTime // Velocidad de movimiento
        );

        // Si est� suficientemente cerca al destino, pasa al siguiente punto
        if (Vector3.Distance(enemigo.transform.position, destino.position) < 0.1f)
        {
            enemigo.indicePatrulla = (enemigo.indicePatrulla + 1) % enemigo.puntosPatrulla.Length;
            // Reinicia a 0 si llega al final (patrulla en bucle)
        }

        // Si el jugador est� dentro del rango de visi�n, cambia al estado de persecuci�n
        if (Vector3.Distance(enemigo.transform.position, enemigo.jugador.position) < enemigo.rangoVision)
        {
            enemigo.ChangeState(enemigo.GetChaseState());
        }
    }

    // M�todo llamado al salir de este estado
    public override void Exit() { }
}