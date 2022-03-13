using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ClothingStats : Stats
{
    public float weight;
    public List<ClothespinStats> connectedPins = new List<ClothespinStats>();
    [SerializeField] private Rigidbody rigidBody;
    private Vector3 hangedPosition;

    public bool CheckHangedStatus()
    {
        hangedPosition = transform.position;
        float total = 0;
        for (int i = 0; i < connectedPins.Count; i++)
        {
            total += connectedPins[i].pullWeight;
        }
        if(total >= weight)
        {
            //mag blijven hangen
            Debug.Log("klopt hoor");
            return true;
        }
        else
        {
            rigidBody.constraints = RigidbodyConstraints.None;
            return false;
            //vallen
        }
    }
}
