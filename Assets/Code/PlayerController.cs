using System.Linq;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    [HideInInspector]public float cooldown = 0;
    public delegate void Command();
    public Stack<Command> commandBuffer = new Stack<Command>();
    private Stack<float> cooldownsBuffer = new Stack<float>();

    [HideInInspector]public Unit unit;
    public bool canPerform;
    
    public Transform shootPoint;

    public Animator animator;
    public PlayerMovement playerMovement;

    
    private void Awake()
    {
        unit = GetComponent<Unit>();
        animator = GetComponent<Animator>();
        Instance = this;
    }

    private void Update()
    {
        CommandCooldown();
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