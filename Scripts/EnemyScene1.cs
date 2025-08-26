using Godot;
using System;

public partial class EnemyScene1 : Area2D
{
	private CharacterBody2D _physicsBody;

	[Export] public Vector2 Velocity { get; set; } = new Vector2(0, 100);
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// _physicsBody = GetNode<CharacterBody2D>("CharacterBody2D");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// _physicsBody.Velocity = Velocity;
		// _physicsBody.MoveAndSlide();
		// Position = _physicsBody.GlobalPosition;
		Position += Velocity * (float)delta;
	}

	public void OnAreaEntered(Area2D area)
	{

		if (area.Name.Equals("Projectile1Hitbox"))
		{
			this.QueueFree();
		}
	}

}
