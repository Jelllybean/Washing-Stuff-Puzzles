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

    [SerializeField] private List<MeshRenderer> renderers;

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
            return true;
        }
        else
        {
            rigidBody.constraints = RigidbodyConstraints.None;
            return false;
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
		//if(transform.root.eulerAngles.y == 90)
		//{
        //    weightText.transform.eulerAngles = Vector3.zero;
		//}
        //else
		//{
        //    weightText.transform.eulerAngles = new Vector3(0, 90, 0);
        //}
    }

    public void SetShaders(Material _material)
    {
        for (int i = 0; i < renderers.Count; i++)
        {
            Material[] mats = renderers[i].materials;
            mats[1] = _material;
            renderers[i].materials = mats;
        }
    }
}
