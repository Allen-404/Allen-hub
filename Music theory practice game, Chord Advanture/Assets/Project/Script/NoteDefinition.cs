using UnityEngine;

public enum Note
{
    None = 0,

    [InspectorName("Basic/C")]
    C,
    [InspectorName("Basic/D")]
    D,
    [InspectorName("Basic/E")]
    E,
    [InspectorName("Basic/F")]
    F,
    [InspectorName("Basic/G")]
    G,
    [InspectorName("Basic/A")]
    A,
    [InspectorName("Basic/B")]
    B,

    [InspectorName("#/C")]
    C1,
    [InspectorName("#/D")]
    D1,
    [InspectorName("#/E")]
    E1,
    [InspectorName("#/F")]
    F1,
    [InspectorName("#/G")]
    G1,
    [InspectorName("#/A")]
    A1,
    [InspectorName("#/B")]
    B1,

    [InspectorName("b/C")]
    C2,
    [InspectorName("b/D")]
    D2,
    [InspectorName("b/E")]
    E2,
    [InspectorName("b/F")]
    F2,
    [InspectorName("b/G")]
    G2,
    [InspectorName("b/A")]
    A2,
    [InspectorName("b/B")]
    B2,
}
[System.Serializable]
public enum NoteKey
{
    None = 0,

    C = 1,
    D = 2,
    E = 3,
    F = 4,
    G = 5,
    A = 6,
    B = 7,

    sharp = 10,
    b = 20,
}