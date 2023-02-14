using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subscription
{
    string _scope;
    string _eventName;

    public Subscription(string scope, string eventName)
    {
        this._scope = scope;
        this._eventName = eventName;

    }

    public string Scope()
    {
        return this._scope;
    }

    public string EventName()
    {
        return this._eventName;
        
    }
    
    public override bool Equals(object obj)
    {
        // If obj is actually "this" then true
        if (Object.ReferenceEquals(this, obj))
            return true;

        if (obj == null)
            return false;

        Subscription other = obj as Subscription;

        
        if (Object.ReferenceEquals(null, other))
            return false;


        if ((!this._eventName.Equals(other._eventName)) && (!this._scope.Equals(other._scope)))
        {
            return false;
        }
        

        return true;
    }

    // Do not forget to override GetHashCode:
    public override int GetHashCode()
    {
        return this._eventName.GetHashCode() + this._scope.GetHashCode();// <- Simplest version; probably you have to put more elaborated one
    }


}
