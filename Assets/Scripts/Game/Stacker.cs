using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stacker : MonoBehaviour
{
    private Transform stackerTransform;

    void Start()
    {
        stackerTransform = GameObject.FindGameObjectWithTag("Stacker").transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsCollectable(other))
        {
            AdjustBulletSpawnerDelay();
            StackCollectable(other);
        }
    }

    private bool IsCollectable(Collider collider)
    {
        return collider.CompareTag("Collectable");
    }

    private void AdjustBulletSpawnerDelay()
    {
        BulletSpawner.spawnDelay -= 0.132f;
    }

    private void StackCollectable(Collider collectable)
    {
        SetCollectableProperties(collectable);
        MoveToStackerPosition(collectable);
        UpdateChildProperties(collectable);
    }

    private void SetCollectableProperties(Collider collectable)
    {
        var collectableParentTransform = collectable.transform.parent.gameObject.transform;

        collectable.tag = "Untagged";
        collectable.isTrigger = true;
        collectable.transform.parent = stackerTransform;

        foreach (Transform child in collectableParentTransform)
        {
            child.SetParent(collectable.transform);
        }
    }

    private void MoveToStackerPosition(Collider collectable)
    {
        collectable.transform.position = new Vector3(
            transform.position.x,
            transform.position.y,
            transform.position.z + 1
        );
    }

    private void UpdateChildProperties(Collider collectable)
    {
        foreach (Transform child in collectable.transform)
        {
            if (child.TryGetComponent<BoxCollider>(out BoxCollider boxCollider))
            {
                boxCollider.tag = "Untagged";
                boxCollider.isTrigger = true;
                child.SetParent(stackerTransform);
            }
        }
    }
}
