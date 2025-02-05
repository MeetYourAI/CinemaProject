using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Video;
using YoutubeExtractor;

public class MovieManager : Singleton<MovieManager>
{
    [SerializeField] bool isPlayOnStart = false;
    public string videoURL = "https://hggamesstudio.biz.id/video/Test.mp4"; // Ganti dengan URL video
    [SerializeField] bool isYoutubeUrl = false;
    //public string videoURL1 = "https://www.learningcontainer.com/wp-content/uploads/2020/05/sample-mp4-file.mp4";
    //public string videoURL2 = "https://hggamesstudio.biz.id/video/Test.mp4";
    [SerializeField] string[] videoUrlsStore;

    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private AudioSource audioSource;
    [Space(20)]
    [SerializeField] SO_MovieDetails SO_movieDetails;
    [SerializeField] MovieChooserBtn buttonPrefab;
    [SerializeField] Transform parent;
    void Start()
    {
        if (isPlayOnStart)
        {
            if (isYoutubeUrl)
                StartCoroutine(WaitPlayYoutubeVideo(videoURL));
            else
                PlayUrlVideo(videoURL);
        }
        else
        {
            foreach(var j in SO_movieDetails.movieList)
            {
                var pp = Instantiate(buttonPrefab, parent);
                pp.Inits(j);
            }
        }
    }

    public void PlayVideoLink(string url)
    {
        if (url.ToLower().Contains("youtube"))
        {
            Debug.Log("Play <color=yellow>Youtube</color>");
            StopUrlVideo();
            //StopYoutubeVideo();
            PlayYoutubeVideo(url);
        }
        else
        {
            Debug.Log("Play <color=yellow>Url</color>");
            StopYoutubeVideo();
            StopUrlVideo();
            PlayUrlVideo(url);
        }
    }
    void StopYoutubeVideo()
    {
        WebviewController.Instance.Close();
    }
    void PlayYoutubeVideo(string url)
    {
        WebviewController.Instance.OpenLink(url);
    }
    void StopUrlVideo()
    {
        videoPlayer.Stop();
    }
    void PlayUrlVideo(string url)
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

    IEnumerator WaitPlayYoutubeVideo(string url)
    {
        yield return new WaitForSeconds(2);
        PlayYoutubeVideo(url);
        //// Dapatkan informasi video (video stream URL) menggunakan YoutubeExtractor
        //IEnumerable<VideoInfo> videoInfos = DownloadUrlResolver.GetDownloadUrls(url);
        //Debug.Log(videoInfos);
        //// Filter stream dengan tipe MP4; kamu bisa memilih resolusi yang diinginkan (misalnya tertinggi)
        //VideoInfo video = videoInfos
        //    .Where(info => info.VideoType == VideoType.Mp4)
        //    .OrderByDescending(info => info.Resolution)
        //    .FirstOrDefault();

        //if (video != null)
        //{
        //    // Jika video dilindungi cipher, YoutubeExtractor akan mengurus dekripsi (jika sudah mendukung)
        //    string newVideoUrl = video.DownloadUrl;
        //    Debug.Log("Video URL yang diekstrak: " + newVideoUrl);

        //    // Set URL VideoPlayer ke URL yang telah diekstrak
        //    videoPlayer.url = newVideoUrl;

        //    // Persiapkan VideoPlayer (prepare) untuk memuat video
        //    videoPlayer.Prepare();

        //    // Tunggu hingga VideoPlayer siap
        //    while (!videoPlayer.isPrepared)
        //    {
        //        yield return null;
        //    }

        //    // Mulai pemutaran video
        //    videoPlayer.Play();
        //}
        //else
        //{
        //    Debug.LogError("Gagal mengekstrak URL video. Pastikan link YouTube benar dan library kompatibel.");
        //}

        //videoPlayer.url = url;

        //// Atur agar video bisa diputar otomatis
        //videoPlayer.playOnAwake = false;
        //videoPlayer.waitForFirstFrame = true;

        //// Event saat video siap diputar
        //videoPlayer.prepareCompleted += (vp) =>
        //{
        //    Debug.Log("Video siap diputar!");
        //    videoPlayer.Play();
        //};

        //// Mulai mempersiapkan video
        //videoPlayer.Prepare();
    }
}
