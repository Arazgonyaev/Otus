using System;
using System.Threading;

namespace Otus_14_vertical_scaling;

public class MyThread {
    public Thread Thread {get; private set;}
    private IQueue queue;
    private BehaviourOptions behaviourOptions;
    private Action<BehaviourOptions> behaviour;

    public MyThread(IQueue queue, Action<BehaviourOptions> behaviour)
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