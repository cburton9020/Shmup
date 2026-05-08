using Godot;
using System;
using System.Net.Http;

public partial class EnemyScene1 : Area2D
{
	private CharacterBody2D _physicsBody;

	public int hpBase = 3;
	public int hp = 3;

	[Export] public Vector2 Velocity { get; set; } = new Vector2(0, 100);
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// _physicsBody = GetNode<CharacterBody2D>("CharacterBody2D");
		hp = hpBase;
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// _physicsBody.Velocity = Velocity;
		// _physicsBody.MoveAndSlide();
		// Position = _physicsBody.GlobalPosition;
		Position += Velocity * (float)delta;

		if (hp <= 0)
        {
            this.QueueFree();
        }
	}

	public void OnAreaEntered(Area2D area)
	{

		if (area.Name.Equals("Projectile1Hitbox"))
		{

			hp -= 1;
			area.GetParent().QueueFree(); // destroys projectile

		}
		
	}

}
