using System.Collections.Generic;

namespace Otus_14_vertical_scaling;

public class СheckСollisionsCommand : ICommand
{
    readonly IMovable movable;
    private readonly IField[] fields;
    private readonly ICollisionChecker checker;
    private readonly IQueue queue;

    public СheckСollisionsCommand(IMovable movable, IField[] fields, ICollisionChecker checker, IQueue queue)
    {
        this.movable = movable;
        this.fields = fields;
        this.checker = checker;
        this.queue = queue;
    }

    public void Execute()
    {
        //организовать проверку коллизий игровых объектов с помощью двух систем окрестностей
        foreach (var field in fields)
        {
            ExecuteForField(field);
        }
    }

    private void ExecuteForField(IField field)
    {
        //определяет окрестность, в которой присутствует игровой объект
        var newAreaId = field.GetAreaId(movable.Position);
        var newArea = field.GetAreaById(newAreaId);

        //если объект попал в новую окрестность, 
        //то удаляет его из списка объектов старой окрестности и добавляет список объектов новой окрестности.
        var oldAreaId = field.GetAreaId(movable.PreviousPosition);
        if (newAreaId != oldAreaId)
        {
            var oldArea = field.GetAreaById(oldAreaId);
            oldArea.Remove(movable);
            newArea.Add(movable);
        }

        //для каждого объекта новой окрестности и текущего движущегося объекта создает команду проверки коллизии 
        //этих двух объектов. Все эти команды помещает в макрокоманду и эту макрокоманду записывает 
        //на место аналогичной макрокоманды для предыдущей окрестности.
        List<ICommand> commands = new();
        foreach (var otherObject in newArea)
        {
            if (movable != otherObject)
                commands.Add(new ActionCommand(()=> checker.Check(movable, otherObject)));
        }
        queue.Add(new MacroCommand(commands.ToArray()));
    }
}
