using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectLibrary : MonoBehaviour
{
	[SerializeField] private SoundEffectGroup[] soundEffectGroups;
	private Dictionary<string, List<AudioClip>> soundDictionary;
	private void Awake()
	{
		InitializeDictionary();
	}
	private void InitializeDictionary()
	{
		soundDictionary = new Dictionary<string, List<AudioClip>>();
		foreach (SoundEffectGroup soundEffectGroup in soundEffectGroups)
		{
			soundDictionary.Add(soundEffectGroup.name, soundEffectGroup.audioClips);
		}
	}
	public AudioClip GetRandomClip(string name)
	{
		if (soundDictionary.ContainsKey(name))
		{
			List<AudioClip> audioClips = soundDictionary[name];
			if (audioClips.Count > 0)
			{
				int randomIndex = Random.Range(0, audioClips.Count);
				return audioClips[randomIndex];
			}
		}
		return null;
	}
}

[System.Serializable]
public class SoundEffectGroup
{
	public string name;
	public List<AudioClip> audioClips;
}