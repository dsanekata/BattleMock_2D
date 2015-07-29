using UnityEngine;
using System.Collections;

public class RunningConst 
{

/*
	/// <summary>
	/// トランスフォームオープン開始イベント
	/// </summary>
	public static event Action<GameObject, RootType> OnOpenTransformEvent = null;
	/// <summary>
	/// トランスフォームオープン終了イベント
	/// </summary>
	public static event Action<GameObject, RootType> OnOpenedTransformEvent = null;
	/// <summary>
	/// トランスフォームクローズ開始イベント
	/// </summary>
	public static event Action<GameObject, RootType> OnCloseTransformEvent = null;
	/// <summary>
	/// トランスフォームクローズ終了イベント
	/// </summary>
	public static event Action<GameObject, RootType> OnClosedTransformEvent = null;
	
	/// <summary>
	/// 対象の盟友情報
	/// </summary>
	//	public static FriendConst.FriendData targetFriendData = null;
	/// <summary>
	/// 対象のお知らせ情報
	/// </summary>
	public static NotificationConst.NotifyData targetNotifyData = null;
	
	/// <summary>
	/// コールバック関数
	/// </summary>
	public static Action callBackAfterClosingWindow = null;
	
	#region [ボタン制御]
	public const string NGUI_BUTTON_POPUP = "01_MenuButtonPop";
	#endregion
	
	#region [ユティリティ]
	public const string NGUI2D_LOADING_ANCHOR = "65_Loading";
	#endregion
	
	#region [メニュー]
	public const string NGUI2D_MENU_ANCHOR = "02_MenuAnchor";
	#endregion
*/

	#region [Running Parameters]
//	public const string PinaName = "Pina";
	/// <summary>
	/// 初期速度
	/// </summary>
	public const float FIRST_SPEED = 7.0f;
	
	/// <summary>
	/// ジャンプの強さ
	/// </summary>
	public const float JUMP_POWER = 700.0f;
	
	/// <summary>
	/// 敵を踏んだときのジャンプの強さ
	/// </summary>
	public const float UPWARD_FORCE = JUMP_POWER * 0.7f;
	
	/// <summary>
	/// 初期のジャンプ回数
	/// </summary>
	public const int JUMP_LIMIT = 2;
	
	/// <summary>
	/// 速度を更新する時間
	/// </summary>
	public const float UPDATE_TIME = 1.0f;
	
	/// <summary>
	/// UPDATE_TIMEに増やす速度
	/// </summary>
	public const float SPEED_RATE = 0.1f;
		
	/// <summary>
	/// ダッシュ時間
	/// </summary>
	public const float DASH_TIME = 3.0f;
	
	/// <summary>
	/// 磁石有効時間
	/// </summary>
	public const float MAGNET_TIME = 4.0f;
	
	/// <summary>
	/// 無敵有効時間
	/// </summary>
	public const float INVINCIBLE_TIME = 5.0f;
	
	/// <summary>
	/// 射撃有効時間
	/// </summary>
	public const float ATACK_TIME = 5.0f;
	
	/// <summary>
	/// 射撃間隔
	/// </summary>
	public const float ATACK_INTERVAL = 0.25f;
	
	/// <summary>
	/// ブーストの持続時間
	/// </summary>
	public const float BOOST_TIME = 5.0f;
	
	/// <summary>
	/// スタート時の待機時間
	/// </summary>
	public const float START_WAIT_TIME = 2.0f;
	
	public const int BOOST_COUNT = 30;
	
	/// <summary>
	/// エフェクトのパス
	/// </summary>
	public const string EFFECT_PATH = "Prefabs/Effects/";
	
	/// <summary>
	/// メインシーンに使うプレハブのパス
	/// </summary>
	public const string RUN_PATH = "Prefabs/Running/";
	
	public enum itemType
	{
		MAGNET = 0,
		ATACK,
		DASH,
		INVINCIBLE,
	}

	#endregion

