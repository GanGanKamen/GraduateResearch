using System.Collections;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.UI;

public class DemoPanelController : MonoBehaviour
{   
    [Header("UI buttons")]
    public Button AlembicPCBtn;
    public Button AlembicMobileBtn;
    public Button VFXSheetBtn;
    public Button DropSpotsBtn;
    public Button DropPuddlesBtn;
    public Button ComplexVFXBtn;
    [Space]
    [Header("Prefabs")]
    public GameObject[] AlembicPC;
    public MeshSwaperAnimation[] AlembicMobile;
    public VisualEffect[] VFXGraphSheet;
    public GameObject SpotParticlesPrefab;
    public GameObject PuddleParticlesPrefab;
    public ComplexVFX ComplexVFX;
    [Space]
    [Header("Environment")]
    public Transform SpawnPoint;
    public GameObject Bricks;
    public GameObject SpotsAndPuddles;

    private IEnumerator _currentSequence;
    private float _sequenceDelay = 1f;

    private void Awake()
    {
        SubscribeButtons();
    }
    private void OnDestroy()
    {
        UnsubscribeButtons();
    }

    public void SubscribeButtons()
    {
        AlembicPCBtn.onClick.AddListener(()=>
        {
            ShowSpotsAndPaddleHelpers(false);
            ResetCurrentSequence(PlayAlembicPC());
        });
        AlembicMobileBtn.onClick.AddListener(() => 
        {
            ShowSpotsAndPaddleHelpers(false);
            ResetCurrentSequence(PlayAlembicMobile());
        });
        VFXSheetBtn.onClick.AddListener(() => 
        {
            ShowSpotsAndPaddleHelpers(false);
            ResetCurrentSequence(PlayVFXGraph());
        });
        DropSpotsBtn.onClick.AddListener(() => 
        {
            ShowSpotsAndPaddleHelpers(true);
            SpawnDrops(true);
        });
        DropPuddlesBtn.onClick.AddListener(() => 
        {
            ShowSpotsAndPaddleHelpers(true);
            SpawnDrops(false);
        });
        ComplexVFXBtn.onClick.AddListener(() => 
        {
            ShowSpotsAndPaddleHelpers(true);
            SpawnComplexVFX();
        });
    }

    public void UnsubscribeButtons()
    {
        AlembicPCBtn.onClick.RemoveAllListeners();
        AlembicMobileBtn.onClick.RemoveAllListeners();
        VFXSheetBtn.onClick.RemoveAllListeners();
        DropSpotsBtn.onClick.RemoveAllListeners();
        DropPuddlesBtn.onClick.RemoveAllListeners();
        ComplexVFXBtn.onClick.RemoveAllListeners();
    }

    /// <summary>
    /// Run Alembic sequence
    /// </summary>
    /// <returns></returns>
    private IEnumerator PlayAlembicPC()
    {
        for (int i = 0; i < AlembicPC.Length; i++)
        { 
            AlembicPC[i].SetActive(true);
            yield return new WaitForSeconds(_sequenceDelay);
            yield return null;
        }
    }
    /// <summary>
    /// Run vertex animation sequence
    /// </summary>
    /// <returns></returns>
    private IEnumerator PlayAlembicMobile()
    {
        for (int i = 0; i < AlembicMobile.Length; i++)
        {
            AlembicMobile[i].Play();
            yield return new WaitForSeconds(_sequenceDelay);
            yield return null;
        }
    }
    /// <summary>
    /// Run 2d VFXGraph sequence
    /// </summary>
    /// <returns></returns>
    private IEnumerator PlayVFXGraph()
    {
        for (int i = 0; i < VFXGraphSheet.Length; i++)
        {
            VFXGraphSheet[i].SendEvent("Play");
            yield return new WaitForSeconds(_sequenceDelay);
            yield return null;
        }
    }

    /// <summary>
    /// Drop spots or paddle particles
    /// </summary>
    /// <param name="IsSpots">true - spawn spots , false - spawn puddle</param>
    private void SpawnDrops(bool IsSpots)
    {
        GameObject spawnedVFX = Instantiate(IsSpots?SpotParticlesPrefab: PuddleParticlesPrefab, SpawnPoint.position,Quaternion.identity,null);
    }

    /// <summary>
    /// Run all effects combined animation
    /// </summary>
    private void SpawnComplexVFX()
    {
        ComplexVFX.Play();
    }

    /// <summary>
    /// Stop current played sequence and play new
    /// </summary>
    /// <param name="newSequence"></param>
    private void ResetCurrentSequence(IEnumerator newSequence)
    {   
        if(_currentSequence != null)
        StopCoroutine(_currentSequence);
        _currentSequence = newSequence;
        StartCoroutine(_currentSequence);
    }
    /// <summary>
    /// Show demonstration gameobjects
    /// </summary>
    /// <param name="isShow"></param>
    private void ShowSpotsAndPaddleHelpers(bool isShow)
    {
        Bricks.SetActive(isShow);
        SpotsAndPuddles.SetActive(isShow);
    }

}
