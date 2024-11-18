using EliCDavis.RecordAndPlay;
using EliCDavis.RecordAndPlay.Playback;
using EliCDavis.RecordAndPlay.Record;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class RecordManager : MonoBehaviour, IActorBuilder, IPlaybackCustomEventHandler
{
    public static RecordManager instance;
    [SerializeField]
    //private BallLauncher ballLauncher;
    //[SerializeField]
    private RocketNew rocket;

    [SerializeField]
    private GameObject[] objectsToTrack;

    private Vector3[] positions;

    private Recorder recorder;

    private PlaybackBehavior playback;

    [SerializeField]
    private GameObject ballActor;

    [SerializeField]
    private GameObject BallLauncherActor;


    [SerializeField]
    private GameObject rocketActor;

    [SerializeField]
    private GameObject deathEffect;

    [SerializeField]
    private GameObject collisionEffect;
    private void Awake()
    {
        instance = this;
    }
    public Actor Build(int actorId, string actorName, Dictionary<string, string> metadata)
    {
        switch (actorName)
        {
            case "BallLauncer":
                return new Actor(Instantiate(BallLauncherActor), this);

            case "Ball":
                return new Actor(Instantiate(ballActor), this);

            case "Rocket":
                return new Actor(Instantiate(rocketActor), this);
        }
        return null;
    }

    public void OnCustomEvent(SubjectRecording subject, CustomEventCapture customEvent)
    {
        switch (customEvent.Name)
        {
            case "Box Destroyed":
                var dirtyCords = customEvent.Contents.Split(' ');
                
                Destroy(Instantiate(deathEffect, new Vector3(float.Parse(dirtyCords[0]), -10, float.Parse(dirtyCords[1])), Quaternion.Euler(-90, 0, 0)), 3f);
                break;

            case "Collision":
                var dirtyCollisionCords = customEvent.Contents.Split(' ');
                Destroy(Instantiate(collisionEffect, new Vector3(float.Parse(dirtyCollisionCords[0]), float.Parse(dirtyCollisionCords[1]), float.Parse(dirtyCollisionCords[2])), Quaternion.identity), 3f);
                break;

            default:
                Debug.LogWarningFormat("Don't know how to handle event type: {0}", customEvent.Name);
                break;
        }
    }

    void Start()
    {
        recorder = ScriptableObject.CreateInstance<Recorder>();
        SetUpObjectsToTrack();

    }
    private void SetUpObjectsToTrack()
    {
        for (int i = 0; i < objectsToTrack.Length; i++)
        {
            SubjectBehavior.Build(objectsToTrack[i], recorder);
        }

    }
    public void StartRecording()
    {
        recorder.ClearSubjects();
        SetUpObjectsToTrack();
        recorder.Start();
        if (playback != null)
        {
            playback.Stop();
            Destroy(playback.gameObject);
        }
    }
    public void StopRecording(string fileName)
    {
        playback = PlaybackBehavior.Build(recorder.Finish(), this, this, true);
        SaveRecording(fileName);
    }
    public void SaveRecording(string fileName)
    {
        using (FileStream fs = File.Create(string.Format("{0}/"+ fileName + ".rap", Application.persistentDataPath)))
        {
            var rec = playback.GetRecording();
            rec.RecordingName = "pp_"+fileName;
            EliCDavis.RecordAndPlay.IO.Packager.Package(fs, rec);
        }
    }
    void OnGUI()
    {
        switch (recorder.CurrentState())
        {
            case RecordingState.Recording:
                if (recorder.CurrentlyRecording() && GUI.Button(new Rect(10, 10, 120, 25), "Pause"))
                {
                    recorder.Pause();
                }


                if (GUI.Button(new Rect(10, 40, 120, 25), "Finish"))
                {
                    playback = PlaybackBehavior.Build(recorder.Finish(), this, this, true);
                    ClearObjectsToTrack();
                }
                break;

            case RecordingState.Paused:
                if (recorder.CurrentlyPaused() && GUI.Button(new Rect(10, 10, 120, 25), "Resume"))
                {
                    recorder.Resume();
                }

                if (GUI.Button(new Rect(10, 40, 120, 25), "Finish"))
                {
                    playback = PlaybackBehavior.Build(recorder.Finish(), this, this, true);
                    ClearObjectsToTrack();
                }
                break;

            case RecordingState.Stopped:
                if (GUI.Button(new Rect(10, 10, 120, 25), "Start Recording"))
                {
                    recorder.ClearSubjects();
                    SetUpObjectsToTrack();
                    recorder.Start();
                    if (playback != null)
                    {
                        playback.Stop();
                        Destroy(playback.gameObject);
                    }
                }
                if (playback != null)
                {
                    GUI.Box(new Rect(10, 50, 120, 250), "Playback");
                    if (playback.CurrentlyPlaying() == false && GUI.Button(new Rect(15, 75, 110, 25), "Start"))
                    {
                        playback.Play();
                    }

                    if (playback.CurrentlyPlaying() && GUI.Button(new Rect(15, 75, 110, 25), "Pause"))
                    {
                        playback.Pause();
                    }

                    GUI.Label(new Rect(55, 105, 100, 30), "Time");
                    GUI.Label(new Rect(55, 125, 100, 30), playback.GetTimeThroughPlayback().ToString("0.00"));
                    playback.SetTimeThroughPlayback(GUI.HorizontalSlider(new Rect(15, 150, 100, 30), playback.GetTimeThroughPlayback(), 0.0F, playback.RecordingDuration()));

                    GUI.Label(new Rect(20, 170, 100, 30), "Playback Speed");
                    GUI.Label(new Rect(55, 190, 100, 30), playback.GetPlaybackSpeed().ToString("0.00"));
                    playback.SetPlaybackSpeed(GUI.HorizontalSlider(new Rect(15, 215, 100, 30), playback.GetPlaybackSpeed(), -8, 8));

                    if (GUI.Button(new Rect(15, 250, 110, 25), "Save"))
                    {

                        //SaveToAssets(playback.GetRecording(), "Demo");
                        using (FileStream fs = File.Create(string.Format("{0}/demo.rap", Application.dataPath)))
                        {
                            var rec = playback.GetRecording();
                            rec.RecordingName = "Demo";
                            EliCDavis.RecordAndPlay.IO.Packager.Package(fs, rec);
                        }

                    }
                }
                break;
        }
    }

    private void ClearObjectsToTrack()
    {
        for (int i = objectsToTrack.Length - 1; i >= 0; i--)
        {
            Destroy(objectsToTrack[i]);
        }
    }

    int boxesDestroyed;

    void Update()
    {
        if (recorder.CurrentlyRecording())
        {
            for (int i = objectsToTrack.Length - 1; i >= 0; i--)
            {
                if (objectsToTrack[i] != null && objectsToTrack[i].transform.position.y < -10)
                {
                    Destroy(objectsToTrack[i]);
                    boxesDestroyed++;
                    //recorder.SetMetaData("Boxes Destroyed", boxesDestroyed.ToString());
                    var pos = objectsToTrack[i].transform.position;
                    recorder.CaptureCustomEvent("Box Destroyed", string.Format("{0} {1}", pos.x, pos.z));
                }
            }

        }
    }



}