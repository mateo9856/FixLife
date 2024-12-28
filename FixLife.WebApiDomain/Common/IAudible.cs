namespace FixLife.WebApiDomain.Common
{
    public interface IAudible
    {
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
