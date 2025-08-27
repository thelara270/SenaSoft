using UnityEngine;

public class Camara : MonoBehaviour
{
    [SerializeField] public Transform objetivo;
    public Vector3 offset = new Vector3(0, 5, -5);
    public float suavizado = 5f;

    void LateUpdate()
    {
        Vector3 posicionDeseada = objetivo.position + objetivo.TransformDirection(offset);

        transform.position = Vector3.Lerp(transform.position, posicionDeseada, suavizado * Time.deltaTime);

        transform.LookAt(objetivo.position + Vector3.up * 1.5f);
    }
}