using UnityEngine;

namespace _Scripts
{
	public class ClientConfigurationManager : MonoBehaviour
	{
		#region Fields
    
		private string key = "PlayerConfiguration";
		public ClientConfiguration clientConfiguration { get; set; }
    
		#endregion
	
		#region Singleton
		private static ClientConfigurationManager _instance;
		public static ClientConfigurationManager Instance { get { return _instance; } }
		#endregion
	
		#region Unity Methods

		private void Awake()
		{
			#region Singleton
			if (_instance != null && _instance != this)
			{
				Destroy(this.gameObject);
			} else {
				_instance = this;
			}
			#endregion
		
			if (PlayerPrefs.HasKey(key))
				LoadPlayerSettings();
			else
				InitDefaultSettings();
		}

		#endregion
	
		private void InitDefaultSettings()
		{
			if (clientConfiguration == null)
				clientConfiguration = new ClientConfiguration();

			clientConfiguration.IsFirstTimePlaying = true;
			clientConfiguration.SawChangelog = false;
			clientConfiguration.SawNewAchievement = true;

			clientConfiguration.preferredLanguage = Language.English;

			clientConfiguration.MusicOn = true;
			clientConfiguration.SoundOn = true;

			clientConfiguration.SoundVolume = 0;
			clientConfiguration.MusicVolume = 0;

			SavePlayerSettings();
		}
	
		#region Save/Load
		public void SavePlayerSettings()
		{
			string json = JsonUtility.ToJson(clientConfiguration);
			PlayerPrefs.SetString(key, json);
		}

		private void LoadPlayerSettings()
		{
			string jsonString = PlayerPrefs.GetString(key);
			clientConfiguration = JsonUtility.FromJson<ClientConfiguration>(jsonString);
		}
		#endregion

		[ContextMenu("Reset All Settings")]
		public void ResetAllSettings()
		{
			PlayerPrefs.DeleteKey(key);
		}
	}
}
