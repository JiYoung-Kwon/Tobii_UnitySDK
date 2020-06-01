//-----------------------------------------------------------------------
// Copyright 2016 Tobii AB (publ). All rights reserved.
//-----------------------------------------------------------------------

// Position : 스크린 아이트래커의 중간에 대해 밀리미터 단위로 측정됨
//          : Head가 회전화는 공간의 지점
// Rotation : Quaternion 사용, 사용자 머리의 회전을 나타냄
//          : 오일러 각도로 변환 시, eulerAngles 속성 사용




using UnityEngine;
using Tobii.Gaming;
using UnityEngine.UI;

public class HeadMovement : MonoBehaviour
{
	public bool LeftEyeClosed { get; private set; }
	public Vector2 LeftEyePosition { get; private set; }
	public bool RightEyeClosed { get; private set; }
	public Vector2 RightEyePosition { get; private set; }

	public Transform Head;
	public float Responsiveness = 10f;
    public Text headpose_t;

    void Update()
	{
		var headPose = TobiiAPI.GetHeadPose();
		if (headPose.IsRecent())
		{
			Head.transform.localRotation = Quaternion.Lerp(Head.transform.localRotation, headPose.Rotation, Time.unscaledDeltaTime * Responsiveness);
		}

		var gazePoint = TobiiAPI.GetGazePoint();
		if (gazePoint.IsRecent() && Camera.main != null)
		{
			var eyeRotation = Quaternion.Euler((gazePoint.Viewport.y - 0.5f) * Camera.main.fieldOfView, (gazePoint.Viewport.x - 0.5f) * Camera.main.fieldOfView * Camera.main.aspect, 0);

			var eyeLocalRotation = Quaternion.Inverse(Head.transform.localRotation) * eyeRotation;

			var pitch = eyeLocalRotation.eulerAngles.x;
			if (pitch > 180) pitch -= 360;
			var yaw = eyeLocalRotation.eulerAngles.y;
			if (yaw > 180) yaw -= 360;

			LeftEyePosition = new Vector2(Mathf.Sin(yaw * Mathf.Deg2Rad), Mathf.Sin(pitch * Mathf.Deg2Rad));
			RightEyePosition = new Vector2(Mathf.Sin(yaw * Mathf.Deg2Rad), Mathf.Sin(pitch * Mathf.Deg2Rad));
		}

        LeftEyeClosed = RightEyeClosed = TobiiAPI.GetUserPresence().IsUserPresent() && (Time.unscaledTime - gazePoint.Timestamp) > 0.15f || !gazePoint.IsRecent();
        //왼쪽,오른쪽 눈 감긴 여부 = 사용자 존재(사용자가 눈 추적 화면 앞에 있음) && (게임시작으로부터 경과한 시간 - 시선 포인트의 타임 스탬프(눈 이미지가 촬영 될 때 획득)) > 0.15f || gazePoint 유효 여부
        // gazePoint가 유효하지 않으면 true

        Debug.Log(LeftEyeClosed + " , " + RightEyeClosed); //값 확인

        headpose_t.text = "HeadPose : " + Head.transform.localRotation.eulerAngles; // 머리 회전값(오일러 각도) 출력
    }
}