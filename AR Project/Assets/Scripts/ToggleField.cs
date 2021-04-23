using System.Collections;
// using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

[RequireComponent(typeof(Toggle))]
public class ToggleField : MonoBehaviour
{
    #region Private Constants

    const string togglePrefKey = "isGameCamera";
    
    #endregion
    #region MonoBehaviour CallBacks
    // Start is called before the first frame update
    void Start()
    {
        Toggle _toggleField = this.GetComponent<Toggle>();
        if (_toggleField != null)
        {
            if (PlayerPrefs.HasKey(togglePrefKey))
            {
                _toggleField.isOn = false;
                if (PlayerPrefs.GetString(togglePrefKey) == "true")
                {
                    _toggleField.isOn = true;
                }
            }
        }
    }
    #endregion

    #region Public Methods
    public void SetToggle(bool value)
    {
        if (value)
            PlayerPrefs.SetString(togglePrefKey, "true");
        else
            PlayerPrefs.SetString(togglePrefKey, "false");
    }
    #endregion
}
