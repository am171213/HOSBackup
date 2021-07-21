/*using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
	public static void SavePlayer(PlayerController player)
	{
		BinaryFormatter formatter = new BinaryFormatter ();
		string path = Application.persistentDataPath + "/player.save";
		FileStream stream = new FileStream (path, FileMode.Create);

		GameMaster game = new GameMaster (player);

		formatter.Serialize (stream, game);
		stream.Close ();
	}

	public static GameMaster LoadPlayer()
	{
		string path = Application.persistentDataPath + "/player.save";
		if (File.Exists (path)) {
			BinaryFormatter formatter = new BinaryFormatter ();
			FileStream stream = new FileStream (path, FileMode.Open);
			formatter.Deserialize (stream);
			GameMaster game = formatter.Deserialize (stream) as GameMaster;		
			stream.Close ();
			return game;
	
		} else {
			Debug.LogError ("Save file not found in " + path);
			return null;
		}

	}
}
*/
