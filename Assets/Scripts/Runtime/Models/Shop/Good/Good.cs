using System;

namespace Shooter.Shop
{
    public sealed class Good : IGood
    {
        private readonly IGoodUsingView _usingView;

        public Good(IGoodUsingView usingView, IGoodData data)
        {
            _usingView = usingView ?? throw new ArgumentNullException(nameof(usingView));
            Data = data ?? throw new ArgumentNullException(nameof(data));
        }
        
        public IGoodData Data { get; }

        public void Use() => _usingView.VisualizeUsing(Data);
    }
}