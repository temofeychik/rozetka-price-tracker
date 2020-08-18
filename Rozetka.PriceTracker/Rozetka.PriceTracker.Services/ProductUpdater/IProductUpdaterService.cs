using System;
using System.Threading.Tasks;

namespace Rozetka.PriceTracker.Services.ProductUpdater
{
    public interface IProductUpdaterService
    {
        Task UpdatePrices(TimeSpan timeBetweenUpdates, int amount = 1000);
    }
}