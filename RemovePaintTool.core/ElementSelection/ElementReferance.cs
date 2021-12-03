using Autodesk.Revit.DB;
using System.Collections.Generic;

namespace RemovePaintTool.core
{
    public class ElementReferance
    {
        private Document _doc;
        public ElementReferance(Document doc)
        {
            _doc = doc;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>List of references to painted elements.</returns>
        public List<Reference> GetReferences()
        {
            var newrefs = new List<Reference>();
            newrefs.AddRange(GetWallReferences());
            newrefs.AddRange(GetFloorReferences());
            newrefs.AddRange(GetCeilingReferences());

            return newrefs;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>List of references to painted wall</returns>
        private List<Reference> GetWallReferences()
        {            
            var elementIDS = GetElementIDS(BuiltInCategory.OST_Walls);

            var newrefs = new List<Reference>();
            foreach (ElementId eid in elementIDS)
            {
                Element e = _doc.GetElement(eid);
                Wall wall = e as Wall;
                GeometryElement geometryElement = wall.get_Geometry(new Options());

                if (IsElementPainted(geometryElement, wall.Id))
                {
                    newrefs.Add(new Reference(e));
                }
            }
            return newrefs;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>List of references to painted floor</returns>
        private List<Reference> GetFloorReferences()
        {
            var elementIDS = GetElementIDS(BuiltInCategory.OST_Floors);

            var newrefs = new List<Reference>();
            foreach (ElementId eid in elementIDS)
            {
                Element e = _doc.GetElement(eid);
                Floor floor = e as Floor;
                GeometryElement geometryElement = floor.get_Geometry(new Options());

                if (IsElementPainted(geometryElement, floor.Id))
                {
                    newrefs.Add(new Reference(e));
                }
            }
            return newrefs;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>List of references to painted ceiling</returns>
        private List<Reference> GetCeilingReferences()
        {
            var elementIDS = GetElementIDS(BuiltInCategory.OST_Ceilings);

            var newrefs = new List<Reference>();
            foreach (ElementId eid in elementIDS)
            {
                Element e = _doc.GetElement(eid);
                Ceiling ceiling = e as Ceiling;
                GeometryElement geometryElement = ceiling.get_Geometry(new Options());

                if (IsElementPainted(geometryElement, ceiling.Id))
                {
                    newrefs.Add(new Reference(e));
                }
            }
            return newrefs;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns>List of specified Elements Ids</returns>
        private IList<ElementId> GetElementIDS(BuiltInCategory name)
        {
            var collector = new FilteredElementCollector(_doc, _doc.ActiveView.Id)
                .OfCategory(name)
                .WhereElementIsNotElementType()
                .ToElementIds();

            return collector as IList<ElementId>;
        }

        /// <summary>
        /// Ckeck if element is painted
        /// </summary>
        /// <param name="geometryElement"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        private bool IsElementPainted(GeometryElement geometryElement, ElementId Id)
        {
            foreach (GeometryObject geometryObject in geometryElement)
            {
                if (geometryObject is Solid)
                {
                    Solid solid = geometryObject as Solid;
                    foreach (Face face in solid.Faces)
                        if (_doc.IsPainted(Id, face) == true)
                            return true;
                }
            }
            return false;
        }
    }
}