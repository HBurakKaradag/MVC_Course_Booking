using System.Collections;

namespace BookingSystem.WebUI.Models.DataTableResponse
{
    public class DataTablesResponse
    {
        public int draw { get; set; }

        /// <summary>
        /// Gets the data collection.
        /// </summary>
        public IEnumerable data { get; set; }

        public int recordsTotal { get; set; }

        public int recordsFiltered { get; set; }

        public DataTablesResponse(int draw, IEnumerable data, int recordsFiltered, int recordsTotal)
        {
            this.draw = draw;
            this.data = data;
            this.recordsFiltered = recordsFiltered;
            this.recordsTotal = recordsTotal;
        }
    }
}