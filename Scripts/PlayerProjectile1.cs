using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class PlayerProjectile1 : CharacterBody2D
{
	// Called when the node enters the scene tree for the first time.

	float speed = 3000f;
	Player player;
	Timer timer;
	public PlayerProjectile1 projectile;
	public PackedScene projetilePackedScene;
	private AnimatedSprite2D _animatedSprite;
	private Timer lifespanTimer;
	Vector2 velocity = new Vector2(0, 1);

	public override void _Ready()
	{
		projetilePackedScene = GD.Load<PackedScene>("res://Scenes/PlayerProjectile1.tscn");
		timer = new Timer();
		player = GetParent().GetNode<Player>("Player");
		// stage = (BaseStage)GetTree().CurrentScene;
		_animatedSprite = GetNode<AnimatedSprite2D>("StarSprite");
		lifespanTimer = new Timer();
        AddChild(lifespanTimer);
        lifespanTimer.WaitTime = 3.0f; // 3 seconds
        lifespanTimer.OneShot = true;
        lifespanTimer.Timeout += OnLifespanEnded;
        lifespanTimer.Start();
	}
	private void OnLifespanEnded()
	{
		QueueFree(); // Remove the projectile
	}
	
	

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		velocity.Y = speed * -1;

		_animatedSprite.Play("MedStar");






		Velocity = velocity;
		MoveAndSlide();
	}


	


	
}
