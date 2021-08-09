using UnityEngine;
using System.Collections;

namespace S3
{
    public interface NPCState_Interface
    {
        void UpdateState();
        void ToPatrolState();
        void ToAlertState();
        void ToPursueState();
        void ToMeleeAttackState();
        void ToRangeAttackState();
    }
}

