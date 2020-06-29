using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshSwaperAnimation : MonoBehaviour
{   
    public Mesh[] AllMeshes;
    public float FrameDelayTime;
    public bool IsPlayOnAwake;
    public bool DestroyOnFinish;
    private MeshFilter _meshFilter;

    void Awake()
    {   
        _meshFilter = GetComponent<MeshFilter>();
        if (IsPlayOnAwake)
            Play();
    }

    /// <summary>
    /// Run animation
    /// </summary>
    public void Play()
    {
        StartCoroutine(PlaySequence());
    }

    private IEnumerator PlaySequence()
    {   
        for (int i = 0; i < AllMeshes.Length; i++)
        {
            _meshFilter.mesh = AllMeshes[i];
            yield return new WaitForSeconds(FrameDelayTime);
        }
        if (DestroyOnFinish)
            DestroyObject();
        else
            _meshFilter.mesh = null;

    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
