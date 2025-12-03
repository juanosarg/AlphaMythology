using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;

namespace AnimalBehaviours;

public class MagicalMenagerie_Mod : Mod
{
	public static MagicalMenagerie_Settings settings;

	public MagicalMenagerie_Mod(ModContentPack content)
		: base(content)
	{
		settings = GetSettings<MagicalMenagerie_Settings>();
	}

	public override string SettingsCategory()
	{
		return "MM_ModName".Translate();
	}

	public override void DoSettingsWindowContents(Rect inRect)
	{
		base.DoSettingsWindowContents(inRect);
		MMToggleableSpawnDef mMToggleableSpawnDef = DefDatabase<MMToggleableSpawnDef>.AllDefsListForReading.Where((MMToggleableSpawnDef k) => k.defName == "MM_ToggleableAnimals").RandomElement();
		if (settings.pawnSpawnStates == null)
		{
			settings.pawnSpawnStates = new Dictionary<string, bool>();
		}
		foreach (string toggleablePawn in mMToggleableSpawnDef.toggleablePawns)
		{
			if (!settings.pawnSpawnStates.ContainsKey(toggleablePawn))
			{
				settings.pawnSpawnStates[toggleablePawn] = false;
			}
		}
		settings.DoWindowContents(inRect);
	}
}
