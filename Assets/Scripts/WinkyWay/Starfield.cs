using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starfield : MonoBehaviour
{
    //generqtes the stqrs qround the plqyer
    private Transform thisTransform;
    private ParticleSystem.Particle[] stars;
    private float starDistanceSqr;
    private float starClipDistanceSqr;

    public Color starColor;
    public int starsMax = 500;
    public float starSize = 0.35f;
    public float starDistance = 60f; //size of the area with stars around the player
    public float starClipDistance = 15f;

    private ParticleSystem _particleSystem;
    // Use this for initialization
    void Start()
    {
        thisTransform = GetComponent<Transform>();
        starDistanceSqr = starDistance * starDistance;
        starClipDistanceSqr = starClipDistance * starClipDistance;

        _particleSystem = GetComponent<ParticleSystem>();
    }

    private void CreateStars()
    {

        stars = new ParticleSystem.Particle[starsMax];
        // assign position and color to stars
        for (int i = 0; i < starsMax; i++)
        {
            stars[i].position = Random.insideUnitSphere * starDistance + thisTransform.position;
            stars[i].color = new Color(starColor.r, starColor.g, starColor.b);
            stars[i].size = starSize;
        }
    }
    void Update()
    {
        if (stars == null)
        {
            CreateStars();
        }
        /*
        for (int i = 0; i < starsMax; i++) //if stars are too far, generate a new position
        {
            if ((stars[i].position - thisTransform.position).sqrMagnitude > starDistanceSqr)
                stars[i].position = Random.insideUnitSphere.normalized * starDistance + thisTransform.position;
            if ((stars[i].position - thisTransform.position).sqrMagnitude <= starClipDistanceSqr)
            {
                float percentage = (stars[i].position - thisTransform.position).sqrMagnitude / starClipDistanceSqr;
                stars[i].color = new Color(1, 1, 1, percentage);
                stars[i].size = percentage * starSize;
            }
        }
        */
        _particleSystem.SetParticles(stars, stars.Length);
    }
}
