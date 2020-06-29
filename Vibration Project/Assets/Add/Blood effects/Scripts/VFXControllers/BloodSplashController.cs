using UnityEngine;

public class BloodSplashController : MonoBehaviour
{   
    [Tooltip("Destroy when animation finish")]
    public bool DestroyOnFinish;

    public void Destroy()
    {
        if (DestroyOnFinish)
            Destroy(gameObject);
        else
            gameObject.SetActive(false);
    }
}
