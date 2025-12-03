using HarmonyLib;
using RimWorld;
using Verse;

namespace AnimalBehaviours;

[HarmonyPatch(typeof(BiomeDef))]
[HarmonyPatch("CommonalityOfAnimal")]
public static class AlphaMythology_BiomeDef_CommonalityOfAnimal_Patch
{
	[HarmonyPostfix]
	public static void MultiplyMythAnimalCommonality(PawnKindDef animalDef, ref float __result)
	{
		if (animalDef.defName.Contains("MM_"))
		{
			float mythAnimalSpawnMultiplier = MagicalMenagerie_Mod.settings.mythAnimalSpawnMultiplier;
			__result *= mythAnimalSpawnMultiplier;
		}
	}
}
