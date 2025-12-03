using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;

namespace AnimalBehaviours;

public class MagicalMenagerie_Settings : ModSettings
{
	private static Vector2 scrollPosition = Vector2.zero;

	public Dictionary<string, bool> pawnSpawnStates = new Dictionary<string, bool>();

	private List<string> pawnKeys;

	private List<bool> boolValues;

	private string searchKey;

	public const float mythAnimalSpawnMultiplierBase = 1f;

	public float mythAnimalSpawnMultiplier = 1f;

	public override void ExposeData()
	{
		base.ExposeData();
		Scribe_Collections.Look(ref pawnSpawnStates, "pawnSpawnStates", LookMode.Value, LookMode.Value, ref pawnKeys, ref boolValues);
		Scribe_Values.Look(ref mythAnimalSpawnMultiplier, "mythAnimalSpawnMultiplier", 1f, forceSave: true);
	}

	public void DoWindowContents(Rect inRect)
	{
		Rect rect = new Rect(inRect.x, inRect.y, inRect.width, inRect.height);
		Text.Anchor = TextAnchor.MiddleLeft;
		Rect rect2 = new Rect(rect.x + 5f, rect.y, 60f, 24f);
		Widgets.Label(rect2, "VEF_AnimalsSearch".Translate());
		Rect rect3 = new Rect(rect2.xMax + 5f, rect2.y, 200f, 24f);
		searchKey = Widgets.TextField(rect3, searchKey);
		Text.Anchor = TextAnchor.UpperLeft;
		Widgets.Label(new Rect(rect.x + 5f, rect.y + 40f, inRect.width, 24f), string.Concat("MM_MythAnimalSpawnMultiplier".Translate() + ": ", mythAnimalSpawnMultiplier.ToString()));
		TooltipHandler.TipRegion(new Rect(rect.x + 5f, rect.y + 40f, inRect.width, 24f), "MM_MythAnimalSpawnMultiplierTooltip".Translate());
		mythAnimalSpawnMultiplier = (float)Math.Round(Widgets.HorizontalSlider(new Rect(rect.x + 5f, rect.y + 64f, inRect.width, 24f), mythAnimalSpawnMultiplier, 0.1f, 5f), 2);
		if (Widgets.ButtonText(new Rect(rect.x + 5f, rect.y + 88f, 90f, 29f), "MM_Reset".Translate(), drawBackground: true, doMouseoverSound: true, active: true, null))
		{
			mythAnimalSpawnMultiplier = 1f;
		}
		List<string> list = (from x in pawnSpawnStates.Keys.ToList()
			orderby DefDatabase<ThingDef>.GetNamedSilentFail(x)?.label
			select x)?.Where((string x) => DefDatabase<ThingDef>.GetNamedSilentFail(x)?.label.ToLower().Contains(searchKey.ToLower()) ?? false)?.ToList();
		Listing_Standard listing_Standard = new Listing_Standard();
		Rect outRect = new Rect(inRect.x, rect3.yMax + 100f, inRect.width, inRect.height - 150f);
		Rect rect4 = new Rect(0f, 0f, inRect.width - 30f, list.Count * 24 + 24);
		Widgets.BeginScrollView(outRect, ref scrollPosition, rect4);
		listing_Standard.Begin(rect4);
		if (list.Count > 0)
		{
			for (int i = 0; i < list.Count; i++)
			{
				bool checkOn = pawnSpawnStates[list[i]];
				if (DefDatabase<PawnKindDef>.GetNamedSilentFail(list[i]) == null)
				{
					pawnSpawnStates.Remove(list[i]);
					continue;
				}
				Rect rect5 = new Rect(0f, (i + 1) * 24, 24f, 24f);
				Rect rect6 = new Rect(30f, (i + 1) * 24, outRect.width - 100f, 24f);
				Widgets.ThingIcon(rect5, PawnKindDef.Named(list[i]).race, null, null, 1f, null, null);
				Widgets.CheckboxLabeled(rect6, "MM_DisableAnimal".Translate(PawnKindDef.Named(list[i]).LabelCap), ref checkOn);
				pawnSpawnStates[list[i]] = checkOn;
			}
		}
		Widgets.EndScrollView();
		listing_Standard.End();
		Write();
	}
}
