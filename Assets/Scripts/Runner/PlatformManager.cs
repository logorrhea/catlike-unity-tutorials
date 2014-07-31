using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlatformManager : MonoBehaviour {

	public Transform prefab;
	public int numberOfCubes;
	
	[Range(10, 100)]
	public float recycleOffset;
	
	public Vector3 startPosition;
	public Vector3 minSize, maxSize, minGap, maxGap;
	public float minY, maxY;
	
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
		objectQueue.Enqueue(trans);
		
		nextPosition += new Vector3(
			Random.Range (minGap.x, minGap.y) + scale.x,
			Random.Range (minGap.y, maxGap.y),
			Random.Range (minGap.z, maxGap.z));
			
		if (nextPosition.y < minY) {
			nextPosition.y = minY + minGap.y;
		} else if (nextPosition.y > maxY) {
			nextPosition.y = maxY - maxGap.y;
		}
	}	
}
