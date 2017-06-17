using UnityEngine;

/// <summary>
/// ランダムにプリミティブ型の3Dオブジェクトを作って配置する
/// </summary>
public class Random3DObject : MonoBehaviour
{

    [SerializeField]
    Transform centerPositionObject;

    [SerializeField]
    float maxLengthFromCenter;

    [SerializeField]
    float maxSize;

    [SerializeField]
    int numberOfObjects;

    [SerializeField]
    PrimitiveType[] primitiveTypes;

    void Start()
    {
        var systemRandom = new System.Random();

        for (int i = 0; i < numberOfObjects; i++)
        {
            var pos = Random.insideUnitSphere * maxLengthFromCenter;
            var primitiveType = primitiveTypes[systemRandom.Next(primitiveTypes.Length)];

            var go = GameObject.CreatePrimitive(primitiveType);
			go.transform.parent = transform;
            go.transform.position = centerPositionObject.transform.position + pos;
            go.transform.localScale = Vector3.one * Random.Range(0.01f, maxSize);
        }
    }
}
