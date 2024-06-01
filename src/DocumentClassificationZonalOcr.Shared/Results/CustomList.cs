using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentClassificationZonalOcr.Shared.Results
{
    public class CustomList<T> 
    {
        public int Count => Items.Count;
        public int? TotalCount { get; set; }
        public int? TotalPages { get; set; }

        public List<T> Items { get; set; }

        public CustomList(List<T> items, int? totalCount, int? totalPages)
        {
            Items = items;
            TotalCount = totalCount;
            TotalPages = totalPages;
        }

        //public IEnumerator<T> GetEnumerator()
        //{
        //    return Items.GetEnumerator();
        //}

        //IEnumerator IEnumerable.GetEnumerator()
        //{
        //    return GetEnumerator();
        //}
    }
}
