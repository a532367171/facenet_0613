using System;
namespace WisFaceBase
{
	[Serializable]
	public struct WisFace
	{
		public WisRect rect;
		public WisPoint ptRightEye;
		public WisPoint ptLeftEye;
		public WisPoint ptNose;
		public WisPoint ptMouthLeft;
		public WisPoint ptMouthRight;
		public float yaw;
		public float roll;
		public float pitch;
		public float conf;
	}
}
