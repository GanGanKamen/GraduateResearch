using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostManager : MonoBehaviour
{
    public enum Action
    {
        Up,
        Down
    }
    [SerializeField] private PostProcessVolume processVolume;
    [SerializeField] private float dampingDelta;
    [Header("Vignette")]
    [SerializeField] private Color vignetteColor;
    [SerializeField] private float vignetteMinIntensity;
    [Header("Grain")]
    [SerializeField] private float grainMinIntensity;
    [Header("Ambient")]
    [SerializeField] private float ambientMaxIntensity;

    public bool IsPost { get { return _isPost; } }

    private bool _isPost = false;
    private FloatParameter vignetteIntensity;
    private FloatParameter grainIntensity;
    private FloatParameter ambientIntensity;
    private float vigIntensity = 0;
    private float graIntensity = 0;
    private float ambIntensity = 0;

    private float vigDelta;
    private float graDelta;
    private float ambDelta;

    public void Init(float maxHP)
    {
        if (processVolume == null) Debug.LogError("processVolume,null");
        var profile = processVolume.profile;
        profile.GetSetting<Vignette>().color.value = vignetteColor;
        vignetteIntensity = profile.GetSetting<Vignette>().intensity;
        vignetteIntensity.value = 0;
        vigDelta = (1 - vignetteMinIntensity) / maxHP;
        profile.GetSetting<Vignette>().active = false;
        grainIntensity = profile.GetSetting<Grain>().intensity;
        grainIntensity.value = 0;
        graDelta = (1 - grainMinIntensity) / maxHP;
        ambientIntensity = profile.GetSetting<AmbientOcclusion>().intensity;
        ambientIntensity.value = 0;
        ambDelta = ambientMaxIntensity / maxHP;

    }

    public void PostON()
    {
        if (_isPost) return;
        _isPost = true;
        vigIntensity = vignetteMinIntensity;
        processVolume.profile.GetSetting<Vignette>().active = true;
        graIntensity = grainMinIntensity;
        ambIntensity = 0;
    }

    public void PostOFF()
    {
        if (_isPost == false) return;
        _isPost = false;
        var profile = processVolume.profile;
        profile.GetSetting<Vignette>().color.value = vignetteColor;
        vignetteIntensity = profile.GetSetting<Vignette>().intensity;
        vignetteIntensity.value = 0;
        profile.GetSetting<Vignette>().active = false;
        grainIntensity = profile.GetSetting<Grain>().intensity;
        grainIntensity.value = 0;
        ambientIntensity = profile.GetSetting<AmbientOcclusion>().intensity;
        ambientIntensity.value = 0;
    }

    public void GetPostAction(Action action)
    {
        if (_isPost == false) return;
        switch (action)
        {
            case Action.Up:
                if (vigIntensity > vignetteMinIntensity)
                {
                    vigIntensity -= vigDelta;
                    if (vigIntensity < vignetteMinIntensity) vigIntensity = vignetteMinIntensity;
                }
                if (graIntensity > grainMinIntensity)
                {
                    graIntensity -= graDelta;
                    if (graIntensity < grainMinIntensity) graIntensity = grainMinIntensity;
                } 
                if (ambIntensity > 0)
                {
                    ambIntensity -= ambDelta;
                    if (ambIntensity < 0) ambIntensity = 0;
                }
                break;
            case Action.Down:
                if(vigIntensity < 1)
                {
                    vigIntensity += vigDelta;
                    if (vigIntensity > 1) vigIntensity = 1;
                }
                if (graIntensity < 1)
                {
                    graIntensity += graDelta;
                    if (graIntensity > 1) graIntensity = 1;
                }
                if(ambIntensity < ambientMaxIntensity)
                {
                    ambIntensity += ambDelta;
                    if (ambIntensity > ambientMaxIntensity) ambIntensity = ambientMaxIntensity;
                }
                break;
        }
    }

    public void PostUpdate()
    {
        if (_isPost == false) return;
        vigUpdate();
        graUpdate();
        ambUpdate();
    }

    private void vigUpdate()
    {
        if(vignetteIntensity.value > vigIntensity)
        {
            vignetteIntensity.value -= Time.deltaTime * dampingDelta;
            if (vignetteIntensity.value <= vigIntensity) vignetteIntensity.value = vigIntensity;
        }
        else if (vignetteIntensity.value < vigIntensity)
        {
            vignetteIntensity.value += Time.deltaTime * dampingDelta;
            if (vignetteIntensity.value >= vigIntensity) vignetteIntensity.value = vigIntensity;
        }
    }

    private void graUpdate()
    {
        if (grainIntensity.value > graIntensity)
        {
            grainIntensity.value -= Time.deltaTime * dampingDelta;
            if (grainIntensity.value <= graIntensity) grainIntensity.value = graIntensity;
        }
        else if (grainIntensity.value < graIntensity)
        {
            grainIntensity.value += Time.deltaTime * dampingDelta;
            if (grainIntensity.value >= graIntensity) grainIntensity.value = graIntensity;
        }
    }

    private void ambUpdate()
    {

        if (ambientIntensity.value > ambIntensity)
        {
            ambientIntensity.value -= Time.deltaTime * dampingDelta;
            if (ambientIntensity.value <= ambIntensity) ambientIntensity.value = ambIntensity;
        }
        else if (ambientIntensity.value < ambIntensity)
        {
            ambientIntensity.value += Time.deltaTime * dampingDelta;
            if (ambientIntensity.value >= ambIntensity) ambientIntensity.value = ambIntensity;
        }
    }
}
