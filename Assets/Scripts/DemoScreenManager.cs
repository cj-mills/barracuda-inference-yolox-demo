using System.Collections;
using UnityEngine;
using CJM.MediaDisplay;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class DemoScreenManager : BaseScreenManager
{
    [Header("GUI")]
    [Tooltip("Toggle to use webcam as input source")]
    [SerializeField] private Toggle useWebcamToggle;
    [Tooltip("Dropdown menu with available webcam devices")]
    [SerializeField] private Dropdown webcamDropdown;

    // Called when the script instance is being loaded.
    private void Start()
    {
        Initialize();
        UpdateDisplay();
        InitializeDropdown();
    }

    // Initialize the GUI dropdown list
    private void InitializeDropdown()
    {
        webcamDropdown.ClearOptions();
        webcamDropdown.AddOptions(GetWebcamNames());
        UpdateWebcamDeviceSelection();
    }

    // Get the names of all available webcams
    private List<string> GetWebcamNames()
    {
        List<string> webcamNames = new List<string>();
        foreach (WebCamDevice device in webcamDevices)
        {
            webcamNames.Add(device.name);
        }
        return webcamNames;
    }

    // Update the selected value of the webcam dropdown
    private void UpdateWebcamDeviceSelection()
    {
        int selectedIndex = Array.FindIndex(webcamDevices, device => device.name == currentWebcam);
        webcamDropdown.SetValueWithoutNotify(selectedIndex);
    }

    // Updates the display with the current texture (either a test texture or the webcam feed).
    protected override void UpdateDisplay()
    {
        // Call the base class implementation first
        base.UpdateDisplay();
        // Update the GUI webcam toggle
        UpdateUseWebcamToggle();
    }

    // Update is called once per frame.
    private void Update()
    {
        // Toggle between image and webcam feed on key press.
        if (Input.GetKeyUp(toggleKey))
        {
            useWebcam = !useWebcam;
            UpdateDisplay(); // Call the function to update the display.
        }
    }

    // Update the useWebcamToggle to match the useWebcam value
    private void UpdateUseWebcamToggle()
    {
        // Check if the useWebcamToggle value matches the useWebcam value before updating
        if (useWebcamToggle.isOn != useWebcam)
        {
            // Set the useWebcamToggle value without invoking its event
            useWebcamToggle.SetIsOnWithoutNotify(useWebcam);
        }
    }


    // Update the useWebcam option and the display when the webcam toggle changes
    public void UpdateWebcamToggle(bool useWebcam)
    {
        this.useWebcam = useWebcam;
        UpdateDisplay();
    }

    // Update the current webcam device and the display when the webcam dropdown selection changes
    public void UpdateWebcamDevice()
    {
        currentWebcam = webcamDevices[webcamDropdown.value].name;
        useWebcam = webcamDevices.Length > 0 ? useWebcam : false;
        UpdateDisplay();
    }

    // Handle the texture change event.
    protected override void HandleMainTextureChanged(Material material)
    {
        // Call the base class implementation first
        base.HandleMainTextureChanged(material);
        // Update the GUI webcam toggle
        UpdateUseWebcamToggle();
    }
}
