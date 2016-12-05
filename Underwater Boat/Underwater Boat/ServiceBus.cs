using System;

namespace Underwater_Boat
{
    public class ServiceBus : IServiceBus
    {
        readonly Random rand = new Random();
        
        public int Next(int minValue, int maxValue)
        {
            return rand.Next(minValue, maxValue);
        }
    }
}
