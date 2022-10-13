using Shooter.GameLogic;

namespace Shooter.Model
{
    public interface ISlider
    {
        void Subscribe(ISliderChangedValueAction sliderChangedValueAction);
        
        void Enable();

        void Disable();
    }
}