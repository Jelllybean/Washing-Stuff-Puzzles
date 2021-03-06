using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothespinStats : Stats
{
    public float pullWeight;
    public bool checkCollision = true;

    private void OnCollisionEnter(Collision collision)
    {
        if (checkCollision)
        {
            if (collision.gameObject.CompareTag("Clothes"))
            {
                ClothingStats _stats = collision.gameObject.GetComponent<ClothingStats>();
                if (_stats.colors == colors && !_stats.connectedPins.Contains(this))
                {
                    _stats.connectedPins.Add(this);
                }
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (checkCollision)
        {
            if (collision.gameObject.CompareTag("Clothes"))
            {
                ClothingStats _stats = collision.gameObject.GetComponent<ClothingStats>();
                if (_stats.colors == colors)
                {
                    _stats.connectedPins.Remove(this);
                }
            }
        }
    }
}
