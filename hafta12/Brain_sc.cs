using System;
using System.Collections.Generic;
using UnityEngine;

public class Brain_sc : MonoBehaviour
{
    [Header("Network Settings")]
    [SerializeField] private int numberOfInputs = 2;
    [SerializeField] private int numberOfOutputs = 1;
    [SerializeField] private int numberOfHiddenLayers = 1;
    [SerializeField] private int numberOfNeuronsPerHiddenLayer = 2;

    [Header("Training Settings")]
    [SerializeField] private double alpha = 0.5;          
    [SerializeField] private int maxEpoch = 100000;      
    [SerializeField] private double targetSSE = 0.001;    
    [SerializeField] private int logEveryEpoch = 1000;    

    private ANN_sc ann;

    private readonly List<List<double>> trainInputs = new()
    {
        new List<double>{0, 0},
        new List<double>{0, 1},
        new List<double>{1, 0},
        new List<double>{1, 1},
    };

    private readonly List<List<double>> trainOutputs = new()
    {
        new List<double>{0},
        new List<double>{1},
        new List<double>{1},
        new List<double>{0},
    };

    void Start()
    {
        ann = new ANN_sc(
            numberOfInputs,
            numberOfOutputs,
            numberOfHiddenLayers,
            numberOfNeuronsPerHiddenLayer,
            alpha
        );

        TrainXOR();
        TestXOR();
    }

    private void TrainXOR()
    {
        double sse = double.MaxValue;

        for (int epoch = 0; epoch < maxEpoch; epoch++)
        {
            sse = 0.0;

            for (int i = 0; i < trainInputs.Count; i++)
            {
               
                List<double> output = ann.Run(trainInputs[i], trainOutputs[i]);

                double error = trainOutputs[i][0] - output[0];
                sse += error * error;
            }

            if (epoch % logEveryEpoch == 0)
            {
                Debug.Log($"Epoch: {epoch} | SSE: {sse}");
            }
            if (sse <= targetSSE)
            {
                Debug.Log($"✅ Training completed at epoch {epoch} | Final SSE: {sse}");
                break;
            }
            if (double.IsNaN(sse) || double.IsInfinity(sse))
            {
                Debug.LogError("SSE became NaN/Infinity. Decrease alpha.");
                break;
            }
        }

        Debug.Log($"Training finished. Final SSE: {sse}");
    }

    private void TestXOR()
    {
        Debug.Log("=== XOR TEST ===");

        for (int i = 0; i < trainInputs.Count; i++)
        {
            List<double> output = ann.Run(trainInputs[i], trainOutputs[i]);

            double raw = output[0];
            int rounded = raw >= 0.5 ? 1 : 0;

            Debug.Log($"Input: {trainInputs[i][0]}, {trainInputs[i][1]} => Output: {raw:F4} (rounded: {rounded})");
        }
    }
}

