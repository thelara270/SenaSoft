using UnityEngine;
using UnityEngine.UIElements;

public class MovimientoJugador : MonoBehaviour
{
    [SerializeField] float velocidad = 10f;
    [SerializeField] float velocidadCorrer = 15f;
    [SerializeField] float sensibilidad = 1;

    private Rigidbody rb;
    private Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
    }

    private void Update()
    {
        Caminar();
        GirarConMouse();
    }

    public void Caminar()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direccion = transform.right * horizontal + transform.forward * vertical;

        float velocidadFinal = Input.GetKey(KeyCode.LeftShift) ? velocidadCorrer : velocidad;

        Vector3 movimiento = direccion.normalized * velocidadFinal;

        rb.linearVelocity = new Vector3(movimiento.x, rb.linearVelocity.y, movimiento.z);

        float velocidadActual = new Vector2(rb.linearVelocity.x, rb.linearVelocity.z).magnitude;

        animator.SetFloat("Velocidad", velocidadActual);
        animator.SetBool("Corriendo", Input.GetKey(KeyCode.LeftShift));
    }

    void GirarConMouse()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensibilidad;

        transform.Rotate(Vector3.up * mouseX);
    }
}
