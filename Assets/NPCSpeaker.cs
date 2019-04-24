using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpeaker : MonoBehaviour
{
    [SerializeField]
    AudioSource _audioSource;

    [SerializeField]
    AudioClip[] _allSounds;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = this.gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SayThing(int soundIndex)
    {
        _audioSource.clip = _allSounds[soundIndex];
        _audioSource.Play();
    }
}
