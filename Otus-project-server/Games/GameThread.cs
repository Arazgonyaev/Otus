

namespace Otus_project_server;

public class GameThread 
{
    public Thread Thread {get; private set;}
    private IQueue queue;
    private BehaviourOptions behaviourOptions;
    private Action<BehaviourOptions> behaviour;
    
    public GameThread(IQueue queue, Action<BehaviourOptions> behaviour)
    {
        this.queue = queue;
        this.behaviour = behaviour;
        CreateThread();
    }

    public void Start()
    {
        Thread.Start();
    }

    public void Stop() 
    {
        behaviourOptions.IsStop = true;
    }

    public void SetBehaviour(Action<BehaviourOptions> behaviour) 
    {
        this.behaviour = behaviour;
        RestartThread();
    }
    
    private void RestartThread()
    {
        Stop();
        CreateThread();
        Start();
    }

    private void CreateThread()
    {
        behaviourOptions = new BehaviourOptions{Queue = queue};
        Thread = new Thread(() => behaviour(behaviourOptions));
    }
}