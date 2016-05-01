using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticProgramming
{
    public class OutlineGeneticSequence : GeneticSequence
    {
        private OutlineMap m_Map;
        public OutlineGeneticSequence(OutlineMap aMap, SituationData aSituationData) : base(aSituationData)
        {
            m_Map = aMap;
        }

        protected override void InitializeFirstPopulation()
        {
            m_Population = new OutlinePopulation(m_SituationData, m_Map);
        }

        protected override void InitializeNewtPopulation()
        {
            m_NewPopulation = new OutlinePopulation(m_SituationData, m_Map);
        }
    }
}