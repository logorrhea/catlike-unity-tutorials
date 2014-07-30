using UnityEngine;
using System.Collections;

public class Fractal : MonoBehaviour {

	public Mesh mesh;
	public Material material;
	
	public int maxDepth = 5;
	public float childScale = 0.5f;
	public float spawnRate = 0.3f;
	
	private int depth = 0;
	private float degrees = 0.0f;
	private bool isRoot = true;
	
	private Vector3[] childDirections = {
		Vector3.up,
		Vector3.left,
		Vector3.right,
		Vector3.forward,
		Vector3.back,
		Vector3.down  // For root object only
	};
	
	private Quaternion[] childOrientations = {
		Quaternion.identity,
		Quaternion.Euler (0f, 0f, 90f),
		Quaternion.Euler (0f, 0f, -90f),
		Quaternion.Euler (90f, 0f, 0f),
		Quaternion.Euler (-90f, 0f, 0f),
		Quaternion.Euler (180f, 0f, 0f)  // For root object only
	};

	// Use this for initialization
	void Start () {
		gameObject.AddComponent<MeshFilter>().mesh = mesh;
		gameObject.AddComponent<MeshRenderer>().material = material;	
		if (depth < maxDepth) {
			StartCoroutine(SpawnChildren());
		}
	}
	
	public IEnumerator SpawnChildren() {
		int numToSpawn = isRoot ? childDirections.Length : childDirections.Length - 1;
		for (int i = 0; i < numToSpawn; i++) {
			yield return new WaitForSeconds(Random.Range (0.1f, spawnRate));
			new GameObject("Child").AddComponent<Fractal>().Initialize(this, i);
		}
	}
	
	public void Initialize(Fractal parent, int childIndex) {
		mesh = parent.mesh;
		material = parent.material;
		maxDepth = parent.maxDepth;
		depth = parent.depth + 1;
		childScale = parent.childScale;
		isRoot = false;
		
		transform.parent = parent.transform;
		transform.localScale = Vector3.one * childScale;
		transform.localPosition = childDirections[childIndex] * (0.5f + 0.5f * childScale);
		transform.localRotation = childOrientations[childIndex];
	}
	
	// Update is called once per frame
	void Update () {
		if (isRoot) {
			degrees = degrees + 1.0f % 360.0f;
			transform.localRotation = Quaternion.AngleAxis(degrees, Vector3.up);
		}
	}
}
