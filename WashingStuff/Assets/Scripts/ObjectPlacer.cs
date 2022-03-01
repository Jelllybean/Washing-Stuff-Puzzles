using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{
	[SerializeField] private Camera camera;
	[SerializeField] private Transform currentObject;
	[SerializeField] private LayerMask lineLayers;
	[SerializeField] private LayerMask objectLayers;


	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		Vector3 mouse = Input.mousePosition;
		Ray castPoint = Camera.main.ScreenPointToRay(mouse);
		RaycastHit hit;
		if (currentObject)
		{
			if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, lineLayers))
			{
				currentObject.position = hit.point;
			}
		}


		if (Input.GetMouseButtonDown(0))
		{
			if (!currentObject)
			{
				if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, objectLayers))
				{
					Debug.Log(hit.collider.gameObject.name);
					currentObject = hit.collider.transform;
				}
			}
			else
			{
				currentObject = null;
			}
		}
	}
}
