using System.Collections;
using System.Collections.Generic;
using Kimeria.Nyx.Tools;

namespace Kimeria.Nyx
{
    /// <summary>
    /// Just a global static instantiation of an Event Manager to helps some project with small needs
    /// </summary>
    public static class GlobalEventManger
    {
        static EventManager evtMgr = new EventManager();

        public static EventManager EventsManager { get => evtMgr; }

        public static void Step(float dt)
        {
            evtMgr.Step(dt);
        }

        public static void AddListener<T>(EventManager.EventDelegate<T> del) where T : IEvent
        {
            evtMgr.AddListener<T>(del);
        }
        public static void AddListenerOnce<T>(EventManager.EventDelegate<T> del) where T : IEvent
        {
            evtMgr.AddListenerOnce<T>(del);
        }
        public static void RemoveListener<T>(EventManager.EventDelegate<T> del) where T : IEvent
        {
            evtMgr.RemoveListener<T>(del);
        }
        public static void RemoveAll()
        {
            evtMgr.RemoveAll();
        }

        public static bool HasListener<T>(EventManager.EventDelegate<T> del) where T : IEvent
        {
            return evtMgr.HasListener<T>(del);
        }

        public static void Trigger(IEvent evt)
        {
            evtMgr.Trigger(evt);
        }

        //Inserts the event into the current queue.
        public static bool Enqueue(IEvent evt)
        {
            return evtMgr.Enqueue(evt);
        }

        public static void Quit()
        {
            evtMgr.Quit();
        }
    }
}
