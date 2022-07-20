namespace ProjectPoint.Common.Paging
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class PageParam
    {
        public const int MinOffset = 0, MinPage = 1;
        public const int MinLimit = 50, MinPageSize = 50;

        private int? offset;
        private int? limit;
        private int? page;
        private int? pageSize;
        private string sortDirection;
        private string sortBy;
        private int totalCount;

        public PageParam()
        {
            this.Offset = MinOffset;
            this.Limit = MinLimit;
            this.sortBy = "desc";
        }
        
        public PageParam(PageParam pageParam)
        {
            this.sortBy = pageParam.SortBy;
            this.SortDirection = pageParam.SortDirection;
            if (pageParam.IsPageProvided)
            {
                this.page = pageParam.GetPage();
                this.pageSize = pageParam.GetPageSize();
            }
            else
            {
                this.offset = pageParam.GetOffset();
                this.limit = pageParam.GetLimit();
            }
        }

        [Range(0, int.MaxValue)]
        public int? Offset
        {
            get
            {
                if (this.IsPageProvided)
                {
                    return (this.page - 1) * this.Limit;
                }

                return this.offset ?? MinOffset;
            }

            set
            {
                this.offset = value;
            }
        }

        [Range(1, int.MaxValue)]
        public int? Limit
        {
            get
            {
                if (this.IsPageProvided)
                {
                    return this.PageSize;
                }

                return this.limit ?? MinLimit;
            }

            set
            {
                this.limit = value;
            }
        }

        public bool IsOffsetProvided
        {
            get
            {
                return this.offset.HasValue;
            }
        }

        [Range(1, int.MaxValue)]
        public int? Page
        {
            get
            {
                return this.page ?? MinPage;
            }

            set
            {
                this.page = value;
            }
        }

        [Range(1, int.MaxValue)]
        public int? PageSize
        {
            get
            {
                return this.pageSize ?? MinPageSize;
            }

            set
            {
                this.pageSize = value;
            }
        }

        public string SortBy
        {
            get
            {
                return this.sortBy;
            }
            set
            {
                this.sortBy = value;
            }
        }

       
        public int TotalCount
        {
            get
            {
                return this.totalCount;
            }
            set
            {
                this.totalCount = value;
            }
        }

        public string SortDirection
        {
            get
            {
                return this.sortDirection;
            }
            set
            {
                this.sortDirection = value;
            }
        }

        public bool IsPageProvided
        {
            get
            {
                return !this.IsOffsetProvided && this.page.HasValue;
            }
        }

        #region Methods

        public int? GetOffset()
        {
            return this.offset;
        }

        public int? GetLimit()
        {
            return this.limit;
        }

        public int? GetPage()
        {
            return this.page;
        }

        public int? GetPageSize()
        {
            return this.pageSize;
        }

        #endregion
    }
}
