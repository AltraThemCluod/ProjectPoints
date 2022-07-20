namespace ProjectPoint.Common.Paging
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    
    public class PagedList<T> : PageParam where T : class
    {
        public PagedList()
        {
            this.Values = new List<T>();
        }
        public PagedList(PageParam pageParam) : base(pageParam)
        {
            this.Values = new List<T>();
        }       
        public long TotalRecords { get; set; }
        public List<T> Values { get; set; }
    }
}
