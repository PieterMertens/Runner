using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMesh : MonoBehaviour {

    private MeshFilter mf;
    private Mesh mesh;
    private Vector3[] vertices;
    private int[] triangles;

    private float length = 0.4f;

	// Use this for initialization
	void Start () {
        mf = GetComponent<MeshFilter>();
        mesh = mf.mesh;

        makeMeshData();
        createMesh();
	}

    void makeMeshData() {
        float l1 = Mathf.Cos(Mathf.PI)*length/2;
        float l2 = l1 * Mathf.Cos(Mathf.PI/6);
        float l3 = l1 * Mathf.Sin(Mathf.PI/6);
        ////Vertices////
        vertices = new Vector3[]
        {
            //Top Poit//
            new Vector3(0, length, 0),
            //Bottom Point//
            new Vector3(0, -length, 0),
            //Middle Point 1//
            new Vector3(0, 0, l1),
            //Middle Point 2//
            new Vector3(l2, 0, -l3),
            //Middle Point 3//
            new Vector3(-l2, 0, -l3),
            //The middle needs to be repeated for lightning reasons// 
            //Middle Point 1 Second Time//
            new Vector3(0, 0, l1),
            //Middle Point 2 Second Time//
            new Vector3(l2, 0, -l3),
            //Middle Point 3 Second Time//
            new Vector3(-l2, 0, -l3),

            //TEST//
            //new Vector3(0,0,0),
            //new Vector3(0,0,1),
            //new Vector3(1,0,0)
        };

        ////Triangles////
        triangles = new int[]
        {
            //Upper Half//
            0,2,3,
            0,3,4,
            0,4,2,
            //Lower Half
            1,6,5,
            1,7,6,
            1,5,7,
        };
    }

    void createMesh() {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
}
