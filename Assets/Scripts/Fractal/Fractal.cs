using UnityEngine;
using System.Collections;

public class Fractal : MonoBehaviour {

	public Mesh[] meshes;
	public Material material;
	
	[Range(0,6)]
	public int maxDepth;
	
	[Range(0,1)]
	public float childScale;
	
	[Range(0,1)]
	public float spawnChance;
	
	[Range(0, 180)]
	public float maxTwist;
	
	public float maxRotationSpeed;
	
	private int depth = 0;
	private bool isRoot = true;
	private float rotationSpeed;
	private Material[,] materials;
	
	private static Vector3[] childDirections = {
		Vector3.up,
		Vector3.left,
		Vector3.right,
		Vector3.forward,
		Vector3.back,
		Vector3.down  // For root object only
	};
	
	private static Quaternion[] childOrientations = {
		Quaternion.identity,
		Quaternion.Euler (0f, 0f, 90f),
		Quaternion.Euler (0f, 0f, -90f),
		Quaternion.Euler (90f, 0f, 0f),
		Quaternion.Euler (-90f, 0f, 0f),
		Quaternion.Euler (180f, 0f, 0f)  // For root object only
	};
	
	private static Color[] colors = {
		Color.blue,
		Color.green,
		Color.yellow,
		Color.magenta,
		Color.cyan,
		Color.red
	};

	// Use this for initialization
	void Start () {
		if (materials == null) {
			InitializeMaterials();
		}
		rotationSpeed = Random.Range (-maxRotationSpeed, maxRotationSpeed);
		transform.Rotate(Random.Range (-maxTwist, maxTwist), 0f, 0f);
		gameObject.AddComponent<MeshFilter>().mesh = meshes[Random.Range (0, meshes.Length)];
		gameObject.AddComponent<MeshRenderer>().material = materials[depth, Random.Range (0, colors.Length)];	
//		renderer.material.color = Color.Lerp(Color.white, colors[Random.Range(0, colors.Length)], (float) depth / maxDepth);
		if (depth < maxDepth) {
			StartCoroutine(SpawnChildren());
		}
	}
	
	public IEnumerator SpawnChildren() {
		int numToSpawn = isRoot ? childDirections.Length : childDirections.Length - 1;
		for (int i = 0; i < numToSpawn; i++) {
			if (Random.value < spawnChance) {
				if (!isRoot) { yield return new WaitForSeconds(Random.Range (0.1f, 0.5f)); }
				new GameObject("Child").AddComponent<Fractal>().Initialize(this, i);
			}
		}
	}
	
	public void Initialize(Fractal parent, int childIndex) {
		meshes = parent.meshes;
		material = parent.material;
		maxDepth = parent.maxDepth;
		depth = parent.depth + 1;
		childScale = parent.childScale;
		spawnChance = parent.spawnChance;
		maxRotationSpeed = parent.maxRotationSpeed;
		maxTwist = parent.maxTwist;
		
		isRoot = false; // or else we'll get a lot of extra thingers
		
		transform.parent = parent.transform;
		transform.localScale = Vector3.one * childScale;
		transform.localPosition = childDirections[childIndex] * (0.5f + 0.5f * childScale);
		transform.localRotation = childOrientations[childIndex];
	}
	
	private void InitializeMaterials() {
		materials = new Material[maxDepth + 1, colors.Length];
		for (int i = 0; i <= maxDepth; i++) {
			float t = i / (maxDepth - 1f);
			t *= t;
			for (int j = 0; j < colors.Length; j++) {
				materials[i, j] = new Material(material);
				materials[i, j].color = Color.Lerp (Color.white, colors[j], t);				
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
	}
}
