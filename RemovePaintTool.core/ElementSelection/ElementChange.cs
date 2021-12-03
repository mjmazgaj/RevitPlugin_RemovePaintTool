using Autodesk.Revit.DB;

namespace RemovePaintTool.core
{
    public class ElementChange
    {
        private Document _document;

        public ElementId Id;
        public Face Face;
        public GeometryElement geometryElement;
        public ElementChange(Document document)
        {
            _document = document;
        }
        /// <summary>
        /// Checks if element is painted
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public bool IsElementPainted(Element e)
        {
            Wall wall = e as Wall;
            Floor floor = e as Floor;
            Ceiling ceiling = e as Ceiling;

            if (wall != null)
            {
                geometryElement = wall.get_Geometry(new Options());
                return IsFacePainted(geometryElement, wall.Id);
            }
            else if (floor != null)
            {
                geometryElement = floor.get_Geometry(new Options());
                return IsFacePainted(geometryElement, floor.Id);
            }
            else if (ceiling != null)
            {
                geometryElement = ceiling.get_Geometry(new Options());
                return IsFacePainted(geometryElement, ceiling.Id);
            }
            else
                return false;
        }
        
        /// <summary>
        /// Checks if face of element is painted
        /// </summary>
        /// <param name="geometryElement"></param>
        /// <param name="EId"></param>
        /// <returns></returns>
        public bool IsFacePainted(GeometryElement geometryElement, ElementId EId)
        {
            foreach (GeometryObject geometryObject in geometryElement)
            {
                if (geometryObject is Solid)
                {
                    Solid solid = geometryObject as Solid;
                    foreach (Face face in solid.Faces)
                        if (_document.IsPainted(EId, face) == true)
                        {
                            Id = EId;
                            Face = face;
                            return true;
                        }
                }
            }
            return false;
        }

    }
}
