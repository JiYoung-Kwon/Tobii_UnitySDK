//-----------------------------------------------------------------------
// Copyright 2017 Tobii Technology AB. All rights reserved.
//-----------------------------------------------------------------------

namespace Tobii.Gaming
{
	/// <summary>
	/// DisplayInfo contains information about the eye-tracked display monitor.
	/// </summary>
	public struct DisplayInfo
	{
		/// <summary>
		/// Creates a DisplayInfo instance.
		/// </summary>
		/// <param name="displayWidthMm"></param>
		/// <param name="displayHeightMm"></param>
		internal DisplayInfo(float displayWidthMm, float displayHeightMm) : this()   //internal : 어셈블리 내부에서만 접근이 가능
        {
			DisplayWidthMm = displayWidthMm; //눈 추적 디스플레이 모니터의 너비 (밀리미터)
            DisplayHeightMm = displayHeightMm; //눈 추적 디스플레이 모니터의 높이 (밀리미터)
        }

        /// <summary>
        /// Creates a DisplayInfo instance representing an invalid state. 잘못된 상태를 나타내는 DisplayInfo 인스턴스 생성
        /// </summary>
        public static DisplayInfo Invalid 
		{
			get { return new DisplayInfo(float.NaN, float.NaN); }
		}

		/// <summary>
		/// Gets the validity of this DisplayInfo instance.
		/// </summary>
		public bool IsValid //유효여부
		{
			get { return !float.IsNaN(DisplayWidthMm) && !float.IsNaN(DisplayHeightMm); }
		}

		/// <summary>
		/// Gets the width in millimeters of the eye tracked display monitor.
		/// </summary>
		public float DisplayWidthMm { get; private set; }

		/// <summary>
		/// Gets the height in millimeters of the eye tracked display monitor.
		/// </summary>
		public float DisplayHeightMm { get; private set; }
	}
}