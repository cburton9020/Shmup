using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
public partial class EnemySpawner : Node2D
{

	private ulong frames = 0;
	private uint timePassed = 0;

	private bool spawningActive = true;

	public PackedScene enemyPattern1;

	private List<WaveData> _waves = new List<WaveData>();
	private int _currentWaveIndex;




	public struct WaveData
	{
		//[JsonPropertyName("time_trigger")]
		public int time_trigger { get; set; }

		//[JsonPropertyName("pattern")]
		public int pattern { get; set; }

	}

	public struct WaveDataRoot
	{
		//public List<WaveData> Waves { get; set; }
		public WaveData[] waves { get; set; }
	}










	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		LoadWavesFromJson(Constants.LEVEL1PATTERNPATH);
		enemyPattern1 = GD.Load<PackedScene>(Constants.ENEMYPATTERN1PATH);
		//GD.Print(_waves[0]);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		frames++;
		timePassed = (uint)(frames / 60);
		WaveTriggers();

	}





	private void LoadWavesFromJson(string filePath)
	{
		_waves.Clear();

		FileAccess file = FileAccess.Open(filePath, FileAccess.ModeFlags.Read);
		if (file == null)
		{
			GD.Print("Balls Exploded (no file)");
			return;
		}

		string jsonText = file.GetAsText();
		file.Close();

		try
		{
			var options = new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			};
			// var root = JsonSerializer.Deserialize<WaveDataRoot>(jsonText);
			// _waves = root.Waves;
			// GD.Print(JsonSerializer.Deserialize<WaveDataRoot>(jsonText));
			WaveDataRoot root = JsonSerializer.Deserialize<WaveDataRoot>(jsonText, options);
			GD.Print(root.waves[0].time_trigger);
			if (root.waves != null && root.waves.Length > 0)
            {
                // THIS IS THE FIX: Actually add the waves to your list!
                _waves.AddRange(root.waves);
                
                GD.Print($"Successfully loaded {_waves.Count} waves");
                
                // Debug print all waves
                for (int i = 0; i < _waves.Count; i++)
                {
                    GD.Print($"Wave {i}: time_trigger={_waves[i].time_trigger}, pattern={_waves[i].pattern}");
                }
            }
            else
            {
                GD.PrintErr("No waves found in JSON or waves array is empty");
            }
			// GD.Print(jsonText);
			// GD.Print("Successfully loaded waves from JSON");
		}
		catch (Exception e)
		{
			GD.PrintErr($"JSON parsing error: {e.Message}");
		}
	}


	private void WaveTriggers()
	{
		for (int i = _currentWaveIndex; i < _waves.Count; i++)
		{
			WaveData wave = _waves[i];

			
			if (timePassed >= wave.time_trigger)
			{
				SpawnWave(wave);
				_currentWaveIndex = i + 1;
				GD.Print($"Spawned wave at {timePassed:F1}s (trigger: {wave.time_trigger}s)");
				break;
			}
		}
	}

		

	private void SpawnWave(WaveData wave)
	{
		switch (wave.pattern)
		{
			case 1:
				SpawnPattern1();
				GD.Print("Spawned Enemy 1");
				break;
			case 2:
				break;
			case 3:
				break;
		}
	}

	private void SpawnPattern1()
	{
		var pattern1 = enemyPattern1.Instantiate<Node2D>();
		foreach (Node child in pattern1.GetChildren())
		{
			if (child is Area2D area)
			{
				GD.Print($"  {area.Name}: Position = {area.Position}");
			}
		}
		AddChild(pattern1);

		GD.Print("After adding to scene:");
        foreach (Node child in pattern1.GetChildren())
        {
            if (child is Area2D area)
            {
                GD.Print($"  {area.Name}: Position = {area.Position}, GlobalPosition = {area.GlobalPosition}");
            }
        }
	}

		
		
		
		

}

