using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop; 
        }
    }

   public void PlaySound(string _name)
    {
        foreach (Sound s in sounds)
        {
            if (s.name == _name)
                s.source.Play();
        }
    }
}
