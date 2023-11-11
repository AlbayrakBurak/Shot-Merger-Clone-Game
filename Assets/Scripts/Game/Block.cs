using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private Transform stackerTransform;
 
    void Start()
    {
        stackerTransform = GameObject.FindGameObjectWithTag("Stacker").transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Barrel") || other.CompareTag("Obstacle"))
        {
            foreach(Transform child in stackerTransform)
            {
                for (int i = child.childCount; i > 0; i--)
                {
                    ActivePieces(child);

                    BulletSpawner.spawnDelay = 2f;
                                       
                    Destroy(child.gameObject);
                }               
            }           
        }
    }

    private void ActivePieces(Transform piecesParent)
    {
        if (piecesParent.GetChild(0).GetComponent<Rigidbody>() != null)
        {
            piecesParent.GetChild(0).GetComponent<Rigidbody>().isKinematic = false;
        }

        piecesParent.GetChild(0).gameObject.AddComponent<BoxCollider>();
        piecesParent.GetChild(0).gameObject.SetActive(true);
        piecesParent.GetChild(0).SetParent(null);
    }
}
