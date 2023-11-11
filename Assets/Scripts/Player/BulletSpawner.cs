using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;

    private float spawnTimer = 0;
    public static float spawnDelay;

    private void Start()
    {
        InitializeDelay();
    }

    private void InitializeDelay()
    {
        spawnDelay = 2f;
    }

    private void Update()
    {
        if (IsGameActive())
        {
            TrySpawnBullet();
        }
    }

    private bool IsGameActive()
    {
        return GameManager.Instance.IsGameStarted() && !GameManager.Instance.IsGameOver();
    }

    private void TrySpawnBullet()
    {
        if (!IsCollectable())
        {
            UpdateTimer();

            if (IsTimerElapsed())
            {
                SpawnBullet();
                ResetTimer();
            }
        }
    }

    private bool IsCollectable()
    {
        return CompareTag("Collectable");
    }

    private void UpdateTimer()
    {
        spawnTimer -= Time.deltaTime;
    }

    private bool IsTimerElapsed()
    {
        return spawnTimer <= 0;
    }

    private void SpawnBullet()
    {
        Instantiate(bulletPrefab, transform.position, Quaternion.identity);
    }

    private void ResetTimer()
    {
        spawnTimer = spawnDelay;
    }
}
