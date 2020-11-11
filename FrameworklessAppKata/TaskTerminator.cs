using System.Threading;
using System.Threading.Tasks;

namespace FrameworklessAppKata
{
    public class TaskTerminator
    {
        public CancellationTokenSource CancellationTokenSource { get; set; }
        public Task Task { get; set; }
    }
}
