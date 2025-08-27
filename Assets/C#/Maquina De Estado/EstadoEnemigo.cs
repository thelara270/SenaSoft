public abstract class EstadoEnemigo
{
    // Referencia al enemigo que posee este estado
    protected ControladorEnemigo enemigo;

    // Constructor: recibe al enemigo due�o de este estado
    public EstadoEnemigo(ControladorEnemigo enemigo)
    {
        this.enemigo = enemigo;
    }

    // M�todo que se llama una vez al entrar al estado
    public abstract void Enter();

    // M�todo que se llama cada frame mientras el estado est� activo
    public abstract void Update();

    // M�todo que se llama una vez al salir del estado
    public abstract void Exit();
}