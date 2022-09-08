using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using SiraUtil.Affinity;
using SiraUtil.Submissions;
using Zenject;

namespace IgnoreWrongColor {
	class BadcutPatch : IAffinity {
		[Inject] Submission submission = null;

		public BadcutPatch(bool doDisableSubmission) {
			if(!doDisableSubmission)
				submission = null;
		}

		[AffinityPrefix, AffinityPatch(typeof(GameNoteController), nameof(GameNoteController.HandleCut))]
		bool HandleCut(GameNoteController __instance, Saber saber) {
			if(saber.saberType.MatchesColorType(__instance.noteData.colorType))
				return true;
			
			if(submission != null) {
				submission.DisableScoreSubmission("IgnoreWrongColor");
				submission = null;
			}

			return false;
		}
	}
}
