public class SpeakerModel
{
    public string Name { get; set; }
    public string Vector { get; set; }
}

public class SpeakerModelExt : SpeakerModel
{
    public double[] Sign { get; set; }
}