using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private GameObject musicToFadeOut;
    [SerializeField] private GameObject musicToFadeIn;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        musicToFadeOut.GetComponent<MusicFade>().StartFadeOut();   
        musicToFadeIn.GetComponent<MusicFade>().StartFadeIn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CollectBugSound()
    {
        
        
        
    }
}
