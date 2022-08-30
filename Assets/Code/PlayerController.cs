using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    [HideInInspector]public float cooldown = 0;
    public delegate void Command();
    public Stack<Command> commandBuffer = new Stack<Command>();
    private Stack<float> cooldownsBuffer = new Stack<float>();

    [FormerlySerializedAs("unit")] public OldUnit oldUnit;
    public bool canPerform;
    
    public UnitMovement unitMovement;
    private void Awake()
    {
        Instance = this;
    }

    public void CloseBuffer()
    {
        canPerform = false;
    }

    public async void CloseBuffer(float duration)
    {
        canPerform = false;
        
        var end = Time.time + duration;
        while (Time.time < end)
        {
            await Task.Yield();
        }
        OpenBuffer();
    }
    
    public void OpenBuffer()
    {
        canPerform = true;
    }

    private void Update()
    {
        CommandCooldown();
        MovementInput();
    }
    
    public void MovementInput()
    {
        if (oldUnit == null) return;

        if (oldUnit.gameObject.TryGetComponent(out UnitMovement um))
        {
            unitMovement = um;
        }
        //Move Forward
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            AddCommand(unitMovement.MoveForward, unitMovement.movementSpeed);
        }

        //Move Backwards
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            AddCommand(unitMovement.MoveBack, unitMovement.movementSpeed);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            //maybe a passive
        }
    }

    public void AddCommand(Command command, float duration)
    {
        if (!canPerform) return;
        commandBuffer.Push(command);
        cooldownsBuffer.Push(duration);
    }
    
    public void AddCommand(Command command)
    {
        if (!canPerform) return;

        commandBuffer.Push(command);
    }

    private void CommandCooldown()
    {
        cooldown = Mathf.Clamp(cooldown, 0, cooldown);

        if (cooldown > 0)
        {
            cooldown -= Time.unscaledDeltaTime;
        }
        else
        {
            ExecuteCommand();
        }
    }

    private void ExecuteCommand()
    {
        if (!commandBuffer.Any()) return;
        var cmnd = commandBuffer.Pop();
        var cd = cooldownsBuffer.Pop();

        cmnd();
        cooldown += cd;

        commandBuffer.Clear();
        cooldownsBuffer.Clear();
    }
}