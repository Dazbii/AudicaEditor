using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Cue : IComparable<Cue> {

    public int tick = 0;
    public int tickLength = 120;
    public int pitch = 0;
    public int velocity = 20;
	public Vector2 gridOffset;
    public int handType = 0;
    public int behavior = 0;

    private int row = 0;
    private int col = 0;

	private void CalcPitch() {
		pitch =  row + (12 * col);
	}

	public int GetTick() {
		return tick;
	}
	public int GetRow() {
		return row;
	}

	public int GetCol() {
		return col;
	}

    public int CompareTo(Cue other) {
        return this.tick.CompareTo(other.tick);
    }

	public Cue SetTick(int tick) {
		this.tick = tick;
		return this;
	}

	public Cue SetPos(int row, int col) {
		this.row = row;
		this.col = col;
		this.CalcPitch();
		return this;
	}
    public Cue SetRow(int row) {
        this.row = row;
        this.CalcPitch();
        return this;
    }

    public Cue SetCol(int col) {
        this.col = col;
        this.CalcPitch();
        return this;
    }

    public Cue SetHandNone() {
        this.handType = 0;
        return this;
    }
	public Cue SetHandLeft() {
		this.handType = 2;
		return this;
	}

    public Cue SetHandRight() {
        this.handType = 2;
        return this;
    }

	// public string ToJson() {
	// 	return "\t\t{\n" +
	// 		"\t\t\t\"tick\": " + tick + ",\n" +
    //         "\t\t\t\"tickLength\": " + tickLength + ",\n" +
    //         "\t\t\t\"pitch\": " + pitch + ",\n" +
    //         "\t\t\t\"velocity\": " + velocity + ",\n" +
	// 		"\t\t\t\"gridOffset\": {\n" + 
	// 		"\t\t\t\t\"x\": " + gridOffsetX + ",\n" +
	// 		"\t\t\t\t\"y\": " + gridOffsetY + "\n" +
	// 		"\t\t\t},\n" +
    //         "\t\t\t\"handType\": " + handType + ",\n" +
    //         "\t\t\t\"behavior\": " + behavior + "\n" +
	// 		"\t\t}";
	// }
}
