using Base.AudioManager;
using Zenject;

namespace Sample
{
	public class GameInstaller : MonoInstaller<GameInstaller>
	{
		public override void InstallBindings()
		{
			Container.Bind<IAudioManager>().FromComponentInNewPrefabResource(@"AudioManager").AsSingle();
		}
	}
}