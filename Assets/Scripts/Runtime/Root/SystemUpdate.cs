using System;
using System.Collections.Generic;
using Shooter.Model;

namespace Shooter.Root
{
    public sealed class SystemUpdate : ISystemUpdate, IUpdateble
    {
        private readonly List<IUpdateble> _updatebles = new();

        public void Add(IUpdateble updateble)
        {
            if (updateble is null)
                throw new ArgumentNullException(nameof(updateble));
            
            _updatebles.Add(updateble);
        }

        public void Add(params IUpdateble[] updatebles)
        {
            if (updatebles is null) 
                throw new ArgumentNullException(nameof(updatebles));
            
            _updatebles.AddRange(updatebles);
        }

        public void Update(float deltaTime)
        {
            _updatebles.ForEach(updateble => updateble.Update(deltaTime));
        }
    }
}