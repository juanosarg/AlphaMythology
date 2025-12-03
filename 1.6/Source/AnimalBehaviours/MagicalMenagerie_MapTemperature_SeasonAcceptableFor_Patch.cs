using System.Linq;
using HarmonyLib;
using Verse;

namespace AnimalBehaviours;

[HarmonyPatch(typeof(MapTemperature))]
[HarmonyPatch("SeasonAcceptableFor")]
public static class MagicalMenagerie_MapTemperature_SeasonAcceptableFor_Patch
{
	[HarmonyPostfix]
	public static void AllowAnimalSpawns(ThingDef animalRace, ref bool __result)
	{
		if (MagicalMenagerie_Mod.settings.pawnSpawnStates != null && MagicalMenagerie_Mod.settings.pawnSpawnStates.Keys.Contains(animalRace.defName) && MagicalMenagerie_Mod.settings.pawnSpawnStates[animalRace.defName])
		{
			__result = false;
		}
	}
}
