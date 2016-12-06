using System;

namespace Underwater_Boat
{
    public interface IServiceBus
    {
        int Next(int minValue, int maxValue);
    }
}