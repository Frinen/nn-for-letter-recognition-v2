using nn_for_letter_recognition.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace nn_for_letter_recognition.Models
{
    class Perceptron
    {
        List<Neuron> neurons;
        double Error;

        public Perceptron(List<Neuron> _neurons)
        {
            neurons = _neurons;
        }

        public void Train(Dictionary<int, List<int>> TrainData, Dictionary<int, List<double>> Desires)
        {

            for (int j = 0; j < 1000; j++)
            {
                for (int t = 0; t < TrainData.Count; t++)
                {
                    for (int i = 0; i < neurons.Count; i++)
                    {
                        neurons[i].Calcutale(TrainData[t]);
                        //  neurons[i].CalculateEroor(Desires[t][i]);
                        // neurons[i].Correction();
                        CalcError(Desires[t]);
                        Correction(Desires[t]);

                    }
                }
            }
            MessageBox.Show(Convert.ToString(Error));

        }

        public void CalcError(List<double> desires)
        {
            Error = 0;
            for (int i = 1; i < neurons.Count; i++)
            {
                Error += Convert.ToDouble(Math.Pow((desires[i] - neurons[i].Output), 2));
            }
            Error = Error * 0.5;

        }

        public void Correction(List<double> desires)
        {
            for (int i = 0; i < desires.Count; i++)
            {
                double deltaWeight;
                double neuronError;
                neuronError = Convert.ToDouble(neurons[i].Output * (1.0 - neurons[i].Output) * (desires[i] - neurons[i].Output));
                for (int j = 0; j < neurons[i].Inputs.Count; j++)
                {

                    deltaWeight = Convert.ToDouble(neurons[i].Eta * neuronError * neurons[i].Inputs[j]);
                    neurons[i].Weights[j] += deltaWeight;
                }
            }
        }

        public string Evaluate(List<int> inputs)
        {
            string letterOut = "";
            List<double> outputs = new List<double>();
            char letter;
            for (int i = 0; i < neurons.Count; i++)
            {
                neurons[i].Calcutale(inputs);
                outputs.Add(neurons[i].Output);
               /* if (neurons[i].Output == 1)
                {
                    letter = Convert.ToChar(97 + i);
                    letterOut += letter;
                }*/
                letterOut += $"{outputs[i]} ";
            }
            int index=0;
            double max = outputs[0];
            for (int i = 0; i < neurons.Count; i++)
            {
                if(max < outputs[i])
                {
                    max = outputs[i];
                    index = i;
                }
            }
            letter = Convert.ToChar(97 + index);
            letterOut += letter;
            return letterOut;
        }

        public string WrightWeight()
        {
            try
            {
                string writePath = @"ath.txt";
                using (StreamWriter sw = new StreamWriter(writePath, false, System.Text.Encoding.Default))
                {
                    foreach (var neuron in neurons)
                    {
                        foreach (var weight in neuron.Weights)
                        {
                            sw.WriteLine(weight);
                        }
                    }
                    return ("success");
                }
            }
            catch (Exception ex)
            {
                return ("error");
            }
        }
        public string ReadWeight()
        {
            try
            {
                string readPath = @"ath.txt";
                using (StreamReader sr = new StreamReader(readPath, System.Text.Encoding.Default))
                {
                    string line;
                    for (int i = 0; i < neurons.Count; i++)
                    {
                        for (int j = 0; j < neurons[i].Weights.Count; j++)
                        {
                            line = sr.ReadLine();
                            neurons[i].Weights[j] = Convert.ToDouble(line);
                        }


                    }
                }
                return ("success");
            }
            catch (Exception ex)
            {
                return ("error");
            }
        }
    }
}
