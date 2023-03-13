using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Audio;


public class Menu : MonoBehaviour
{

    public AudioMixer audioMixer1; //creates a variable to store the audiomixer
    public AudioMixer audioMixer2; //creates a variable to store the audiomixer
    public void LevelSelect()
    {
        string name = EventSystem.current.currentSelectedGameObject.name; //creates a string containing the name of the object holding the script
        int i = System.Convert.ToInt32(name); //converts the name to an integer
        SceneManager.LoadScene(i); //loads the scene of the corresponding index
    }

    //this method is called when the music volume slider is changed
    public void SetMusicVolume(float volume)
    {
        audioMixer1.SetFloat("MusicVolume", volume); //the mixer "MusicVolume" is set to the value of the slider
    }

    //this method is called when the sound effects volume slider is changed
    public void SetSoundEffectsVolume(float volume)
    {

        audioMixer2.SetFloat("SoundEffectsVolume", volume); //the mixer "SoundEffectsVolume" is set to the value of the slider
    }
}


