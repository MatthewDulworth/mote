using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
   // ------------------------------------------------------
   // Member Vars
   // ------------------------------------------------------
   public string Name;
   public AudioClip Clip;

   public bool PlayOnAwake;
   public bool Loop;

   [Range(0,1)] public float Volume; 
   [Range(0.1f,3)] public float Pitch;

   [HideInInspector] public AudioSource Source;

   // ------------------------------------------------------
   // Member Vars
   // ------------------------------------------------------
   public void Init(AudioSource src)
   {
      Source = src;
      Source.clip = Clip;
      Source.playOnAwake = PlayOnAwake;
      Source.loop = Loop;
      Source.volume = Volume;
      Source.pitch = Pitch;
   }
}
