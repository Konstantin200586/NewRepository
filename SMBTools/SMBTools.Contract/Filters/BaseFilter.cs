namespace SMBTools.Contract.Filters
{
    public class BaseFilter
    {
        public Guid? Id { get; set; }
        public Guid[] Ids { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
