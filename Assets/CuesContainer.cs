using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CuesContainer {
    public List<Cue> cues;

	public CuesContainer() {
		cues = new List<Cue>();
	}
}
