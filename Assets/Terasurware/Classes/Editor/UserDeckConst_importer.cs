using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;
using System.Xml.Serialization;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

public class UserDeckConst_importer : AssetPostprocessor {
	private static readonly string filePath = "Assets/BundleConst/Excel/UserDeckConst.xls";
	private static readonly string exportPath = "Assets/BundleConst/Resources/UserDeckConst.asset";
	private static readonly string[] sheetNames = { "Members", };
	
	static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
	{
		foreach (string asset in importedAssets) {
			if (!filePath.Equals (asset))
				continue;
				
			Entity_Members data = (Entity_Members)AssetDatabase.LoadAssetAtPath (exportPath, typeof(Entity_Members));
			if (data == null) {
				data = ScriptableObject.CreateInstance<Entity_Members> ();
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

					Entity_Members.Sheet s = new Entity_Members.Sheet ();
					s.name = sheetName;
				
					for (int i=1; i<= sheet.LastRowNum; i++) {
						IRow row = sheet.GetRow (i);
						ICell cell = null;
						
						Entity_Members.Param p = new Entity_Members.Param ();
						
					cell = row.GetCell(0); p.ArmyOrder = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(1); p.id = (int)(cell == null ? 0 : cell.NumericCellValue);
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