	#region [NGUIButton]
	public const string NGUIJumpButtonPath = "NGUIMenu/Camera/Anchor/Panel/JumpButton";
//	public const string NGUI2D_OILGAMEPLAY_ANCHOR = "1X_OILGamePlayAnchor";
//	public const string NGUI2D_OILGAMERESULT_ANCHOR = "1X_OILGameResultAnchor";
	#endregion

/*
	#region [盟友]
	// 盟友一覧/盟友承認待ち/盟友検索
	public const string NGUI2D_FRIEND_ANCHOR = "37_FriendWindow";
	public const string NGUI2D_FRIEND_FOLLOW = "35_FriendFollowerList";
	public const string NGUI2D_FRIEND_SEARCH = "34_FriendSearch";
	
	public const string NGUI2D_FRIEND_DETAIL = "Panel36_FriendFollowerDetail";
	public const string NGUI2D_FRIENDSEARCH_USERDETAIL = "Panel36_FriendSearchUserDetail";
	public const string NGUI2D_FRIENDFOLLOW_COMPLETE = "Panel36_FriendFollowComplete";
	public const string NGUI2D_FRIENDUNFOLLOW_POPUP = "Panel36_FriendUnFollowConfirmation";
	public const string NGUI2D_FRIENDGREETEDNEWS_DETAIL = "Panel36_FriendGreetedNews";
	public const string NGUI2D_FRIENDSEARCH_POPUP = "Panel36_FriendSearchPopup";
	
	// 盟友詳細
	public const string NGUI_POPUP_FRIENDDETAIL = "Panel34_FriendDetail";
	// 盟友承認
	public const string NGUI_POPUP_FRIENDAPPOVE = "Panel36_ApproveFriend";
	// 盟友申請
	public const string NGUI_POPUP_FRIENDREQUEST = "Panel39_RequestFriend";
	// すでに盟友
	public const string NGUI_POPUP_FRIENDALREADY = "Panel39_AlreadyFriend";
	// 無効フレンドID
	public const string NGUI_POPUP_FRIENDINVALID = "Panel39_InvalidFriend";
	#endregion
	
	#region [ショップ]
	// ショップトップ
	public const string NGUI2D_SHOP_ANCHOR = "22_ShopAnchor";
	// 財産購入確認ポップアップ
	public const string NGUI_POPUP_SHOP_PURCHASE_CONFIRM_PROPERTY = "Panel22_ShopPopup01";
	// 財産購入成功ポップアップ
	public const string NGUI_POPUP_SHOP_PURCHASE_SUCCESS_PROPERTY = "Panel22_ShopPopup02";
	// 財産購入失敗ポップアップ
	public const string NGUI_POPUP_SHOP_PURCHASE_FAILED_PROPERTY = "Panel22_ShopPopup03";
	// 建物購入確認ポップアップ
	public const string NGUI_POPUP_SHOP_PURCHASE_CONFIRM_BUILDING = "Panel22_ShopPopup04";
	// 財産購入失敗ポップアップ
	public const string NGUI_POPUP_SHOP_PURCHASE_FAILED_BUILDING = "Panel22_ShopPopup05";
	#endregion
	
	#region [ガレージ]
	// アイテム一覧画面
	public const string NGUI2D_GARAGE_ANCHOR = "60_GarageItem";
	// ガレージアイテム詳細ポップアップ
	public const string NGUI_POPUP_GARAGE_DETAIL = "Panel60_ItemDetail";
	// ガレージ売却確認ポップアップ
	public const string NGUI_POPUP_GARAGE_SELL_CONFIRM = "Panel60_ItemSale";
	// ガレージ使用確認ポップアップ
	public const string NGUI_POPUP_GARAGE_USE_CONFIRM = "Panel60_ItemUse";
	// ガレージ枠拡張確認ポップアップ
	public const string NGUI_POPUP_GARAGE_EXTEND_CONFIRM = "Panel60_ItemExtension";
	#endregion
	
	#region [お知らせ]
	// お知らせ
	public const string NGUI2D_NOTIFICATION_ANCHOR = "54_news";
	// お知らせ詳細
	public const string NGUI_POPUP_NOTIFICATION_DETAIL = "Panel54_news";
	#endregion
	
	#region [設定]
	// 設定プロフィール画面
	public const string NGUI2D_SETTING_ANCHOR = "28_SettingAnchor";
	#endregion
	
	#region [ガチャ]
	// 設定プロフィール画面
	public const string NGUI2D_GACHA_ANCHOR = "04_Gacha";
	#endregion
	
	#region [プレゼント]
	// プレゼント画面
	public const string NGUI2D_PRESENT_ANCHOR = "53_PresentBox";
	// プレゼント詳細画面
	public const string NGUI_POPUP_PRESENT_DETAIL = "Panel53_PresentBox";
	#endregion
	
	#region [招待]
	// 招待画面
	public const string NGUI2D_INVITE_ANCHOR = "25_InviteAnchor";
	#endregion
	
	#region [ファクトリ]
	// ユニット一覧フィルタポップアップ
	public const string NGUI_POPUP_FACTORY_FILTER = "Panel63_LAB_Popup";
	#endregion
	
	#region [設備機能]
	public const string NGUI2D_GRADEUP_ANCHOR       = "42_GradeUp";
	public const string NGUI2D_INFO_ANCHOR 			= "72_Information";
	
	public const string NGUI2D_CAVEMENU_ANCHOR      = "18_CaveGameMenuAnchor";
	public const string NGUI2D_CAVEPLAY_ANCHOR      = "18_CaveGamePlayAnchor";
	public const string NGUI2D_CAVERESULT_ANCHOR    = "18_CaveGameResultAnchor";
	public const string NGUI2D_CAVESTARTANIM_ANCHOR = "56_GrottoStart";
	public const string NGUI2D_CAVEENDANIM1_ANCHOR  = "57_GrottoEnd1";
	public const string NGUI2D_CAVEENDANIM2_ANCHOR  = "58_GrottoEnd2";
	
	//ラボ
	//public const string NGUI2D_LAB_ANCHOR           = "63_LAB";
	public const string NGUI2D_LAB_ANCHOR           = "63_LAB_Ver.2";
	
	public const string POPUP_LAB_UNIT_ANCHOR       = "Panel63_LAB_Unit";
	
	
	//ラボのレベルアップが押された時に表示
	public const string POPUP_LAB_SKILLUP_ANCHOR = "Panel63_LAB_SkillUp";
	
	public const string NGUI2D_BOOST_ANCHOR         = "66_Boost";
	
	public const string NGUI2D_PRODUP_ANCHOR        = "67_Productioncapacity";
	
	public const string NGUI2D_FACTORY_ANCHOR       = "69_UnitProduction";
	
	public const string NGUI2D_MACHINARY_ANCHOR		= "73_Machinary";
	
	public const string NGUI2D_COMMAND_ANCHOR		= "78_Command";
	
	public const string NGUI2D_COMMANDSELECT_ANCHOR	= "78_CommandSelect";
	
	public const string POPUP_COMMAND_DETAIL		= "Panel78_CommandDetail";
	#endregion
	
	#region [グレードアップ]
	// グレードアップ確認ポップアップ
	public const string NGUI_POPUP_GRADEUP_CONFIRM 	= "Panel42_GradeUpPopup01";
	// グレードアップ成功ポップアップ
	public const string NGUI_POPUP_GRADEUP_SUCCESS 	= "Panel42_GradeUpPopup02";
	// グレードアップ失敗ポップアップ
	public const string NGUI_POPUP_GRADEUP_FAILED 	= "Panel42_GradeUpPopup03";
	// グレードアップ失敗ポップアップ（ジェム資源が足りない）
	public const string NGUI_POPUP_GRADEUP_ONGEM  	= "Panel42_GradeUpPopup04";
	
	#endregion
	// グレードアップ確認ポップアップ
	public const string NGUI2D_SERIALCODE		 	= "62_Serialcode";
	#region [特典]
	
	#endregion
	#region [ブースト / キャンセル]
	// ブースト確認ポップアップ
	public const string NGUI_POPUP_BOOST_CONFIRM    = "Panel66_BoostPopup";
	// キャンセル確認ポップアップ
	public const string NGUI_POPUP_CANCEL_CONFIRM    = "Panel66_CancelPopup";
	#endregion
	
	#region [生産量アップ]
	public const string NGUI_POPUP_PRODUP_CONFIRM   = "Panel67_ProductioncapacityPopup";
	#endregion
	
	#region [障害物除去]
	public const string NGUI_POPUP_OBSTRUCT_CONFIRM    = "Panel75_ObstructRidPopup";
	
	public const string NGUI_POPUP_OBSTRUCT_FAILED     = "Panel75_ObstructRidPopup02";
	#endregion
	
	#region [リーダ選択]
	public const string NGUI2D_LEADER_ANCHOR = "71_LeaderUnit";
	public const string NGUI_POPUP_LEADER_CONFIRM = "Panel71_LeaderPopUpConfirm";
	#endregion
	
	#region [マシーナリー]
	public const string NGUI_POPUP_MACHINARY_CONFIRM = "Panel73_Machinery_Evo";
	#endregion
	
	
	#region [招待・特典]
	public const string NGUI_POPUP_SERIAL_CODE = "Panel74_Serialcode";
	#endregion
	
	
	#region [ビルダー購入]
	public const string NGUI_POPUP_BUILDER_ANCHOR = "Panel99_BuilderConfirmPopup";
	#endregion
	
	#region [データ移行]
	public const string NGUI_POPUP_DATATRANSFER_INPUT	= "Panel64_DataMigration02";
	#endregion
	#region [共通]
	// システムエラーポップアップ
	public const string NGUI_POPUP_COMMON_WARNING   = "Panel68_CommonError";
	
	// 完了共通ポップアップ
	public const string NGUI_POPUP_COMMON_COMPLETE01  = "Panel68_CommonPopup01";
	
	// メッセージ調整完了共通ポップアップ
	public const string NGUI_POPUP_COMMON_COMPLETE02  = "Panel68_CommonPopup02";
	
	// メッセージ調整完了ポップアップ
	public const string NGUI_POPUP_COMMON_COMPLETE03  = "Panel68_CommonComplete02";
	
	// ローディングポップアップ :: Titleシーン
	public const string NGUI_POPUP_TITLELOADING		  = "Panel65_Loading";
	
	// ウェブビュー
	public const string NGUI_POPUP_WEBVIEW			  = "Panel100_WebView";
	#endregion
	
	#region [ミッション]
	public const string NGUI2D_AREASELECT_ANCHOR = "02_AreaSelect";
	public const string NGUI2D_MISSIONSELECT_ANCHOR = "02_MissionSelect";
	public const string NGUI2D_MISSIONDETAIL_ANCHOR = "03_MissionDetail";
	
	public const string NGUI_POPUP_MISSION_CONFIRM = "Panel70_MissionPopup";
	public const string NGUI_POPUP_GEMLSS_MISSION = "Panel70_GemNotFluent";
	#endregion
	
	#region [ チュートリアル ]
	public const string NGUI_2D_TITLE_DISPLAY = "01_TitleScreen Anchor";
	public const string NGUI_2D_SPEAK_SCENE = "80_SpeakSceneUI";
	public const string NGUO_POPUP_SKIP_CONFIRM = "Panel80_SkipConfirm";
	public const string NGUI_POPUP_USER_INPUT = "Panel81_TutorialUserInput";
	public const string NGUI_2D_PLANT_BUILD = "80_Tutorial_Base_Building";
	#endregion
	
	#region [設定/プロフィール]
	public const string NGUI_2D_PRIVACY = "Panel97_PrivacyPolicy";
	public const string NGUI_2D_TRANSACTION = "Panel99_AboutTransaction";
	public const string NGUI_2D_QUESTIONAIRE = "Panel98_Question_Answer";
	public const string NGUI_POPUP_PROFILE_COMMENT = "Panel72_EditCommentPopup";
	public const string NGUI_POPUP_PROFILE_LEADER = "Panel71_LeaderUnit";
	#endregion
	
	#region [ 司令室 ]
	public const string NGUI_2D_TRANSFER = "74_OperationRoom_Transfer";
	#endregion
	
	#region [ ログインボーナス ]
	public const string NGUI_POPUP_LOGINBONUS = "Panel63_LOGINBONUS";
	#endregion
	
	#region [司令室]
	public const string NGUI_2D_OPERATIONROOM_TRANSFER = "74_OperationRoom_Transfer";
	#endregion
	/// <summary>
	/// ルート種類
	/// </summary>
	public enum RootType
	{
		/// <summary>
		/// 画面
		/// </summary>
		Window,
		/// <summary>
		/// ポップアップ
		/// </summary>
		Popup,
		/// <summary>
		/// メニュー
		/// </summary>
		Menu,
		/// <summary>
		/// スピーカー
		/// </summary>
		Speaker,
		/// <summary>
		/// 不明
		/// </summary>
		Unknown = -1,
	}
	
	/// <summary>
	/// 初期化したかフラグ
	/// </summary>
	private static bool isInitialized = false;
	/// <summary>
	/// トランスフォームオープン/クローズ中かフラグ
	/// </summary>
	private static bool isPlaying = false;
	
	/// <summary>
	/// トランスフォームクローズドしてるかフラグ
	/// </summary>
	private static bool isClosedWindowTransform = true;
	
	/// <summary>
	/// 画面履歴
	/// </summary>
	private static List<string> historyWindowNames = new List<string>();
	/// <summary>
	/// 現在開いている画面
	/// </summary>
	private static CycleEventReceiver currentWindow = null;
	/// <summary>
	/// 現在開いている画面名
	/// </summary>
	private static string currentWindowName = "";
	/// <summary>
	/// 現在開いているポップアップ名
	/// </summary>
	private static string currentPopupName = "";
	/// <summary>
	/// 現在開いているメニュー名
	/// </summary>
	private static string currentMenuName = "";
	/// <summary>
	/// 次に開く画面名
	/// </summary>
	private static string nextWindowName = "";
	
	/// <summary>
	/// 次に開く画面名
	/// </summary>
	private static string nextPopupName = "";
	
	/// <summary>
	/// 次に開くメニュー名
	/// </summary>
	private static string nextMenuName = "";
	/// <summary>
	/// 戻るフラグ
	/// </summary>
	private static bool isBack = true;
	/// <summary>
	/// 履歴クリアフラグ
	/// </summary>
	private static bool isClearHistory = false;
	/// <summary>
	/// 現在開いている画面があるかのプロパティ
	/// </summary>
	private static bool hasWindow
	{
		get{ return (currentWindow != null); }
	}
	
	/// <summary>
	/// 現在開いてるオブジェクト
	/// </summary>
	private static GameObject currentGameObject = null;
	
	
	/// <summary>
	/// 選択したポップアップボタンの結果
	/// </summary>
	public static string popupButtonResult = "";
	/// <summary>
	/// ポップアップボタン結果：肯定ボタン
	/// </summary>
	public const string PopupButtonResultPositive = "Positive";
	/// <summary>
	/// ポップアップボタン結果：否定ボタン
	/// </summary>
	public const string PopupButtonResultNegative = "Negative";
*/

//	/// <summary>
//	/// シーン変更時の初期化関数
//	/// </summary>
//	public static void ReInitializeBySceneChange()
//	{
//		isPlaying = false;
//		isClosedWindowTransform = true;
//		currentWindowName = "";
//		currentPopupName = "";
//	}
//	
//	/// <summary>
//	/// ポップアップ閉じる時の初期化関数
//	/// </summary>
//	public static void ReInitializePopup()
//	{
//		isPlaying = false;
//		currentPopupName = "";
//		Debug.Log ("reinitialize currentPopname");
//	}
//	/// <summary>
//	/// 初期化処理
//	/// </summary>
//	private static void Initialize()
//	{
//		// 初期化していない
//		if( !isInitialized )
//		{
//			// 初期化した
//			isInitialized = true;
//			
//			// イベント設定
//			CycleEventReceiver.OnOpenTransformEvent = OnOpenTransform;
//			CycleEventReceiver.OnOpenedTransformEvent = OnOpenedTransform;
//			CycleEventReceiver.OnCloseTransformEvent = OnCloseTransform;
//			CycleEventReceiver.OnClosedTransformEvent = OnClosedTransform;
//		}
//	}
//	
//	/// <summary>
//	/// トランスフォームオープン開始イベント
//	/// </summary>
//	/// <param name="target">サイクル対象のゲームオブジェクト</param>
//	/// <param name="rootType">ルート種類</param>
//	private static void OnOpenTransform(GameObject target, RootType rootType)
//	{
//		// イベントの実行
//		FWUtility.LogWarning("1: " + target.name + " [OnOpenTransform] " + rootType);
//		if( OnOpenTransformEvent != null ) OnOpenTransformEvent(target, rootType);
//	}
//	
//	/// <summary>
//	/// トランスフォームオープン終了イベント
//	/// </summary>
//	/// <param name="target">サイクル対象のゲームオブジェクト</param>
//	/// <param name="rootType">ルート種類</param>
//	private static void OnOpenedTransform(GameObject target, RootType rootType)
//	{
//		// ルート種類固有処理
//		switch(rootType)
//		{
//		case RootType.Window:
//			// アニメーション完了
//			currentWindowName = target.name;
//			isPlaying = false;
//			isClosedWindowTransform = false;
//			break;
//		case RootType.Popup:
//			// アニメーション完了
//			// utsumi add 140222				
//			currentPopupName = target.name;
//			
//			isPlaying = false;
//			break;
//		case RootType.Menu:
//			// アニメーション完了
//			// utsumi add 140222				
//			currentMenuName = target.name;
//			
//			isPlaying = false;
//			break;
//		}
//		
//		// イベントの実行
//		FWUtility.LogWarning("2: " + target.name + " [OnOpenedTransform] " + rootType);
//		if( OnOpenedTransformEvent != null ) OnOpenedTransformEvent(target, rootType);
//	}
//	
//	/// <summary>
//	/// トランスフォームクローズ開始イベント
//	/// </summary>
//	/// <param name="target">サイクル対象のゲームオブジェクト</param>
//	/// <param name="rootType">ルート種類</param>
//	private static void OnCloseTransform(GameObject target, RootType rootType)
//	{
//		// イベントの実行
//		FWUtility.LogWarning("3: " + target.name + " [OnCloseTransform] " + rootType);
//		if( OnCloseTransformEvent != null ) OnCloseTransformEvent(target, rootType);
//	}
//	
//	/// <summary>
//	/// トランスフォームクローズ終了イベント
//	/// </summary>
//	/// <param name="target">サイクル対象のゲームオブジェクト</param>
//	/// <param name="rootType">ルート種類</param>
//	private static void OnClosedTransform(GameObject target, RootType rootType)
//	{
//		// ルート種類固有処理
//		switch(rootType)
//		{
//		case RootType.Window:
//			// アニメーション完了
//			isPlaying = false;
//			isClosedWindowTransform = true;
//			
//			if (callBackAfterClosingWindow != null)
//			{
//				callBackAfterClosingWindow ();
//				callBackAfterClosingWindow = null;
//			}
//			
//			break;
//		case RootType.Popup:
//			// utsumi add 140208
//			isPlaying = false;
//			break;
//		case RootType.Menu:
//			// utsumi add 140208
//			isPlaying = false;
//			break;
//		}
//		
//		// イベントの実行
//		FWUtility.LogWarning("4: " + target.name + " [OnClosedTransform] " + rootType);
//		if( OnClosedTransformEvent != null ) OnClosedTransformEvent(target, rootType);
//		
//		// ルート種類固有処理
//		switch(rootType)
//		{
//		case RootType.Window:
//			// 現在開いている画面情報を破棄
//			currentWindowName = "";
//			break;
//		case RootType.Popup:
//			// ポップアップボタンの結果を初期化する
//			currentPopupName = "";
//			//popupButtonResult = "";
//			
//			break;
//		}
//		
//		//Debug.Log ("isBack " + isBack);
//		//Debug.Log ("called OnClosedTransform");
//		
//		// 前の画面に戻る
//		if( isBack )
//		{
//			// 戻るフラグを落とす
//			isBack = false;
//			
//			// 履歴の更新
//			if( historyWindowNames.Count > 0 )
//			{
//				historyWindowNames.RemoveAt(historyWindowNames.Count - 1);
//			}
//			
//			// 履歴に画面が残っている
//			if( historyWindowNames.Count > 0 )
//			{
//				// 新しい最後尾を次の画面として開く
//				OpenWindow(historyWindowNames[historyWindowNames.Count -1]);
//			}
//		}
//		
//		// 別の画面を開こうとしていた
//		{
//			// 次に開く画面がある
//			if( !string.IsNullOrEmpty(nextWindowName) )
//			{
//				// 次の画面を開く
//				OpenWindow(nextWindowName, true);
//				nextWindowName = "";
//			}
//			
//			if (!string.IsNullOrEmpty(nextMenuName ) )
//			{
//				//Debug.Log ("called nextMenuName" + nextMenuName);
//				// 次のメニューを開く
//				OpenMenu (nextMenuName);
//				nextMenuName = "";
//			}
//			
//			if (!string.IsNullOrEmpty(nextPopupName ) )
//			{
//				//Debug.Log ("called nextPopupName" + nextPopupName);
//				OpenPopup (nextPopupName);
//				nextPopupName = "";
//				ReInitializePopup ();
//			}
//		}
//		
//	}
//	
//	/// <summary>
//	/// ウィンドウを開く
//	/// </summary>
//	/// <returns>開閉したゲームオブジェクト</returns>
//	/// <param name="windowName">ウィンドウ名</param>
//	/// <param name="history">履歴に残すか</param>
//	public static GameObject OpenWindow(string windowName, bool history = true)
//	{
//		//Debug.Log ("OpenWindow " + windowName + " : " + isPlaying);
//		//Debug.Log ("CurrentWindowName  " + currentWindowName + " : " + isPlaying);
//		
//		// アニメーション中は開閉できない
//		if( isPlaying )
//		{
//			FWUtility.LogWarning("WindowController.isPlaying is true. You can't open " + currentWindowName);
//			return null;
//		}
//		
//		// アニメーション中にする
//		isPlaying = true;
//		
//		// 空は指定できない
//		if( string.IsNullOrEmpty(windowName) ) return null;
//		
//		// 同じ画面は開けない
//		if( currentWindowName == windowName ) return null;
//		
//		// 動作したゲームオブジェクト
//		GameObject window = null;
//		
//		// イベントの初期化
//		Initialize();
//		
//		// 現在開いている画面がある
//		//Debug.Log ("currentWindow " + currentWindow);
//		//Debug.Log ("statusClosed" + isClosedWindowTransform);
//		if (currentWindow != null || !IsWindowAnimationClosed() )
//		{
//			// 次に開く画面名
//			nextWindowName = windowName;
//			//Debug.Log ("nextWindowName called" + nextWindowName);
//			// 現在開いている画面を閉じる
//			window = ActiveChange(NGUIConstants.NGUI_2DCAMERA, currentWindowName, false);
//			Effect.ResourceEffectManager.Instance.OnChangeWindowSequence();
//			// 現在開いている画面名
//			currentWindowName = windowName;
//			
//		}
//		
//		// 何も開いていない
//		else
//		{
//			// 履歴の更新
//			int index = historyWindowNames.IndexOf(windowName);
//			if (index != -1)
//			{
//				historyWindowNames.RemoveRange(index, historyWindowNames.Count - index);
//			}
//			if (history)
//			{
//				historyWindowNames.Add(windowName);
//			}
//			
//			// 現在開いている画面名
//			currentWindowName = windowName;
//			//Debug.Log ("currentWindowName " + currentWindowName);
//			
//			// 新しく開く
//			window = ActiveChange(NGUIConstants.NGUI_2DCAMERA, currentWindowName, true);
//			if (window = null)
//				CloseWindow ();
//			
//			Effect.ResourceEffectManager.Instance.OnChangeWindowSequence();
//			
//			if( window != null ) currentWindow = window.GetComponent<CycleEventReceiver>();
//		}
//		// utsumi changed 強制的にfalse化。34_Friendだけなぜかうまくうごかない。未解析@140207
//		// isPlaying = false;
//		
//		return window;
//	}
//	
//	/// <summary>
//	/// 現在開いている画面を閉じる
//	/// </summary>
//	public static void CloseWindow(Action callBack = null )
//	{
//		if (callBack != null)
//			callBackAfterClosingWindow = callBack;
//		
//		Debug.Log ("currentName " + currentWindowName);
//		Debug.Log ("hasWindow " + hasWindow);
//		
//		/*
//		// 閉じる画面がない
//		if( !hasWindow )
//		{
//			FWUtility.LogWarning("Not Found opend window. You can't close window");
//			return;
//		}
//		*/
//		// アニメーション中は開閉できない
//		if( isPlaying )
//		{
//			FWUtility.LogWarning("WindowController.isPlaying is true. You can't close " + currentWindowName);
//			return;
//		}
//		
//		// イベントの初期化
//		Initialize();
//		
//		// アニメーション中にする
//		isPlaying = true;
//		
//		// 画面履歴をクリアする
//		if( isClearHistory )
//		{
//			historyWindowNames.Clear();
//		}
//		isClearHistory = true;
//		
//		// 現在開いている画面を閉じる
//		GameObject window = ActiveChange(NGUIConstants.NGUI_2DCAMERA, currentWindowName, false);
//		
//		isPlaying = false;
//		isBack = false;
//		
//		//後始末
//		currentWindow = null;
//		currentGameObject = null;
//		currentWindowName = "";
//		
//		//		// 動作したオブジェクトがない
//		//		if( window == null )
//		//		{
//		//			isPlaying = false;
//		//			isBack = false;
//		//		}
//	}
//	
//	
//	
//	/// <summary>
//	/// 前の画面に戻る
//	/// </summary>
//	public static void BackWindow()
//	{
//		/*
//		// 閉じる画面がない
//		if( !hasWindow ) return;
//		*/
//		
//		// 戻るフラグを立てる
//		isBack = true;
//		
//		// 履歴をクリアしない
//		isClearHistory = false;
//		
//		// 現在の画面を閉じる
//		//※OnClosedTransformイベントで次の画面を開きます
//		CloseWindow();
//	}
//	
//	/// <summary>
//	/// ポップアップを開く
//	/// </summary>
//	/// <returns>開閉したゲームオブジェクト</returns>
//	/// <param name="popupName">ポップアップ名</param>
//	public static GameObject OpenPopup(string popupName)
//	{
//		//Debug.Log("currentPopup " + currentPopupName);
//		//Debug.Log("popupName " + popupName);
//		
//		//		FWUtility.Log("currentGO " + currentGameObject);
//		//FWUtility.Log("currentWindow " + currentWindowName);
//		
//		// 空は指定できない
//		if( string.IsNullOrEmpty(popupName) ) return null;
//		
//		// イベントの初期化
//		Initialize();
//		
//		//同じPOPUPが開いてたら閉じる
//		if ( currentPopupName == popupName )
//		{
//			return null;
//		}
//		
//		// アニメーション中にする
//		isPlaying = true;
//		
//		// 現在開いている画面がある
//		if (currentWindow != null || !IsWindowAnimationClosed() )
//		{
//			// 次に開く画面名
//			nextPopupName = popupName;
//		}
//		
//		
//		//強制的にポップアップを閉じる
//		if ( string.IsNullOrEmpty(currentPopupName))
//		{
//			if ( currentGameObject != null )
//			{
//				//currentGameObject.SetActive (false);
//				//currentGameObject = null;
//			}
//			//ClosePopup();
//		}
//		
//		// ポップアップを開く
//		GameObject popup = ActiveChange(NGUIConstants.NGUI_POPUPANCHOR, popupName, true);
//		
//		// 対象ポップアップがある
//		currentPopupName = ( popup != null ) ? popupName : "";
//		// 開いたポップアップを返す
//		return popup;
//	}
//	
//	/// <summary>
//	/// メニューを開く
//	/// </summary>
//	/// <returns>開閉したゲームオブジェクト</returns>
//	/// <param name="popupName">ポップアップ名</param>
//	public static GameObject OpenMenu(string menuName)
//	{
//		//FWUtility.Log("popupName " + menuName);
//		//FWUtility.Log("currentMenu " + currentMenuName);
//		
//		// Window && POPUP
//		if (IsWindowOpen () || IsPopupOpen ())
//			return null;
//		
//		// 空は指定できない
//		if( string.IsNullOrEmpty(menuName) ) return null;
//		
//		// イベントの初期化
//		Initialize();
//		
//		//同じメニューが開いてたら閉じる
//		if ( currentMenuName == menuName )
//		{	
//			//Debug.Log ("currentMenuName " + currentMenuName);
//			//CloseMenu();
//			return null;
//		}
//		
//		// ポップアップを開く
//		GameObject menu = ActiveChange(NGUIConstants.NGUI_MENUANCHOR, menuName, true);
//		
//		// 対象ポップアップがある
//		currentMenuName = ( menu != null ) ? menuName : "";
//		currentGameObject = menu;
//		
//		// 開いたポップアップを返す
//		//Menu.OpenMenu ();
//		return menu;
//	}
//	
//	/// <summary>
//	/// メニューを閉じる
//	/// </summary>
//	/// <returns>The menu.</returns>
//	public static GameObject CloseMenu( bool isReselected = false )
//	{
//		// イベントの初期化
//		Initialize();
//		
//		// 開いているメニューがない
//		if( string.IsNullOrEmpty(currentMenuName) )
//		{
//			return null;
//		}
//		
//		// ポップアップを閉じる
//		GameObject menu = ActiveChange(NGUIConstants.NGUI_MENUANCHOR, currentMenuName, false);
//		
//		// isReselectedならセット
//		if ( isReselected )
//		{
//			nextMenuName = currentMenuName;
//		}
//		
//		// 後始末
//		currentMenuName = "";
//		
//		// 閉じたポップアップを返す
//		Menu.CloseMenu ();
//		return menu;
//	}
//	
//	/// <summary>
//	/// ポップアップを閉じる
//	/// </summary>
//	/// <returns>開閉したゲームオブジェクト</returns>
//	public static GameObject ClosePopup()
//	{
//		// プレイ中の強制終了はNG
//		if (isPlaying)
//			//Debug.Log ("passed ClosePopup return ");
//			return null;			
//		
//		// イベントの初期化
//		Initialize();
//		
//		//Debug.Log ("currentPopupName" + currentPopupName);
//		// 開いているポップアップがない
//		if( string.IsNullOrEmpty(currentPopupName) )
//		{
//			return null;
//		}
//		
//		
//		// ポップアップを閉じる
//		GameObject popup = ActiveChange(NGUIConstants.NGUI_POPUPANCHOR, currentPopupName, false);
//		
//		// 後始末
//		currentPopupName = "";
//		currentGameObject = null;
//		//Debug.Log ("currentPopup null ");
//		// 閉じたポップアップを返す
//		return popup;
//	}
//	
//	/// <summary>
//	/// ウィンドウの表示切替処理
//	/// </summary>
//	/// <returns>開閉したゲームオブジェクト</returns>
//	/// <param name="parentName">親オブジェクト名</param>
//	/// <param name="targetName">対象オブジェクト名</param>
//	/// <param name="active">表示するか</param>
//	private static GameObject ActiveChange(string parentName, string targetName, bool active)
//	{
//		//Debug.Log ("called ClosedWindow parentName " + parentName );
//		//Debug.Log ("called ClosedWindow targetName " + targetName );
//		
//		if (string.IsNullOrEmpty(parentName) || string.IsNullOrEmpty(targetName))
//			return null;
//		
//		//Debug.Log("parentName " + parentName);
//		//Debug.Log("targetName " + targetName);
//		//Debug.Log("active " + active);
//		
//		// UIを取得する
//		Transform menuPanel;
//		
//		if(SceneManager.Instance.CheckIsTargetScene(SceneType.Main))
//		{
//			if(parentName == NGUIConstants.NGUI_2DCAMERA)
//			{
//				menuPanel = MapMenuController.Instance.MenuCreate(targetName, MenuType.Window).transform;
//			}
//			else if(parentName == NGUIConstants.NGUI_POPUPANCHOR)
//			{
//				menuPanel = MapMenuController.Instance.MenuCreate(targetName, MenuType.Popup).transform;
//			}
//			else
//			{
//				menuPanel = GameObject.Find(parentName).transform.FindChild(targetName);
//			}
//		}
//		else
//		{
//			menuPanel = GameObject.Find(parentName).transform.FindChild(targetName);
//		}
//		
//		// UIの表示を切り替える
//		if (menuPanel != null)
//		{
//			CycleEventReceiver receiver = menuPanel.GetComponent<CycleEventReceiver>();
//			if( receiver != null )
//			{
//				// トランスフォームオープンの実行
//				if( active )
//				{
//					// キャッシュオン
//					//currentGameObject = menuPanel.gameObject;
//					receiver.OpenTransform();
//				}
//				// トランスフォームクローズの実行
//				else
//				{
//					//currentGameObject = null;
//					receiver.CloseTransform();
//				}
//			}
//			else
//			{
//				// トランスフォームオープン系イベントの通知
//				if( active )
//				{
//					menuPanel.SendMessage(CycleEventReceiver.OpenTransformFunctionName, SendMessageOptions.DontRequireReceiver);
//					menuPanel.SendMessage(CycleEventReceiver.OpenedTransformFunctionName, SendMessageOptions.DontRequireReceiver);
//					
//				}
//				// トランスフォームクローズ系イベントの通知
//				else
//				{
//					menuPanel.SendMessage(CycleEventReceiver.CloseTransformFunctionName, SendMessageOptions.DontRequireReceiver);
//					menuPanel.SendMessage(CycleEventReceiver.ClosedTransformFunctionName, SendMessageOptions.DontRequireReceiver);
//				}
//				menuPanel.gameObject.SetActive(active);
//			}
//			
//			// 盟友情報を設定する :: 盟友専用機能
//			//			if (targetFriendData != null)
//			//			{
//			//				WindowFriendItem item = menuPanel.GetComponent<WindowFriendItem>();
//			//				if (item != null)
//			//					item.Initialize(targetFriendData);
//			//			}
//			
//			//			targetFriendData = null;
//			
//			return menuPanel.gameObject;
//		}
//		
//		FWUtility.LogWarning("Not found " + parentName + "/" + targetName);
//		//		targetFriendData = null;
//		return null;
//	}
//	
//	/// <summary>
//	/// ウィンドウが現在ひらいてるかどうか
//	/// </summary>
//	/// <returns><c>true</c> if is window open; otherwise, <c>false</c>.</returns>
//	public static bool IsWindowOpen()
//	{
//		// データが入ってない場合はあいてない
//		if ( string.IsNullOrEmpty( currentWindowName ) )
//			return false;
//		//Debug.Log ("windowName " + currentWindowName);
//		return true;
//	}
//	/// <summary>
//	/// ポップアップが現在ひらいてるかどうか
//	/// </summary>
//	/// <returns><c>true</c> if is window open; otherwise, <c>false</c>.</returns>
//	public static bool IsPopupOpen()
//	{
//		// データが入ってない場合はあいてない
//		if ( string.IsNullOrEmpty( currentPopupName ) )
//			return false;
//		//Debug.Log ("popupName " + currentPopupName);
//		return true;
//	}
//	
//	public static bool IsMenuOpen()
//	{
//		// データが入ってない場合はあいてない
//		if ( string.IsNullOrEmpty( currentMenuName ) )
//			return false;
//		//Debug.Log ("menuName " + currentMenuName);
//		return true;
//		
//	}
//	/// <summary>
//	/// 現在のウィンドウを返す（ウィンドウ / ポップアップ）
//	/// </summary>
//	/// <returns>The now open window.</returns>
//	public static GameObject GetNowOpenWindow()
//	{
//		if (currentWindow == null)
//			return null;
//		return currentWindow.gameObject;
//	}
//	
//	/// <summary>
//	/// 次のシーンにて利用する場合の初期化処理
//	/// </summary>
//	public static void SetCurrentGameObjectByNil()
//	{
//		if ( currentGameObject != null )
//			currentGameObject = null;
//		
//		if ( !string.IsNullOrEmpty( currentPopupName ) )
//			currentPopupName = "";
//		if (!string.IsNullOrEmpty (currentWindowName))
//			currentWindowName = "";
//		
//	}
//	
//	/// <summary>
//	/// トランスフォームが完全にしまったかどうか
//	/// </summary>
//	/// <returns><c>true</c> if is window closed; otherwise, <c>false</c>.</returns>
//	public static bool IsWindowAnimationClosed()
//	{
//		return isClosedWindowTransform;
//	}



}
