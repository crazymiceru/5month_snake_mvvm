using System;
using System.Collections.Generic;
using UnityEngine;

namespace Snake
{
    internal sealed class ListControllers : IExecute, IInitialization, ILateExecute
    {
        //internal static ListControllers inst;

        private event Action _init = delegate { };
        private event Action<float> _execute = delegate { };
        private event Action _lateExecute = delegate { };
        private event Action _destroy = delegate { };

        private bool isInitHasPassed = false;

        public static int countClass = 0;
        private int _countClass;
        public static int countAddListControllers = 0;

        public ListControllers()
        {
            countClass++;
            _countClass = countClass;
            countAddListControllers = 0;
            Debug.Log($"Init ListControllers {countClass}");
            //inst = this;
        }

        public void Execute(float deltaTime)
        {
            _execute(deltaTime);
        }

        public void Initialization()
        {
            isInitHasPassed = true;
            _init();
        }

        public void LateExecute()
        {
            _lateExecute();
        }

        public void Destroy()
        {
            _destroy();
        }

        internal void Add(IController controller, string name = "")
        {
            countAddListControllers++;
            if (controller is IInitialization init)
            {
                _init += init.Initialization;
                if (isInitHasPassed) init.Initialization();
            }
            if (controller is IExecute execute)
            {
                _execute += execute.Execute;
            }
            if (controller is ILateExecute lateExecute)
            {
                _lateExecute += lateExecute.LateExecute;
            }
            if (controller is IDestroy destroy)
            {
                _destroy += destroy.Destroy;
            }
        }

        internal void Delete(IController controller)
        {
            countAddListControllers--;
            if (controller is IInitialization init)
            {
                _init -= init.Initialization;
            }
            if (controller is IExecute execute)
            {
                _execute -= execute.Execute;
            }
            if (controller is ILateExecute lateExecute)
            {
                _lateExecute -= lateExecute.LateExecute;
            }
            if (controller is IDestroy destroy)
            {
                _destroy -= destroy.Destroy;
            }
        }
    }
}
