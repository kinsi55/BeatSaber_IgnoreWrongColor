using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

namespace IgnoreWrongColor {
	class SoloInstaller : MonoInstaller {
		public override void InstallBindings() {
			var difficultyBeatmap = Container.Resolve<IDifficultyBeatmap>();

			var hasRequirement = SongCore.Collections.RetrieveDifficultyData(difficultyBeatmap)?
					.additionalDifficultyData?
					._suggestions?.Any(x => x == "IgnoreWrongColor") == true;

			if(!hasRequirement && !Config.Instance.forceIgnoreWrongColor)
				return;

			Container.BindInterfacesTo<BadcutPatch>().AsSingle().WithArguments(!hasRequirement);
		}
	}
}
