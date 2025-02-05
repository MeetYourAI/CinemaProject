
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Scriptable Objects/MovieLists", fileName = "movieList")]
public class SO_MovieDetails : ScriptableObject
{
    public List<MovieDetails> movieList = new List<MovieDetails>();
}
