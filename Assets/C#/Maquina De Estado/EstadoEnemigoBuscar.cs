using UnityEngine;

// Estado en el que el enemigo busca al jugador tras haberlo perdido de vista
public class EstadoEnemigoBuscar : EstadoEnemigo
{
    private float tiempoRestante; // Temporizador para cuánto tiempo buscar al jugador

    // Constructor: recibe el enemigo y lo pasa a la clase base
    public EstadoEnemigoBuscar(ControladorEnemigo enmigo) : base(enmigo) { }

    // Se ejecuta una vez al entrar en este estado
    public override void Enter()
    {
        // Cambia animacion a "buscar"
        //enemigo.animator.SetInteger("Estado", 0);

        // Inicializa el tiempo de búsqueda según lo definido en el controlador
        tiempoRestante = enemigo.tiempoBusqueda;
    }

    // Se ejecuta cada frame mientras el enemigo está en búsqueda
    public override void Update()
    {
        // Resta tiempo al temporizador de búsqueda
        tiempoRestante -= Time.deltaTime;

        // Si el jugador vuelve a estar dentro del rango de visión, vuelve al estado de persecución
        if (Vector3.Distance(enemigo.transform.position, enemigo.jugador.position) < enemigo.rangoVision)
        {
            enemigo.ChangeState(enemigo.GetChaseState());
        }
        // Si se termina el tiempo de búsqueda sin encontrar al jugador, regresar al punto de origen
        else if (tiempoRestante <= 0)
        {
            //enemigo.ChangeState(enemigo.GetReturnState());
        }
    }

    // Se ejecuta al salir del estado de búsqueda
    public override void Exit()
    {
    }
}
