using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlacementManager : MonoBehaviour
{
    [Header("AR References")]
    public ARRaycastManager raycastManager;
    public ARAnchorManager anchorManager;

    [Header("Training Area")]
    public GameObject trainingAreaPrefab;

    private GameObject spawnedTrainingArea;

    private static readonly System.Collections.Generic.List<ARRaycastHit> hits =
        new System.Collections.Generic.List<ARRaycastHit>();

    void Update()
    {
        if (spawnedTrainingArea != null)
            return;

        if (Touchscreen.current == null)
            return;

        if (!Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
            return;

        Vector2 touchPosition =
            Touchscreen.current.primaryTouch.position.ReadValue();

        // Ignore UI
        if (EventSystem.current != null &&
            EventSystem.current.IsPointerOverGameObject())
            return;

        if (raycastManager.Raycast(
            touchPosition,
            hits,
            TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = hits[0].pose;

            SpawnTrainingArea(hitPose);
        }
    }

    void SpawnTrainingArea(Pose pose)
    {
        GameObject anchorObject = new GameObject("TrainingAreaAnchor");

        anchorObject.transform.SetPositionAndRotation(
            pose.position,
            pose.rotation
        );

        ARAnchor anchor = anchorObject.AddComponent<ARAnchor>();

        if (anchor == null)
        {
            Debug.LogError("Failed to create AR Anchor!");
            Destroy(anchorObject);
            return;
        }

        spawnedTrainingArea = Instantiate(
            trainingAreaPrefab,
            anchor.transform
        );

        spawnedTrainingArea.transform.localPosition = Vector3.zero;
        spawnedTrainingArea.transform.localRotation = Quaternion.identity;
        spawnedTrainingArea.transform.localScale = Vector3.one;

        Debug.Log("Training Area spawned successfully!");
    }
}