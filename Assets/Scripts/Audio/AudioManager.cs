using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
   // ------------------------------------------------------
   // Member Vars
   // ------------------------------------------------------
   [SerializeField] private Sound[] sounds;
   public static AudioManager Instance;

   // ------------------------------------------------------
   // Init
   // ------------------------------------------------------
   void Awake()
   {
      if (Instance == null)
      {
         Instance = this;
      }
      else
      {
         Destroy(this.gameObject);
         return;
      }
      DontDestroyOnLoad(this);

      foreach (Sound s in sounds)
      {
         s.Init(gameObject.AddComponent<AudioSource>());
      }
   }

   // ------------------------------------------------------
   // Player
   // ------------------------------------------------------
   public void Play(string name)
   {
      Sound s = Array.Find(sounds, sound => sound.Name == name);
      if (s == null)
      {
         Debug.LogWarning("Sound: " + name + " not found");
         return;
      }
      s.Source.Play();
   }
}
