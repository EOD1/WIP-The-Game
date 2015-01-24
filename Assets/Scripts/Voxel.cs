using UnityEngine;
using System.Collections;

public class Voxel : MonoBehaviour {

	//modifyable settings
	public Vector2 chunkSize;
	
	public Vector2 worldSize;
	
	public int terrainDetailFactor;

	public int seed;

	public Material mat;
	
	//private variables
	private GameObject[] chunk;

	private float[,] heightMap;
	
	private noise noise;

	private mesh chunkMesh;
	
	// Use this for initialization

	IEnumerator Start () {
		noise = (noise)gameObject.AddComponent ("noise");
		noise.seed = seed;
		noise.resolution = (int)Mathf.Max (worldSize.x * chunkSize.x, worldSize.y * chunkSize.x) / terrainDetailFactor;
		noise.depth = terrainDetailFactor;

		chunk = new GameObject[(int)worldSize.x * (int)worldSize.y];

		chunkMesh = (mesh)gameObject.AddComponent ("mesh");

		noise.begin ();

		heightMap = new float[(int)chunkSize.x + 1, (int)chunkSize.x + 1];

		for (int wx = 0; wx < worldSize.x; wx++) {
			for (int wy = 0; wy < worldSize.y; wy++) {
				genChunk (wx * (int)chunkSize.x, wy * (int)chunkSize.x, wx, wy);
				yield return 1;
			}
		}

	}

	void genChunk(int posX, int posZ, int wx, int wz){
		for (int x = 0; x < chunkSize.x + 1; x++) {
			for (int z = 0; z < chunkSize.x + 1; z++) {
				int t = 12;
				float valueNoise = 0;
				for(int i = 1; i <= t; i++){
					valueNoise += noise.ValueNoise((int)((x + posX) / (i * 0.125)), (int)((z + posZ) / (i * 0.125)));
				}

				heightMap [x, z] = ((valueNoise / t) * chunkSize.y - chunkSize.y / 4);

			}
		}
		chunk[posZ / (int)chunkSize.x + posX / (int)chunkSize.x * (int)worldSize.x] = chunkMesh.newChunk(posX, posZ, (int)chunkSize.x + 1, heightMap, mat);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
