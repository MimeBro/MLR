using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    //public Unit unit;

    public float cooldown = 0;

    public delegate void Command();
    public Stack<Command> commandBuffer = new Stack<Command>();
    private Stack<float> cooldownsBuffer = new Stack<float>();

    public bool canPerform;

    private void Awake()
    {
        //unit = GetComponent<Unit>();
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