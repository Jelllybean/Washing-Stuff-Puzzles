using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{
	[SerializeField] private Camera camera;
	[SerializeField] private Transform currentObject;
	[SerializeField] private LayerMask lineLayers;
	[SerializeField] private LayerMask objectLayers;
	[SerializeField] private List<GameObject> InvisibleLines = new List<GameObject>();
	[SerializeField] private List<GameObject> OverlapPreventors = new List<GameObject>();

	private GameObject currentTextObject;

	private void Start()
	{
		SetOverlapPreventors(false);
	}

	void Update()
	{
		Vector3 mouse = Input.mousePosition;
		Ray castPoint = Camera.main.ScreenPointToRay(mouse);
		RaycastHit hit;

		if (!currentObject)
		{
			if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
			{
				if (hit.collider.CompareTag("Clothes"))
				{
					currentTextObject.SetActive(false);
					currentTextObject = null;
					currentTextObject = hit.collider.gameObject.transform.GetChild(1).gameObject;
					currentTextObject.SetActive(true);
				}
				else
				{
					Debug.Log(currentTextObject.name);
					currentTextObject.SetActive(false);
				}
			}
			if (Input.GetMouseButtonDown(0))
			{
				if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, objectLayers))
				{
					currentObject = hit.collider.transform;
					currentObject.eulerAngles = Vector3.zero;
					if (currentObject.CompareTag("ClothesPin"))
					{
						SetInvisibleLines(false);
						currentObject.transform.position = Vector3.zero;
					}
					else if (currentObject.CompareTag("Clothes"))
					{
						SetInvisibleLines(true);
						SetOverlapPreventors(true);
						currentObject.GetChild(0).gameObject.SetActive(false);
					}
				}
			}
		}
		else if (currentObject)
		{
			if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, lineLayers))
			{
				if (hit.transform.CompareTag("LineCollision"))
				{
					currentObject.position = new Vector3(hit.transform.position.x, hit.transform.position.y, hit.point.z);
					currentObject.eulerAngles = new Vector3(currentObject.eulerAngles.x, hit.transform.eulerAngles.y, currentObject.eulerAngles.z);
					if (Input.GetMouseButtonDown(0))
					{
						currentObject = null;
						SetOverlapPreventors(false);
					}
				}
				else if (hit.transform.CompareTag("BackgroundCollision"))
				{
					currentObject.position = hit.point;
				}
			}
		}
	}

	private void SetInvisibleLines(bool _state)
	{
		for (int i = 0; i < InvisibleLines.Count; i++)
		{
			InvisibleLines[i].SetActive(_state);
		}
	}

	private void SetOverlapPreventors(bool _state)
	{
		for (int i = 0; i < OverlapPreventors.Count; i++)
		{
			OverlapPreventors[i].SetActive(_state);
		}
	}
}
