using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{
	[SerializeField] private Camera camera;
	[SerializeField] private Transform currentObject;
	[SerializeField] private LayerMask lineLayers;
	[SerializeField] private LayerMask objectLayers;
	[SerializeField] private List<GameObject> InvisibleLines = new List<GameObject>();

	void Update()
	{
		Vector3 mouse = Input.mousePosition;
		Ray castPoint = Camera.main.ScreenPointToRay(mouse);
		RaycastHit hit;

		if (Input.GetMouseButtonDown(0))
		{
			if (!currentObject)
			{
				if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, objectLayers))
				{
					currentObject = hit.collider.transform;
					currentObject.eulerAngles = Vector3.zero;
					if(currentObject.CompareTag("ClothesPin"))
                    {
						SetInvisibleLines(false);
						currentObject.transform.position = Vector3.zero;
                    }
					else if (currentObject.CompareTag("Clothes"))
					{
						SetInvisibleLines(true);
					}
				}
			}
			//else
			//{
			//	currentObject = null;
			//}
		}

		if (currentObject)
		{
			if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, lineLayers))
			{
				//currentObject.position = hit.point;
				//currentObject.localPosition = new Vector3(hit.point.x, hit.transform.localPosition.y, hit.transform.localPosition.z);
				if(hit.transform.CompareTag("LineCollision"))
                {
					currentObject.position = new Vector3(hit.transform.position.x, hit.transform.position.y, hit.point.z);
					currentObject.eulerAngles = new Vector3(currentObject.eulerAngles.x, hit.transform.eulerAngles.y, currentObject.eulerAngles.z);
					if (Input.GetMouseButtonDown(0))
                    {
						currentObject = null;
					}
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
}
