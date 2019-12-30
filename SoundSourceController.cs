using UnityEngine;
using UnityEngine.Events;

namespace Base.AudioManager
{
	[DisallowMultipleComponent]
	[RequireComponent(typeof(AudioSource))]
	public class SoundSourceController : MonoBehaviour
	{
		private AudioSource _audioSource;

		public AudioSource AudioSource => _audioSource ? _audioSource : _audioSource = GetComponent<AudioSource>();
		
		public UnityEvent DestroyEvent { get; } = new UnityEvent();

		private void OnDestroy()
		{
			DestroyEvent.Invoke();
			DestroyEvent.RemoveAllListeners();
		}
	}
}