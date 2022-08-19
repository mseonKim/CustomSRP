using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCreator : MonoBehaviour
{
    public GameObject spherePrefab;
    public int spawnNumber = 76;
    public float spawnPositionOffset = 5f;

    private static int baseColorId = Shader.PropertyToID("_BaseColor");
    private static MaterialPropertyBlock m_Block;
    private Transform m_Transform;
    private List<Color> m_SpawnColors = new List<Color>();

    void Awake()
    {
        m_Transform = transform;

        // Initialize colors
        m_SpawnColors.Add(Color.red);
        m_SpawnColors.Add(Color.blue);
        m_SpawnColors.Add(Color.green);
        m_SpawnColors.Add(Color.cyan);
        m_SpawnColors.Add(Color.white);
        m_SpawnColors.Add(Color.black);

        if (m_Block == null)
        {
            m_Block = new MaterialPropertyBlock();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        int spawnColorCount = m_SpawnColors.Count;

        for (int i = 0; i < spawnNumber; i++)
        {
            var obj = Instantiate(spherePrefab, new Vector3(Random.value * spawnPositionOffset, Random.value * spawnPositionOffset, Random.value * spawnPositionOffset), Quaternion.identity, m_Transform);
            // obj.GetComponent<Renderer>().material.SetColor("_BaseColor", m_SpawnColors[i % spawnColorCount]);
            m_Block.SetColor(baseColorId, m_SpawnColors[i % spawnColorCount]);
            obj.GetComponent<Renderer>().SetPropertyBlock(m_Block);
        }
    }

    
}
