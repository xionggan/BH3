using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class ShowShadowTail : MonoBehaviour {
    public float fadeSpeed = 6;
    public float createLength = 0.2f;
    private Vector3 previousPosition = Vector3.zero;

    private SkinnedMeshRenderer []meshRenders;
    private MeshFilter []meshFilters;
	void Awake()
    {
        meshRenders = this.GetComponentsInChildren<SkinnedMeshRenderer>();
        meshFilters = this.GetComponentsInChildren<MeshFilter>();
    }
    struct MeshInfo {
        public Mesh mesh;
        public Matrix4x4 _matrix_PR; //存儲位置和旋转角度；
        public Material material;
    };
    List<MeshInfo> meshInfoList = new List<MeshInfo>();
	void Update () {

        for (int i = 0; i < meshInfoList.Count;)
        {
            MeshInfo meshInfo = meshInfoList[i];
            Graphics.DrawMesh(meshInfo.mesh, meshInfo._matrix_PR, meshInfo.material, gameObject.layer);
            float timer = meshInfo.material.GetFloat("_Alpha") - Time.deltaTime * fadeSpeed;
            meshInfo.material.SetFloat("_Alpha", timer > 0 ? timer : 0);
            if (meshInfo.material.GetFloat("_Alpha") <= 0)
            {
                meshInfoList.Remove(meshInfo);
            }
            else
            {
                i++;
            }
        }

        if (createLength > 0)
        {
            if ((transform.position - previousPosition).magnitude > createLength)
            {
                previousPosition = transform.position;
                foreach (SkinnedMeshRenderer meshRender in meshRenders)
                {
                    Mesh mesh = new Mesh();
                    meshRender.BakeMesh(mesh);
                    MeshInfo meshInfo = new MeshInfo();
                    meshInfo.mesh = mesh;
                    meshInfo._matrix_PR = meshRender.localToWorldMatrix;
                    meshInfo.material = new Material(Shader.Find("Shader Forge/Alternately"));
                    meshInfoList.Add(meshInfo);
                }
                foreach (MeshFilter filter in meshFilters)
                {
                    MeshInfo meshInfo = new MeshInfo();
                    meshInfo.mesh = filter.mesh;
                    meshInfo._matrix_PR = filter.transform.localToWorldMatrix;
                    meshInfo.material = new Material(Shader.Find("Shader Forge/Alternately"));
                    meshInfoList.Add(meshInfo);
                }
            }
        }
	}
}