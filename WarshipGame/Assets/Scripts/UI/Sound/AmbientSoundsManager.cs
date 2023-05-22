using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientSoundsManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] audioClips;
    [SerializeField] private AudioListener audioListener;
    private float _audioInterval = 25;
    private AudioSource _audioSource;
    // Start is called before the first frame update
    private void Awake()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();
    }
 
    // Update is called once per frame
    private void Update()
    {
        _audioInterval -= Time.deltaTime;
        
            if (_audioInterval > 0) return;
        _audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
        _audioSource.Play();
        _audioInterval = 25;
    }
}
