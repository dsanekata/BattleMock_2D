using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;
using System.Xml.Serialization;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

public class CharacterConst_importer : AssetPostprocessor {
	private static readonly string filePath = "Assets/BundleConst/Excel/CharacterConst.xls";
	private static readonly string exportPath = "Assets/BundleConst/Excel/CharacterConst.asset";
	private static readonly string[] sheetNames = { "Army","Enemy", };
	
	static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
	{
		foreach (string asset in importedAssets) {
			if (!filePath.Equals (asset))
				continue;
				
			Entity_Army data = (Entity_Army)AssetDatabase.LoadAssetAtPath (exportPath, typeof(Entity_Army));
			if (data == null) {
				data = ScriptableObject.CreateInstance<Entity_Army> ();
				AssetDatabase.CreateAsset ((ScriptableObject)data, exportPath);
				data.hideFlags = HideFlags.NotEditable;
			}
			
			data.sheets.Clear ();
			using (FileStream stream = File.Open (filePath, FileMode.Open, FileAccess.Read)) {
				IWorkbook book = new HSSFWorkbook (stream);
				
				foreach(string sheetName in sheetNames) {
					ISheet sheet = book.GetSheet(sheetName);
					if( sheet == null ) {
						Debug.LogError("[QuestData] sheet not found:" + sheetName);
						continue;
					}

					Entity_Army.Sheet s = new Entity_Army.Sheet ();
					s.name = sheetName;
				
					for (int i=1; i<= sheet.LastRowNum; i++) {
						IRow row = sheet.GetRow (i);
						ICell cell = null;
						
						Entity_Army.Param p = new Entity_Army.Param ();
						
					cell = row.GetCell(0); p.id = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(1); p.maxHp = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(2); p.attack = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(3); p.defence = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(4); p.speed = (float)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(5); p.attackRange = (float)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(6); p.attackInterval = (float)(cell == null ? 0 : cell.NumericCellValue);
						s.list.Add (p);
					}
					data.sheets.Add(s);
				}
			}

			ScriptableObject obj = AssetDatabase.LoadAssetAtPath (exportPath, typeof(ScriptableObject)) as ScriptableObject;
			EditorUtility.SetDirty (obj);
		}
	}
}
