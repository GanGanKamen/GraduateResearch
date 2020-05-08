using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace System
{
    public class GameSystem : MonoBehaviour
    {
        [SerializeField] private string nextSceneName;
        static public Player.BaseInfo baseInfo;


        // Start is called before the first frame update
        void Start()
        {
            DontDestroyOnLoad(gameObject);
            UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneName);
        }
    }
}

