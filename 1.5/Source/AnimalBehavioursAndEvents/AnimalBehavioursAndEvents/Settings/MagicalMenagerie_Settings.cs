using RimWorld;
using UnityEngine;
using Verse;
using System.Collections.Generic;
using System.Linq;
using System;


namespace AnimalBehaviours
{
    public class MagicalMenagerie_Settings : ModSettings

    {
        private static Vector2 scrollPosition = Vector2.zero;
        public Dictionary<string, bool> pawnSpawnStates = new Dictionary<string, bool>();
        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Collections.Look(ref pawnSpawnStates, "pawnSpawnStates", LookMode.Value, LookMode.Value, ref pawnKeys, ref boolValues);
        }
        private List<string> pawnKeys;
        private List<bool> boolValues;
        private string searchKey;

        public const float mythAnimalSpawnMultiplierBase = 1;
        public float mythAnimalSpawnMultiplier = mythAnimalSpawnMultiplierBase;

        public void DoWindowContents(Rect inRect)
        {

           

            var rect = new Rect(inRect.x, inRect.y, inRect.width, inRect.height);
            Verse.Text.Anchor = TextAnchor.MiddleLeft;
            var searchLabel = new Rect(rect.x + 5, rect.y, 60, 24);
            Widgets.Label(searchLabel, "VEF_AnimalsSearch".Translate());
            var searchRect = new Rect(searchLabel.xMax + 5, searchLabel.y, 200, 24f);
            searchKey = Widgets.TextField(searchRect, searchKey);
            Verse.Text.Anchor = TextAnchor.UpperLeft;

            List<string> keys = pawnSpawnStates.Keys.ToList().OrderBy(x => DefDatabase<ThingDef>.GetNamedSilentFail(x)?.label)?.Where(x => DefDatabase<ThingDef>.GetNamedSilentFail(x)?.label.ToLower().
            Contains(searchKey.ToLower()) == true)?.ToList();
            Listing_Standard ls = new Listing_Standard();
           
            ls.Label("MM_MythAnimalSpawnMultiplier".Translate() + ": " + mythAnimalSpawnMultiplier, -1, "MM_MythAnimalSpawnMultiplierTooltip".Translate());
            mythAnimalSpawnMultiplier = (float)Math.Round(ls.Slider(mythAnimalSpawnMultiplier, 0.1f, 5f), 2);

            if (ls.Settings_Button("MM_Reset".Translate(), new Rect(0f, searchRect.yMax + 50, 180f, 29f)))
            {
                mythAnimalSpawnMultiplier = mythAnimalSpawnMultiplierBase;
            }

            Rect outRect = new Rect(inRect.x, searchRect.yMax + 100, inRect.width, inRect.height-50);
            Rect viewRect = new Rect(0f, 0f, inRect.width - 30f, keys.Count * 24 + 24);
            Widgets.BeginScrollView(outRect, ref scrollPosition, viewRect, true);
            ls.Begin(viewRect);

            if (keys.Count > 0)
            {
                for (int num = 0; num < keys.Count; num++)
                {

                    bool test = pawnSpawnStates[keys[num]];
                    if (DefDatabase<PawnKindDef>.GetNamedSilentFail(keys[num]) == null)
                    {
                        pawnSpawnStates.Remove(keys[num]);
                    }
                    else
                    {
                        var iconRect = new Rect(0, (num + 1) * 24, 24, 24);
                        var labelRect = new Rect(30, (num + 1) * 24, outRect.width - 100f, 24);
                        Widgets.ThingIcon(iconRect, PawnKindDef.Named(keys[num]).race);
                        Widgets.CheckboxLabeled(labelRect, "MM_DisableAnimal".Translate(PawnKindDef.Named(keys[num]).LabelCap), ref test);


                        pawnSpawnStates[keys[num]] = test;
                    }
                }

            }
            Widgets.EndScrollView();

            

            ls.End();
            
            base.Write();


        }


    }

   

}
