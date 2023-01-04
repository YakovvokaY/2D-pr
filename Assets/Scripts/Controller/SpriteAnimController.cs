using System;
using System.Collections.Generic;
using UnityEngine;

namespace MVCMPlatformer
{
    public class SpriteAnimController : IDisposable
    {
        private sealed class Animation
        {
            public AnimState Track;
            public List<Sprite> Sprites;
            public bool Loop = true;
            public float Speed = 10;
            public float Counter = 0;
            public bool Sleep;

            public void Update()
            {
                if (Sleep) return;
                Counter += Time.captureDeltaTime * Speed;

                if(Loop)
                {
                    while (Counter> Sprites.Count)
                    {
                        Counter -= Sprites.Count;
                    }
                }
                else if (Counter>Sprites.Count)
                {
                    Counter = Sprites.Count;
                    Sleep = true;
                }
            }
        }
        private AnimConfig _config;
        private Dictionary<SpriteRenderer, Animation> _activeAnim = new Dictionary<SpriteRenderer, Animation>();
        public SpriteAnimController(AnimConfig config)
        {
            _config = config;
        }
        public void StartAnimation(SpriteRenderer spriteRenderer,AnimState track,bool loop, float speed)
        {
            if (_activeAnim.TryGetValue(spriteRenderer, out var animation))
            {
                animation.Loop = loop;
                animation.Speed = speed;
                animation.Sleep = false;
                if (animation.Track != track)
                {
                    animation.Track = track;
                    animation.Sprites = _config.Sequences.Find(secuence => secuence.Track == track).Sprites;
                    animation.Counter = 0;
                }
            }
            else
            {
                _activeAnim.Add(spriteRenderer, new Animation()
                {
                    Track = track,
                    Sprites = _config.Sequences.Find(sequence => sequence.Track == track).Sprites,
                    Loop = loop,
                    Speed = speed
                });
            }
        }

        public void StopAnim(SpriteRenderer sprite)
        {
            if (_activeAnim.ContainsKey(sprite))
            {
                _activeAnim.Remove(sprite);
            }
        }

        public void Update()
        {
            foreach (var animation in _activeAnim)
            {
                animation.Value.Update();

                if (animation.Value.Counter<animation.Value.Sprites.Count)
                {
                    animation.Key.sprite = animation.Value.Sprites[(int)animation.Value.Counter];
                }
            }
        }
        public void Dispose()
        {
            _activeAnim.Clear();
        }
    }
}