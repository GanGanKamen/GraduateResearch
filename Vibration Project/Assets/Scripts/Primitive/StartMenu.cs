using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Primitive
{
    public class StartMenu : MonoBehaviour
    {
        [SerializeField] private Button oldButton;
        [SerializeField] private Button neoButton;

        private bool isAction = false;
        // Start is called before the first frame update
        void Start()
        {
            oldButton.onClick.AddListener(() => GotoNext("Vib"));
            neoButton.onClick.AddListener(() => GotoNext("VibHeat"));
        }

        private void Update()
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

        private void GotoNext(string name)
        {
            if(isAction == false)
            {
                Fader.FadeIn(2f, name,true);
                isAction = true;
            }

        }
    }
}


