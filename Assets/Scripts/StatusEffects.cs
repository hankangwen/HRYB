using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public struct StatusEffect
{
    public string name;
    public string desc;

    public Action<Actor, Actor> onApplied;//���ѳ�, �ǳ�
    public Action<Actor> onUpdated;
    public Action<Actor> onEnded;

    public StatusEffect(string n, string d, Action<Actor, Actor> app, Action<Actor> upd, Action<Actor> end)
	{
        name = n;
        desc = d;
        onApplied = app;
        onUpdated = upd;
        onEnded = end;
	}
}

public class StatusEffects
{
    public Hashtable idStatEffPairs;
    int id = 0;

	public StatusEffects()
	{
		idStatEffPairs.Add(id++, new StatusEffect("�� ����", "���� ����ġ�� �Ѿ���ϴ�!", OnWoodDebuffActivated, OnWoodDebuffUpdated, OnWoodDebuffEnded));
		idStatEffPairs.Add(id++, new StatusEffect("ȭ ����", "ȭ�� ����ġ�� �Ѿ���ϴ�!", OnFireDebuffActivated, OnFireDebuffUpdated, OnFireDebuffEnded));
		idStatEffPairs.Add(id++, new StatusEffect("�� ����", "�䰡 ����ġ�� �Ѿ���ϴ�!", OnEarthDebuffActivated, OnEarthDebuffUpdated, OnEarthDebuffEnded));
		idStatEffPairs.Add(id++, new StatusEffect("�� ����", "���� ����ġ�� �Ѿ���ϴ�!", OnMetalDebuffActivated, OnMetalDebuffUpdated, OnMetalDebuffEnded));
		idStatEffPairs.Add(id++, new StatusEffect("�� ����", "���� ����ġ�� �Ѿ���ϴ�!", OnWaterDebuffActivated, OnWaterDebuffUpdated, OnWaterDebuffEnded));
	}

    void OnWoodDebuffActivated(Actor self, Actor inflicter)
	{
        
	}

    void OnWoodDebuffUpdated(Actor self)
    {

    }

    void OnWoodDebuffEnded(Actor self)
    {

    }

    void OnFireDebuffActivated(Actor self, Actor inflicter)
    {
        self.atk.effTime *= 2;
    }

    void OnFireDebuffUpdated(Actor self)
    {

    }

    void OnFireDebuffEnded(Actor self)
    {
        self.atk.effTime *= 0.5f;
    }

    void OnEarthDebuffActivated(Actor self, Actor inflicter)
    {
        
    }

    void OnEarthDebuffUpdated(Actor self)
    {

    }

    void OnEarthDebuffEnded(Actor self)
    {

    }

    void OnMetalDebuffActivated(Actor self, Actor inflicter)
    {
        self.sight.sightRange *= 0.5f;
        GameManager.instance.CalcCamVFov(-20);
    }

    void OnMetalDebuffUpdated(Actor self)
    {

    }

    void OnMetalDebuffEnded(Actor self)
    {
        self.sight.sightRange *= 2f;
        GameManager.instance.CalcCamVFov(20);
    }

    void OnWaterDebuffActivated(Actor self, Actor inflicter)
    {
        self.move.speed *= 0.5f;
        self.atk.atkGap *= 2;
    }

    void OnWaterDebuffUpdated(Actor self)
    {

    }

    void OnWaterDebuffEnded(Actor self)
    {
        self.move.speed *= 2f;
        self.atk.atkGap *= 0.5f;
    }
}
