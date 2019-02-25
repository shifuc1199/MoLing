using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 
public class FSMMachines
{
     
    public Dictionary<string, FSMState> m_states = new Dictionary<string, FSMState>();
    public FSMState m_curretstate;
    public FSMState m_laststate;
    public FSMMachines()
    {
        m_curretstate = null;
        m_laststate = null;
    }
    public void RegisterState(FSMState state)
    {
        if(m_states.ContainsKey(state.id))
        {
            return;
        }
        m_states.Add(state.id, state);
    }
    public void ChangeState(string id)
    {
        if(!m_states.ContainsKey(id))
        {
            return;
        }
        m_laststate = m_curretstate;
        m_curretstate = m_states[id];
        if (m_laststate!=null)
        {
            m_laststate.OnExit();
        }
        m_curretstate.OnEter();
    }
   
}
