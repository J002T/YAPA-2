﻿using System;
using System.Windows.Input;
using YAPA.Contracts;

namespace YAPA.Shared
{
    public class StopCommand : ICommand
    {
        private readonly IPomodoroEngine _engine;
        public StopCommand(IPomodoroEngine engine)
        {
            _engine = engine;
            _engine.PropertyChanged += _engine_PropertyChanged;
        }

        private void _engine_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_engine.Phase))
            {
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public bool CanExecute(object parameter)
        {
            return _engine.Phase == PomodoroPhase.Work || _engine.Phase == PomodoroPhase.Break || _engine.Phase == PomodoroPhase.Pause;
        }

        public void Execute(object parameter)
        {
            _engine.Stop();
        }

        public event EventHandler CanExecuteChanged;
    }
}
