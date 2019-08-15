using UnityEngine;

[DisallowMultipleComponent]
public class ParallaxEffect : MonoBehaviour {

	[SerializeField, TooltipAttribute ("移動係数")] private float shiftRatio = 3f;
	[SerializeField, TooltipAttribute ("深度")] private float deepness = 1f;
	[SerializeField, TooltipAttribute ("可動半径")] private float movableRadius = 1000f;
	[SerializeField, TooltipAttribute ("復帰係数")] private float returnRatio = 0.013f;
	[SerializeField, TooltipAttribute ("水平有効")] private bool Horizontal = true;
	[SerializeField, TooltipAttribute ("垂直有効")] private bool Vertical = true;

	#region static
	public static bool Reactable = true; // 有効
	private static int useCount = 0; // 総使用数
	private static bool detection; // 検出
	private static Vector3 initialRotationRate; // ジャイロの初期ローテーション

	private static bool gyroEnabled { // 有効/無効
		get { return (ParallaxEffect.useCount > 0); }
		set {
			if (value) {
				if (ParallaxEffect.useCount++ <= 0) {
					Input.gyro.enabled = true;	// Gyro 有効化
				}
			} else {
				if (--ParallaxEffect.useCount <= 0) {
					Input.gyro.enabled = false;	// Gyro 無効化
					ParallaxEffect.useCount = 0;
				}
			}
		}
	}
	#endregion

	private bool lastDetection; // 最後の検出状態
	private Vector3 homePosition; // ホームポジション

	// 初期化
	void Awake () {
		ParallaxEffect.gyroEnabled = true;
		this.homePosition = this.transform.localPosition;
	}
	void Start () {
		this.lastDetection = ParallaxEffect.detection;
		if (!this.lastDetection) {
			ParallaxEffect.initialRotationRate = Input.gyro.rotationRateUnbiased;
		}
	}

	// 駆動
	void Update () {
		if (ParallaxEffect.gyroEnabled && Reactable) {
			var rate = Input.gyro.rotationRateUnbiased;
			// 検出
			if (!ParallaxEffect.detection && rate != ParallaxEffect.initialRotationRate) {
				ParallaxEffect.detection = true;
			}
			if (this.lastDetection != ParallaxEffect.detection) {
				this.lastDetection = ParallaxEffect.detection;
			}
			// シフト
			if (ParallaxEffect.detection && this.shiftRatio != 0f) {	// ジャイロあり、駆動意思あり
				var pos = this.transform.localPosition;
				pos += new Vector3 (Horizontal ? -rate.y : 0, Vertical ? rate.x : 0, 0) *  this.shiftRatio * this.deepness;
				if ((pos - this.homePosition).magnitude > this.movableRadius) {
					pos = this.homePosition + (pos - this.homePosition).normalized * this.movableRadius;
				}
				this.transform.localPosition = pos;
			}
			// 経時復帰
			if (this.returnRatio != 0f) {
				var distance = homePosition - this.transform.localPosition;
				if (distance.sqrMagnitude <= 1f) {
					this.transform.localPosition = homePosition;
				} else {
					this.transform.localPosition += distance * Time.deltaTime *  this.deepness;
				}
			}
		} else {
			this.transform.localPosition = homePosition;
		}
	}

	// 消滅
	private void OnDestroy () {
		ParallaxEffect.gyroEnabled = false;
	}

	// 復旧
	private void OnApplicationPause (bool pause) {
		if (!pause) {
			if (ParallaxEffect.useCount > 0) {
				Input.gyro.enabled = true;	// Gyro 有効化
				this.transform.localPosition = homePosition;
			}
		}
	}

}
