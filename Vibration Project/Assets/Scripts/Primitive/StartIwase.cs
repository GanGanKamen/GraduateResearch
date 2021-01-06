using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartIwase : MonoBehaviour
{
    [SerializeField] private Transform foward;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Sprite iwase1;
    [SerializeField] private Sprite iwase2;
    [SerializeField] private Sprite iwase3;
    [SerializeField] private Sprite iwase4;
    [SerializeField] private AudioClip[] voiceVol1;
    [SerializeField] private AudioClip[] voiceVol2;
    [SerializeField] private AudioClip[] voiceVol3;
    [SerializeField] private AudioClip voiceVol4;
    [SerializeField] private UnityEngine.UI.Text serialText;

    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;
    private bool canStart = false;
    private bool preCanStart = false;
    private bool isAction = false;
    private SerialPortUtility.SerialPortUtilityPro serialPort;
    private bool hasTest = false;
    static public bool CheckOnce = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SetUpCoroutine());

    }

    // Update is called once per frame
    void Update()
    {
        CheckFrontUpdate();
        CheckCanStartUpdate();
        KeyCtrlUpdate();
    }

    public void TestRead(object data)
    {
        if (hasTest) return;
        var text = data as string;
        float paramater = 0;
        string[] arr = text.Split(',');

        if (arr.Length < 1) return;

        var canPerse = float.TryParse(arr[0], out paramater);
        if (canPerse) paramater = float.Parse(arr[0]);
        if (!float.IsNaN(paramater))
        {
            StartCoroutine(SerialTestOver());
        }

    }

    private IEnumerator SerialTestOver()
    {
        hasTest = true;
        serialPort.Write("z");
        yield return new WaitForSeconds(1f);
        serialPort.Close();
        serialText.text = "Sucess!";
        yield break;
    }
    private IEnumerator SetUpCoroutine()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(RandomAudioClip(voiceVol1));
        spriteRenderer.sprite = iwase1;
        serialPort = GameObject.Find("SerialPort").GetComponent
           <SerialPortUtility.SerialPortUtilityPro>();
        if (CheckOnce == false)
        {
           
            serialPort.ReadCompleteEventObject.AddListener(TestRead);
            serialPort.Write("z");
            yield return new WaitForSeconds(1f);
            serialPort.Close();
            yield return new WaitForSeconds(1f);
            serialPort.Open();
            serialPort.Write("t");
            CheckOnce = true;
        }
        else
        {
            serialPort.Close();
            serialText.text = "Sucess!";
        }
            
        yield break;
    }

    private void GotoNext(string name)
    {
        if (isAction == false)
        {
            Fader.FadeIn(5f, name, true);
            isAction = true;
            audioSource.PlayOneShot(RandomAudioClip(voiceVol3));
            spriteRenderer.sprite = iwase3;
        }

    }

    private void CheckFrontUpdate()
    {
        var vec = Vector3.Scale((foward.position - transform.position)
            , new Vector3(1, 0, 1)).normalized;
        var cameraVec = Vector3.Scale(mainCamera.transform.forward,
            new Vector3(1, 0, 1));

        var dot = Vector3.Dot(vec, cameraVec);
        //Debug.Log(dot); // dot<-0.9f
        if (dot <= -0.9f) canStart = true;
        else canStart = false;
    }

    private void CheckCanStartUpdate()
    {
        if (canStart != preCanStart)
        {
            if (isAction == false)
            {
                switch (canStart)
                {
                    case true:
                        spriteRenderer.sprite = iwase2;
                        if (audioSource.isPlaying == false)
                            audioSource.PlayOneShot(RandomAudioClip(voiceVol2));
                        break;
                    case false:
                        spriteRenderer.sprite = iwase1;
                        if (audioSource.isPlaying == false)
                            audioSource.PlayOneShot(RandomAudioClip(voiceVol1));
                        break;
                }
            }

            preCanStart = canStart;
        }
    }

    private void KeyCtrlUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(GameQuit());
        }
        if (canStart)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                GotoNext("Vib");
            }
            else if (Input.GetKeyDown(KeyCode.Z))
            {
                GotoNext("VibHeat");
            }
        }
    }

    private AudioClip RandomAudioClip(AudioClip[] clips)
    {
        var size = clips.Length;
        var r = Random.Range(0, size);
        return clips[r];
    }

    private IEnumerator GameQuit()
    {
        serialPort.Write("z");
        isAction = true;
        audioSource.PlayOneShot(voiceVol4);
        spriteRenderer.sprite = iwase4;
        while (audioSource.isPlaying) yield return null;
        yield return new WaitForSeconds(1f);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
      UnityEngine.Application.Quit();
#endif
    }
}
