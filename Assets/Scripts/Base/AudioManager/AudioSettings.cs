using System.Collections.Generic;
using UnityEngine;

namespace Base.AudioManager
{
	public abstract class AudioSettings : ScriptableObject
	{
		public abstract Dictionary<SystemLanguage, Dictionary<string, AudioClip>> Clips { get; }
	}
}