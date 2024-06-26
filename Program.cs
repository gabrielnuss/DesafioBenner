﻿using System;
using System.Collections.Generic;
using System.Linq;

public class Network
{
    private List<(int, int)> conexoesDiretas;
    int tamanhoMax;

    public Network(int tamanho)
    {
        if (tamanho < 0) {
            throw new ArgumentException("Número de posições deve ser positivo.");
        }
        conexoesDiretas = new List<(int, int)>();
        tamanhoMax = tamanho;
    }
    public void Connect(int elemento1, int elemento2)
    {
        if (elemento1 < 0 || elemento2 < 0 || elemento1 > tamanhoMax || elemento2 > tamanhoMax)
        {
            throw new ArgumentException("Valores inválidos para conexão.");
        }
        conexoesDiretas.Add((elemento1, elemento2));
    }

    public bool query(int elemento1, int elemento2) {
        if (elemento1 < 0 || elemento2 < 0 || elemento1 > tamanhoMax || elemento2 > tamanhoMax)
        {
            throw new ArgumentException("Valores inválidos para busca de conexão.");
        }
        if (VerificarConexaoDireta(elemento1, elemento2)) {
            return true;
        }  
        else
        {
            List<int> visitados = new List<int>();
            return VerificarConexaoIndireta(elemento1, elemento2, visitados);
        }
    }

    private bool VerificarConexaoDireta(int elemento1, int elemento2)
    {
        return conexoesDiretas.Contains((elemento1, elemento2));
    }

    private bool VerificarConexaoIndireta(int atual, int destino, List<int> visitados)
    {
        if (atual == destino)
        {
            return true;
        }
        if (!visitados.Contains(atual)) {
            visitados.Add(atual);
        }

        foreach (var (origem, proximo) in conexoesDiretas)
        {
            if (origem == atual && !visitados.Contains(proximo))
            {
                if (VerificarConexaoIndireta(proximo, destino, visitados))
                {
                    return true;
                }
            }
        }

        return false;
    }

    public static void Main(string[] args)
    {
        Network rede = new Network(10);
        rede.Connect(1, 2);
        rede.Connect(2, 3);
        rede.Connect(3, 4);

        Console.WriteLine(rede.query(1, 2)); 
        Console.WriteLine(rede.query(1, 3)); 
        Console.WriteLine(rede.query(1, 4));  
        Console.WriteLine(rede.query(1, 5));
        Console.WriteLine(rede.query(4, 5));
    }
}