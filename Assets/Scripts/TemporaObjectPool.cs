using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaObjectPool : MonoBehaviour
{
	public GameObject temporaPrefab;

	private Stack<GameObject> inactiveInstances = new Stack<GameObject>();

	//returns an instance of the prefab
	public GameObject GetObject()
	{
		GameObject spawnedGameObject;

		//if there's an inactive instance of the prefab ready to return, return that
		if(inactiveInstances.Count > 0)
		{
			// remove the instance from the inactive instances
			spawnedGameObject = inactiveInstances.Pop();
		}
		else //otherwise, make a new instance
		{
			spawnedGameObject = (GameObject)GameObject.Instantiate(temporaPrefab);

			// add the PooledObject component to the prefab so we know it came from this pool
			PooledObject pooledObject = spawnedGameObject.AddComponent<PooledObject>();
			pooledObject.pool = this;
		}

		//put the instance in the root of the scene and enable it
		spawnedGameObject.transform.SetParent(null);
		spawnedGameObject.SetActive(true);

		// return a reference to the instance
		return spawnedGameObject;
	}
	 // return an instance of the prefab to the pool
	public void ReturnObject(GameObject toReturn)
	{
		PooledObject pooledObject = toReturn.GetComponent<PooledObject>();

		// if it came from this pool, return it to this pool
		if(pooledObject != null && pooledObject.pool == this)
		{
			// make the instance a child of this and disable it
			toReturn.transform.SetParent(transform);
			toReturn.SetActive(false);

			// add the instance to the collection of inactive instances
			inactiveInstances.Push(toReturn);
		}
		else // otherwise, kill it
		{
			Destroy(toReturn);
		}
	}
}

public class PooledObject : MonoBehaviour
{
	public TemporaObjectPool pool;
}