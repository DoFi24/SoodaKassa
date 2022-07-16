using System.ComponentModel;

namespace GitMarket.Domain.Models.APIResponseRequest
{
    public class REQUEST_RESPONSE_MODEL<T>
    {
        private uint? page_ = 1;

        private uint? pageSize_ = 1;

        private uint? pageCount_ = 1;
        public BindingList<T> rows { get; set; }
        public uint? Page { get { return page_; } set { page_ = value; } }
        public uint? PageSize { get { return pageSize_; } set { pageSize_ = value; } }
        public uint? PageCount { get { return pageCount_; } set { pageCount_ = value; } }
    }
}
