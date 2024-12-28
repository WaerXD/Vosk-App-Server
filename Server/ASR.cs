using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AForge.Math;
using AForge.Math.Metrics;

internal class ASR
{
    internal static List<SpeakerModelExt> listUserModels;
    internal static CosineSimilarity sim = new CosineSimilarity();
    internal void LoadModelsUsers()
    {
        var userFileData = File.ReadAllText($"{AppDomain.CurrentDomain.BaseDirectory}speakers.json");
        var speakers = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SpeakerModel>>(userFileData);
        listUserModels = speakers.Select(s => new SpeakerModelExt()
        {
            Name = s.Name,
            Vector = s.Vector,
            Sign = GetDoubleArrayFromString(s.Vector)
        }).ToList();



    }

    internal double[] GetDoubleArrayFromString(string vector)
    {
        var array = new double[vector.Length];
        var items = vector.Split(';');
        array = items.Select(i => double.Parse(i)).ToArray();
        return array;
    }

    public ResultSpeaker GetSpeaker(List<double[]> vectors)
    {
        var resultSpeakers = new List<ResultSpeaker>();
        foreach (var vector in vectors)
        {
            foreach (var modelSpeaker in listUserModels)
            {
                var cosDist = sim.GetSimilarityScore(modelSpeaker.Sign, vector);
                if (cosDist > 0.6)
                {
                    var result = resultSpeakers.FirstOrDefault(rs => rs.Name.ToLower() == modelSpeaker.Name.ToLower());
                    if (result == null)
                    {
                        result = new ResultSpeaker()
                        {
                            Name = modelSpeaker.Name,
                            Count = 1,
                            Accuracy = cosDist
                        };
                        resultSpeakers.Add(result);
                    }
                    else
                    {
                        result.Count++;
                        result.Accuracy += cosDist;
                    }
                }
            }
        }
        if (resultSpeakers.Any())
        {
            foreach (var rs in resultSpeakers)
            {
                rs.Accuracy = rs.Accuracy / rs.Count;
            }
            return resultSpeakers.FirstOrDefault(r => r.Accuracy == resultSpeakers.Max(rs => rs.Accuracy));
        }
        return null;

    }
}

internal class ResultSpeaker
{
    public string Name { get; set; }
    public int Count { get; set; }
    public double Accuracy { get; set; }
}




