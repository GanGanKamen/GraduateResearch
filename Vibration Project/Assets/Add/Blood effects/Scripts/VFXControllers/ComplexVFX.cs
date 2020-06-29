using UnityEngine;
using UnityEngine.VFX;
using Random = UnityEngine.Random;

public class ComplexVFX : MonoBehaviour
{
    [Header("VFX graph")]
    public GameObject[] VFXGraphs;
    [Range(0,5)]
    public float VFXGraphsSizeMultipler;
    public float VFXGraphsAutodestroyTime;
    [Space]
    [Header("Alembic")]
    public GameObject[] Alembics;
    [Range(0, 5)]
    public float AlembicsSizeMultipler;
    public float AlembicsAutodestroyTime;
    [Space]
    [Header("SFX")]
    public AudioClip[] SFXClip;
    [Space]
    [Header("Decals particle system")]
    public GameObject[] ParticleDecal;
    public float ParticleDecalAutodestroyTime;

    private AudioSource _audioSource;

    private void Awake()
    {
        CheckSFX();        
    }

    //Add and set <AudioSource> component
    private void CheckSFX()
    {
        _audioSource = GetComponent<AudioSource>();
        if(_audioSource == null && SFXClip != null)
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    //Playing random audioclip
    private void PlayAudio()
    {
        if (_audioSource != null && SFXClip.Length > 0)
        {
            _audioSource.clip = SFXClip[Random.Range(0, SFXClip.Length-1)];
            _audioSource.Play();
        }        

    }

    //Drop particle system spots and puddles
    private void DropDecals()
    {
        if (ParticleDecal.Length < 1)
            return;

        for (int i = 0; i < ParticleDecal.Length; i++)
        {
            GameObject drop = Instantiate(ParticleDecal[i], gameObject.transform.position, Quaternion.identity, null);
            Destroy(drop, ParticleDecalAutodestroyTime);
        }
    }

    //Spawn and play random VFX graph
    private void PlayVFXGraphs()
    {
        if (VFXGraphs.Length == 0 || VFXGraphs == null)
            return;
        
        else
        {
            int randomElement = Random.Range(0, VFXGraphs.Length);
            GameObject spawnedVFXGraph = Instantiate(VFXGraphs[randomElement],transform.position,Quaternion.identity,null);
            spawnedVFXGraph.transform.localScale *= VFXGraphsSizeMultipler; 
            VisualEffect spawnedGraph = spawnedVFXGraph.GetComponent<VisualEffect>();
            spawnedGraph.SendEvent("Play");
            Destroy(spawnedVFXGraph, VFXGraphsAutodestroyTime);
        }

    }

    //Spawn and play random vertex animation
    private void PlayAlembic()
    {
        if (Alembics.Length == 0 || Alembics == null)
            return;

        else
        {
            int randomElement = Random.Range(0, Alembics.Length);
            GameObject spawnedAlembic = Instantiate(Alembics[randomElement], transform.position, Quaternion.identity,null);
            spawnedAlembic.transform.localScale *= AlembicsSizeMultipler;
            Destroy(spawnedAlembic,AlembicsAutodestroyTime);
        }
    }

    /// <summary>
    /// StartSimulation
    /// </summary>
    public void Play()
    {
        PlayVFXGraphs();
        DropDecals();
        PlayAudio();
        PlayAlembic();
    }


}
