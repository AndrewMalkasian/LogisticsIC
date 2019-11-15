using IndividualCapstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;


namespace IndividualCapstone
{
    public interface IMyAbstractFactory
    {
        IAbstractShipmentA CreateShipmentA();

        IAbstrtactShipmentB CreateShipmentB();

    }
    class ConcreteFactory1 : IMyAbstractFactory
    {
        public IAbstractShipmentA CreateShipmentA()
        {
            return new ConcreteShipmentA1();
        }
        public IAbstractShipmentB CreateShipmentB()
        {
            return new ConcreteShipmentB1();
        }
    }
    class ConcreteFactory2 : IMyAbstractFactory
    {
        public IAbstractShipmentA CreateShipmentA()
        {
            return new ConcreteShipmentA2();
        }
        public IAbstractShipmentB CreateShipmentB()
        {
            return new ConcreteShipmentB2();
        }

        public interface IAbstractShipmentA
        {
            Task<string> DistanceFromPickupBackToHubString();
        }

        class ConcreteShipmentA1 : IAbstractShipmentA
        {
           public string DistanceFromPickupBackToHubString()
            {
                
            }
        }
        class ConcreteProductA2 : IAbstractProductA
        {
            public string DistanceFromPickupBackToHubString()
            {

            }
            public string PickupDistanceBetweenHubAndDelivery()
            {

            }
        }


    }
}

           