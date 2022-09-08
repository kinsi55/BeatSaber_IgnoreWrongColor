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
		bool doAllowSubmission = false;

		public BadcutPatch(bool doDisableSubmission) {
			doAllowSubmission = !doDisableSubmission;
		}

		[AffinityPrefix, AffinityPatch(typeof(GameNoteController), nameof(GameNoteController.HandleCut))]
		bool HandleCut(GameNoteController __instance, Saber saber) {
			if(saber.saberType.MatchesColorType(__instance.noteData.colorType))
				return true;
			
			if(!doAllowSubmission && submission != null) {
				submission.DisableScoreSubmission("IgnoreWrongColor");
				submission = null;
			}

			return false;
		}
	}
}
