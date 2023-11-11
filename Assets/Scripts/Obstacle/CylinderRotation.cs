using UnityEngine;

public class CylinderMovement : MonoBehaviour
{
    [SerializeField] private Vector3 pointA;
    [SerializeField] private Vector3 pointB;
    [SerializeField] private float movementSpeed = 2f;

    private void Update()
    {
        MoveCylinderBetweenPoints();
        RotateCylinder();
    }

    private void MoveCylinderBetweenPoints()
    {
        float t = Mathf.PingPong(Time.time * movementSpeed, 1f); // PingPong fonksiyonu, 0 ile 1 arasında bir değeri döngü içinde sürekli olarak gidip gelir.
        transform.position = Vector3.Lerp(pointA, pointB, t);
    }

    private void RotateCylinder()
    {
        float rotationSpeed = 300f;
        float rotationAmount = rotationSpeed * Time.deltaTime;
        transform.Rotate(Vector3.forward, rotationAmount);
    }
}
