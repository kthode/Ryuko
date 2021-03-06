﻿// Author: Viyrex(aka Yuyu)
// Contact: mailto:viyrex.aka.yuyu@gmail.com
// Github: https://github.com/0x0001F36D

namespace Ryuko.ProcessModel.StateMachine.TaskModels
{
    using Ryuko.ProcessModel.StateMachine.Delegates;
    using Ryuko.ProcessModel.StateMachine.Interfaces;

    using System;
    using System.Diagnostics;

    [DebuggerDisplay("Type: Do | Task: {Task} | NodeKind: {NodeKind}")]
    public sealed class DoTask : IStatement
    {
        private TaskQueue<IStatement> _queue;

        EventNodeKinds IStatement.NodeKind => EventNodeKinds.Do;
        public DoTaskHandler Task { get; }

        Delegate IStatement.Task => this.Task;

        internal DoTask(DoTaskHandler task, TaskQueue<IStatement> queue)
        {
            this.Task = task;
            queue.Enqueue(this);
            this._queue = queue;
        }

        public DoTask Do(DoTaskHandler task)
        {
            return new DoTask(task, this._queue);
        }

        public GetTask<T> Get<T>(GetTaskHandler<T> task)
        {
            return new GetTask<T>(task, this._queue);
        }

        public IWorkflow Stop()
        {
            return new Workflow(this._queue);
        }
    }
}