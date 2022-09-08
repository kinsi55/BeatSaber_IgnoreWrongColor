using HarmonyLib;
using IPA;
using IPA.Config;
using IPA.Config.Stores;
using SiraUtil.Zenject;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
using IPALogger = IPA.Logging.Logger;

namespace IgnoreWrongColor {
	[Plugin(RuntimeOptions.SingleStartInit)]
	public class Plugin {
		internal static Plugin Instance;
		internal static IPALogger Log;

		[Init]
		public Plugin(IPALogger logger, IPA.Config.Config conf, Zenjector zenjector) {
			Instance = this;
			Log = logger;
			Config.Instance = conf.Generated<Config>();

			zenjector.Install<SoloInstaller>(Location.StandardPlayer);

			SongCore.Collections.RegisterCapability("IgnoreWrongColor");
		}

		[OnEnable]
		public void OnEnable() {
			BeatSaberMarkupLanguage.GameplaySetup.GameplaySetup.instance.AddTab("IgnoreWrongColor", "IgnoreWrongColor.toggle.bsml", Config.Instance);
		}
	}
}
