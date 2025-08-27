using UnityEngine;

// Estado en el que el enemigo persigue al jugador
public class EstadoEnemigoPerseguir : EstadoEnemigo
{
    // Constructor que recibe el controlador del enemigo y lo pasa al estado base
    public EstadoEnemigoPerseguir(ControladorEnemigo enemigo) : base(enemigo) { }

    // Se ejecuta una vez al entrar en este estado
    public override void Enter()
    {
        // Cambia la animacion a "correr"
        //enemigo.animator.SetInteger("Estado", 2);
    }

    // Se ejecuta en cada frame mientras el enemigo esta persiguiendo
    public override void Update()
    {
        // Calcula la dirección desde el enemigo hacia el jugador y la normaliza
        Vector3 direccion = (enemigo.jugador.position - enemigo.transform.position).normalized;
        direccion.y = 0f; // Para mantener la rotación horizontal

        // Rota al enemigo suavemente hacia el jugador
        if (direccion != Vector3.zero)
        {
            Quaternion rotacionDeseada = Quaternion.LookRotation(direccion); // Hacia dónde mirar
            enemigo.transform.rotation = Quaternion.Slerp(
                enemigo.transform.rotation,     // Rotación actual
                rotacionDeseada,                // Rotación deseada
                Time.deltaTime * 5f             // Suavidad de rotación
            );
        }

        // Mueve al enemigo hacia el jugador
        enemigo.transform.position = Vector3.MoveTowards(
            enemigo.transform.position,        // Posición actual
            enemigo.jugador.position,          // Objetivo: posición del jugador
            enemigo.velocidad * Time.deltaTime // Velocidad por frame
        );

        // Calcula la distancia actual al jugador
        float distancia = Vector3.Distance(enemigo.transform.position, enemigo.jugador.position);

        // Si el jugador se aleja más allá del rango de visión, cambiar al estado de búsqueda
        if (distancia > enemigo.rangoVision + 2f)
        {
            enemigo.ChangeState(enemigo.GetSearchState());
        }
    }

    // Se ejecuta al salir del estado
    public override void Exit() { }
}