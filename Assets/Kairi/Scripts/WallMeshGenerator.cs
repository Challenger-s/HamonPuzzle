using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMeshGenerator : MonoBehaviour
{
    [SerializeField] float EdgeCutLength = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        MeshGenerate();
    }
    
    void MeshGenerate()
    {
        // メッシュインスタンスの作成
        var mesh = new Mesh();

        // 頂点情報の登録
        float scaleX = this.transform.localScale.x;
        float scaleY = this.transform.localScale.y;

        mesh.vertices = new Vector3[] {
            new Vector3 (-scaleX + EdgeCutLength, scaleY),
            new Vector3 (-scaleX + EdgeCutLength, scaleY-EdgeCutLength),
            new Vector3 (-scaleX, scaleY - EdgeCutLength),

            new Vector3 ( scaleX - EdgeCutLength, scaleY),
            new Vector3 ( scaleX , scaleY - EdgeCutLength),
            new Vector3 ( scaleX - EdgeCutLength,-scaleY + EdgeCutLength),

            new Vector3 (-scaleX, -scaleY + EdgeCutLength),
            new Vector3 (-scaleX + EdgeCutLength, -scaleY+EdgeCutLength),
            new Vector3 (-scaleX + EdgeCutLength, -scaleY),

            new Vector3 ( scaleX - EdgeCutLength, -scaleY + EdgeCutLength),
            new Vector3 ( scaleX, -scaleY + EdgeCutLength),
            new Vector3 ( scaleX - EdgeCutLength, -scaleY),
        };

        // 三角形の登録
        mesh.triangles = new int[] {
            0, 1, 2,
            3, 4, 5,
            6, 7, 8,
            9, 10,11,
            0, 3, 11,
            0, 11,8,
            2, 1, 7,
            2, 7, 6,
            5, 4, 10,
            5, 10,9
        };

        // 法線の再計算
        mesh.RecalculateNormals();

        // メッシュを設定
        var filter = GetComponent<MeshFilter>();
        filter.sharedMesh = mesh;
    }

}
