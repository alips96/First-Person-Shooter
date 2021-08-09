using Zenject;
using UnityEngine;
using System.Collections;

namespace S3
{
    public class TestInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            //Container.Bind<string>().FromInstance("Hello World!");
            //Container.Bind<Greeter>().AsSingle().NonLazy();
            Container.Bind<Result>().FromInstance(new Result());
            Container.Bind<Caller>().AsSingle().NonLazy();
        }
    }

    //public class Greeter
    //{
    //    public Greeter(string message)
    //    {
    //        Debug.Log(message);
    //    }
    //}
}
