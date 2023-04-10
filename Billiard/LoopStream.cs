using NAudio.Wave;

public class LoopStream : WaveStream
{
    private readonly WaveStream sourceStream;

    public LoopStream(WaveStream stream)
    {
        this.sourceStream = stream;
        this.sourceStream.Position = 0;
    }

    public override WaveFormat WaveFormat => this.sourceStream.WaveFormat;

    public override long Length => long.MaxValue;

    public override long Position
    {
        get => this.sourceStream.Position;
        set => this.sourceStream.Position = value;
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
        int totalBytesRead = 0;

        while (totalBytesRead < count)
        {
            int bytesRead = this.sourceStream.Read(buffer, offset + totalBytesRead, count - totalBytesRead);

            if (bytesRead == 0)
            {
                // Reached the end of the stream, so loop back to the beginning
                this.sourceStream.Position = 0;
            }
            else
            {
                totalBytesRead += bytesRead;
            }
        }

        return totalBytesRead;
    }
}