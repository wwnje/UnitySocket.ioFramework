﻿using NUnit.Framework;
using Zenject;
using UniRx;
using Game.Login;
using UnityEngine;
using Game.Login.Internal;

[TestFixture]
public class LoadServerInfoTest : ZenjectUnitTestFixture
{
    ServerInfoSetting setting = new ServerInfoSetting();

    [Inject(Id = 1)]
    IServerInfoLoader loader_local = null;

    [Inject(Id = 2)]
    IServerInfoLoader loader_online = null;

    [SetUp]
    public void CommonInstall()
    {
        Container.Bind<IServerInfoLoader>().WithId(1).To<ServerInfoLocalLoader>().AsSingle();
        Container.Bind<IServerInfoLoader>().WithId(2).To<ServerInfoOnLineLoader>().AsSingle();
        Container.BindInstance(setting);
        Container.Inject(this);
    }

    const int VERSION_NOW = 1;

    [Test]
    public void Run_Test_Local()
    {
        loader_local.Load()
        .Subscribe(info =>
        {
            Debug.Log(info.announcement);

            Assert.IsNotNull(info);
            Assert.AreEqual(info.vision, VERSION_NOW);
        });
    }

    [Test]
    public void Run_Test_Online()
    {
        loader_online.Load()
        .Subscribe(info =>
        {
            Debug.Log(info.announcement);

            Assert.IsNotNull(info);
            Assert.AreEqual(info.vision, VERSION_NOW);
        });
    }
}
