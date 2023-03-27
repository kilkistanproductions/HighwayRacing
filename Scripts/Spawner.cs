using Godot;
using System;

public class Spawner : Node2D
{
	private ObjectPool _objectPool;
    private Vector2 _screenSize;
	private float _couldown;
    private Area2D _player;
	private Timer _timer;
    private int[] _pos;
	

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_objectPool = GetNode<ObjectPool>("/root/Node/ObjectPool");
		_timer = GetNode<Timer>("Timer");
        _player = GetNode<Area2D>("/root/Node/Player");
		_couldown = 2;
        _screenSize = GetViewportRect().Size;
        _pos = new int[] {
            200, 300, 415, 520
        };
		//Start timer
		_timer.Start(_couldown);

	}
    
    private void Spawn()
    {
        //Get object and check for errors
        var obj = _objectPool.GetObject();
        if(obj == null)
            return;
        //Change location and enable object 
        GD.Randomize(); //Randomize seed
        var index = (int)GD.RandRange(0, 4);
        var posY = _player.Position.y - _screenSize.y;
        obj.GlobalPosition = new Vector2(_pos[index], posY);
        obj.SetProcess(true);
    }
	
    private void OnTimerTimeout()
	{
        Spawn();
        _timer.Start(_couldown);
	}

}

