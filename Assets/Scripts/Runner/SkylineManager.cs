using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkylineManager : MonoBehaviour {

	public Transform prefab;
	public Vector3 startPosition;
	public int numberOfCubes;
	public Vector3 minSize, maxSize;
	
	[Range(10, 100)]
	public float recycleOffset;
	
	private Vector3 nextPosition;
	private int currentNumberOfCubes;
	private Queue<Transform> objectQueue;


	// Use this for initialization
	void Start () {
		nextPosition = startPosition;
		objectQueue = new Queue<Transform>(numberOfCubes);
		for (int i = 0; i < numberOfCubes; i++) {
			objectQueue.Enqueue((Transform) Instantiate(prefab));
		}
		for (int i = 0; i < numberOfCubes; i++) {
			Recycle();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
		// Check to see if we need to shuffle cubes around
		if (objectQueue != null || objectQueue.Count == 0) {
			if (objectQueue.Peek().localPosition.x + recycleOffset < Runner.distanceTraveled) {
				Recycle ();
			}
		}
		
	}
	
	void Recycle() {
		Vector3 scale = new Vector3(
			Random.Range (minSize.x, maxSize.x),
			Random.Range (minSize.y, maxSize.y),
			Random.Range (minSize.z, maxSize.z));
			
		Vector3 position = nextPosition;
		position.y += scale.y * 0.5f;
		position.x += scale.x * 0.5f;
			
		Transform trans = objectQueue.Dequeue();
		trans.localScale = scale;
		trans.localPosition = position;
		nextPosition.x += scale.x;
		objectQueue.Enqueue(trans);
	}	
}
