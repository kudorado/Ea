using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
	

		public static class func{
		
			public static void InvokeSafe(this Action @this){
			if (@this != null)
				@this.Invoke ();
			}
	}
public static class array{
	public static T [] clear <T>(this T [] @this,T dflt =default(T)){
		int leng = @this.Length;
		for(int i =0; i < leng; i++){
			@this [i] = dflt;
		}
		return @this;
	}
}
	public static class int32{
		public static int log(this int @this,int @base){
			return Mathf.Log (@this, @base).round();
		}
		public static int log2(this int @this){
			return Mathf.Log (@this, 2).round();
		}
		public static int abs(this int @this){
			return @this < 0 ? Mathf.Abs(@this) : @this;
		}
		public static int round(this float @this){
			return Mathf.RoundToInt (@this);
		}
		public static int floor(this float @this){
			return Mathf.FloorToInt (@this);
		}
		public static int toInt(this byte @this)
		{
			return (int)@this;
		}
		public static int floor(this int value,int target){
			return (target * (value / target) - (value % target));
		}
		/// <summary>
		/// Clamp the specified value, min and max.
		/// </summary>
		public static int clamp (this int @this, int min = 0, int max = 1){
			return @this < min ? min : @this > max ? max : @this;
		}

	}
		public static class EaBool{
			public static bool nullPtr <T> (this T [] @this){
				return !(@this != null && @this.Length > 0);
			}	
			public static bool nullPtr <T> (this T  @this){
				return !(@this != null );
			}
			public static bool nullPtr <T> (this T  @this,Action callback){
				var result = @this.nullPtr ();	
				callback ();
				return result;
			}	
			public static bool nullPtr <T> (this T [] @this,Action<bool> callback){
				var result = @this.nullPtr ();
				callback (result);
				return (result);
			}	
		}

		public static class int64{
		/// <summary>
		/// Clamp the specified value, min and max.
		/// </summary>
		public static float abs(this float @this){
			return @this.round().abs();
		}
		public static float log(this float @this,int @base){
			return Mathf.Log (@this, @base);
		}
		public static float log2(this float @this){
			return Mathf.Log (@this, 2).round();
		}
		public static float clamp (this float @this, float min = 0, float max = 1){
			return @this < min ? min : @this > max ? max : @this;
		}
		public static float floor(this float value,float target){
			return (target * (value / target) - (value % target));
		}
		public static float toFloat(this double value){
			return (value > float.MaxValue ? float.MaxValue : value < float.MinValue ? float.MinValue : (float)value);
		}

	}
	public static class EaVec2 {
		public static Vector2 abs(this Vector2 @this, bool x = true,bool y = true){
			@this.x = x  ? Mathf.Abs(@this.x) : @this.x;
			@this.y = y ? Mathf.Abs (@this.y) : @this.y;
			return @this;
		}	
		public static Vector2 neg(this Vector2 @this, bool x = true,bool y = true){
			@this.x = x  ? -Mathf.Abs(@this.x) : @this.x;
			@this.y = y ? -Mathf.Abs (@this.y) : @this.y;
			return @this;

		}
		public static  Vector2 lerp (this Vector2 @this, Vector2 target,float t){
			return Vector2.Lerp (@this, target, t);
		}
		public static Vector2 round(this Vector2 @this, bool x = true,bool y = true){
			@this.x = x  ? Mathf.Round(@this.x) : @this.x;
			@this.y = y ? Mathf.Round (@this.y) : @this.y;
			return @this;
		}
		public static Vector2 floor(this Vector2 @this, bool x = true,bool y = true){
			@this.x = x  ? Mathf.Floor(@this.x) : @this.x;
			@this.y = y ? Mathf.Floor (@this.y) : @this.y;
			return @this;
		}

	}
		public static class EaVec3 {
		public static Vector3 abs(this Vector3 @this, bool x = true,bool y = true,bool z = true){
			@this.x = x  ? Mathf.Abs(@this.x) : @this.x;
			@this.y = y ? Mathf.Abs (@this.y) : @this.y;
			@this.z = z ? Mathf.Abs (@this.z) : @this.z;
			return @this;
		}
		public static Vector3 lerp(this Vector3 @this, Vector3 target,float t){
			return Vector3.Lerp (@this, target, t);
		}
		public static Vector3 round(this Vector3 @this, bool x = true,bool y = true,bool z = true){
			@this.x = x  ? Mathf.Round(@this.x) : @this.x;
			@this.y = y ? Mathf.Round (@this.y) : @this.y;
			@this.z = z ? Mathf.Round (@this.z) : @this.z;
			return @this;
		}
		public static Vector3 floor(this Vector3 @this, bool x = true,bool y = true,bool z = true){
			@this.x = x  ? Mathf.Floor(@this.x) : @this.x;
			@this.y = y ? Mathf.Floor (@this.y) : @this.y;
			@this.z = z ? Mathf.Floor (@this.z) : @this.z;
			return @this;
		}


	}
		public static class EaColor{}
		public static class EaColor32{}
		public static class estring{
		public static string color (this string @this, Color color){
			byte maxBytes = 255;
			string  hex = (color.r *maxBytes).round().toHex() + (color.g *maxBytes).round().toHex() + (color.b *maxBytes).round().toHex() + (color.a *maxBytes).round().toHex();
			return string.Format ("<color=#{1}>{0}</color>",@this,hex);
		}
		public static string color (this string @this, Color32 color){
			string  hex = color.r.toInt().toHex() + color.g.toInt().toHex() + color.b.toInt().toHex() + color.a.toInt().toHex();
			return string.Format ("<color=#{1}>{0}</color>",@this,hex);
		}
		public static string color (this string @this, string color){
			return string.Format ("<color=#{1}>{0}</color>",@this,color);
		}
//		public static string toHex (this Color color){
//
//
//		}

		private static string getHexString(this int @this,bool doubleZero = false){
			var hexChar = "abcdef";
			if (doubleZero && @this == 0)
				return "00";
			
			var result = @this <= 9  ? @this.ToString () : hexChar [(hexChar.Length - 1) - (15 - @this)].ToString(); 
			return result;
		} 
		public static string endline(this string @this){
			return @this + "\n";
		}
		public static string toHex(this int @this,string result = ""){
			if (@this < 16) {
				result = result.insertBefore (@this.getHexString (true));
				return result;
			}
			var odd = (@this % 16f).round();

			result = result.insertBefore (odd.getHexString());
			var newValue = ((@this - odd) / 16f).round();
			return toHex(newValue,result);
		}
			public static string insertBefore (this string @this ,string insertor){
				return insertor + @this;
			}
			public static string insertAfter(this string @this, string insertor){
				return @this + insertor;
			}
			public static string insert(this string @this,string before,string after){
				return before + @this + after;
			}
			
		}

	
