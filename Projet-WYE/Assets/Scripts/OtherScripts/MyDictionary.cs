using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///Script by Louis Viktor Celeyron

[System.Serializable]
public struct DictionaryEntry<Tk,Tv> 
{
    public Tk key;
    public Tv value;

    public DictionaryEntry(Tk key, Tv value)
    {
        this.key = key;
        this.value = value;
    }

    public override string ToString()
    {
        return "Key : " + key.ToString() + " Value : " + value.ToString();
    }
}

public abstract class BaseDictionary
{
    //ça reste du marouflage
    /// <summary>
    /// Current size of the Dictionary
    /// </summary>
    public virtual int Size => 0;
    /// <summary>
    /// Is the dictionary not empty or null 
    /// </summary>
    public virtual bool IsValid => false;
    /// <summary>
    /// Element Name displayed on the drawer
    /// </summary>
    public virtual string ElementName(int i) => "Element"; 
    
    /// <summary>
    /// Add Default entry
    /// </summary>
    public virtual void Add() { }
    /// <summary>
    /// Remove entry at index
    /// </summary>
    /// <param name="i"></param>
    public virtual void Remove(int i) { }

    /// <summary>
    /// Calls a debug warning if 2 keys have the same values for example
    /// </summary>
    public virtual void CheckIfEverythingIsOkay()
    {

    }
}

[System.Serializable]
public class MyDictionary<Tk, Tv> : BaseDictionary
{

    public List<Tk> keys;
    public List<Tv> values;

    public DictionaryEntry<Tk, Tv> this[int index]
    {
        get
        {
            return new DictionaryEntry<Tk, Tv>(keys[index], values[index]);
        }
    }
    public Tv this[Tk key]
    {
        get
        {
            return GetValue(key);
        }
    }

    //Base vars
    public override bool IsValid
    {
        get
        {
            return
            keys != null
            &&
            keys.Count > 0
            &&
            values != null
            &&
            values.Count > 0
            &&
            keys.Count == values.Count;
        }
    }
    public override int Size => IsValid ? keys.Count : 0;

    //See in base
    public override string ElementName(int i)
    {
        var _isOk = keys[i] != null;

        return _isOk ? keys[i].ToString() : base.ElementName(i);
    }
    
    //See in base
    public override void CheckIfEverythingIsOkay()
    {
        
    }
    
    //See in base
    public override void Add()
    {
        if(CheckKey(default))
        {
            Debug.LogError("You can't add an entry if a key already exist in an entry of the dictionary");
        }
        Debug.Log("Add");

        keys.Add(default);
        values.Add(default);
    }
    /// <summary>
    /// Add a new entry to the dictionary
    /// </summary>
    /// <param name="key">key of the entry</param>
    /// <param name="value">value of the entry</param>
    public void Add(Tk key, Tv value)
    {

        //negate the methods if the key exist
        if (CheckKey(key))
        {
            Debug.LogError("You can't add an entry if a key already exist in an entry of the dictionary");
        }

        //Add a new entry for the dictionary
        keys.Add(key);
        values.Add(value);
    }
    
    //See in base
    public override void Remove(int i)
    {
        values.RemoveAt(i);
        keys.RemoveAt(i);
    }
    /// <summary>
    /// Remove an entry of the dictionary
    /// </summary>
    /// <param name="key"> key of the entry to remove</param>
    public void Remove(Tk key)
    {
        var _index = GetIndex(key);
        if (_index < 0)
        {
            Remove(_index);
        }
    }


    public void ForEach(System.Action<Tk,Tv> action)
    {
        for (int i = 0; i < Size; i++)
        {
            action.Invoke(keys[i],values[i]);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key">key to check</param>
    /// <returns>Does the key exist in the dictionary</returns>
    public bool CheckKey(Tk key)
    {
        if (keys == null)
        {
            keys = new List<Tk>();
        }
        if (values == null)
        {
            values = new List<Tv>();
        }


        foreach (var _entry in keys)
        {
            
            if (_entry == null||_entry.Equals(key))
            {
                return true;
            }
        }

        return false;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="value">value to check</param>
    /// <returns>Does the value exist in the dictionary</returns>
    public bool CheckValue(Tv value)
    {
        foreach (var _entry in values)
        {
            if (_entry.Equals(value))
            {
                return true;
            }
        }
        return false;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="value">value to check</param>
    /// <param name="howMany"> how many value are in the dictionary</param>
    /// <returns>Does the value exist in the dictionary</returns>
    public bool CheckValue(Tv value, out int howMany)
    {
        howMany = 0;

        //Raise how many if value exist
        foreach (var _entry in values)
        {
            if (_entry.Equals(value))
            {
                howMany++;
            }
        }

        return howMany > 0;
    }

    /// <summary>
    /// Return the current index of the entered  key
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public int GetIndex(Tk key)
    {
        for (int i = 0; i < Size; i++)
        {
            if (key.Equals(keys[i]))
            {
                return i;
            }
        }
        return -1;
    }
    /// <summary>
    /// Return the matching value of a key
    /// </summary>
    /// <returns>value in the dictionary at the entry "key", return default if key do not exist</returns>
    public Tv GetValue(Tk key)
    {
        var _index = GetIndex(key);

        if (_index >= 0)
        {
            return values[_index];
        }

        return default;
    }


    /// <summary>
    /// Return the matching value of a key
    /// </summary>
    /// <param name="isValid"> out true if a value is returned</param>
    /// <returns>value in the dictionary at the entry "key", return default if key do not exist</returns>
    public Tv GetValue(Tk key, out bool isValid)
    {
        var _index = GetIndex(key);

        if (_index >= 0)
        {
            isValid = true;
            return values[_index];
        }

        isValid = false;
        return default;
    }

    
}

public class MyStringDictionary<Tv> : MyDictionary<string, Tv>
{ }