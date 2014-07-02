using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class LevelCreator : MonoBehaviour {

	public GameObject _prefab;

	public List<GameObject> _instances = new List<GameObject>();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider collider)
	{
		if(collider.tag == "End")
		{
			// destroy this
			_instances.Remove(collider.gameObject.transform.parent.gameObject);
			GameObject.Destroy(collider.gameObject.transform.parent.gameObject);

			GameObject instance = (GameObject)GameObject.Instantiate(_prefab, _instances.Last().transform.position + new Vector3(0,0,90), Quaternion.identity);

			_instances.Add(instance);
		}
	}
}
