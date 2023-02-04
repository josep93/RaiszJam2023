using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    AudioSource audio;
    float maxVolume = 0.5f;

    enum State
    {
        MainMenu,
        Playing
    }

    [SerializeField] AudioClip[] clips = new AudioClip[Enum.GetNames(typeof(State)).Length];

    State state;

    public static MusicManager current;

    private void Start()
    {
        if (current != null) {
            //whut??!! Esto nunca debería de pasar
            Destroy(gameObject);
        }
        state = State.MainMenu;
        current = this;
        audio = GetComponent<AudioSource>();
        ChoseTrack();
        audio.loop = true;
        audio.Play();
    }

    void ChoseTrack() {
        StartCoroutine(LowerVolume());
    }

    public void StartGame() {
        state = State.Playing;
        ChoseTrack();
    }

    IEnumerator RaiseVolume()
    {
        while (audio.volume < maxVolume)
        {
            yield return null;
        }
    }

    IEnumerator LowerVolume()
    {
        while (audio.volume > 0)
        {
            audio.volume -= 0.05f;
            yield return new WaitForSeconds(0.05f);
        }
        audio.volume = maxVolume;
        audio.clip = clips[(int)state];
        audio.Play();
    }
}
