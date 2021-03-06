﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New Scuture Stats", menuName = "Structures/New Structure Stats")]
public class StructureStats : ScriptableObject
{
    //TODO: Remove projectile info from this shit
    [SerializeField] private float baseHealth = 0;
    [SerializeField] private float baseDamage = 0;
    [SerializeField] private float baseRange = 0;
    [SerializeField] private float baseAttackRate = 0;
    [SerializeField] private float resourceBuildCost = 0;
    [SerializeField] private float resourceUpgradeCost = 0;
    [SerializeField] private TowerProjectile projectile = null;
    [SerializeField] private float projectileSpeed = 0;

    public float BaseHealth => baseHealth;
    public float BaseDamage => baseDamage;
    public float BaseRange => baseRange;
    public float BaseAttackRate => baseAttackRate;
    public float ResourceBuildCost => resourceBuildCost;
    public float ResourceUpgradeCost => resourceUpgradeCost;
    public TowerProjectile Projectile => projectile;
    public float ProjectileSpeed => projectileSpeed;
}

