using UnityEngine;
using TMPro; 

public class DetectionUI : MonoBehaviour
{
    [SerializeField] private GameObject uiPanel;
    [SerializeField] private TextMeshProUGUI displayNameText;

    private void OnEnable()
    {
        DetectionEvents.OnDetect += ShowUI;
        DetectionEvents.OnDetectCleared += HideUI;
    }

    private void OnDisable()
    {
        DetectionEvents.OnDetect -= ShowUI;
        DetectionEvents.OnDetectCleared -= HideUI;
    }

    private void ShowUI(string displayName)
    {
        if (!string.IsNullOrEmpty(displayName))
        {
            displayNameText.text = displayName;
            uiPanel.SetActive(true);
        }
    }

    private void HideUI()
    {
        uiPanel.SetActive(false);
    }
}
