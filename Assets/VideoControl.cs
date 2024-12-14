using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Video;

public class VideoControl : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Reference to the Video Player component
    public GameObject enableobject;

    public void Awake()
    {
        enableobject.SetActive(false);
    }
    private void Start()
    {
        // Subscribe to the loopPointReached event
        videoPlayer.loopPointReached += OnVideoEnd;
    }
    // Method to pause the video
    [Button]
    public void PauseVideo()
    {
        if (videoPlayer.isPlaying)
        {
            videoPlayer.Pause();
            Debug.Log("Video paused");
        }
    }
    [Button]
    // Method to resume the video
    public void ResumeVideo()
    {
        if (!videoPlayer.isPlaying && videoPlayer.time > 0)
        {
            videoPlayer.Play();
            Debug.Log("Video resumed");
        }
    }

    // Method to reset the video
    [Button]
    public void ResetVideo()
    {
        videoPlayer.Stop(); // Stops the video and resets the playback position
        videoPlayer.Play(); // Optional: Automatically starts playing from the beginning
        Debug.Log("Video reset");
    }
    private void OnVideoEnd(VideoPlayer vp)
    {
       enableobject.SetActive(true);
          
    }
    private void OnDestroy()
    {
        // Unsubscribe from the event to avoid memory leaks
        videoPlayer.loopPointReached -= OnVideoEnd;
    }
}
