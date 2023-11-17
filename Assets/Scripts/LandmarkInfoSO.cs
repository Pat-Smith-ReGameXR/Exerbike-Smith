using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLandmark", menuName = "Custom Objects/Landmark Info", order = 1)]
public class LandmarkInfoSO : ScriptableObject
{
    [SerializeField]
    [Tooltip("Place the name for the landmark here.")]
    string landmarkName;

    [Tooltip("Keep this checked if you are using Latitude-Longitude coordinates, uncheck it if you are using in-game Vector3 coordinates. " +
        "If using Lat-Long, the game will automatically convert it to Vector3. (planned, not yet implemented)")]
    public bool convertFromCoordinatesToVector3 = true;

    [Tooltip("Use this for the real-life Latitude-Longitude coordinates of a landmark.")]
    public Vector2 realLifeCoords;
    [Tooltip("Use this for the in-game Vector2 coordinates of a landmark.")]
    public Vector2 inGameVector2;

    [SerializeField]
    [Tooltip("When a player approaches this landmmark, it will play this audio clip describing the object.")]
    AudioClip descriptionAudioClip;

    public string LandmarkName { get { return landmarkName; } }
    public AudioClip LandmarkDescAudio { get { return descriptionAudioClip; } }

    public Vector3 GetLandmarkLocation()
    {
        Vector3 newCoords = Vector3.zero;

        if (convertFromCoordinatesToVector3)
        {
            //TODO: convert coordinates to Vector3 here!!!
            newCoords = GPSEncoder.GPSToUCS(realLifeCoords);
            inGameVector2 = new Vector2 (newCoords.x, newCoords.z);
        }
        else
        {
            newCoords = new Vector3(inGameVector2.x, -20f, inGameVector2.y);
            realLifeCoords = GPSEncoder.USCToGPS(newCoords);
        }

        return newCoords;
    }
}
