using UnityEngine;

public static class HelperClass
{
	public static T Random<T>(this System.Collections.Generic.List<T> list){
		return list[UnityEngine.Random.Range(0, list.Count)];
	}

	public static T Random<T>(this T[] array){
		return array[UnityEngine.Random.Range(0, array.Length)];
	}

	public static float Round(this float v,int signs){
		if(signs == 0){
			return Mathf.FloorToInt(v);
		}else{
			int p = Mathf.FloorToInt(Mathf.Pow(10, signs));
			return Mathf.Floor(v*p)/p;
		}
	}

	public static float FloorToSpecificInt(this float v,int baseV){
		return Mathf.FloorToInt((v/baseV))*baseV;
	}

	public static Rect NextLine(this Rect r){
		r.y += 18f;
		return r;
	}

	public static Rect Indent(this Rect r){
		r.x += 10f;
		r.width -=10f;
		return r;
	}

	public static Rect Unindent(this Rect r){
		r.x -= 10f;
		r.width +=10f;
		return r;
	}

}

