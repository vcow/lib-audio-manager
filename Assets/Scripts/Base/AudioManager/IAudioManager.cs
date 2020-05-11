using System;
using System.Collections.Generic;
using UnityEngine;

namespace Base.AudioManager
{
	public class VolumeChangedEventArgs : EventArgs
	{
		public float Value { get; }
		public float PreviousValue { get; }

		public VolumeChangedEventArgs(float value, float previousValue)
		{
			Value = value;
			PreviousValue = previousValue;
		}
	}

	public class SoundStateChangedEventArgs : EventArgs
	{
		public int SoundId { get; }
		public bool IsPlaying { get; }

		public SoundStateChangedEventArgs(int soundId, bool isPlaying)
		{
			SoundId = soundId;
			IsPlaying = isPlaying;
		}
	}

	public class MuteChangedEventArgs : EventArgs
	{
		public bool IsMuted { get; }

		public MuteChangedEventArgs(bool isMuted)
		{
			IsMuted = isMuted;
		}
	}

	public interface IAudioManager
	{
		/// <summary>
		/// Зарегистрировать аудиоклипы.
		/// </summary>
		/// <param name="clips">Список клипов в формате [идентификатор]:[клип].</param>
		/// <param name="language">Язык, которому соответствуют регистрируемые клипы.</param>
		void RegisterClips(Dictionary<string, AudioClip> clips, SystemLanguage language = SystemLanguage.Unknown);

		/// <summary>
		/// Удалить регистрацию для клипов.
		/// </summary>
		/// <param name="clipIds">Список идентификаторов удаляемых клипов.</param>
		void UnregisterClips(IEnumerable<string> clipIds);

		/// <summary>
		/// Играть фоновую музыку.
		/// </summary>
		/// <param name="id">Идентификатор клипа в AudioSettings.</param>
		/// <param name="fadeDuration">Время фейда с предыдущим треком.</param>
		/// <param name="language">Язык, для которого воспроизводится музыка.</param>
		/// <param name="restart">Следует ли перезапустить файл если он уже воспроизводится.</param>
		/// <returns>Возвращает <code>true</code>, если музыка успешно воспроизведена.</returns>
		bool PlayMusic(string id, float fadeDuration = 1f, SystemLanguage language = SystemLanguage.Unknown,
			bool restart = true);

		/// <summary>
		/// Играть звук.
		/// </summary>
		/// <param name="id">Идентификатор клипа в AudioSettings, или null, если музыку следует выключить.</param>
		/// <param name="muffleOthersPercent">Процент гашения (0...1) других звуков на время проигрывания звука.</param>
		/// <param name="priority">Приоритет при проигрывании (звуки с высоким приоритетом в последнюю очередь
		/// удаляются при превышении лимита воспроизводимых звуков).</param>
		/// <param name="loopCount">Количество воспроизведений, бесконечно, если 0.</param>
		/// <param name="language">Язык, для которого воспроизводится звук.</param>
		/// <param name="audioSource">Источник звука, <code>null</code>, если источник не известен.</param>
		/// <returns>Возвращает Уникальный идентификатор воспроизводимого звука,
		/// или 0, если звук не воспроизведен.</returns>
		int PlaySound(string id, float muffleOthersPercent = 0, int priority = 0,
			int loopCount = 1, SystemLanguage language = SystemLanguage.Unknown, AudioSource audioSource = null);

		/// <summary>
		/// Проверить наличие клипа для указанной локализации.
		/// </summary>
		/// <param name="id">Идентификатор клипа.</param>
		/// <param name="language">Язык, для которого проверяется наличие клипа, если <code>null</code>,
		/// то возвращается <code>true</code> при наличии клипа для любого языка.</param>
		/// <returns>Возвращает <code>true</code>, если клип с указанным идентификатором найден.</returns>
		bool HasClip(string id, SystemLanguage? language = null);

		/// <summary>
		/// Остановить воспроизведение звука.
		/// </summary>
		/// <param name="soundId">Идентификатор звука, полученный из PlaySound().</param>
		void StopSound(int soundId);

		/// <summary>
		/// Задать новое значение уровня громкости для музыки.
		/// </summary>
		/// <param name="value">Новое значение уровня громкости (0...1).</param>
		void SetMusicVolume(float value);

		/// <summary>
		/// Задать новое значение уровня громкости для звуков.
		/// </summary>
		/// <param name="value">Новое значение уровня громкости (0...1).</param>
		void SetSoundVolume(float value);

		/// <summary>
		/// Заглушить музыку.
		/// </summary>
		bool MuteMusic { set; get; }

		/// <summary>
		/// Заглушить звуки.
		/// </summary>
		bool MuteSound { set; get; }

		/// <summary>
		/// Текущий уровень громкости для музыки.
		/// </summary>
		float MusicVolume { get; }

		/// <summary>
		/// Текущий уровень громкости для звуков.
		/// </summary>
		float SoundVolume { get; }

		/// <summary>
		/// Событие изменения гашения музыки.
		/// </summary>
		event EventHandler<MuteChangedEventArgs> MuteMusicChangedEvent;

		/// <summary>
		/// Событие изменения гашения звука.
		/// </summary>
		event EventHandler<MuteChangedEventArgs> MuteSoundChangedEvent;

		/// <summary>
		/// Событие изменения уровня громкости музыки.
		/// </summary>
		event EventHandler<VolumeChangedEventArgs> MusicVolumeChangedEvent;

		/// <summary>
		/// Событие изменения уровня громкости звука.
		/// </summary>
		event EventHandler<VolumeChangedEventArgs> SoundVolumeChangedEvent;

		/// <summary>
		/// Событие начала/завершения воспроизведения звука.
		/// </summary>
		event EventHandler<SoundStateChangedEventArgs> SoundStateChangedEvent;
	}
}