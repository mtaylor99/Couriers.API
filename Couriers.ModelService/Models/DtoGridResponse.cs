using System.Collections.Generic;

namespace Couriers.ModelService.Models
{
    public class DtoGridResponse<T>
    {
        public List<T> Result { get; set; }

        public int Count { get; set; }
    }
}
