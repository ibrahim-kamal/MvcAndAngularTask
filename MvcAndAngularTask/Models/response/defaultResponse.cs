namespace MvcAndAngularTask.Models.response
{
    public class defaultResponse
    {
        public int status {  get; set; }
        public String message{  get; set; }
        public object data{  get; set; }
        public defaultResponse(string message, int status, Object data ) {
            this.status = status;
            this.message = message;
            this.data = data;
        }
    }
}
