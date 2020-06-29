using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class VFXController : MonoBehaviour
{
    public VisualEffect[] AllVFX;
    public int CurrentVFXindex;
    [Space]
    public bool IsAutoplay;
    public bool IsPlayAll;
    public float AutoPlayDelays;

    private void Start()
    {
        if (IsAutoplay)
            RunSimulation();
    }
    private void PlayOnPosition(Vector3 positionToTransform)
    {
        AllVFX[CurrentVFXindex].gameObject.transform.position = positionToTransform;
        Play();
    }
    [ContextMenu("Run simulation")]
    public void RunSimulation()
    {
        if (IsAutoplay)
            StartCoroutine(PlayWithDelays());
        else
            Play();
        
    }

    private IEnumerator PlayWithDelays()
    {
        while (IsAutoplay)
        {
            Play();
            yield return new WaitForSeconds(AutoPlayDelays);
            yield return null;
        }
    }

    private void Play()
    {
        if (IsPlayAll)
        {
            for (int i = 0; i < AllVFX.Length; i++)
            {
                AllVFX[i].SendEvent("Play");
            }

        }
        else
        AllVFX[CurrentVFXindex].SendEvent("Play");
    }
}
