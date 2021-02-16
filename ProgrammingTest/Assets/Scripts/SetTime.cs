using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetTime : MonoBehaviour
{
    [SerializeField] private GameObject hours, minutes, seconds;
    [SerializeField] private GameObject separatorHM, separatorMS, amPM;
    [SerializeField] private Color activeText, standardText;

    private System.DateTime newTime;
    private Clock display;
    private bool format12hr = false;
    private bool formatAM = false;
    private bool timeDisplay;
    private int nHour, nMinute, nSecond;

    private Text hText, mText, sText, amText;

    private List<Text> activeElements;
    private int selectedElement = 0;

    private void Awake()
    {
        //get all text elements
        hText = hours.GetComponentInChildren<Text>();
        mText = minutes.GetComponentInChildren<Text>();
        sText = seconds.GetComponentInChildren<Text>();
        amText = amPM.GetComponentInChildren<Text>();

        activeElements = new List<Text>() { hText, mText, sText, amText };
    }

    void Update()
    {
        //display time
        sText.text = DoubleDigit(nSecond);
        mText.text = DoubleDigit(nMinute);
        hText.text = DoubleDigit(nHour);

        if (formatAM)
        {
            amText.text = Clock.AMString;
        }
        else
        {
            amText.text = Clock.PMString;
        }

        HandleKeyboardNavigation();

    }

    public void CloseSetTimePanel()
    {
        newTime = new System.DateTime(1, 1, 1, CalcHour(nHour), nMinute, nSecond);
        display.SetTime(newTime, formatAM);
        activeElements[selectedElement].color = standardText;
        selectedElement = 0;
        gameObject.SetActive(false);
    }

    public void StartSetTime(Clock display)
    {
        this.display = display;

        nHour = display.GetHour();
        nMinute = display.GetMinute();
        nSecond = display.GetSecond();

        if (display.GetType().ToString() == "TimeDisplay")
        {
            timeDisplay = true;
            format12hr = display.Get12hr();
            formatAM = display.GetAM();
        }
        else
        {
            timeDisplay = false;
        }

        gameObject.SetActive(true);

        if (timeDisplay)
        {
            if (format12hr)
            { //12hr display
                separatorMS.SetActive(false);
                seconds.SetActive(false);
                amPM.SetActive(true);
            }
            else
            { //24hr display
                separatorMS.SetActive(false);
                seconds.SetActive(false);
                amPM.SetActive(false);
            }
        }
        else
        { //countdown display
            separatorMS.SetActive(true);
            seconds.SetActive(true);
            amPM.SetActive(false);
            format12hr = false;
        }

        activeElements[selectedElement].color = activeText;
    }

    // ----- Time Change Buttons ---------------

    public void IncrementHours()
    {
        nHour++;
        if(timeDisplay && format12hr && nHour > Clock.MaxHours12)
        {
            nHour = Clock.MinHours12;
        }
        else if (nHour > Clock.MaxHours24)
        {
            nHour = Clock.MinimumHMS;
        }
    }

    public void DecrementHours()
    {
        nHour--;
        if (timeDisplay && format12hr && nHour < Clock.MinHours12)
        {
            nHour = Clock.MaxHours12;
        }
        else if (nHour < Clock.MinimumHMS)
        {
            nHour = Clock.MaxHours24;
        }
    }

    public void IncrementMinutes()
    {
        nMinute++;
        if(nMinute > Clock.MaxMinutes)
        {
            nMinute = Clock.MinimumHMS;
        }
    }

    public void DecrementMinutes()
    {
        nMinute--;
        if (nMinute < Clock.MinimumHMS)
        {
            nMinute = Clock.MaxMinutes;
        }
    }

    public void IncrementSeconds()
    {
        nSecond++;
        if (nSecond > Clock.MaxSeconds)
        {
            nSecond = Clock.MinimumHMS;
        }
    }

    public void DecrementSeconds()
    {
        nSecond--;
        if (nSecond < Clock.MinimumHMS)
        {
            nSecond = Clock.MaxSeconds;
        }
    }

    public void ToggleAmPm()
    {
        formatAM = !formatAM;
    }

    // ----- Keyboard Navigation ---------------

    private void HandleKeyboardNavigation()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            GoToNextDigit();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            GoToPrevDigit();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            switch (selectedElement)
            {
                case 0:
                    IncrementHours();
                    break;
                case 1:
                    IncrementMinutes();
                    break;
                case 2:
                    IncrementSeconds();
                    break;
                case 3:
                    ToggleAmPm();
                    break;
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            switch (selectedElement)
            {
                case 0:
                    DecrementHours();
                    break;
                case 1:
                    DecrementMinutes();
                    break;
                case 2:
                    DecrementSeconds();
                    break;
                case 3:
                    ToggleAmPm();
                    break;
            }
        }
    }

    private void GoToNextDigit()
    {
        activeElements[selectedElement].color = standardText;
        selectedElement++;

        if(selectedElement >= activeElements.Count)
        {
            selectedElement = 0;
        }

        //Skip over elements that are inactive/hidden
        while (!activeElements[selectedElement].IsActive())
        {
            selectedElement++;

            if (selectedElement >= activeElements.Count)
            {
                selectedElement = 0;
            }
        }

        activeElements[selectedElement].color = activeText;
    }

    private void GoToPrevDigit()
    {
        activeElements[selectedElement].color = standardText;
        selectedElement--;

        if (selectedElement < 0)
        {
            selectedElement = activeElements.Count - 1;
        }

        //Skip over elements that are inactive/hidden
        while (!activeElements[selectedElement].IsActive())
        {
            selectedElement--;

            if (selectedElement < 0)
            {
                selectedElement = activeElements.Count - 1;
            }
        }

        activeElements[selectedElement].color = activeText;
    }


    // ----- Formatting ---------------

    private int CalcHour(int hour)
    {
        if (format12hr)
        {
            if (!formatAM && hour != Clock.MaxHours12)
            {
                hour += Clock.MaxHours12;
            }
            else if (formatAM && hour == Clock.MaxHours12)
            {
                hour = Clock.MinimumHMS;
            }
        }

        return hour;
    }

    private static string DoubleDigit(int time)
    {
        return time.ToString().PadLeft(2, '0');
    }

}
