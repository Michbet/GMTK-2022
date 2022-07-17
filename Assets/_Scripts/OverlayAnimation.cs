using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts
{
    public class OverlayAnimation : MonoBehaviour
    {
        public Animator anim;
        public Image image;

        private void Start()
        {
            anim = anim == null ? GetComponent<Animator>() : anim;
            image = image == null ? GetComponent<Image>() : image;
        }

        private void Update()
        {
            if (anim)
            {
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("None"))
                    image.enabled = false;
                else
                    image.enabled = true;
            }
            else
                image.enabled = false;

        }

        public void Play(string stateName)
        {
            anim.Play(stateName);
        }
    }
}