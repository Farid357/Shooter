using System;
using System.Collections.Generic;
using Shooter.Model;

namespace Shooter.Root
{
    public sealed class SystemUpdate : ISystemUpdate, IUpdateble
    {
        private readonly List<IUpdateble> _updatebles = new();
        
        public void Add(params IUpdateble[] updatebles)
        {
            if (updatebles is null) 
                throw new ArgumentNullException(nameof(updatebles));
            
            _updatebles.AddRange(updatebles);
        }

        public void Remove(IUpdateble updateble)
        {
            if(updateble is null)
                throw new ArgumentNullException(nameof(updateble));
            
            if (_updatebles.Contains(updateble) == false)
                throw new InvalidOperationException(nameof(Remove));
            
            _updatebles.Remove(updateble);
        }

        public void Update(float deltaTime)
        {
            try
            {
                for (var i = 0; i < _updatebles.Count; i++)
                {
                    _updatebles[i].Update(deltaTime);
                }
            }
            
            catch (Exception)
            {
                // ignored
            }
        }
    }
}