using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public BuildingPreview[] previews;

    [SerializeField] private SpriteRenderer previewRenderer;
    public GameObject preview;
    private BuildingPreview currentPreview;
    private int currentPreviewIndex;

    private bool inBuildMode;

    void Start()
    {
        currentPreviewIndex = 0;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            SwitchBuildMode();
        }

        if (inBuildMode)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SetCurrentPreview(0);
            }

            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SetCurrentPreview(1);
            }

            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                SetCurrentPreview(2);
            }

            PreviewFollowMouse();


            Vector2 rayStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Physics2D.Raycast(rayStart, Vector3.forward, 1, currentPreview.buildingLayer))
            {
                previewRenderer.color = new Color32(255, 255, 255, 100);

                if (Input.GetMouseButtonDown(0))
                {
                    Instantiate(currentPreview.buildObject, rayStart, Quaternion.identity);
                }
            }

            else
            {
                previewRenderer.color = new Color32(255, 0, 0, 100);
            }
        }
    }

    private void SwitchBuildMode()
    {
        if (inBuildMode)
        {
            DisableBuildMode();
        }

        else
        {
            EnableBuildMode();
        }
    }

    private void EnableBuildMode()
    {
        inBuildMode = true;
        previewRenderer.enabled = true;
        SetCurrentPreview(currentPreviewIndex);
    }

    private void DisableBuildMode()
    {
        inBuildMode = false;
        previewRenderer.enabled = false;
    }

    private void PreviewFollowMouse()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;
        preview.transform.position = mouseWorldPos;
    }

    private void SetCurrentPreview(int index)
    {
        currentPreviewIndex = index;
        currentPreview = previews[currentPreviewIndex];
        previewRenderer.sprite = currentPreview.previewSprite;
    }

    private void OnDrawGizmos()
    {
        Vector3 rayStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        rayStart.z = 0;
        Gizmos.DrawRay(rayStart, Vector3.forward);
    }
}

