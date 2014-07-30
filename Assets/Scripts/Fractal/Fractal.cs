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

	// Use this for initialization
	void Start () {
		gameObject.AddComponent<MeshFilter>().mesh = mesh;
		gameObject.AddComponent<MeshRenderer>().material = material;	
		if (depth < maxDepth) {
			StartCoroutine(SpawnChildren());
		}
	}
	
	public IEnumerator SpawnChildren() {
		yield return new WaitForSeconds(spawnRate);
		new GameObject("Child").AddComponent<Fractal>()
			.Initialize(this, Vector3.right, Quaternion.Euler(0f, 0f, -90f));
		yield return new WaitForSeconds(spawnRate);
		new GameObject("Child").AddComponent<Fractal>()
			.Initialize(this, Vector3.up, Quaternion.identity);
		yield return new WaitForSeconds(spawnRate);
		new GameObject("Child").AddComponent<Fractal>()
			.Initialize(this, Vector3.left, Quaternion.Euler (0f, 0f, 90f));
		yield return new WaitForSeconds(spawnRate);
		new GameObject("Child").AddComponent<Fractal>()
			.Initialize(this, Vector3.back, Quaternion.Euler (-90f, 0f, 0f));
		yield return new WaitForSeconds(spawnRate);
		new GameObject("Child").AddComponent<Fractal>()
			.Initialize(this, Vector3.forward, Quaternion.Euler (90f, 0f, 0f));
		if (isRoot) {
			yield return new WaitForSeconds(spawnRate);
			new GameObject("Child").AddComponent<Fractal>()
				.Initialize(this, Vector3.down, Quaternion.Euler (180f, 0f, 0f));
		}
	}
	
	public void Initialize(Fractal parent, Vector3 direction, Quaternion orientation) {
		mesh = parent.mesh;
		material = parent.material;
		maxDepth = parent.maxDepth;
		depth = parent.depth + 1;
		childScale = parent.childScale;
		isRoot = false;
		
		transform.parent = parent.transform;
		transform.localScale = Vector3.one * childScale;
		transform.localPosition = direction * (0.5f + 0.5f * childScale);
		transform.localRotation = orientation;
	}
	
	// Update is called once per frame
	void Update () {
		if (isRoot) {
			degrees = degrees + 1.0f % 360.0f;
			transform.localRotation = Quaternion.AngleAxis(degrees, Vector3.up);
		}
	}
}
