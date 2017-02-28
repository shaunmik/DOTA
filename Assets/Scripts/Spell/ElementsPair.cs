using System;
using System.Collections.Generic;

public class ElementsPair
{
    private Elements.elemEnum first;
    private Elements.elemEnum second;

    public ElementsPair() : this(Elements.elemEnum.none, Elements.elemEnum.none) { }
    
    public ElementsPair(Elements.elemEnum first, Elements.elemEnum second)
    {
        this.first = first;
        this.second = second;
    }

    public Elements.elemEnum First
    {
        get { return first; }
    }

    public Elements.elemEnum Second
    {
        get { return second; }
    }

    public bool isNonePair()
    {
        return this.first == Elements.elemEnum.none && this.second == Elements.elemEnum.none;
    }

    public bool containsElement(Elements.elemEnum element)
    {
        return this.first == element || this.second == element;
    }

    public void pushIfPossibleElseClearAndPush(Elements.elemEnum element)
    {
        if (this.first == Elements.elemEnum.none)
        {
            this.first = element;
        } else if (this.second == Elements.elemEnum.none)
        {
            this.second = element;
        } else
        {
            clear();
            this.first = element;
        }

    }

    public int getNumAssignedElements()
    {
        int num = 0;
        if (this.first != Elements.elemEnum.none)
        {
            num++;
        }
        if (this.second != Elements.elemEnum.none)
        {
            num++;
        }
        return num;
    }

    public void clear()
    {
        this.first = Elements.elemEnum.none;
        this.second = Elements.elemEnum.none;

    }


    public bool Equals(ElementsPair other)
    {
        if (other == null)
        {
            return false;
        }
        return Equals(this.First, other.First) && Equals(this.Second, other.Second);
    }

    public override bool Equals(object o)
    {
        return Equals(o as ElementsPair);
    }

    public override int GetHashCode()
    {
        return first.GetHashCode() * 7 + second.GetHashCode();
    }
}