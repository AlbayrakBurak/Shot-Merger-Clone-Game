using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] public float bulletSpeed;

    private void Update()
    {
        MoveBullet();
        DestroyBulletAfterTimeout();
    }
   
    private void MoveBullet()
    {
        transform.position += Vector3.forward * Time.deltaTime * bulletSpeed;
    }

    private void DestroyBulletAfterTimeout()
    {
        Destroy(gameObject, 2f);
    }

    private void OnTriggerEnter(Collider other)
    {
        TryDealDamage(other);
    }

    private void TryDealDamage(Collider other)
    {
        if (IsBarrel(other))
        {
            DealDamageToBarrel(other);
            DestroyBullet();
        }
    }

    private bool IsBarrel(Collider collider)
    {
        return collider.CompareTag("Barrel");
    }

    private void DealDamageToBarrel(Collider barrelCollider)
    {
        Barrel barrel = barrelCollider.GetComponent<Barrel>();
        if (barrel != null)
        {
            barrel.DecreaseHealth();
        }
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
