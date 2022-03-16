using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ClothingStats : Stats
{
    public float weight;
    public List<ClothespinStats> connectedPins = new List<ClothespinStats>();
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private GameObject weightText;
    private Vector3 hangedPosition;
    private Vector3 hangedRotation;

    public bool CheckHangedStatus()
    {
        hangedPosition = transform.position;
        hangedRotation = transform.eulerAngles;
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

    public void ResetClothes()
	{
        transform.position = hangedPosition;
        transform.eulerAngles = hangedRotation;
        rigidBody.constraints = RigidbodyConstraints.FreezeAll;
	}

	private void Update()
	{
		if(transform.eulerAngles.y == 90)
		{
            weightText.transform.eulerAngles = new Vector3(weightText.transform.eulerAngles.x, 0, weightText.transform.eulerAngles.z);
		}
        else
		{
            weightText.transform.eulerAngles = new Vector3(weightText.transform.eulerAngles.x, 90, weightText.transform.eulerAngles.z);
        }
    }
}
