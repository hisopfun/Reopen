using System;


public class TicksEventArgs : EventArgs { 
    public string tick { get; set; }
}
class TickEncoder
{
    public delegate void TickEncoderEventHandler(object source, TicksEventArgs args);
    public event TickEncoderEventHandler TickEncoded;

    public void Encode(string str) {
        OnTickEncoded(str);
    }

    protected virtual void OnTickEncoded(string str) {
        if (TickEncoded != null)
            TickEncoded(this, new TicksEventArgs() { tick = str});
    }
}

