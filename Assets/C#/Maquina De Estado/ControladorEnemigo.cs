using UnityEngine;
public class ControladorEnemigo : MonoBehaviour
{
    private EstadoEnemigo estadoActual;

    [HideInInspector] public Animator animator;

    // Instancias de los estados
    private EstadoEnemigoPatrullaje estadoPatrullaje;
    private EstadoEnemigoPerseguir estadoPerseguir;
    private EstadoEnemigoBuscar estadoBuscar;
    //private EstadoEnemigoVolver estadoVolver;

    [Header("Referencias")]
    public Transform jugador;              // Transform del jugador
    public Transform[] puntosPatrulla;     // Puntos entre los que patrulla el enemigo

    [Header("Movimiento")]
    public float velocidad = 2f;           // Velocidad de movimiento
    public float rangoVision = 5f;         // Rango de visi�n para detectar al jugador
    public float tiempoBusqueda = 3f;      // Tiempo que busca antes de rendirse

    [HideInInspector] public Vector3 posicionInicio; // Guarda la posici�n original
    [HideInInspector] public int indicePatrulla = 0; // �ndice del punto actual de patrulla

    void Start()
    {
        //animator = GetComponent<Animator>();

        posicionInicio = transform.position; // Guarda posici�n inicial

        // Crea instancias de todos los estados, pas�ndose a s� mismo como referencia
        estadoPatrullaje = new EstadoEnemigoPatrullaje(this);
        estadoPerseguir = new EstadoEnemigoPerseguir(this);
        estadoBuscar = new EstadoEnemigoBuscar(this);
        //estadoVolver = new EstadoEnemigoVolver(this);

        // Comienza en el estado de patrulla
        ChangeState(estadoPatrullaje);
    }

    void Update()
    {
        // Llama al m�todo Update() del estado actual
        estadoActual?.Update();
    }

    // Cambia el estado actual por uno nuevo
    public void ChangeState(EstadoEnemigo newState)
    {
        estadoActual?.Exit();      // Llama a Exit() del estado anterior si existe
        estadoActual = newState;   // Cambia al nuevo estado
        estadoActual.Enter();      // Llama a Enter() del nuevo estado
    }

    // M�todos p�blicos para acceder a los estados desde otros scripts
    public EstadoEnemigoPatrullaje GetPatrolState() => estadoPatrullaje;
    public EstadoEnemigoPerseguir GetChaseState() => estadoPerseguir;
    public EstadoEnemigoBuscar GetSearchState() => estadoBuscar;
    //public EstadoEnemigoVolver GetReturnState() => estadoVolver;
}