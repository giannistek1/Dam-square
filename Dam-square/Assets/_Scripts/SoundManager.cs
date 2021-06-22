using System.Runtime.InteropServices;
using _Scripts;
using UnityEngine;
using UnityEngine.UI;

public static class SoundManager 
{
    #region Fields
	public enum Sound 
	{
		ButtonClick,
		ObjectPlaced,
		ObjectCannotBePlaced,
		ObjectPickedUp
	}
	#endregion

	#region Unity Methods
    
	#endregion

	public static void PlaySound(Sound sound)
	{
		GameObject soundGameObject = new GameObject("Sound");
		AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
		audioSource.volume = 0.2f;
		audioSource.PlayOneShot(GetAudioClip(sound));
	}

	private static AudioClip GetAudioClip(Sound sound)
	{
		foreach (GameAssets.SoundAudioClip soundAudioClip in GameAssets.Instance.soundAudioClipArray)
		{
			if (soundAudioClip.sound == sound)
			{
				return soundAudioClip.audioClip;
			}
		}
		Debug.LogError("Sound " + sound + " not found!");
		return null;
	}

	/*public static void AddButtonSounds(this Button buttonUI)
	{
		buttonUI.ClickFunc += () => SoundManager.PlaySound(Sound.ButtonClick);
		
	}*/
}
