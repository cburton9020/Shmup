using Godot;
using System;





public partial class Player : CharacterBody2D
{
	public const float Speed = 2500.0f;
	public PackedScene projetilePackedScene;
	private float shootCooldown = 0.1f; // Time between shots in seconds
	private ulong lastShotTime = 0;

	private bool CanShoot()
	{
		return Time.GetTicksMsec() - lastShotTime >= shootCooldown * 1000;
	}


	public override void _Ready()
	{
		projetilePackedScene = GD.Load<PackedScene>("res://Scenes/PlayerProjectile1.tscn");
		
		// stage = (BaseStage)GetTree().CurrentScene;
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		// GD.Print(Position);





		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{

			if (Input.IsActionPressed("movement_augment"))
			{
				velocity.X = direction.X * (Speed / 2);

			}
			else
			{
				velocity.X = direction.X * (Speed);
			}


			if (Input.IsActionPressed("movement_augment"))
			{
				velocity.Y = direction.Y * (Speed / 2);

			}
			else
			{
				velocity.Y = direction.Y * (Speed);
			}


		}
		else
		{
			velocity.X = 0;
			velocity.Y = 0;
		}

		Velocity = velocity;
		MoveAndSlide();



		if (Input.IsActionPressed("Shoot1") && CanShoot())
		{


			PlayerProjectile1 projectile = projetilePackedScene.Instantiate<PlayerProjectile1>();
			GetTree().CurrentScene.AddChild(projectile);
			projectile.Position = new Vector2(GlobalPosition.X, GlobalPosition.Y);


			lastShotTime = Time.GetTicksMsec();

		}
	}
}

