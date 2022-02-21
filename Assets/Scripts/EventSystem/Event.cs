using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyEventSystem
{
	public abstract class Event {
		public delegate void Handler(Event e);
	}
#region GamePlayEvent
	public class FootPrintEvent: Event{
		public Vector3 printPos;
		public FootPrintEvent(Vector3 pos){printPos = pos;}
	}
#endregion
}
