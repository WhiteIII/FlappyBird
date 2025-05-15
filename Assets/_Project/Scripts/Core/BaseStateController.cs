using System;
using System.Linq;
using System.Threading.Tasks;
using Unity.VisualScripting;

namespace Project.Core
{
    public class BaseStateController : IInitializable, IDisposable
    {
        private readonly IState[] _states;
        private readonly ITransition[] _transitions;
        private readonly Type _initialState;

        private IState _current;

        public BaseStateController(
            IState[] states, 
            ITransition[] transitions,
            Type initialState)
        {
            _states = states;
            _transitions = transitions;
            _initialState = initialState;
        }

        public void Initialize() =>
            Translate(_initialState);

        public void Dispose()
        {
            foreach (var state in _states) 
            { 
                if (state is  IDisposable disposable) 
                    disposable.Dispose();
            }
        }
        
        public async Task Translate(Type type)
        {
            if (_current is IExitAsyncState exitStateAsync)
                await exitStateAsync.ExitAsync();
            else if (_current is IExitState exitState)
                exitState.Exit();

            ITransition transition = _transitions.First(x => x.To == type);
            _current = _states.First(x => x.GetType() == transition.To);
            
            if (_current is IEnterAsyncState enterStateAsync)
                await enterStateAsync.EnterAsync();
            else if (_current is IEnterState enterState)
                enterState.Enter();
        }
    }
}
