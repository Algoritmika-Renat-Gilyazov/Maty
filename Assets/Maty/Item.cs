using UnityEngine;

namespace Maty
{
    public abstract class Item : MonoBehaviour
    {
        public virtual string item_name => "Unknown";
        public abstract string id { get; }
        public virtual int max_stack_size => 1;
        public int current_stack_size = 1;

        public void Add()
        {
            current_stack_size++;
            OnAdd();
        }

        protected virtual void OnAdd()
        {
        }

        public void Remove()
        {
            current_stack_size--;
            OnRemove();
        }

        protected virtual void OnRemove()
        {
        }
    }
}