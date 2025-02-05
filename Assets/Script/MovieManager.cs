using UnityEngine;
using UnityEngine.Video;

public class MovieManager : Singleton<MovieManager>
{
    public string videoURL = "https://hggamesstudio.biz.id/video/Test.mp4"; // Ganti dengan URL video
    public string videoURL1 = "https://www.learningcontainer.com/wp-content/uploads/2020/05/sample-mp4-file.mp4";
    public string videoURL2 = "https://hggamesstudio.biz.id/video/Test.mp4";

    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private AudioSource audioSource;
    void Start()
    {
        PlayVideo(videoURL);
    }

    public void PlayVideo(string url)
    {
        //// Tambahkan VideoPlayer ke GameObject
        //videoPlayer = gameObject.AddComponent<VideoPlayer>();

        //// Tambahkan AudioSource untuk suara video
        //audioSource = gameObject.AddComponent<AudioSource>();

        // Atur sumber video ke URL
        //videoPlayer.source = VideoSource.Url;
        //videoPlayer.url = videoURL;
        videoPlayer.url = url;

        // Atur agar video bisa diputar otomatis
        videoPlayer.playOnAwake = false;
        videoPlayer.waitForFirstFrame = true;

        // Sinkronkan AudioSource dengan VideoPlayer
        //videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        //videoPlayer.EnableAudioTrack(0, true);
        //videoPlayer.SetTargetAudioSource(0, audioSource);

        // Event saat video siap diputar
        videoPlayer.prepareCompleted += (vp) =>
        {
            Debug.Log("Video siap diputar!");
            videoPlayer.Play();
        };

        // Mulai mempersiapkan video
        videoPlayer.Prepare();
    }
}
