using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System;
using System.Threading;
[System.Serializable]
public class EaSerializable : IEaSerializable{
	public string path{get;set;}
	public bool cryption{get;set;}
}

public interface IEaSerializable{
	string path{ get; set;}
	bool cryption {get;set;}
}
	
public static class EaFile{
		private static void delete(this string path){
			File.Delete (path);
		}

		public static T Open<T>  (string fileName = "") where T : IEaSerializable, new()
		{
		EaMonoSingleton<FileDelegate>.Initialize ();
		string path = EaDevice.filePath (typeof(T).Name.insert("(",")") + fileName + EaDevice.instance.fileType);
				T @out = new T ();
				ThreadStart fileResult = new ThreadStart (() => {
				BinaryFormatter bf = new BinaryFormatter ();
				if (File.Exists(path)){
					using (FileStream fs = File.Open (path, FileMode.Open)) {
						try {
							
								string decryptor = (string)bf.Deserialize(fs);
								@out = JsonUtility.FromJson<T>(Cryptography.Decrypt(decryptor));

						} catch (Exception fileFormatException) {
							Debug.LogError ("Can't deserialize, file format not found!");
							throw fileFormatException;
						}

					}
				} else
				using (FileStream fs = File.Create (path)) {
						try {
						string encryptor = Cryptography.Encrypt(JsonUtility.ToJson(@out));
								bf.Serialize(fs,encryptor);
						} catch (Exception failedSerializeException) {
							Debug.LogError ("Can't serialize object,make sure the object have attribute [System.Serializable]");
							throw failedSerializeException;
						}
					}	
				@out.cryption = true;
				@out.path = path;
			if(!FileDelegate.openedFiles.Contains(path)){
				FileDelegate.openedFiles.Add(path);
				FileDelegate.onQuit  += delegate {
					Thread microThread = new Thread(new ThreadStart(()=>{
						@out.Save();

					}));
					microThread.Start();
					microThread.Join();

					};

				FileDelegate.onPause += status => {
					Thread microThread = new Thread(new ThreadStart(()=>{
						if(status)
						@out.Save();
					}));
					microThread.Start();
					microThread.Join();


					};
				}
			});
			Thread fileThread = new Thread(fileResult);
			fileThread.Start ();
			fileThread.Join ();

			return @out;
		}
		public static void Save<T> (this T file) where T: IEaSerializable{
		bool encryption = file.cryption;
		if (File.Exists(file.path))
			file.path.delete ();
		Thread fileSave = new Thread (() => {
			using (FileStream fs = File.Open (file.path, FileMode.Create)) {
				BinaryFormatter bf = new BinaryFormatter ();
				string	cryptor = Cryptography.Encrypt(JsonUtility.ToJson(file));
				bf.Serialize(fs,cryptor);

			}
//			Debug.Log (typeof(T).Name.color(Color.red) + " saved!");	

		});
		fileSave.Start ();
		fileSave.Join ();
		}


	}



			
			