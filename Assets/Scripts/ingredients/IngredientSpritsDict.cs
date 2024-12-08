using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AYellowpaper.SerializedCollections;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class IngredientSpritesDict : ScriptableObject
{
    public SerializedDictionary<Ingredient, Sprite> spriteDict = new SerializedDictionary<Ingredient, Sprite>();

#if UNITY_EDITOR
    [MenuItem("Assets/Create/IngredientListSprites")]
    public static void CreateRailTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save pic dic", "New pic dic", "Asset", "Save pic dic", AssetDatabase.GetAssetPath(Selection.activeObject));
        if (path == "") return;
        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<IngredientSpritesDict>(), path);
    }
#endif
}
