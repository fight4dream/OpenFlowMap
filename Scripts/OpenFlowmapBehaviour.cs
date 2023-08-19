using UnityEngine;

[ExecuteInEditMode]
public class OpenFlowmapBehaviour : MonoBehaviour
{
    [SerializeField] OpenFlowmap m_openFlowmap;
    [SerializeField] bool m_drawGizmos = true;

    private MeshRenderer m_meshRenderer;
    private MeshFilter m_meshFilter;
    private void Awake()
    {
        m_meshFilter = GetComponent<MeshFilter>();
        m_meshRenderer = GetComponent<MeshRenderer>();
        Initialize();
    }

    public void Initialize()
    {
        if (m_openFlowmap != null)
        {
            m_openFlowmap.SetData(m_meshFilter.sharedMesh.bounds.size, new Plane(transform.up, transform.position), transform.position);
            m_openFlowmap.Initialize();
        }
    }

    private void OnValidate()
    {
        Initialize();
    }

    private void Update()
    {
        if (m_openFlowmap != null && m_drawGizmos)
            m_openFlowmap.Update();

        if (transform.hasChanged)
        {
            transform.hasChanged = false;
            Initialize();
        }
    }
}