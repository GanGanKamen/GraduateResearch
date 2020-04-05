using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sound
{
    public class PlayerSound : MonoBehaviour
    {
        [SerializeField] AudioClip jump;
        [SerializeField] AudioClip land;
        [SerializeField] AudioClip hit;

        public void Jump()
        {
            GetComponent<AudioSource>().PlayOneShot(jump);
        }

        public void Land()
        {
            GetComponent<AudioSource>().PlayOneShot(land);
        }

        public void Hit()
        {
            GetComponent<AudioSource>().PlayOneShot(hit);
        }
    }
}


