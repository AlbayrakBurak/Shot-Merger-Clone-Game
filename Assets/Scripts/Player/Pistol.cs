using UnityEngine;

public class AnotherPistol : MonoBehaviour
{
    [SerializeField] private float finishLineZ;

    private void OnTriggerEnter(Collider other)
    {
        if (IsBarrelOrObstacle(other))
        {
            BreakPistol();

            if (IsPastFinishLine())
            {
                GameManager.Instance.OnGameFinished();
            }
            else
            {
                GameManager.Instance.OnGameFailed();
            }
        }

        if (IsX10Powerup(other))
        {
            GameManager.Instance.OnGameFinished();
        }
    }

    private bool IsBarrelOrObstacle(Collider collider)
    {
        return collider.CompareTag("Barrel") || collider.CompareTag("Obstacle");
    }

    private void BreakPistol()
    {
        foreach (Transform child in transform)
        {
            Rigidbody childRigidbody = child.GetComponent<Rigidbody>();
            BoxCollider childCollider = child.GetComponent<BoxCollider>();

            if (childRigidbody == null && childCollider == null)
            {
                child.gameObject.AddComponent<Rigidbody>();
                child.gameObject.AddComponent<BoxCollider>();
            }
        }
    }

    private bool IsPastFinishLine()
    {
        return transform.position.z > finishLineZ;
    }

    private bool IsX10Powerup(Collider collider)
    {
        return collider.CompareTag("X10");
    }
}
