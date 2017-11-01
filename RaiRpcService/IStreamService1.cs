using System.IO;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace StreamService
{

    [ServiceContract]
    public interface IRpcStreamImpl
    {
        [OperationContract]
        [WebInvoke(Method = "POST",
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "getblocks")]
        void GetBlocks(Stream data);

    }
}