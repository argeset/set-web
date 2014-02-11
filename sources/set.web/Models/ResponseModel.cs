namespace set.web.Models
{
    public class ResponseModel : BaseModel
    {
        public bool IsOk { get; set; }
        public object Result { get; set; }
    }
}