using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FrameworkExtension
{
    [RequireComponent(typeof(Image))]
	public class UIFrameAnimation : MonoBehaviour
	{
        public List<Sprite> Sprites = new List<Sprite>();
        public int Frame;
        public bool Loop;
        public bool StartOnAwake;

        private Image image;
        private float time;
        private float span;
        private int index;
        private int count;
        private bool play = false;

        private void Awake()
        {
            image = GetComponent<Image>();
            count = Sprites.Count;
            if (count == 0)
            {
                enabled = false;
                return;
            }
            image.sprite = Sprites[0];
            span = 1f / Frame;
            index = 0;
            if (StartOnAwake)
            {
                play = true;
            }
        }

        public void Play()
        {
            play = true;
        }

        public void Reinit()
        {
            image.sprite = Sprites[0];
            play = false;
            index = 0;
        }

        private void Update()
        {
            if (!play)
            {
                return;
            }
            time += Time.deltaTime;
            if (time >= span)
            {
                index++;
                if (index >= count)
                {
                    if (!Loop)
                    {
                        play = false;
                        return;
                    }
                    index = 0;
                }
                image.sprite = Sprites[index];
                time = time - span;
            }
        }
    }
}
