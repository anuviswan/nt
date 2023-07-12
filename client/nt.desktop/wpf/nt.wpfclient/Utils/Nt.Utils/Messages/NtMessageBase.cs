namespace Nt.Utils.Messages;

public abstract class NtMessageBase
{
    public NtMessageBase(object sender) => Sender = sender;
    public object Sender { get; set; }
}
