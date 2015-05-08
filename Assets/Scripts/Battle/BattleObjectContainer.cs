﻿using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace Submarine
{
    public class BattleObjectContainer : IInitializable, IDisposable, ITickable
    {
        readonly SubmarineFactory submarineFactory;
        readonly TorpedoFactory torpedoFactory;

        readonly List<IBattleObject> battleObjects = new List<IBattleObject>();

        public IEnumerable<ISubmarine> Submarines { get { return battleObjects.OfType<ISubmarine>(); } }
        public IEnumerable<ITorpedo> Torpedos { get { return battleObjects.OfType<ITorpedo>(); } }

        public BattleObjectContainer(
            SubmarineFactory submarineFactory,
            TorpedoFactory torpedoFactory)
        {
            this.submarineFactory = submarineFactory;
            this.torpedoFactory = torpedoFactory;
        }

        public void Initialize()
        {
            BattleEvent.OnPhotonBehaviourCreate += OnPhotonBehaviourCreate;
            BattleEvent.OnPhotonBehaviourDestroy += OnPhotonBehaviourDestroy;
        }

        public void Dispose()
        {
            BattleEvent.OnPhotonBehaviourCreate -= OnPhotonBehaviourCreate;
            BattleEvent.OnPhotonBehaviourDestroy -= OnPhotonBehaviourDestroy;
        }

        public void Tick()
        {
            foreach (var battleObject in battleObjects)
            {
                battleObject.Tick();
            }
        }

        public ISubmarine SpawnSubmarine(Vector3 position)
        {
            var submarine = submarineFactory.Create(position);
            submarine.Initialize();
            battleObjects.Add(submarine);
            return submarine;
        }

        public ITorpedo SpawnTorpedo(Vector3 position, Quaternion rotation)
        {
            var torpedo = torpedoFactory.Create(position, rotation);
            torpedo.Initialize();
            battleObjects.Add(torpedo);
            return torpedo;
        }

        public void Remove(IBattleObject battleObject)
        {
            var result = battleObjects.Remove(battleObject);
            if (result)
            {
                battleObject.Dispose();
            }
        }

        void OnPhotonBehaviourCreate(IBattleObjectHooks battleObjectHooks)
        {
            if (battleObjectHooks.photonView.isMine)
            {
                return;
            }

            switch (battleObjectHooks.Type)
            {
                case BattleObjectType.Submarine:
                    var submarine = submarineFactory.Create(battleObjectHooks as SubmarineHooks);
                    submarine.Initialize();
                    battleObjects.Add(submarine);
                    break;
                case BattleObjectType.Torpedo:
                    var torpedo = torpedoFactory.Create(battleObjectHooks as TorpedoHooks);
                    torpedo.Initialize();
                    battleObjects.Add(torpedo);
                    break;
            }
        }

        void OnPhotonBehaviourDestroy(IBattleObjectHooks battleObjectHooks)
        {
            var battleObject = battleObjects.Find(s => s.BattleObjectHooks == battleObjectHooks);
            Remove(battleObject);
        }
    }
}