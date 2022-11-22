using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FadeMusic
{
    // Start is called before the first frame update

    public static IEnumerator StartFade(AudioSource music, float duration, float targetVol){
        float currentTime = 0;
        float start = music.volume;

        while(currentTime < duration){
            currentTime += Time.deltaTime;
            music.volume = Mathf.Lerp(start, targetVol, currentTime/duration);
            yield return null;
        }
        yield break;
    }
}
